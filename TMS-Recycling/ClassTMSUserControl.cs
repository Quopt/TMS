using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.Objects;
using System.Data;
using System.Data.Objects.DataClasses;
using System.Data.Metadata.Edm;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Compilation;

namespace TMS_Recycling
{
    public class ClassTMSUserControl : System.Web.UI.UserControl
    {
        public ClassTMSUserControl()
        {
            this.Load += new EventHandler(this.Page_Load);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((_ControlObjectContext == null) && (!this.DesignMode))
            {
                _ControlObjectContext = new ModelTMSContainer(Page.Session["CustomerConnectString"].ToString(), Session);
            } 
            
            if (!IsPostBack)
            {
            }
        }

        // the data item being edited
        object _DataItem = null;
        public object DataItem 
        {
            get
            {
                if (KeyID != Guid.Empty)
                {
                    if (_DataItem == null)
                    {
                        _DataItem = GetDataItem(KeyID);
                    }

                    return _DataItem;
                }
                else
                {
                    return null;
                }
            }
        }

        public Boolean DataItemPresent
        {
            get
            {
                return _DataItem != null;
            }
        }

        // the set name of the objet (without the Set additive, ie Location)
        string _SetName = ""; 
        public string SetName
        {
            set
            {
                _SetName = value;
                CheckForRequiredControls();
            }
            get
            {
                return _SetName;
            }
        }

        // the object context used by some of the methods in this base class
        private ModelTMSContainer _ControlObjectContext = null;
        public ModelTMSContainer ControlObjectContext
        {
            get
            {
                if (_ControlObjectContext == null)
                {
                    _ControlObjectContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);
                }

                return _ControlObjectContext;
            }
            set
            {
                _ControlObjectContext = value;
            }
        }

        // indicates if a refresh is required from the container, meaning that an object is either updated or deleted
        public Boolean RefreshRequired
        {
            set
            {
                CheckForRequiredControls();
               (FindControl ("CheckBoxRefreshRequired") as ICheckBoxControl).Checked = value;
            }
            get
            {
                CheckForRequiredControls();
                return (FindControl("CheckBoxRefreshRequired") as ICheckBoxControl).Checked;
            }
        }

        // the ID of the object this user control has to edit
        public System.Guid KeyID
        {
            get
            {
                CheckForRequiredControls();

                return new System.Guid((FindControl("LabelId") as System.Web.UI.WebControls.Label).Text);
            }
            set
            {
                CheckForRequiredControls();

                (FindControl("LabelId") as System.Web.UI.WebControls.Label).Text = value.ToString();

                if (DataItem != null)
                {
                    _DataItem = GetDataItem(new Guid(value.ToString()));
                    if (ControlObjectContext.GetObjectByKey((DataItem as IEntityWithKey).EntityKey) == null)
                    {
                        ControlObjectContext.Attach(DataItem as IEntityWithKey);
                    }

                    ClassCustomBinding.BindToControls("ModelTMSContainer." + SetName + "Set", value, Controls, DataItem, ControlObjectContext);
                }
            }
        }

        public void RebindControls()
        {
            ClassCustomBinding.BindToControls("ModelTMSContainer." + SetName + "Set", KeyID, Controls, DataItem, ControlObjectContext);
        }

        public object GetDataItem(Guid IDKey)
        {
            if (SetName != "")
            {
                EntityKey TempKey = new EntityKey("ModelTMSContainer." + SetName + "Set", "Id", IDKey);
                return ControlObjectContext.GetObjectByKey(TempKey);
            }
            else
            {
                return null;
            }
        }

        public void CheckForRequiredControls()
        {
            if (FindControl("LabelId") == null)
            {
                // the default required controls are not present in this form. Add them.
                System.Web.UI.WebControls.Label TempLabel = new System.Web.UI.WebControls.Label();
                TempLabel.ID = "LabelID";
                TempLabel.Text = Guid.Empty.ToString();
                TempLabel.Visible = false;
                Controls.Add(TempLabel);
                System.Web.UI.WebControls.CheckBox TempCheckbox = new System.Web.UI.WebControls.CheckBox();
                TempCheckbox.ID = "CheckBoxRefreshRequired";
                TempCheckbox.Visible = false;
                Controls.Add(TempCheckbox);
            }
        }

        #region Event handler helpers
        protected void StandardButtonCancelClickHandler(object sender, EventArgs e)
        {
            KeyID = KeyID;
        }

        protected void StandardButtonSaveClickHandler(object sender, EventArgs e)
        {
            StandardSaveHandler(sender, e, true);
        }

        public bool SaveData(bool ExecuteSaveChanges)
        {
            return StandardSaveHandler(null, null, ExecuteSaveChanges);
        }

        protected Boolean StandardSaveHandler(object sender, EventArgs e, bool ExecuteSaveChanges)
        {
            Boolean result = false;

            try
            {
                SaveDataIntoDataItemFromControls();

                if (ExecuteSaveChanges) { ControlObjectContext.SaveChanges(SaveOptions.DetectChangesBeforeSave); }

                result= true;
            }
            catch (ControlArgumentException ex)
            {
                Page.RegisterClientScriptBlock("Alert", "<script>alert('Het opslaan van de gegevens is mislukt. Corrigeer de waarde in het veld waar de cursor staat.');</script>");
                ex.CausingControl.Focus();
            }
            catch (Exception ex)
            {
                // inform user
                Common.InformUserOnTransactionFail(ex, Page);

                KeyID = KeyID;
            }
            return result;
        }

        public void SaveDataIntoDataItemFromControls()
        {
            ClassCustomBinding.BindToObject("ModelTMSContainer." + SetName + "Set", KeyID, Controls, DataItem, ControlObjectContext);
        }

        public Boolean AllowDelete()
        {
            // check if this object may be deleted
            // get the navigation properties with a 1:N relation from this object and check for count = 0
            Boolean AllowDel = true;
            var properties = DataItem.GetType().GetProperties();
            foreach (var property in properties)
            {
                //if (property.PropertyType.IsGenericType)
                if (property.PropertyType.Name.IndexOf("EntityCollection") == 0)
                { // this is a navigation property of a collection of objects
                    var t = property.PropertyType.GetGenericArguments().First();

                    // Navigation properties for EntityCollection? Check this by checking the type of the first object in this list. 
                    if (t.BaseType.Name == "EntityObject")
                    {
                        // now get the entity collection and determine if something is there, and if so, if there are more than 0 objects linked
                        ICollection<EntityObject> tempEnum = property.GetValue(DataItem, null) as ICollection<EntityObject>;
                        if ((tempEnum != null) && (tempEnum.Count() > 0))
                        {
                            AllowDel = false;
                            break;
                        }
                    }
                }
            }

            return AllowDel;
        }

        protected void StandardButtonDeleteClickHandler(object sender, EventArgs e)
        {
            StandardButtonDeleteClickMethod(sender, e);
        }

        protected Boolean StandardButtonDeleteClickMethod(object sender, EventArgs e)
        {
            Boolean result = false;

            // check if this object may be deleted
            Boolean AllowDel = AllowDelete();

            // if this object may not be deleted then warn about this
            if (!AllowDel)
            {
                Page.RegisterClientScriptBlock("Alert", "<script>alert('Dit gegeven kan niet worden verwijderd omdat dit elders in het systeem wordt gebruikt. Deactiveer het object ipv het te verwijderen.');</script>");
            }
            else // else delete this object
            {
                RefreshRequired = true; //request a refresh from the container

                try
                {
                    ControlObjectContext.DeleteObject(DataItem);

                    ControlObjectContext.SaveChanges(SaveOptions.DetectChangesBeforeSave);

                    result = true;

                    Page.RegisterClientScriptBlock("Alert", "<script>alert('De gegevens zijn succesvol verwijderd.');</script>");
                }
                catch (ControlArgumentException ex)
                {
                    string tempStr = ex.Message.Replace("'", "\"").Replace("\n", "").Replace("\r", "");
                    Page.RegisterClientScriptBlock("Alert", "<script>alert('Het verwijderen van de gegevens is mislukt. Mogelijk heeft een andere medewerker dit gegeven al verwijderd. (" + tempStr + ")');</script>");
                }
                catch (Exception ex)
                {
                    string tempStr = "";
                    if (ex.InnerException == null)
                    {
                        tempStr = ex.Message.Replace("'", "\"").Replace("\n", "").Replace("\r", "");
                    }
                    else
                    {
                        tempStr = ex.Message + " / " + ex.InnerException.ToString(); 
                        tempStr = tempStr.Replace("'", "\"").Replace("\n", "").Replace("\r", "");
                    }
                    Page.RegisterClientScriptBlock("Alert", "<script>alert('Het opslaan van de gegevens is mislukt omdat iemand anders de gegevens al heeft verwijderd of omdat er nog relaties zijn naar ander informatie die niet meer verwijderd kunnen worden. Probeer het nogmaals. (" + tempStr + ")');</script>");
                    KeyID = KeyID;
                }
            }

            return result;
        }
        #endregion


    }
}