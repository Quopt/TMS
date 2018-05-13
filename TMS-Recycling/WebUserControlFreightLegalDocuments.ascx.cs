using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebUserControlFreightLegalDocuments : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetName = "Freight";

            if (!IsPostBack)
            {

                Common.AddFreightOurRoleTypeList(ComboBox_TransportRole_SelectedValue.Items, true);
                Common.AddFreightTransportNotificationTransportType(ComboBox_TransportNotificationTransportType_SelectedValue.Items, true);
                Common.AddFreightTransportNotificationRemovalType(ComboBox_TransportNotificationRemovalType_SelectedValue.Items, true);
                Common.AddWrappingTypeList(ComboBox_TransportWrapping_SelectedValue.Items, true);
                Common.AddDCodeList(ComboBox_TransportDRCode_SelectedValue.Items, true, true);
                Common.AddRCodeList(ComboBox_TransportDRCode_SelectedValue.Items, false, false);
                Common.AddDCodeList(ComboBox_TransportDestructionAction_SelectedValue.Items, true, true);
                Common.AddRCodeList(ComboBox_TransportDestructionAction_SelectedValue.Items, false, false);
                Common.AddTransportTypeList(ComboBox_TransportType_SelectedValue.Items, true); 
                
                if (Request.Params["FreightId"] != null)
                {
                    KeyID = new Guid(Request.Params["FreightId"]);
                }

                Freight frg = DataItem as Freight;
                
                // check if this freight has to be initialised
                if (frg != null)
                {
                    LabelCustomerType.Text = frg.FreightDirection == "To warehouse" ? "Creditor" : "Debtor";
                    EntityDataSourceCustomers.DataBind();

                    if ((frg.TransportSourceCustomer == null) && (frg.TransportDestinationCustomer==null))
                    {
                        // initialise freight
                        frg.TransportSourceCustomer = frg.FreightDirection == "To warehouse" ? frg.FromRelation : null;
                        frg.TransportUltimateSourceCustomer = frg.TransportSourceCustomer;
                        frg.TransportCompanyCustomer = null;
                        frg.TransportDestinationCustomer = frg.FreightDirection == "To warehouse" ? null : frg.FromRelation;
                        frg.TransportDestructorCustomer = frg.TransportDestinationCustomer;
                        ControlObjectContext.SaveChanges();
                    }

                    RebindControls();
                }
            }
        }

        protected void ButtonContinue_Click(object sender, EventArgs e)
        {
            StandardButtonSaveClickHandler(sender, e);

            PanelFirstWeighing.Visible=true;
            PanelFirstWeighing.Enabled = false;
            PanelForms.Visible = true;
            ButtonPrevious.Visible = true;
            ButtonContinue.Visible = false;

            FrameShowFormA.Attributes["src"] = "WebFormPopup.aspx?UC=ShowReport&d=DataSetFreight&r=ReportAppendix7&Id=" + Request.Params["FreightId"];
            FrameShowFormB.Attributes["src"] = "WebFormPopup.aspx?UC=ShowReport&d=DataSetFreight&r=ReportCMR&Id=" + Request.Params["FreightId"];
            FrameShowFormC.Attributes["src"] = "WebFormPopup.aspx?UC=ShowReport&d=DataSetFreight&r=ReportGuidanceLetter&Id=" + Request.Params["FreightId"];
            FrameShowFormD.Attributes["src"] = "WebFormPopup.aspx?UC=ShowReport&d=DataSetFreight&r=ReportAppendix1A&Id=" + Request.Params["FreightId"];
            FrameShowFormE.Attributes["src"] = "WebFormPopup.aspx?UC=ShowReport&d=DataSetFreight&r=ReportAppendix1B&Id=" + Request.Params["FreightId"];
        }

        protected void ButtonPrevious_Click(object sender, EventArgs e)
        {
            PanelFirstWeighing.Visible = true;
            PanelFirstWeighing.Enabled = true;
            PanelForms.Visible = false;
            ButtonPrevious.Visible = false;
            ButtonContinue.Visible = true;


        }

    }
}