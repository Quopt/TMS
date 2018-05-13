using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Collections.Specialized;
using Microsoft.Reporting;
using Microsoft.Reporting.WebForms;
using System.ComponentModel;

namespace TMS_Recycling
{
    public class ClassDataSetHelper
    {
        public static void Save(DataSet data, SqlConnection connection)
        {
            /// Dictionary for associating adapters to tables.
            Dictionary<DataTable, SqlDataAdapter> adapters = new Dictionary<DataTable, SqlDataAdapter>();

            foreach (DataTable table in data.Tables)
            {
                /// Find the table adapter using Reflection.
                Type adapterType = GetTableAdapterType(table);
                SqlDataAdapter adapter = SetupTableAdapter(adapterType, connection);
                adapters.Add(table, adapter);
            }

            /// Save the data.
            Save(data, adapters);
        }

        public static void Load(DataSet data, SqlConnection connection, NameValueCollection Pars )
        {
            /// Dictionary for associating adapters to tables.
            Dictionary<DataTable, SqlDataAdapter> adapters = new Dictionary<DataTable, SqlDataAdapter>();

            foreach (DataTable table in data.Tables)
            {
                /// Find the table adapter using Reflection.
                Type adapterType = GetTableAdapterType(table);
                SqlDataAdapter adapter = SetupTableAdapter(adapterType, connection);
                adapters.Add(table, adapter);

                if ((adapter.SelectCommand.Parameters.Count > 0) && (Pars != null))
                {
                    // scan for parameters to fill
                    foreach (SqlParameter sp in adapter.SelectCommand.Parameters)
                    {
                        if (Pars[sp.ParameterName] != null)
                        {
                            adapter.SelectCommand.Parameters[sp.ParameterName].Value = Pars[sp.ParameterName];
                        }
                    }
                }

                table.Load(adapter.SelectCommand.ExecuteReader());
            }
        }

        public static void Load(DataSet data, ReportDataSourceCollection ReportData, SqlConnection connection, NameValueCollection Pars)
        {
            foreach (DataTable table in data.Tables)
            {
                /// Find the table adapter using Reflection.
                Type adapterType = GetTableAdapterType(table);
                object adapterObj = SetupTableAdapterObject(adapterType, connection);
                SqlCommand[] sqColl = (SqlCommand[])GetPropertyValue(adapterType, adapterObj, "CommandCollection");
                SqlCommand sqc = sqColl[0];

                if ((sqc != null) && (Pars != null))
                {
                    // scan for parameters to fill
                    foreach (SqlParameter sp in sqc.Parameters)
                    {
                        string ParName = sp.ParameterName.Remove(0, 1);
                        if (Pars[ParName] != null)
                        {
                            if (sqc.Parameters[sp.ParameterName].DbType == DbType.Guid)
                            {
                                if (Pars[ParName] != "")
                                {
                                    sqc.Parameters[sp.ParameterName].Value = new Guid(Pars[ParName]);
                                }
                                else
                                {
                                    sqc.Parameters[sp.ParameterName].Value = Guid.Empty;
                                }
                            }
                            else
                            {
                                sqc.Parameters[sp.ParameterName].Value = Pars[ParName];
                            }
                        }
                    }
                }

                // rip off the constraints on the fields in this datatable
                foreach (DataColumn dc in table.Columns)
                {
                    if (dc.MaxLength > 0) { dc.MaxLength = -1; }
                    dc.AllowDBNull = true;
                }

                try
                {
                    connection.Open();
                    SqlDataReader sdr = sqc.ExecuteReader();

                    table.Load(sdr);
                    
                    ReportDataSource rds = new ReportDataSource();
                    rds.Name = table.TableName;
                    rds.Value = table;
                    ///ReportData.Add(new ReportDataSource(table.TableName, table));
                    ReportData.Add(rds);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        static Type GetTableAdapterType(DataTable table)
        {
            /// Find the adapter type for the table using the namespace conventions generated by dataset code generator.
            string nameSpace = table.GetType().Namespace;
            string adapterTypeName = nameSpace + "." + table.DataSet.DataSetName + "TableAdapters." + table.TableName + "TableAdapter";
            Type adapterType = Type.GetType(adapterTypeName);
            return adapterType;
        }

        static SqlDataAdapter SetupTableAdapter(Type adapterType, SqlConnection connection)
        {
            /// Set connection to TableAdapter and extract SqlDataAdapter (which is private anyway).
            object adapterObj = Activator.CreateInstance(adapterType);
            SqlDataAdapter sqlAdapter = (SqlDataAdapter)GetPropertyValue(adapterType, adapterObj, "Adapter");
            SetPropertyValue(adapterType, adapterObj, "Connection", connection);

            return sqlAdapter;
        }

        static object SetupTableAdapterObject(Type adapterType, SqlConnection connection)
        {
            /// Set connection to TableAdapter and extract SqlDataAdapter (which is private anyway).
            object adapterObj = Activator.CreateInstance(adapterType);
            SqlDataAdapter sqlAdapter = (SqlDataAdapter)GetPropertyValue(adapterType, adapterObj, "Adapter");
            SetPropertyValue(adapterType, adapterObj, "Connection", connection);

            return adapterObj;
        }
        static object GetPropertyValue(Type type, object instance, string propertyName)
        {
            return type.GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.Instance).GetValue(instance, null);
        }

        static void SetPropertyValue(Type type, object instance, string propertyName, object propertyValue)
        {
            type.GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Instance).SetValue(instance, propertyValue, null);
        }

        static void Save(DataSet data, Dictionary<DataTable, SqlDataAdapter> adapters)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            if (adapters == null)
                throw new ArgumentNullException("adapters");

            Dictionary<DataTable, bool> procesedTables = new Dictionary<DataTable, bool>();
            List<DataTable> sortedTables = new List<DataTable>();

            while (true)
            {
                DataTable rootTable = GetRootTable(data, procesedTables);
                if (rootTable == null)
                    break;

                sortedTables.Add(rootTable);
            }

            /// Updating Deleted rows in Child -> Parent order.
            for (int i = sortedTables.Count - 1; i >= 0; i--)
            {
                Update(adapters, sortedTables[i], DataViewRowState.Deleted);
            }

            /// Updating Added / Modified rows in Parent -> Child order.
            for (int i = 0; i < sortedTables.Count; i++)
            {
                Update(adapters, sortedTables[i], DataViewRowState.Added | DataViewRowState.ModifiedCurrent);
            }
        }

        static void Update(Dictionary<DataTable, SqlDataAdapter> adapters, DataTable table, DataViewRowState states)
        {
            SqlDataAdapter adapter = null;

            if (adapters.ContainsKey(table))
                adapter = adapters[table];

            if (adapter != null)
            {
                DataRow[] rowsToUpdate = table.Select("", "", states);

                if (rowsToUpdate.Length > 0)
                    adapter.Update(rowsToUpdate);
            }
        }

        static DataTable GetRootTable(DataSet data, Dictionary<DataTable, bool> procesedTables)
        {
            foreach (DataTable table in data.Tables)
            {
                if (!procesedTables.ContainsKey(table))
                {
                    if (IsRootTable(table, procesedTables))
                    {
                        procesedTables.Add(table, false);
                        return table;
                    }
                }
            }

            return null;
        }

        static bool IsRootTable(DataTable table, Dictionary<DataTable, bool> procesedTables)
        {
            foreach (DataRelation relation in table.ParentRelations)
            {
                DataTable parentTable = relation.ParentTable;
                if (parentTable != table && !procesedTables.ContainsKey(parentTable))
                    return false;
            }

            return true;
        }
    }
}