using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public class ClassSecurity
    {
        private Page _Page;
        private ModelTMSContainer _ControlObjectContext;
        private StaffMember sm = null;

        private string FormName()
        {
            return _Page.Request.Url.AbsolutePath.Substring(_Page.Request.Url.AbsolutePath.IndexOf('/') + 1);
        }

        private Boolean AllowAccessToButton(Button btn)
        {
            Boolean Result = false;

            if (sm != null)
            {
                if (sm.HasVMSAccount)
                {
                    Result = sm.CheckAccessToElement(_ControlObjectContext, FormName(), btn.ID, btn.Text, AccessType.Execute);
                }
            }

            if (Common.IsMasterLoggedIn(_Page.Session))
            {
                return true;
            }
            else
            {
                return Result;
            }

        }

        private void CheckAccessToTreeNode(TreeView tv, TreeNodeCollection tnc)
        {
            foreach (TreeNode tn in tnc)
            {
                if (tn.ChildNodes.Count > 0)
                {
                    CheckAccessToTreeNode(tv, tn.ChildNodes);
                }
                else
                {
                    if (tn.NavigateUrl.Trim() != "")
                    {
                        string NavigateURL = "";
                        NavigateURL = tn.NavigateUrl;
                        if (tn.NavigateUrl.IndexOf('?') > 0)
                        {
                            NavigateURL = NavigateURL.Substring(0, NavigateURL.IndexOf('?'));
                        }

                        if ( (!sm.CheckAccessToElement(_ControlObjectContext, "~SubMenu." + tv.ID, NavigateURL, tn.Text, AccessType.Execute)) &&
                             (!Common.IsMasterLoggedIn(_Page.Session) )
                           )
                        {
                            tn.SelectAction = TreeNodeSelectAction.None;
                        }
                    }
                }
            }
        }

        private void AllowAccessToTreeView(TreeView tv)
        {
            if (sm != null)
            {
                if (sm.HasVMSAccount)
                {
                    CheckAccessToTreeNode(tv, tv.Nodes);
                }
            }
        }

        private void AllowAccessToMenu(Menu mnu)
        {
            List<MenuItem> RemoveList = new List<MenuItem>(); 
            if (sm != null)
            {
                if (sm.HasVMSAccount)
                {
                    foreach (MenuItem mi in mnu.Items)
                    {
                        mi.Selectable = sm.CheckAccessToElement(_ControlObjectContext, "~Menu" , mi.Value, mi.Text, AccessType.Execute) || (Common.IsMasterLoggedIn(_Page.Session));
                        if (!mi.Selectable)
                        {
                            RemoveList.Add(mi);
                        }
                    }

                    // remove the non selectable items from the menu
                    foreach (MenuItem mi in RemoveList)
                    {
                        mnu.Items.Remove(mi);
                    }
                }
            }
        }

        private void EnumerateSecuredControls(ControlCollection ctrlContainer)
        {
            foreach (Control ctrl in ctrlContainer)
            {
                // buttons dont work properly so disable that
                //if (ctrl.GetType() == typeof(Button))
                //{
                    //Button btn = ctrl as Button;
                    //Boolean AllowAccess = AllowAccessToButton(btn);
                    //btn.Visible = btn.Visible && AllowAccess;
                //}

                if (ctrl.GetType() == typeof(TreeView))
                {
                    TreeView btn = ctrl as TreeView;
                    AllowAccessToTreeView(btn);
                }

                if (ctrl.GetType() == typeof(Menu))
                {
                    Menu btn = ctrl as Menu;
                    AllowAccessToMenu(btn);
                }

                if (ctrl.HasControls())
                {
                    EnumerateSecuredControls(ctrl.Controls);
                }
            }
        }

        public Boolean AccessToFormAllowed(Page pg, ModelTMSContainer ControlObjectContext, string FormTitle)
        {
            _Page = pg;
            _ControlObjectContext= ControlObjectContext;
            sm = Common.CurrentLoggedInUser(_Page.Session, _ControlObjectContext);
            sm.SecurityRole.Load();

            bool Result= false;

            if (sm != null)
            {
                if (sm.HasVMSAccount)
                {
                    Result = sm.CheckAccessToElement(_ControlObjectContext, FormName(), "", FormTitle, AccessType.Execute);
                }
            }

            ControlObjectContext.SaveChanges();

            if (Common.IsMasterLoggedIn(pg.Session))
            {
                return true;
            }
            else
            {
                return Result;
            }
        }

        public void InstallUserRightsIntoPage(Page pg, ModelTMSContainer ControlObjectContext)
        {
            _Page = pg;
            _ControlObjectContext= ControlObjectContext;
            sm = Common.CurrentLoggedInUser(_Page.Session, _ControlObjectContext);
            sm.SecurityRole.Load();

            EnumerateSecuredControls(pg.Controls);

            ControlObjectContext.SaveChanges();
        }

    }
}