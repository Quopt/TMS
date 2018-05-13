using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects;
using System.Reflection;
using System.Data;
using System.Runtime.Serialization;
using AjaxControlToolkit;

namespace TMS_Recycling
{
    public class ControlArgumentException : ArgumentException, ISerializable
    {
          public Control CausingControl = null;

          public ControlArgumentException()
              : base()
           {
           }

           public ControlArgumentException(string Message)
              : base(Message)
           {
           }

           public ControlArgumentException(string Message,
                                           Exception InnerException)
              : base(Message, InnerException)
           {
           }

           public ControlArgumentException(string Message, string paramName)
               : base(Message, paramName)
           {
           }

           public ControlArgumentException(string Message, string paramName, Control _CausingControl)
               : base(Message, paramName)
           {
               CausingControl = _CausingControl;
               return;
           }

           protected ControlArgumentException(SerializationInfo Info,
                                              StreamingContext Context)
              : base(Info, Context)
           {
           }

    }

    public class ClassCustomBinding
    {
        public static PropertyInfo GetProperty(Object Data, string PropertyName)
        {
            PropertyInfo Result = null;

            PropertyInfo[] TempProp = Data.GetType().GetProperties();
            foreach (PropertyInfo p in TempProp)
            {
                if (p.Name == PropertyName)
                {
                    Result = p;
                    break;
                }
            }

            return Result;
        }

        public static void BindToControls(string EntitySet, System.Guid ID, ControlCollection ControlSet, Object Data, ModelTMSContainer _ControlObjectContext)
        {
            BindMe(EntitySet, ID, ControlSet, Data, true, _ControlObjectContext);
        }

        private static void BindMe(string EntitySet, System.Guid ID, ControlCollection ControlSet, Object Data, bool BindDirection, ModelTMSContainer _ControlObjectContext)
        {
            BindMeBase(EntitySet, ID, ControlSet, Data, BindDirection, _ControlObjectContext);
            BindMeBase(EntitySet, ID, ControlSet, Data, BindDirection, _ControlObjectContext); // do this 2 times for any combo boxes that might be linked to eachother
        }

        private static void BindMeBase(string EntitySet, System.Guid ID, ControlCollection ControlSet, Object Data, bool BindDirection, ModelTMSContainer _ControlObjectContext)
        {
            foreach (Control C in ControlSet)
            {
                // check for child controls
                if (C.HasControls()) { BindMeBase(EntitySet, ID, C.Controls, Data, BindDirection, _ControlObjectContext); }

                if ((C.ID != null) && (C.ID.IndexOf("_") > 0))
                {
                    // there is a underscore in the ID of this control 

                    // extract the first and optional second argument
                    string FirstArg;
                    string SecondArg = "Text";

                    FirstArg = C.ID.Substring(C.ID.IndexOf("_") + 1);
                    if (FirstArg.IndexOf("_") > 0)
                    {
                        SecondArg = FirstArg.Substring( FirstArg.IndexOf("_") + 1);
                        FirstArg = FirstArg.Substring(0, FirstArg.IndexOf("_"));
                    }

                    string ControlType = C.GetType().Name;

                    PropertyInfo[] TempProp = C.GetType().GetProperties();
                    foreach (PropertyInfo p in TempProp)
                    {
                        if (p.Name == SecondArg  )
                        {
                            PropertyInfo[] TempPropData = Data.GetType().GetProperties();
                            foreach (PropertyInfo pd in TempPropData)
                            {
                                if (pd.Name == FirstArg)
                                {
                                    string PropName = "";
                                    try
                                    {
                                        if (BindDirection)
                                        {
                                            if ( ((ControlType == "DropDownList") || (ControlType.IndexOf("ComboBox") >= 0)) && (SecondArg == "Text")) // if secondarg is not set to something else but Text than we need to link an object in
                                            { // drop down list boxes need special treatment
                                                // this should be an object with an ID field with a linking guid
                                                if (pd.PropertyType.Name == "Guid")
                                                {
                                                    // this is a linking guid, but with an unknown type. This is always a StaffMember. try to set this.
                                                    String TempGuid = pd.GetValue(Data, null).ToString();
                                                    ListControl ddl = C as ListControl;
                                                    if ( (TempGuid != Guid.Empty.ToString()) && (ddl.Items.FindByValue(TempGuid) == null))
                                                    {
                                                        // this staffmember is deactivated. add it
                                                        ObjectQuery<StaffMember> oqsm = _ControlObjectContext.StaffMemberSet.Where("it.Id=@id", new ObjectParameter("id", Guid.Parse(TempGuid)));
                                                        if (oqsm.Count() > 0)
                                                        {
                                                            ddl.Items.Add(new ListItem( oqsm.First().Description, oqsm.First().Id.ToString() ) );
                                                        }
                                                        else
                                                        {
                                                            ddl.Items.Add( new ListItem("Unknown staffmember",TempGuid));
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (ddl.Items.FindByValue(TempGuid) != null)
                                                        {
                                                            ddl.SelectedValue = TempGuid;
                                                        }
                                                        else
                                                        {
                                                            ddl.SelectedIndex = -1;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    object tempObj = pd.GetValue(Data, null);
                                                    if (tempObj != null)
                                                    {
                                                        PropertyInfo PropID = Common.GetPropertyFromObject(tempObj, "id");
                                                        PropertyInfo PropDescription = Common.GetPropertyFromObject(tempObj, "Description");

                                                        // get the necessary values from the linked object
                                                        Guid TempID = Guid.Empty;
                                                        if ((PropID != null) && (tempObj != null))
                                                        {
                                                            TempID = Guid.Parse(PropID.GetValue(tempObj, null).ToString());
                                                        }

                                                        string TempDescription = "";
                                                        if ((PropDescription != null) && (tempObj != null))
                                                        {
                                                            TempDescription = PropDescription.GetValue(tempObj, null).ToString();
                                                        }

                                                        // set the value in the listbox if we have something to set
                                                        if (TempID != Guid.Empty)
                                                        {
                                                            ListControl ddl = C as ListControl;
                                                            ddl.DataBind(); // make sure we got correct items if none are loaded yet
                                                            ListItem tempLI = ddl.Items.FindByValue(TempID.ToString());
                                                            if (tempLI != null)
                                                            {
                                                                ddl.SelectedValue = tempLI.Value;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // nothing to set, make sure the listbox does not show a selection
                                                        ListControl ddl = C as ListControl;
                                                        ddl.DataBind(); // make sure we got correct items if none are loaded yet
                                                        ListItem tempLI = ddl.Items.FindByValue("");
                                                        if (tempLI != null)
                                                        {
                                                            ddl.SelectedValue = tempLI.Value;
                                                        }
                                                        else
                                                        {
                                                            ddl.SelectedIndex = -1;
                                                        }
                                                    }
                                                }

                                            }
                                            else
                                            { // regular controls
                                                string s = p.PropertyType.ToString();
                                                PropName = p.Name;
                                                if (s == "System.String")
                                                {
                                                    string tempStr = pd.GetValue(Data, null).ToString().Trim();
                                                    p.SetValue(C, tempStr, null);
                                                }
                                                else
                                                    if (s == "System.Boolean")
                                                    {
                                                        p.SetValue(C, System.Convert.ToBoolean(pd.GetValue(Data, null)), null);
                                                    }
                                                else
                                                    if (s == "System.DateTime")
                                                    {
                                                        p.SetValue(C, System.Convert.ToDateTime(pd.GetValue(Data, null)), null);
                                                    }
                                            }
                                        }
                                        else
                                        {
                                            if (((ControlType == "DropDownList") || (ControlType.IndexOf("ComboBox") >= 0)) && (SecondArg == "Text")) // if secondarg is not set to something else but Text than we need to link an object in
                                            { // drop down list boxes need special treatment
                                                ListControl ddl = C as ListControl;

                                                // if a value is set
                                                if ( (ddl.SelectedValue != "") && (ddl.SelectedValue != Guid.Empty.ToString() ) )
                                                {
                                                    object tempObj = pd.GetValue(Data, null);  
                                                    // get the object type
                                                    string tempStr = pd.PropertyType.Name;

                                                    // load the ID associated with this object type and get the object that way
                                                    if (tempStr == "Guid")
                                                    {  // staffmembers are not linked, just bij guid. correct this here.
                                                        pd.SetValue(Data, Guid.Parse(ddl.SelectedValue), null);
                                                    }
                                                    else
                                                    {
                                                        object newObj = _ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer." + tempStr + "Set", "Id", Guid.Parse(ddl.SelectedValue)));
                                                        pd.SetValue(Data, newObj, null);
                                                    }
                                                }
                                                else
                                                {
                                                    pd.SetValue(Data, null, null);
                                                }
                                            }
                                            else if (ControlType == "Label")
                                            {
                                                // do nothing for a label
                                            }
                                            else
                                            { // regular controls
                                                string s = pd.PropertyType.ToString();
                                                PropName = pd.Name;
                                                if (s == "System.String")
                                                {
                                                    string tempStr = p.GetValue(C, null).ToString().Trim();
                                                    pd.SetValue(Data, tempStr, null);
                                                }
                                                else if (s == "System.Boolean")
                                                {
                                                    pd.SetValue(Data, System.Convert.ToBoolean(p.GetValue(C, null)), null);
                                                }
                                                else if (s == "System.Int64")
                                                {
                                                    pd.SetValue(Data, System.Convert.ToInt64(p.GetValue(C, null)), null);
                                                }
                                                else if (s == "System.Byte")
                                                {
                                                    pd.SetValue(Data, System.Convert.ToByte(p.GetValue(C, null)), null);
                                                }
                                                else if (s == "System.Double")
                                                {
                                                    pd.SetValue(Data, System.Convert.ToDouble(p.GetValue(C, null)), null);
                                                }
                                                else if (s == "System.DateTime")
                                                {
                                                    pd.SetValue(Data, System.Convert.ToDateTime(p.GetValue(C, null)), null);
                                                }
                                            }
                                        }
                                    }
                                    catch
                                    {
                                        throw new ControlArgumentException("Parameter value does not conform to expected format.", PropName, C);
                                    }

                                    break;
                                }
                            }

                            break;
                        }
                    }
                }
            }
        }

        public static void BindToObject(string EntitySet, System.Guid ID, ControlCollection ControlSet, Object Data, ModelTMSContainer _ControlObjectContext)
        {
            BindMe(EntitySet, ID, ControlSet, Data, false, _ControlObjectContext);
        }

    }
}