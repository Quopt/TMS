using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects;

namespace TMS_Recycling
{
    public partial class WebUserControlCompanyLocation : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetName = "Location";

            if (!IsPostBack)
            {
                Common.AddCurrencyList(DropDownList_PreferredCurrency_SelectedValue.Items, true);
                Common.AddCountryList(DropDownList_Country_SelectedValue.Items, true);
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ImageInvoiceLogo.ImageUrl = "ImageHandler.ashx?Table=LocationSet&Field=CompanyLogoImage&Id=" + KeyID.ToString();
            ImageMembershipsLogo.ImageUrl = "ImageHandler.ashx?Table=LocationSet&Field=CompanyMembershipsLogo&Id=" + KeyID.ToString();
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            StandardButtonCancelClickHandler(sender, e);
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            StandardButtonSaveClickHandler(sender, e);
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            StandardButtonDeleteClickHandler(sender, e);
        }

        protected void URLPopUpControlAddCashLedgerForThisLocation_OnBeforePopUpOpened(object sender, EventArgs e)
        {
            SaveDataIntoDataItemFromControls();

            // create a new ledger and show this as a popup
            Ledger NewMat = new Ledger();

            NewMat.LedgerType = "Cash";
            NewMat.LimitToLocation = DataItem as Location;
            NewMat.LimitToLocation.CashLedger = NewMat;

            ControlObjectContext.AddToLedgerSet(NewMat);
            ControlObjectContext.SaveChanges(SaveOptions.DetectChangesBeforeSave);

            URLPopUpControlAddCashLedgerForThisLocation.URLToPopup = "webformpopup.aspx?uc=LedgerBase&Id=" + NewMat.Id.ToString();
        }

        protected void URLPopUpControlEditCashLedger_OnBeforePopUpOpened(object sender, EventArgs e)
        {
            ButtonSave_Click(sender,e);

            URLPopUpControlEditCashLedger.URLToPopup = "webformpopup.aspx?uc=LedgerBase&Id=" + DropDownList_CashLedger.SelectedValue;
        }

        protected void URLPopUpControlAddBankLedgerForThisLocation_OnBeforePopUpOpened(object sender, EventArgs e)
        {
            SaveDataIntoDataItemFromControls();

            // create a new ledger and show this as a popup
            Ledger NewMat = new Ledger();

            NewMat.LedgerType = "Bank";
            NewMat.LimitToLocation = DataItem as Location;
            NewMat.LimitToLocation.BankLedger = NewMat;

            ControlObjectContext.AddToLedgerSet(NewMat);
            ControlObjectContext.SaveChanges(SaveOptions.DetectChangesBeforeSave);

            URLPopUpControlAddBankLedgerForThisLocation.URLToPopup = "webformpopup.aspx?uc=LedgerBase&Id=" + NewMat.Id.ToString();
        }

        protected void URLPopUpControlEditBankLedger_OnBeforePopUpOpened(object sender, EventArgs e)
        {
            ButtonSave_Click(sender, e);

            URLPopUpControlEditBankLedger.URLToPopup = "webformpopup.aspx?uc=LedgerBase&Id=" + DropDownList_BankLedger.SelectedValue;
        }

        protected void URLPopUpControlAddMaterialForThisLocation_OnBeforePopUpOpened(object sender, EventArgs e)
        {
            SaveDataIntoDataItemFromControls();

            // create a new ledger and show this as a popup
            Material NewMat = new Material();

            NewMat.MaterialUnit = ControlObjectContext.MaterialUnitSet.First();
            NewMat.PurchaseLedgerBookingCode = ControlObjectContext.LedgerBookingCodeSet.First();;
            NewMat.SalesLedgerBookingCode = ControlObjectContext.LedgerBookingCodeSet.First(); ;
            NewMat.Category = "Other";
            NewMat.VATPercentage = 19;
            NewMat.Location = DataItem as Location;
            NewMat.Location.MaterialForDirt = NewMat;

            ControlObjectContext.AddToMaterialSet(NewMat);
            ControlObjectContext.SaveChanges(SaveOptions.DetectChangesBeforeSave);

            URLPopUpControlAddMaterialForThisLocation.URLToPopup = "webformpopup.aspx?uc=StockMaterial&Id=" + NewMat.Id.ToString();
        }

        protected void URLPopUpControlEditMaterial_OnBeforePopUpOpened(object sender, EventArgs e)
        {
            ButtonSave_Click(sender, e);

            URLPopUpControlEditMaterial.URLToPopup = "webformpopup.aspx?uc=StockMaterial&Id=" + DropDownList_MaterialForDirt.SelectedValue;
        }

        protected void URLPopUpControlAddWorkMaterialForThisLocation_OnBeforePopUpOpened(object sender, EventArgs e)
        {
            SaveDataIntoDataItemFromControls();

            // create a new ledger and show this as a popup
            Material NewMat = new Material();

            NewMat.MaterialUnit = ControlObjectContext.MaterialUnitSet.First();
            NewMat.PurchaseLedgerBookingCode = ControlObjectContext.LedgerBookingCodeSet.First(); ;
            NewMat.SalesLedgerBookingCode = ControlObjectContext.LedgerBookingCodeSet.First(); ;
            NewMat.Category = "Other";
            NewMat.VATPercentage = 19;
            NewMat.Location = DataItem as Location;
            NewMat.Location.MaterialForWork = NewMat;

            ControlObjectContext.AddToMaterialSet(NewMat);
            ControlObjectContext.SaveChanges(SaveOptions.DetectChangesBeforeSave);

            URLPopUpControlAddWorkMaterialForThisLocation.URLToPopup = "webformpopup.aspx?uc=StockMaterial&Id=" + NewMat.Id.ToString();
        }

        protected void URLPopUpControlEditWorkMaterial_OnBeforePopUpOpened(object sender, EventArgs e)
        {
            ButtonSave_Click(sender, e);

            URLPopUpControlEditWorkMaterial.URLToPopup = "webformpopup.aspx?uc=StockMaterial&Id=" + DropDownList_MaterialForWork.SelectedValue;
        }

        protected void GeneralOnPopupClosed(object sender, EventArgs e)
        {
            // refresh all entitydatatsources
            string s1 = DropDownList_BankLedger.SelectedValue;
            string s2 = DropDownList_CashLedger.SelectedValue;
            string s3 = DropDownList_MaterialForDirt.SelectedValue;
            string s4 = DropDownList_MaterialForWork.SelectedValue;
            string s5 = DropDownList_RelationBuy.SelectedValue;
            string s6 = DropDownList_RelationSale.SelectedValue;

            EntityDataSourceBankLedgers.ConnectionString = EntityDataSourceBankLedgers.ConnectionString;
            EntityDataSourceCashLedgers.ConnectionString = EntityDataSourceCashLedgers.ConnectionString;
            EntityDataSourceMaterialsForThisLocation.ConnectionString = EntityDataSourceMaterialsForThisLocation.ConnectionString;
            EntityDataSourceHourMaterialsForThisLocation.ConnectionString = EntityDataSourceHourMaterialsForThisLocation.ConnectionString;
            EntityDataSourceCustomers.ConnectionString = EntityDataSourceCustomers.ConnectionString;

            DropDownList_BankLedger.DataBind();
            if (DropDownList_BankLedger.Items.FindByValue(s1) != null) { DropDownList_BankLedger.SelectedValue = s1; }

            DropDownList_CashLedger.DataBind(); 
            if (DropDownList_CashLedger.Items.FindByValue(s2) != null) { DropDownList_CashLedger.SelectedValue = s2; }

            DropDownList_MaterialForDirt.DataBind();
            if (DropDownList_MaterialForDirt.Items.FindByValue(s3) != null) { DropDownList_MaterialForDirt.SelectedValue = s3; }

            DropDownList_MaterialForWork.DataBind();
            if (DropDownList_MaterialForWork.Items.FindByValue(s4) != null) { DropDownList_MaterialForWork.SelectedValue = s4; }
            
            DropDownList_RelationBuy.DataBind();
            if (DropDownList_RelationBuy.Items.FindByValue(s5) != null) { DropDownList_RelationBuy.SelectedValue = s5; }

            DropDownList_RelationSale.DataBind();
            if (DropDownList_RelationSale.Items.FindByValue(s6) != null) { DropDownList_RelationSale.SelectedValue = s6; }

        }

        protected void ButtonUploadInvoiceLogo_Click(object sender, EventArgs e)
        {
            try
            {
                (DataItem as Location).CompanyLogoImage = Common.ConvertImageToByteArray(
                                              System.Drawing.Image.FromStream(FileUploadInvoiceLogo.PostedFile.InputStream),
                                              System.Drawing.Imaging.ImageFormat.Png);
                ControlObjectContext.SaveChanges(SaveOptions.DetectChangesBeforeSave);
            }
            catch (Exception ex)
            {
                Common.InformUserOnGeneralFail(ex, Page, "Het opslaan van de afbeelding is mislukt.");
            }
        }

        protected void ButtonUploadMembershipsLogo_Click(object sender, EventArgs e)
        {
            try
            {
                (DataItem as Location).CompanyMembershipsLogo = Common.ConvertImageToByteArray(
                                              System.Drawing.Image.FromStream(FileUploadMembershipsLogo.PostedFile.InputStream),
                                              System.Drawing.Imaging.ImageFormat.Png);
                ControlObjectContext.SaveChanges(SaveOptions.DetectChangesBeforeSave);
            }
            catch (Exception ex)
            {
                Common.InformUserOnGeneralFail(ex, Page, "Het opslaan van de afbeelding is mislukt.");
            }
        }



        protected void URLPopUpControlEditBuyCustomer_OnBeforePopUpOpened(object sender, EventArgs e)
        {
            ButtonSave_Click(sender, e);

            URLPopUpControlEditBuyCustomer.URLToPopup = "webformpopup.aspx?uc=CustomerRelation&Id=" + DropDownList_RelationBuy.SelectedValue;
        }

        protected void URLPopUpControlAddBuyCustomer_OnBeforePopUpOpened(object sender, EventArgs e)
        {
            SaveDataIntoDataItemFromControls();

            // create a new ledger and show this as a popup
            Relation NewRel = new Relation();

            NewRel.CustomerType = "Creditor";

            ControlObjectContext.AddToRelationSet(NewRel);
            ControlObjectContext.SaveChanges(SaveOptions.DetectChangesBeforeSave);

            URLPopUpControlAddBuyCustomer.URLToPopup = "webformpopup.aspx?uc=CustomerRelation&Id=" + NewRel.Id.ToString();
        }

        protected void URLPopUpControlEditSaleCustomer_OnBeforePopUpOpened(object sender, EventArgs e)
        {
            ButtonSave_Click(sender, e);

            URLPopUpControlEditSaleCustomer.URLToPopup = "webformpopup.aspx?uc=CustomerRelation&Id=" + DropDownList_RelationSale.SelectedValue;
        }

        protected void URLPopUpControlAddSaleCustomer_OnBeforePopUpOpened(object sender, EventArgs e)
        {
            SaveDataIntoDataItemFromControls();

            // create a new ledger and show this as a popup
            Relation NewRel = new Relation();

            NewRel.CustomerType = "Debtor";

            ControlObjectContext.AddToRelationSet(NewRel);
            ControlObjectContext.SaveChanges(SaveOptions.DetectChangesBeforeSave);

            URLPopUpControlAddSaleCustomer.URLToPopup = "webformpopup.aspx?uc=CustomerRelation&Id=" + NewRel.Id.ToString();
        }

    }
}