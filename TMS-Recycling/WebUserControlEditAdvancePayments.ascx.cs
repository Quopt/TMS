using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;
using System.Collections;
using System.Data;
using System.Data.Objects;

namespace TMS_Recycling
{
    public partial class WebUserControlEditAdvancePayments : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LabelAdvancePaymentData.Text != "") { LoadAPLines(); }
            XmlDataSourceAdvancePaymentCorrections.Data = LabelAdvancePaymentData.Text;

            if (!IsPostBack)
            {
                if (Request.Params["CustId"] != null)
                {
                    try
                    {
                        CustomerID = new System.Guid(Request.Params["CustId"].ToString());
                    }
                    catch (Exception)
                    {
                        /* suffocate */
                    }
                }
                if (Request.Params["InvoiceId"] != null)
                {
                    try
                    {
                        InvoiceID = new System.Guid(Request.Params["InvoiceId"].ToString());
                    }
                    catch (Exception)
                    {
                        /* suffocate */
                    }
                }
            }
        }


        protected void Page_PreRender(object sender, EventArgs e)
        {
            XmlDataSourceAdvancePaymentCorrections.Data = LabelAdvancePaymentData.Text;

            DropDownListAdvancePayments_SelectedIndexChanged(null, null);
        }
        
        // this control may have its own control object context
        private ModelTMSContainer _ControlObjectContext = null;
        public ModelTMSContainer ControlObjectContext
        {
            set
            {
                _ControlObjectContext = value;
            }
            get
            {
                if (_ControlObjectContext == null)
                {
                    _ControlObjectContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);
                } 
                return _ControlObjectContext;
            }
        }

        // customer id for this ap
        private System.Guid _CustomerID;
        public System.Guid CustomerID
        {
            set
            {
                _CustomerID = value;
                LabelCustID.Text = _CustomerID.ToString();
            }
            get
            {
                return new System.Guid(LabelCustID.Text);
            }
        }

        // order id for this ap
        private System.Guid _InvoiceID;
        public System.Guid InvoiceID
        {
            set
            {
                _InvoiceID = value;
                LabelInvoiceID.Text = _InvoiceID.ToString();

                // try to load the orderlines from this order
                string Query = "SELECT VALUE it FROM InvoiceLineSet as it WHERE it.Invoice.id = @Invoice_ID and it.RelationAdvancePayment <> null";

                ObjectQuery<InvoiceLine> query = new ObjectQuery<InvoiceLine>(Query, _ControlObjectContext);
                query.Parameters.Add( new ObjectParameter("Invoice_ID", _InvoiceID));
                ObjectResult<InvoiceLine> ilines = query.Execute(MergeOption.AppendOnly);

                foreach (InvoiceLine il in ilines)
                {
                    AdvancePaymentLines.Add(il);
                }

                SaveAPLines();
            }
            get
            {
                return new System.Guid(LabelInvoiceID.Text);
            }
        }

        // has potential advance payments
        public Boolean CustomerHasOpenAdvancePayments()
        {
            DropDownListAdvancePayments.DataBind();
            return DropDownListAdvancePayments.Items.Count > 0;
        }

        // advance payment correction data
        public ArrayList AdvancePaymentLines = new ArrayList();
        public void LoadAPLines()
        {
            if (LabelAdvancePaymentData.Text != "")
            {
                StringReader TempData = new StringReader(LabelAdvancePaymentData.Text);
                XmlTextReader CurrentAPs = new XmlTextReader(TempData);
                AdvancePaymentLines = new ArrayList();

                while (CurrentAPs.Read())
                {
                    if (CurrentAPs.Name == "AdvancePayment")
                    {
                        RelationAdvancePayment AP = new RelationAdvancePayment();

                        AP.Id = Guid.Parse(CurrentAPs.GetAttribute("id"));
                        AP.Description = CurrentAPs.GetAttribute("description");
                        AP.Amount = Convert.ToDouble(CurrentAPs.GetAttribute("amount"));
                        AdvancePaymentLines.Add(AP);
                    }
                }
            }
            else
            {
                SaveAPLines();
            }
        }
        public void SaveAPLines()
        {
            StringWriter TempData = new StringWriter();
            XmlTextWriter CurrentAPs = new XmlTextWriter(TempData);

            CurrentAPs.WriteStartElement("AdvancePaymentLines");
            for (int i = 0; i < AdvancePaymentLines.Count; i++)
            {
                RelationAdvancePayment TempAP = AdvancePaymentLines[i] as RelationAdvancePayment;

                CurrentAPs.WriteStartElement("AdvancePayment");

                CurrentAPs.WriteAttributeString("id", TempAP.Id.ToString());
                CurrentAPs.WriteAttributeString("description", TempAP.Description);
                CurrentAPs.WriteAttributeString("amount", TempAP.Amount.ToString());

                CurrentAPs.WriteEndElement();
            }
            CurrentAPs.WriteEndElement();

            TempData.Close();
            LabelAdvancePaymentData.Text = TempData.ToString();
        }

        protected void DropDownListAdvancePayments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListAdvancePayments.SelectedValue != "")
            {
                Guid SelID = Guid.Parse(DropDownListAdvancePayments.SelectedValue);

                try
                {
                    RelationAdvancePayment TempAP = _ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RelationAdvancePaymentSet", "Id", SelID)) as RelationAdvancePayment;

                    LabelAdvancePaymentInformation.Text =
                        LabelAPPayDate.Text + TempAP.PaymentDateTime.ToString() + "<BR>" +
                        LabelAPTotalAmount.Text + TempAP.Amount + "<BR>" +
                        LabelAPPaidBackAmout.Text + TempAP.AmountPaidBack + "<BR>" +
                        LabelAPStillOpen.Text + Convert.ToString(TempAP.Amount - TempAP.AmountPaidBack);
                }
                catch (Exception)
                {
                }

                try
                {
                    RelationWork TempWork = _ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RelationWorkSet", "Id", SelID)) as RelationWork;

                    LabelAdvancePaymentInformation.Text =
                        LabelAPPayDate.Text + TempWork.AgreementDateTime.ToString() + "<BR>" +
                        LabelAPTotalAmount.Text + TempWork.AmountEXVat + "<BR>" +
                        LabelAPPaidBackAmout.Text + TempWork.AmountPaidBack + "<BR>" +
                        LabelAPStillOpen.Text + Convert.ToString(TempWork.AmountEXVat - TempWork.AmountPaidBack);
                    if (TempWork.IsVATApplicable)
                    {
                        LabelAdvancePaymentInformation.Text = LabelAdvancePaymentInformation.Text + "<BR>" + String.Format(LabelAPVATWarning.Text, TempWork.VATPercentage);
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        protected void ButtonAddAdvancePaymentCorrection_Click(object sender, EventArgs e)
        {
            Boolean Continue = false;
            if (TextBoxAdvancePaymentAmountToBeCorrected.Text != "")
            {
                // values filled, check for numbers
                try
                {
                    Double Temp = Convert.ToDouble(TextBoxAdvancePaymentAmountToBeCorrected.Text);
                    Continue = (Temp != 0);
                }
                catch (Exception)
                { /*just suffocate */ }
            }

            if (Continue)
            {
                // add the material, type and ID to the grid
                LoadAPLines();

                RelationAdvancePayment TempLine = new RelationAdvancePayment();

                TempLine.Id = Guid.Parse(DropDownListAdvancePayments.SelectedValue);
                TempLine.Description = DropDownListAdvancePayments.SelectedItem.Text;
                TempLine.Amount = -Convert.ToDouble(TextBoxAdvancePaymentAmountToBeCorrected.Text);
                AdvancePaymentLines.Add(TempLine);

                SaveAPLines();
            }
        }

        protected void GridViewAdvancePaymentCorrections_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            e.Cancel = true;

            LoadAPLines();

            Guid SearchGuidID = Guid.Parse(e.Keys[0].ToString());

            for (int i = AdvancePaymentLines.Count - 1; i >= 0; i--)
            {
                RelationAdvancePayment TempLine = AdvancePaymentLines[i] as RelationAdvancePayment;
                if (TempLine.Id == SearchGuidID)
                {
                    AdvancePaymentLines.RemoveAt(i);
                    break;
                }
            }

            SaveAPLines();
        }

        public void SwitchPurchaseType(InvoiceType it)
        {
            switch (it)
            {
                case InvoiceType.Buy:
                    LabelPaymentType.Text = "Paid";
                    LabelWorkType.Text = "ByUs";
                    break;
                case InvoiceType.Sell:
                    LabelPaymentType.Text = "Received";
                    LabelWorkType.Text = "ByCustomer";
                    break;
            }
        }
    }
}