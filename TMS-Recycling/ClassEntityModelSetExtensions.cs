using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects;
using System.Globalization;
using System.Transactions;
using System.Data.Common;

namespace TMS_Recycling
{

    #region SystemSettingSet
    public class SystemSettingSet
    {

        public static void SetSystemSettingValue(ModelTMSContainer Context, string PropertyName, string PropertyValue, string PropertyDescription)
        {
            SystemSetting ThisSetting;
            ObjectQuery<SystemSetting> TempSetting = Context.SystemSettingSet.Where("it.Description = @PropName", 
                 new ObjectParameter ("PropName",PropertyName));

            if (TempSetting.Count() == 0)
            {
                ModelTMSContainer NewContext = new ModelTMSContainer(Context.connectString, Context.Session); // since we are going to save now we do not want to spoil the real context
                ThisSetting = new SystemSetting();
                NewContext.AddToSystemSettingSet(ThisSetting);
                ThisSetting.Description = PropertyName;
                ThisSetting.Value = PropertyValue;
                ThisSetting.Comments = PropertyDescription;
                NewContext.SaveChanges();
            }
            else
            {
                ThisSetting = TempSetting.First();
                ThisSetting.Value = PropertyValue;
                ThisSetting.Comments = PropertyDescription;
            }
        }

        public static string GetSystemSettingValue(ModelTMSContainer Context, string PropertyName, string DefaultValue)
        {
            string RetVal = DefaultValue;
            ObjectQuery<SystemSetting> TempSetting = Context.SystemSettingSet.Where("it.Description = @PropName",
                 new ObjectParameter("PropName", PropertyName));

            if (TempSetting.Count() == 1)
            {
                RetVal = TempSetting.First().Value;
            }

            return RetVal;
        }

        public static SystemSetting GetSystemSetting(ModelTMSContainer Context, string PropertyName)
        {
            SystemSetting RetVal = null;
            ObjectQuery<SystemSetting> TempSetting = Context.SystemSettingSet.Where("it.Description = @PropName",
                 new ObjectParameter("PropName", PropertyName));

            if (TempSetting.Count() >= 1)
            {
                RetVal = TempSetting.First();
            }

            return RetVal;
        }

        public static bool IsSystemSettingPresent(ModelTMSContainer Context, string PropertyName)
        {
            bool RetVal = false;
            ObjectQuery<SystemSetting> TempSetting = Context.SystemSettingSet.Where("it.Description = @PropName", 
                 new ObjectParameter ("PropName",PropertyName));

            if (TempSetting.Count() == 1)
            {
                RetVal = true;
            }

            return RetVal;

        }

        public static Int32 GetAmountOfUserLicenses(ModelTMSContainer Context)
        {
            int RetVal = Convert.ToInt32(GetSystemSettingValue(Context, "License.AmountOfUserLicenses", "0"));

            if (RetVal == 0)
            {
                RetVal = 1;
                if (!IsSystemSettingPresent(Context, "License.AmountOfUserLicenses"))
                {
                    SetAmountOfUserLicenses(Context, RetVal);
                }
            }

            return RetVal;
        }

        public static void SetAmountOfUserLicenses(ModelTMSContainer Context, int AmountOfLicenses)
        {
            SetSystemSettingValue(Context, "License.AmountOfUserLicenses", AmountOfLicenses.ToString(), "Maximum amount of TMS user accounts"); 
        }

        public static Int64 GetNextCounterValue(ModelTMSContainer Context, string CounterName)
        {
            return GetNextCounterValue(Context, CounterName, true);
        }

        public static Int64 GetNextCounterValueWithNoSave(ModelTMSContainer Context, string CounterName)
        {
            return GetNextCounterValue(Context, CounterName, false);
        }

        private static Int64 GetNextCounterValue(ModelTMSContainer Context,string CounterName, bool Save)
        {
            String PropName = "Counter." + CounterName;
            Int64 Temp = Convert.ToInt64( GetSystemSettingValue(Context, PropName, "0") );

            Temp = Temp + 1;

            if (Save)
            {
                SetSystemSettingValue(Context, PropName, Temp.ToString(), "Integer counter value for " + CounterName + ". Do not modify value manually !!!");
            }

            return Temp;  
        }

        public static Int64 GetNextOrderNumber(ModelTMSContainer Context)
        {
            return GetNextCounterValue(Context, "OrderNumber");
        }

        public static Int64 GetNextOrderNumberWithNoSave(ModelTMSContainer Context)
        {
            return GetNextCounterValueWithNoSave(Context, "OrderNumber");
        }

        public static Int64 GetNextInvoiceNumber(ModelTMSContainer Context)
        {
            return GetNextCounterValue(Context, "InvoiceNumber");
        }

        public static Int64 GetNextInvoiceNumberWithNoSave(ModelTMSContainer Context)
        {
            return GetNextCounterValueWithNoSave(Context, "InvoiceNumber");
        }

        public static Int64 GetNextRentalItemNumber(ModelTMSContainer Context)
        {
            return GetNextCounterValue(Context, "RentalItemNumber");
        }

        public static Int64 GetNextRentalItemNumberWithNoSave(ModelTMSContainer Context)
        {
            return GetNextCounterValueWithNoSave(Context, "RentalItemNumber");
        }

        public static Int64 GetNextMaterialMutationNumber(ModelTMSContainer Context)
        {
            return GetNextCounterValue(Context, "MaterialMutationNumber");
        }

        public static Int64 GetNextMaterialMutationNumberWithNoSave(ModelTMSContainer Context)
        {
            return GetNextCounterValueWithNoSave(Context, "MaterialMutationNumber");
        }


        public static Int64 GetNextRentNumber(ModelTMSContainer Context)
        {
            return GetNextCounterValue(Context, "RentNumber");
        }

        public static Int64 GetNextRentNumberWithNoSave(ModelTMSContainer Context)
        {
            return GetNextCounterValueWithNoSave(Context, "RentNumber");
        }

        public static DateTime GetSystemSettingDateTime(ModelTMSContainer Context, string PropertyName, DateTime DefaultValue)
        {
            DateTime retVal = DefaultValue;
            string DefVal = DefaultValue.ToString(Common.constDateTimeFormatString );

            string ToParse = SystemSettingSet.GetSystemSettingValue(Context, PropertyName, DefVal);

            retVal = DateTime.ParseExact(ToParse, Common.constDateTimeFormatString, CultureInfo.InvariantCulture);
            return retVal;
        }

        public static void SetSystemSettingDateTime(ModelTMSContainer Context, string PropertyName, DateTime PropertyValue, string PropertyDescription)
        {
            SystemSettingSet.SetSystemSettingValue(Context, PropertyName, PropertyValue.ToString(Common.constDateTimeFormatString), PropertyDescription);
        }

        public static DateTime GetLastMaterialClosureDateTime(ModelTMSContainer Context)
        {
            DateTime LastClosure = GetSystemSettingDateTime(Context, "LastMaterialClosureDateTime", new DateTime(2000, 1, 1));

            if (LastClosure.Year == 2000)
            {
                // the date has not been set

                // assume today is the last closure datetime
                LastClosure = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59) ;

                // check if this is correct
                try
                {
                    MaterialMutation mt = Context.MaterialMutationSet.OrderBy(o => o.CreateDateTime).First<MaterialMutation>();
                    if (mt != null)
                    {
                        // there have been earlier material mutations. Use that date.
                        LastClosure = new DateTime(mt.MutationDateTime.Year, mt.MutationDateTime.Month, mt.MutationDateTime.Day, 23, 59, 59);
                    }
                }
                catch
                {
                }
            }
            LastClosure = new DateTime(LastClosure.Year, LastClosure.Month, LastClosure.Day, 23, 59, 59);
            return LastClosure;
        }

        public static void SetLastMaterialClosureDateTime(ModelTMSContainer Context, DateTime LastClosure)
        {
            LastClosure = new DateTime(LastClosure.Year, LastClosure.Month, LastClosure.Day, 23, 59, 59);
            SystemSettingSet.SetSystemSettingDateTime(Context, "LastMaterialClosureDateTime", LastClosure, "The last closure date for the Materials (stock).");
        }

        public static DateTime GetLastLedgerClosureDateTime(ModelTMSContainer Context, string LedgerDescription)
        {
            DateTime LastClosure = GetSystemSettingDateTime(Context, "Last" + LedgerDescription + "ClosureDateTime", new DateTime(2000, 1, 1));

            if (LastClosure.Year == 2000)
            {
                // the date has not been set

                // assume today is the last closure datetime
                LastClosure = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59);

                // check if this is correct
                try
                {
                    LedgerMutation mt = Context.LedgerMutationSet.OrderBy(o => o.CreateDateTime).First<LedgerMutation>();
                    if (mt != null)
                    {
                        // there have been earlier material mutations. Use that date.
                        LastClosure = new DateTime(mt.BookingDateTime.Year, mt.BookingDateTime.Month, mt.BookingDateTime.Day, 23, 59, 59);
                    }
                }
                catch
                {
                }
            }
            LastClosure = new DateTime(LastClosure.Year, LastClosure.Month, LastClosure.Day, 23, 59, 59);
            return LastClosure;
        }

        public static void SetLastLedgerClosureDateTime(ModelTMSContainer Context, DateTime LastClosure, string LedgerDescription)
        {
            LastClosure = new DateTime(LastClosure.Year, LastClosure.Month, LastClosure.Day, 23, 59, 59);
            SystemSettingSet.SetSystemSettingDateTime(Context, "Last" + LedgerDescription + "ClosureDateTime", LastClosure, "The last closure date for the ledger.");
        }

    }
    #endregion

    #region MaterialSet
    public class MaterialSet
    {
        public static void CheckMaterialClosures(ModelTMSContainer Context, System.Web.UI.Page PageX)
        // Note : in contrast to regular application architecture this method uses its own transactions !!!
        {
            bool Success = true;
            DateTime LastClosureDateTime = SystemSettingSet.GetLastMaterialClosureDateTime(Context);

            // start the closure process until we are at today
            while ((LastClosureDateTime < DateTime.Today.AddDays(-1)) && (Success))
            {
                    // start transaction
                    using (TransactionScope TS = new TransactionScope())
                    {
                        try
                        {
                            foreach (Material mat in Context.MaterialSet.Where<Material>(m => m.IsActive))
                            {
                                //if ((mat.IsActive) && (mat.GetMaterialStockPosition(Context) == mat))
                                if (mat.IsActive)
                                {
                                    MaterialClosure mc = new MaterialClosure();

                                    mc.Material = mat;
                                    mc.Description = mat.Description;
                                    mc.ClosureDateTime = LastClosureDateTime;
                                    mc.RecalcTotals(Context);

                                    Context.AddToMaterialClosureSet(mc);
                                }
                            } //foreach

                            // next closure date please
                            LastClosureDateTime = LastClosureDateTime.AddDays(1);
                            SystemSettingSet.SetLastMaterialClosureDateTime(Context, LastClosureDateTime);
                            
                            // commit the transaciton
                            Context.SaveChanges();
                            TS.Complete();
                        }
                        catch (Exception ex) // commit or procedure failed somewhere
                        {
                            // rollback transaction 
                            TS.Dispose();

                            // inform user
                            Common.InformUserOnTransactionFail(ex, PageX);

                            Success = false;
                        }
                    } //using

            } // while

        }
    }
    #endregion

    #region LedgerSet
    public class LedgerSet
    {
        public static void CheckLedgerClosures(ModelTMSContainer Context, System.Web.UI.Page PageX)
        // Note : in contrast to regular application architecture this method uses its own transactions !!!
        {
            bool Success = true;
            DateTime LastClosureDateTime = SystemSettingSet.GetLastLedgerClosureDateTime(Context, "Ledger");

            // start the closure process until we are at today
            while ((LastClosureDateTime < DateTime.Today.AddDays(-1)) && (Success))
            {
                // start transaction
                using (TransactionScope TS = new TransactionScope())
                {
                    try
                    {
                        foreach (Ledger led in Context.LedgerSet.Where<Ledger>(m => m.IsActive))
                        {
                            LedgerClosure lc = new LedgerClosure();
                            lc.ClosureDate = LastClosureDateTime;
                            lc.Ledger = led;
                            lc.Description = "Sluitstand dagboek " + led.Description;

                            lc.RecalcTotals(Context, false, false);
                        }

                        // next closure date please
                        LastClosureDateTime = LastClosureDateTime.AddDays(1);
                        SystemSettingSet.SetLastLedgerClosureDateTime(Context, LastClosureDateTime, "Ledger");

                        // commit the transaciton
                        Context.SaveChanges();
                        TS.Complete();
                    }
                    catch (Exception ex) // commit or procedure failed somewhere
                    {
                        // rollback transaction 
                        TS.Dispose();

                        // inform user
                        Common.InformUserOnTransactionFail(ex, PageX);

                        Success = false;
                    }
                } //using

            } // while

        }
    }
    #endregion

    #region LedgerBookingCodeSet
    public class LedgerBookingCodeSet
    {
        public static void CheckLedgerBookingCodeClosures(ModelTMSContainer Context, System.Web.UI.Page PageX)
        // Note : in contrast to regular application architecture this method uses its own transactions !!!
        {
            bool Success = true;
            DateTime LastClosureDateTime = SystemSettingSet.GetLastLedgerClosureDateTime(Context, "LedgerBookingCode");

            // start the closure process until we are at today
            while ((LastClosureDateTime < DateTime.Today.AddDays(-1)) && (Success))
            {
                // start transaction
                using (TransactionScope TS = new TransactionScope())
                {
                    try
                    {
                        foreach (LedgerBookingCode led in Context.LedgerBookingCodeSet.Where<LedgerBookingCode>(m => m.IsActive))
                        {
                            LedgerClosure lc = new LedgerClosure();
                            lc.ClosureDate = LastClosureDateTime;
                            lc.LedgerBookingCode = led;
                            lc.Description = "Sluitstand " + led.Description; 

                            lc.RecalcTotals(Context, true, true);
                        }

                        // next closure date please
                        LastClosureDateTime = LastClosureDateTime.AddDays(1);
                        SystemSettingSet.SetLastLedgerClosureDateTime(Context, LastClosureDateTime, "LedgerBookingCode");

                        // commit the transaciton
                        Context.SaveChanges();
                        TS.Complete();
                    }
                    catch (Exception ex) // commit or procedure failed somewhere
                    {
                        // rollback transaction 
                        TS.Dispose();

                        // inform user
                        Common.InformUserOnTransactionFail(ex, PageX);

                        Success = false;
                    }
                } //using

            } // while

        }
    }
    #endregion

    #region Staffmember set
    public class StaffMemberSet
    {
        public static int AmountOfStaffMembersWithTMSLogin(ModelTMSContainer context)
        {
            ObjectQuery oq = context.CreateQuery<DbDataRecord>(@"select count(0) 
                from StaffMemberSet as it 
                where it.IsActive and it.HasVMSAccount");

            int Counter = 0;
            foreach (DbDataRecord rec in oq)
            {
                Counter = rec.GetInt32(0);
            }

            return Counter;
        }
    }
    #endregion

}