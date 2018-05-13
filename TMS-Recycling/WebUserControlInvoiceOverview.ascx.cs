using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebUserControlInvoiceOverview : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetName = "Invoice";

            if (!IsPostBack)
            {
                // set the correct filters
                Common.AddInvoiceStatusList(DropDownListBookingType.Items, false);
                CalendarControlStartDate.SelectedDate = new DateTime(Common.CurrentClientDateTime(Session).Year, 1, 1);
                CalendarControlEndDate.SelectedDate = Common.CurrentClientDateTime(Session).Date;

                ButtonSearch_Click(sender, e);

                if (Request.Params["InvoiceNumber"] != null)
                {
                    TextBoxInvoiceNo.Text = Request.Params["InvoiceNumber"];
                    EntityDataSourcePurchaseInvoices.CommandParameters["InvoiceNumber"].DefaultValue = TextBoxInvoiceNo.Text;
                    EntityDataSourcePurchaseInvoices.DataBind();

                    if (GridViewSelectedInvoices.Rows.Count == 1)
                    {
                        GridViewSelectedInvoices.SelectedIndex = 0;
                        GridViewSelectedInvoices_SelectedIndexChanged(null, null);
                    }
                }
            }
        }

        void Page_PreRender(object sender, EventArgs e)
        {
            if (WebUserControlInvoiceBase1.RefreshRequired)
            {
                WebUserControlInvoiceBase1.RefreshRequired = false;
                WebUserControlInvoiceBase1.Visible = false;
                ButtonSearch_Click(sender, e);
            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            EntityDataSourcePurchaseInvoices.DefaultContainerName = EntityDataSourcePurchaseInvoices.DefaultContainerName;

            EntityDataSourcePurchaseInvoices.CommandParameters["Description"].DefaultValue = TextBoxFilterName.Text == "" ? "%" : "%" + TextBoxFilterName.Text + "%";
            EntityDataSourcePurchaseInvoices.CommandParameters["StartDate"].DefaultValue = CalendarControlStartDate.SelectedDate.ToString();
            EntityDataSourcePurchaseInvoices.CommandParameters["EndDate"].DefaultValue = CalendarControlEndDate.SelectedDate.AddDays(1).ToString();

            EntityDataSourcePurchaseInvoices.CommandParameters["InvoiceNumber"].DefaultValue = TextBoxInvoiceNo.Text == "" ? "%" : "%" + TextBoxInvoiceNo.Text + "%";
            EntityDataSourcePurchaseInvoices.CommandParameters["InvoiceStatus"].DefaultValue = DropDownListBookingType.SelectedValue == "" ? "%" : DropDownListBookingType.SelectedValue;
            EntityDataSourcePurchaseInvoices.CommandParameters["MaterialDescription"].DefaultValue = DropDownListMaterial.SelectedValue == "" ? "%" : DropDownListMaterial.SelectedValue;
            EntityDataSourcePurchaseInvoices.CommandParameters["CorrectedStatus"].DefaultValue = DropDownListCorrected.SelectedValue == "" ? "%" : DropDownListCorrected.SelectedValue;
            EntityDataSourcePurchaseInvoices.CommandParameters["RelationDescription"].DefaultValue = TextBoxCustomer.Text == "" ? "%" : "%" + TextBoxCustomer.Text + "%";
            EntityDataSourcePurchaseInvoices.CommandParameters["LocationDescription"].DefaultValue = TextBoxLocation.Text == "" ? "%" : "%" + TextBoxLocation.Text + "%";

            EntityDataSourcePurchaseInvoices.DataBind();

            WebUserControlInvoiceBase1.Visible = false;
        }

        protected void GridViewSelectedInvoices_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowSelectedInvoice(GridViewSelectedInvoices.SelectedDataKey.Value.ToString());
        }

        private void ShowSelectedInvoice(string InvoiceID)
        {
            WebUserControlInvoiceBase1.InitUserControl();
            WebUserControlInvoiceBase1.KeyID = new System.Guid(InvoiceID);
            WebUserControlInvoiceBase1.Visible = true;
            WebUserControlInvoiceBase1.DataBind();
        }

        public void SwitchPurchaseType(InvoiceType it)
        {
            ButtonNew.Visible = false;
            switch (it)
            {
                case InvoiceType.Buy:
                    LabelInvoiceType.Text = "Buy";
                    LabelSubInvoiceType.Text = "Purchase";
                    break;
                case InvoiceType.Sell:
                    LabelInvoiceType.Text = "Sell";
                    LabelSubInvoiceType.Text = "Purchase";
                    break;
                case InvoiceType.Rent:
                    LabelInvoiceType.Text = "Sell";
                    LabelSubInvoiceType.Text = "Rent";
                    break;
                case InvoiceType.BuyLedger:
                    LabelInvoiceType.Text = "Buy";
                    LabelSubInvoiceType.Text = "Ledger";
                    ButtonNew.Visible = true;
                    break;
                case InvoiceType.SellLedger:
                    LabelInvoiceType.Text = "Sell";
                    LabelSubInvoiceType.Text = "Ledger";
                    ButtonNew.Visible = true;
                    break;
            }
            WebUserControlInvoiceBase1.SwitchPurchaseType(it);
        }

        protected void GridViewSelectedInvoices_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row != null) && (e.Row.DataItem != null))
            {
                e.Row.Cells[5].Text = Common.TranslateEnumValue(e.Row.Cells[5].Text, DropDownListBookingType.Items);
            }
        }

        protected void ButtonNew_Click(object sender, EventArgs e)
        {
            // create new empty invoice
            Invoice NewInvoice = new Invoice();
            NewInvoice.GenerateInvoiceNumber(ControlObjectContext);
            NewInvoice.Description = "Factuur " + NewInvoice.InvoiceNumber.ToString() + " dd " + NewInvoice.BookingDateTime.ToString();
            NewInvoice.InvoiceType = LabelInvoiceType.Text;
            NewInvoice.InvoiceSubType = LabelSubInvoiceType.Text;
            NewInvoice.Ledger = ControlObjectContext.LedgerSet.First();

            ControlObjectContext.AddToInvoiceSet(NewInvoice);

            // save 
            ControlObjectContext.SaveChanges();

            // show
            ShowSelectedInvoice(NewInvoice.Id.ToString() );
        }
    }

}