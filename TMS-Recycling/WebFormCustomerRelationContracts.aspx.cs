using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Objects;
using System.Data.Common;
using System.Drawing;

namespace TMS_Recycling
{
    public partial class WebFormCustomerRelationContracts : ClassTMSWebPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // get the relation description
                ModelTMSContainer _ControlObjectContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);
                EntityKey TempKey = new EntityKey("ModelTMSContainer.RelationSet", "Id", Guid.Parse(Request.Params["Id"]));
                Relation TempObj = _ControlObjectContext.GetObjectByKey(TempKey) as Relation;
                LabelObjectName.Text = TempObj.Description;

                Common.AddRelationContractStatusList(DropDownListContractStatus.Items, true);
                DropDownListContractStatus.SelectedIndex = 0;

                DetailTable.Visible = false;
            }
        }

        void Page_PreRender(object sender, EventArgs e)
        {
            if (WebUserControlCustomerRelationContract1.RefreshRequired)
            {
                WebUserControlCustomerRelationContract1.RefreshRequired = false;
                WebUserControlCustomerRelationContract1.Visible = false;
                DetailTable.Visible = false;
                ButtonSearch_Click(sender, e);
            }


        }

        protected void GridViewResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            WebUserControlCustomerRelationContract1.DataBind();
            WebUserControlCustomerRelationContract1.KeyID = new System.Guid(GridViewResults.SelectedDataKey.Value.ToString());
            LabelContractID.Text = WebUserControlCustomerRelationContract1.KeyID.ToString();
            WebUserControlCustomerRelationContract1.Visible = true;
            DetailTable.Visible = true;
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            EntityDataSourceRelation.DefaultContainerName = EntityDataSourceRelation.DefaultContainerName;
            EntityDataSourceRelation.DataBind();
        }

        protected void ButtonNew_Click(object sender, EventArgs e)
        {
            RelationContract NewObj = new RelationContract();
            ModelTMSContainer Temp = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);
            EntityKey TempKey = new EntityKey("ModelTMSContainer.RelationSet", "Id", Guid.Parse(Request.Params["Id"]));
            Relation TempObj = Temp.GetObjectByKey(TempKey) as Relation;

            NewObj.Relation = TempObj;

            NewObj.ContractDate = Common.CurrentClientDate(Session); 
            NewObj.ContractStartDate = Common.CurrentClientDate(Session); 
            NewObj.ContractEndDate = Common.CurrentClientDate(Session).AddMonths(1); 

            NewObj.ContractType = "Buy";
            NewObj.ContractPriority = 5;
            NewObj.ContractStatus = "Open";

            NewObj.YourReference = "";
            NewObj.PaymentConditions = "";
            NewObj.DeliveryConditions = "";

            NewObj.HasContractGuidance = false;

            Temp.AddToRelationContractSet(NewObj);
            Temp.SaveChanges(SaveOptions.DetectChangesBeforeSave);

            WebUserControlCustomerRelationContract1.KeyID = NewObj.Id;
            LabelContractID.Text = WebUserControlCustomerRelationContract1.KeyID.ToString();
            WebUserControlCustomerRelationContract1.Visible = true;
            DetailTable.Visible = true;
        }

        protected void GridViewContractMaterials_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void ButtonNewContractMaterial_Click(object sender, EventArgs e)
        {
            if (WebUserControlCustomerRelationContract1.SaveData(true))
            {

                RelationContractMaterial NewObj = new RelationContractMaterial();
                ModelTMSContainer Temp = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);
                EntityKey TempKey = new EntityKey("ModelTMSContainer.RelationContractSet", "Id", WebUserControlCustomerRelationContract1.KeyID);
                RelationContract TempObj = Temp.GetObjectByKey(TempKey) as RelationContract;

                NewObj.RelationContract = TempObj;
                NewObj.Material = Temp.MaterialSet.First();
                NewObj.Description = "Nieuw materiaal";

                Temp.AddToRelationContractMaterialSet(NewObj);
                Temp.SaveChanges(SaveOptions.DetectChangesBeforeSave);

                RefreshContractMaterials();
            }
        }

        protected void ButtonRefresh_Click(object sender, EventArgs e)
        {
            RefreshContractMaterials();
        }

        private void RefreshContractMaterials()
        {
            GridViewContractMaterials.DataBind();
        }

        protected void GridViewContractMaterials_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // add the edit button
            if (e.Row.DataItem != null)
            {
                TableCell tc = e.Row.Cells[e.Row.Cells.Count - 1];

                DbDataRecord ddr = e.Row.DataItem as DbDataRecord;
                URLPopUpControl upc = LoadControl("URLPopUpControl.ascx") as URLPopUpControl;

                upc.URLToPopup = "WebFormPopup.aspx?UC=CustomerRelationContractMaterial&Id=" + ddr.GetValue(0).ToString();
                upc.Text = "Toon contractmateriaal";
                tc.Controls.Add(upc);
            }
        }

        protected void GridViewResults_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
    }
}