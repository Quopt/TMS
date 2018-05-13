using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects;
using System.Data;
using System.Data.Objects.DataClasses;

namespace TMS_Recycling
{
    public partial class WebUserControlStaffMember : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetName = "StaffMember";

            if (!IsPostBack)
            {
                Common.AddCountryList(DropDownList_IDNationality_SelectedValue.Items, true);
                Common.AddIDTypeList(DropDownList_IDType_SelectedValue.Items, true);
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            CheckBoxOldVMSAccount.Checked = CheckBox_HasVMSAccount_Checked.Checked;
        }
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            StandardButtonCancelClickHandler(sender, e);
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            // save the role memberships into the staffmember
            StaffMember sm = (DataItem as StaffMember);
            sm.SecurityRole.Load();
            if (sm != null)
            {
                foreach (SecurityRole sr in sm.SecurityRole.ToArray<SecurityRole>())
                {
                    sm.SecurityRole.Remove(sr);
                }

                foreach (ListItem ListI in CheckBoxListRoleMemberships.Items)
                {
                    if (ListI.Selected)
                    {
                        SecurityRole sr = ControlObjectContext.GetObjectByKey( new EntityKey("ModelTMSContainer.SecurityRoleSet","Id", new Guid(ListI.Value)) ) as SecurityRole;
                        sm.SecurityRole.Add(sr);
                    }
                }
            }


            // check if the nr of staffmembers does not exceed the max amount of allowed staff members
            if ((CheckBox_HasVMSAccount_Checked.Checked) && (CheckBoxOldVMSAccount.Checked != CheckBox_HasVMSAccount_Checked.Checked) &&
                ((StaffMemberSet.AmountOfStaffMembersWithTMSLogin(ControlObjectContext)+1) > SystemSettingSet.GetAmountOfUserLicenses(ControlObjectContext)) )
            {
                Common.InformUser(Page, "Het maximale aantal TMS logins is opgebruikt. U kunt niet nog een TMS gebruiker toevoegen. Breidt uw licentie uit of ontneem de TMS licentie van een bestaande gebruiker.");
                CheckBox_HasVMSAccount_Checked.Checked = false;
            }

            StandardButtonSaveClickHandler(sender, e);
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            StandardButtonDeleteClickHandler(sender, e);
        }

        public void LoadUserRoles()
        {
            // load role settings into the list
            StaffMember sm = (DataItem as StaffMember);
            sm.SecurityRole.Load();
            if (sm != null)
            {
                foreach (SecurityRole sr in sm.SecurityRole)
                {
                    ListItem li = CheckBoxListRoleMemberships.Items.FindByValue(sr.Id.ToString() );
                    if (li != null)
                    {
                        li.Selected = true;
                    }
                }
            }
        }

    }
}