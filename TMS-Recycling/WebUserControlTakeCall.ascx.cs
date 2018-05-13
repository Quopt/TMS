using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects;

namespace TMS_Recycling
{
    public partial class WebUserControlTakeCall : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // set handler to current handler
                ClassComboBoxHandler.DataBind();
                if ((Session["CurrentUserID"] != null) && (ClassComboBoxHandler.Items.FindByValue(Session["CurrentUserID"].ToString()) != null))
                {
                    ClassComboBoxHandler.SelectedValue = Session["CurrentUserID"].ToString();
                }

                // set date to current
                CalendarWithTimeControlReminder.SelectedDateTime = Common.CurrentClientDateTime(Session);

            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            // check if the company and contact person exists
            // if not check if the user has flagged them as new
            // if they are new but nog flagged as new then warn user and set the new flags
            // response.redirect after OK for new call

            List<string> Result = new List<string>();
            ModelTMSContainer ControlObjectContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);

            ObjectQuery QueryResult;
            QueryResult = ControlObjectContext.CreateQuery<Relation>("select value rs from RelationSet as rs where rs.Description like @prefixText order by rs.description LIMIT(1)",
                new ObjectParameter("prefixText", TextBoxClient.Text)                );
            Relation Rel = null;
            foreach (Relation Rel2 in QueryResult) { Rel = Rel2; }

            QueryResult = ControlObjectContext.CreateQuery<RelationContact>("select value rs from RelationContactSet as rs where rs.Description like @prefixText order by rs.description LIMIT(1)",
                new ObjectParameter("prefixText", TextBoxPerson.Text)                );
            RelationContact RelCon = null;
            foreach (RelationContact RelCon2 in QueryResult) { RelCon = RelCon2; }

            string ErrorMessage = "";
            if ((Rel == null) && (!CheckBoxNewRelation.Checked))
            {
                ErrorMessage = "De aangegeven relatie bestaat niet en u heeft niet aangegeven of dit een nieuwe relatie is. Bekijk de informatie goed of het inderdaad een nieuwe relatie is en sla dan de gegevens op.";
                CheckBoxNewRelation.Checked = true;
            }
            if ((RelCon == null) && (!CheckBoxNewContact.Checked))
            {
                ErrorMessage = ErrorMessage +  "\nHet aangegeven contact bestaat niet en u heeft niet aangegeven dat dit een nieuw contact is. Bekijk de informatie goed of het inderdaad een nieuwe contactpersoon is en sla dan de gegevens op.";
                CheckBoxNewContact.Checked = true;
            }

            if ( (TextBoxClient.Text.Trim() =="") || (TextBoxPerson.Text.Trim() =="") || (TextBoxMessage.Text.Trim() =="") )
            {
                ErrorMessage = ErrorMessage +  "\nVul de velden bedrijf, contactpersoon en bericht in.";
            }

            if (ErrorMessage == "")
            {
                // create new relation if desired
                if ((CheckBoxNewContact.Checked) && (Rel == null))
                {
                    Rel = new Relation();

                    Rel.Description = TextBoxClient.Text.Trim();

                    ControlObjectContext.AddToRelationSet(Rel);
                }

                // create new contact if desired
                if ((CheckBoxNewContact.Checked) && (RelCon == null))
                {
                    RelCon = new RelationContact();

                    RelCon.Description = TextBoxPerson.Text.Trim();
                    RelCon.Relation = Rel;

                    ControlObjectContext.AddToRelationContactSet(RelCon);
                }

                // create new message
                RelationContactLog rcl = new RelationContactLog();

                rcl.Description = "Contact dd " + Common.CurrentClientDateTime(Session).ToString();
                rcl.ContactDateTime = Common.CurrentClientDateTime(Session);
                rcl.FollowUpDateTime = CalendarWithTimeControlReminder.SelectedDateTime;
                rcl.PausedUntilDateTime = rcl.FollowUpDateTime;
                rcl.FollowUpState = "Unhandled";
                if (CheckBoxNoReminder.Checked) { rcl.FollowUpState = "Handled"; }
                rcl.Handler = new Guid(ClassComboBoxHandler.SelectedValue);
                rcl.ContactType = "Phone";
                rcl.RelationContact = RelCon;
                rcl.Comments = TextBoxMessage.Text;

                ControlObjectContext.AddToRelationContactLogSet(rcl);

                // save 
                ControlObjectContext.SaveChanges();

                // redirect
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            else
            {
                LabelErrorMessage.Text = ErrorMessage;
            }

        }

    }
}