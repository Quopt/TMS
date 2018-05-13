using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects;
using System.Web.Security;
using System.IO;
using System.Configuration;

namespace TMS_Recycling
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if ((Request.Params["cust"] == null) && (Session["LoginCustParameter"] != null))
                {
                    if (Request.Params.Count == 0)
                    {
                        Response.Redirect("login.aspx?cust=" + Session["LoginCustParameter"].ToString().Trim(), false);
                    }
                    else
                    {
                        Response.Redirect(Request.Url.PathAndQuery + "&cust=" + Session["LoginCustParameter"].ToString().Trim(), false);
                    }
                }

                // check if we have a customer or the general database
                if (Request.Params["cust"] == null)
                {
                    if ( ConfigurationManager.AppSettings.Get("SupportMultiTenantByDefault").ToString() == "No")
                    {
                        Response.Redirect("login.aspx?cust=Default", true);
                    }
                }
                if (Request.Params["cust"] != null)
                {
                    Session["LoginCustParameter"] = Request.Params["cust"].ToString();
                    Session["CustomerConnectString"] = "metadata=res://*/ModelTMS.csdl|res://*/ModelTMS.ssdl|res://*/ModelTMS.msl;provider=System.Data.SqlClient;provider connection string=\"Data Source=" + ConfigurationManager.AppSettings.Get("DataSource").ToString() + ";Integrated Security=True;MultipleActiveResultSets=True;Initial Catalog=" + ConfigurationManager.AppSettings.Get("CatalogPrefix").ToString() + Request.Params["cust"].ToString() + "\"";
                    Session["CustomerConnectSQLString"] = "Data Source=" + ConfigurationManager.AppSettings.Get("DataSource").ToString() + ";Integrated Security=True;Initial Catalog=" + ConfigurationManager.AppSettings.Get("CatalogPrefix").ToString() + Request.Params["cust"].ToString();

                    //LabelMessage.Text = Session["CustomerConnectString"].ToString() + "<BR>" + Session["CustomerConnectSQLString"].ToString();
                    if ( (ConfigurationManager.AppSettings.Get("SupportMultiTenantByDefault").ToString() == "No") &&
                         (Request.Params["cust"].ToString() == "Default") )
                    {
                        CreateAndCheckDatabase();
                    }

                    ButtonLogin.Visible = true;
                }
                else
                {
                    ButtonLogin.Visible = false;
                }
            }
            else
            { // IsPostback
                // set the clients time zone offset compared to UTC
                Session["ClientTimeZoneOffset"] = TimeZoneOffset.Value == "" ? "0" : TimeZoneOffset.Value;
            }

            if ((Session != null) && (Session["LogoutMessage"] != null))
            {
                LabelMessage.Text = Session["LogoutMessage"].ToString();
            }
        }

        private void CreateAndCheckDatabase()
        {
            // check if the database is there
            TMSService.CheckDatabase(Session);

            // check if the database fillings are correct
            // check if all required objects are present (the database cannot be accessed and is therefore already upgraded)
            ModelTMSContainer TempContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);

            // locations
            Location location = null;
            LedgerBookingCode TempLedgerBookingCode = null;

            // ledgerbookingcodes not present? Then init the database ...
            if (TempContext.LedgerBookingCodeSet.Count() == 0)
            {
                TempLedgerBookingCode = new LedgerBookingCode();
                //LedgerBookingCode DebugCode = new LedgerBookingCode();
                TempContext.AddToLedgerBookingCodeSet(TempLedgerBookingCode);
                //TempContext.AddToLedgerBookingCodeSet(DebugCode);

                TempLedgerBookingCode.Description = ClassTranslate.TranslateString("DBDefaults","StandardBookingCode", "Standard booking code");
                TempLedgerBookingCode.IsActive = true;
                TempLedgerBookingCode.IsDebugLedgerCode = false;
                TempLedgerBookingCode.LedgerCurrency = "Eur";

                // debug code 
                //DebugCode.Description = ClassTranslate.TranslateString("DBDefaults","StandardBookingDebugCode", "Standard book debugging code");
                //DebugCode.IsActive = true;
                //DebugCode.IsDebugLedgerCode = true;

                TempContext.SaveChanges();

                // location
                location = new Location();
                TempContext.AddToLocationSet(location);
                location.Description = "Standard location";
                location.DefaultWeighingTariffBookingCode = TempLedgerBookingCode;
                location.DefaultBailPriceBookingCode = TempLedgerBookingCode;

                TempContext.SaveChanges();

                // ledgers (bank and cash MUST be present)
                Ledger BankLedger = new Ledger();
                Ledger CashLedger = new Ledger();
                //Ledger DebugLedger = new Ledger();
                //TempContext.AddToLedgerSet(DebugLedger);
                TempContext.AddToLedgerSet(BankLedger);
                TempContext.AddToLedgerSet(CashLedger);

                BankLedger.Description = ClassTranslate.TranslateString("DBDefaults", "BankCode", "Bank");
                BankLedger.Comments = ClassTranslate.TranslateString("DBDefaults", "BankCodeComment", "Automatisch aangemaakt. Vul aub de gegevens van uw bankrekening hier in.");
                BankLedger.LedgerCurrency = "Eur";
                BankLedger.LedgerLevel = 0;
                BankLedger.LedgerType = "Bank";
                BankLedger.IsActive = true;
                BankLedger.LimitToLocation = location;

                CashLedger.Description = ClassTranslate.TranslateString("DBDefaults", "CashCode", "Kas");
                CashLedger.Comments = ClassTranslate.TranslateString("DBDefaults", "CashCodeComment", "Automatisch aangemaakt. Vul hier aub de gegevens van uw kas register in.");
                CashLedger.LedgerCurrency = "Eur";
                CashLedger.LedgerLevel = 0;
                CashLedger.LedgerType = "Cash";
                CashLedger.IsActive = true;
                CashLedger.LimitToLocation = location;
                /*
                DebugLedger.Description = ClassTranslate.TranslateString("DBDefaults","DebugCode","Debug");
                DebugLedger.Comments = ClassTranslate.TranslateString("DBDefaults","DebugCodeComment","Auto created. Please fill in all details of your Cash account. This is the system debugging ledger for testing purposes only. Please do not use for business purposes.");
                DebugLedger.LedgerCurrency = "Eur";
                DebugLedger.LedgerLevel = 0;
                DebugLedger.LedgerType = "Bank";
                DebugLedger.IsActive = true;
                DebugLedger.LimitToLocation = null;
                DebugLedger.IsDebugLedger = true;
                */
                location.BankLedger = BankLedger;
                location.CashLedger = CashLedger;
                TempContext.SaveChanges();

                // standard material units
                MaterialUnit KGUnit = new MaterialUnit();
                TempContext.AddToMaterialUnitSet(KGUnit);

                KGUnit.Description = "KG";
                KGUnit.IsActive = true;
                KGUnit.StockKgMultiplier = 1;
                KGUnit.StockUnit = "KG";

                TempContext.SaveChanges();

                //KGUnit = new MaterialUnit();
                //TempContext.AddToMaterialUnitSet(KGUnit);

                //KGUnit.Description = "N/A";
                //KGUnit.IsActive = true;
                //KGUnit.StockKgMultiplier = 1;
                //KGUnit.StockUnit = "N/A";

                //TempContext.SaveChanges();

                // standard material
                // material 
                Material TempMat = new Material();
                // man hours
                Material TempHours = new Material();
                // dirt / excess material
                Material TempDirt = new Material();
                TempContext.AddToMaterialSet(TempMat);
                TempContext.AddToMaterialSet(TempHours);
                TempContext.AddToMaterialSet(TempDirt);

                LedgerBookingCode MatBookingCode = TempContext.LedgerBookingCodeSet.First();

                TempMat.Description = ClassTranslate.TranslateString("DBDefaults", "StandardMaterial", "Standaard materiaal");
                TempMat.MaterialUnit = TempContext.MaterialUnitSet.First();
                TempMat.Location = TempContext.LocationSet.First();
                TempMat.PurchaseLedgerBookingCode = MatBookingCode;
                TempMat.SalesLedgerBookingCode = MatBookingCode;
                TempMat.Category = "Other";
                TempMat.VATPercentage = 21;

                TempHours.Description = ClassTranslate.TranslateString("DBDefaults", "ManHours", "Man uren");
                TempHours.IsWorkInsteadOfMaterial = true;
                TempHours.MaterialUnit = TempContext.MaterialUnitSet.First();
                TempHours.PurchaseLedgerBookingCode = MatBookingCode;
                TempHours.SalesLedgerBookingCode = MatBookingCode;
                TempHours.Location = TempContext.LocationSet.First();
                TempHours.Category = "Other";
                TempHours.VATPercentage = 21;

                TempDirt.Description = ClassTranslate.TranslateString("DBDefaults", "Dirt", "Vuil");
                TempDirt.StockMayBeNegative = true;
                TempDirt.MaterialUnit = TempContext.MaterialUnitSet.First();
                TempDirt.PurchaseLedgerBookingCode = MatBookingCode;
                TempDirt.SalesLedgerBookingCode = MatBookingCode;
                TempDirt.Location = TempContext.LocationSet.First();
                TempDirt.VATPercentage = 21;
                TempDirt.Category = "Other";

                // settings : standard dirt material, standard work material
                SystemSettingSet.SetSystemSettingValue(TempContext, "Standard.Material.Dirt", TempDirt.Id.ToString(), ClassTranslate.TranslateString("DBDefaults", "StandardMaterialForDirt", "Standaard materiaal voor vuil."));
                SystemSettingSet.SetSystemSettingValue(TempContext, "Standard.Material.Labour", TempHours.Id.ToString(), ClassTranslate.TranslateString("DBDefaults", "StandardMaterialForLabour", "Standaard materiaal voor uren."));

                // VAT percentages
                /*
                MaterialVAT TempVAT = new MaterialVAT();
                TempContext.AddToMaterialVATSet(TempVAT);
                TempVAT.Description = "19%";
                TempVAT.VATPercentage = 19;
                TempVAT.Material = TempMat;
                TempVAT.Location = TempContext.LocationSet.First();

                TempVAT = new MaterialVAT();
                TempContext.AddToMaterialVATSet(TempVAT);
                TempVAT.Description = "19%";
                TempVAT.VATPercentage = 19;
                TempVAT.Material = TempHours;
                TempVAT.Location = TempContext.LocationSet.First();

                TempVAT = new MaterialVAT();
                TempContext.AddToMaterialVATSet(TempVAT);
                TempVAT.Description = "19%";
                TempVAT.VATPercentage = 19;
                TempVAT.Material = TempDirt;
                TempVAT.Location = TempContext.LocationSet.First();

                TempContext.SaveChanges();
                 */

                // standard rental type equipment
                RentalType TempRental = new RentalType();
                RentalTypeVAT TempRentalVAT = new RentalTypeVAT();

                TempContext.AddToRentalTypeSet(TempRental);
                TempContext.AddToRentalTypeVATSet(TempRentalVAT);

                TempRental.Description = ClassTranslate.TranslateString("DBDefaults", "AnyRental", "Elke verhuur");
                TempRental.LedgerBookingCode = TempContext.LedgerBookingCodeSet.First();
                TempRentalVAT.VATPercentage = 21;
                TempRentalVAT.Description = "21%";
                TempRentalVAT.RentalType = TempRental;
                TempRentalVAT.Location = TempContext.LocationSet.First();

                TempContext.SaveChanges();

                // standard truck
                Truck TempTruck = new Truck();
                TempContext.AddToTruckSet(TempTruck);

                TempTruck.Description = ClassTranslate.TranslateString("DBDefaults", "Any truck", "Een vrachtwagen");
                TempTruck.CurrentTruckLocation = TempContext.LocationSet.First();
                TempTruck.HomeTruckLocation = TempContext.LocationSet.First();

                TempContext.SaveChanges();

                // standard security role
                SecurityRole SecRole = new SecurityRole();
                TempContext.AddToSecurityRoleSet(SecRole);

                SecRole.Description = ClassTranslate.TranslateString("DBDefaults", "UnlimitedAccess", "Onbeperkte toegang");
                SecRole.HasUnlimitedAccess = true;

                TempContext.SaveChanges();

                // standard relations (general purchase, stock correction and general sales account)
                Relation Sales = new Relation();
                Relation Purchase = new Relation();
                Relation StockCorrection = new Relation();
                TempContext.AddToRelationSet(Sales);
                TempContext.AddToRelationSet(Purchase);
                TempContext.AddToRelationSet(StockCorrection);

                Sales.Description = ClassTranslate.TranslateString("DBDefaults", "DirectSales", "Directe verkoop");
                Sales.CustomerType = "Debtor";
                Sales.IsSystemUser = true;
                Sales.PreferredLocation = location;

                Purchase.Description = ClassTranslate.TranslateString("DBDefaults", "DirectPurchase", "Directe inkoop");
                Purchase.CustomerType = "Creditor";
                Purchase.IsSystemUser = true;
                Purchase.PreferredLocation = location;

                StockCorrection.Description = ClassTranslate.TranslateString("DBDefaults", "StockCorrection", "Voorraad correctie");
                StockCorrection.CustomerType = "Both";
                StockCorrection.IsSystemUser = true;
                StockCorrection.PreferredLocation = location;

                // settings: standard sales, standard purchase, standard stock correction customer
                SystemSettingSet.SetSystemSettingValue(TempContext, "Standard.Relation.Purchase", Purchase.Id.ToString(), ClassTranslate.TranslateString("DBDefaults", "StandardPurchaseRelationUnnamed", "Standard relatie voor onbekende inkopers."));
                SystemSettingSet.SetSystemSettingValue(TempContext, "Standard.Relation.Sales", Sales.Id.ToString(), ClassTranslate.TranslateString("DBDefaults", "StandardSalesRelationUnnamed", "Standard relatie voor onbekende kopers."));
                SystemSettingSet.SetSystemSettingValue(TempContext, "Standard.Relation.StockCorrection", StockCorrection.Id.ToString(), ClassTranslate.TranslateString("DBDefaults", "StandardStockCorrectionRelation", "Standard relatie voor voorraad correcties."));

                TempContext.SaveChanges();
            }


            // standard user if there are none
            if (TempContext.StaffMemberSet.Count() == 0)
            {
                StaffMember TempMem = new StaffMember();
                TempContext.AddToStaffMemberSet(TempMem);

                TempMem.Description = ClassTranslate.TranslateString("DBDefaults", "DefaultUser", "Standaard aanwezig gebruiker");
                TempMem.HasVMSAccount = true;
                TempMem.AccountName = "Admin";
                TempMem.Password = "Admin";
                TempMem.HomeLocation = TempContext.LocationSet.First();
                TempMem.SecurityRole.Add(TempContext.SecurityRoleSet.First());

                TempContext.SaveChanges();
            }

        }

        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            // check for master login
            bool LoginOK = false;
            bool MasterLogin = false;

            if (Session["LogoutMessage"] != null) { Session.Remove("LogoutMessage"); }

            if ((TextBoxUserName.Text == "Administrator") && 
                (TextBoxPassword.Text == ConfigurationManager.AppSettings.Get("DefaultAdminPassword").ToString()) && 
                (ConfigurationManager.AppSettings.Get("DefaultAdminPassword").ToString() != "No") )
            {
                LoginOK = true;
                MasterLogin = true;

                CreateAndCheckDatabase(); // only with a master login the database will be created !!!

                Session["CurrentUserID"] = Guid.Empty;
                Session["CurrentUserName"] = "Administrator";
                Session["CurrentUserIDPreferredCulture"] = "nl-NL";
            }

            // log deze gebruiker in in het systeem
            ModelTMSContainer ThisContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);
            ObjectQuery<StaffMember> LoginQry;

            LoginQry = ThisContext.StaffMemberSet
                   .Where("it.IsActive and it.HasVMSAccount and it.AccountName = @AccountName and it.Password = @Password",
                   new ObjectParameter("AccountName", TextBoxUserName.Text),
                   new ObjectParameter("Password", TextBoxPassword.Text)); 
            

            if (!MasterLogin)
            {
                try
                {
                    LoginOK = (LoginQry.Count() == 1) && (TextBoxUserName.Text.Trim() != "") && (TextBoxPassword.Text.Trim() != "");
                }
                catch
                {
                    //LabelMessage.Text = "INVALID DATABASE! CONTACT THE HELPDESK (OR TRY STOPPING TO HACK THIS SYSTEM)!";
                }
            }


            LabelLoginResultFailed.Visible = false;
            if (LoginOK)
            {
                Page.Session["LoginFailedCount"] = 0;

                if (!MasterLogin)
                {
                    // success, check dates
                    StaffMember LoginMember = LoginQry.First();

                    if (!((LoginMember.InServiceDate <= Common.CurrentClientDateTime(Session)) && (LoginMember.OutOfServiceDate >= Common.CurrentClientDateTime(Session))))
                    {
                        // this staff member is not in service any more
                        LoginOK = false;
                    }
                    else
                    {
                        Session["CurrentUserID"] = LoginMember.Id;
                        Session["CurrentUserName"] = TextBoxUserName.Text;
                        Session["CurrentUserIDPreferredCulture"] = "nl-NL";
                    }
                }

                if (LoginOK)
                {
                    Session["MasterLogin"] = MasterLogin ? "T" : "F";
                    Application[Session["CurrentUserID"].ToString()] = Session.SessionID;
                    FormsAuthentication.RedirectFromLoginPage(TextBoxUserName.Text, false);
                }

            }
            else
            {
                if (Page.Session["LoginFailedCount"] == null)
                {
                    Page.Session["LoginFailedCount"] = 0;
                }
                if (System.Convert.ToInt32(Page.Session["LoginFailedCount"]) < 10)
                {
                    Page.Session["LoginFailedCount"] = System.Convert.ToInt32(Page.Session["LoginFailedCount"]) + 1;
                }

                System.Threading.Thread.Sleep( System.Convert.ToInt32(Page.Session["LoginFailedCount"]) * 1000);
                LabelLoginResultFailed.Visible = true; 
            }

        }
    }
}