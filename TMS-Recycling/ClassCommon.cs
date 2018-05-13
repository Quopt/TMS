using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Web.UI;
using System.Collections;
using System.Data.Objects.DataClasses;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Web.SessionState;
using System.Data;
using System.Threading;

namespace TMS_Recycling
{

    public enum InvoiceType { Buy, Sell, Rent, BuyLedger, SellLedger, Unknown };
    public enum OrderType { Buy, Sell, Both };
    public enum UserControlLedgerType { Cash, Bank };
    public enum AccessType {Create,Read,Update,Delete,Execute};

    public class Common
    {
        public const string constDateTimeFormatString = "yyyyMMdd HH:mm:ss";

        public static void DisableViewStateOnButtons(ControlCollection ctrlContainer)
        {
            foreach (Control ctrl in ctrlContainer)
            {
                if (ctrl.GetType() == typeof(Button))
                {
                    Button btn = ctrl as Button;
                    btn.EnableViewState = false;
                }
                if (ctrl.HasControls())
                {
                    DisableViewStateOnButtons(ctrl.Controls);
                }
            }
        }

        public static void InformUserOnTransactionFail(Exception ex, Page Page)
        {
            // inform user
            string tempStr = "";
            if (ex.InnerException != null)
            {
                tempStr = ex.Message + "/" + ex.InnerException.Message;
            }
            else
            {
                tempStr = ex.Message;
            }
            tempStr = tempStr.Replace("'", "\"").Replace("\n", "").Replace("\r", "");
            Page.RegisterClientScriptBlock("Alert", "<script>alert('Het opslaan van de gegevens is mislukt, mogelijk omdat iemand anders de gegevens al heeft gewijzigd. Probeer het nogmaals. (" + tempStr + ")');</script>");
        }

        public static void InformUserOnGeneralFail(Exception ex, Page Page, string Message)
        {
            // inform user
            string tempStr = "";
            if (ex.InnerException != null)
            {
                tempStr = ex.Message + "/" + ex.InnerException.Message;
            }
            else
            {
                tempStr = ex.Message;
            }
            tempStr = tempStr.Replace("'", "\"").Replace("\n", "").Replace("\r", "");
            Page.RegisterClientScriptBlock("Alert", "<script>alert('"+Message+". (" + tempStr + ")');</script>");
        }

        public static void InformUser(Page Page, string Message)
        {
            Page.RegisterClientScriptBlock("Alert", "<script>alert('" + Message + "');</script>");
        }
        
        public static void CloneProperties(Object FromObject, Object ToObject)
        {
            PropertyInfo[] TempProp = FromObject.GetType().GetProperties();
            foreach (PropertyInfo p in TempProp)
            {
                string PropType = p.PropertyType.ToString();
                //string DeclType = p.DeclaringType.ToString();
                if ((p.CanWrite) && (p.Name.ToString() != "Id") && (PropType.IndexOf("EntityCollection") < 0) &&
                    (PropType.IndexOf("EntityReference") < 0) && (PropType.IndexOf("EntityKey") < 0) && (PropType.IndexOf("TMS_Recycling.") < 0))
                {
                    // set property on object B
                    p.SetValue(ToObject, p.GetValue(FromObject, null), null);
                }
            }        
        }

        public static StaffMember CurrentLoggedInUser(HttpSessionState Session, ModelTMSContainer ControlObjectContext)
        {
            StaffMember StaffMemberFound = null;

            if (Session["CurrentUserID"] != null)
            {
                Guid StaffMemberGuid = new Guid(Session["CurrentUserID"].ToString());

                StaffMemberFound = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.StaffMemberSet", "Id", StaffMemberGuid )) as StaffMember;
            }

            return StaffMemberFound;
        }

        public static Boolean IsUserLoggedIn(HttpSessionState Session)
        {
            if (Session["CurrentUserID"] != null)
            {
                return true;
            }

            return false;
        }

        public static Boolean IsMasterLoggedIn(HttpSessionState Session)
        {
            if (Session["MasterLogin"] != null)
            {
                return (Session["MasterLogin"] == "T");
            }
            else
            {
                return false;
            }
        }

        public static void LimitLocationList(ListItemCollection Items, HttpSessionState Session, ModelTMSContainer ControlObjectContext)
        {
            StaffMember CurrentStaffMember = CurrentLoggedInUser(Session, ControlObjectContext);

            if (CurrentStaffMember != null)
            {
                if (CurrentStaffMember.LimitAccessToThisLocation != null)
                {
                    ListItem liFound=null;
                    foreach (ListItem li in Items)
                    {
                        if (li.Value == CurrentStaffMember.LimitAccessToThisLocation.Id.ToString() )
                        {
                            liFound = li;
                        }
                    }
                    if (liFound!=null)
                    {
                        Items.Clear();
                        Items.Add(liFound);
                    }
                }
            }

        }

        public static void SetCustomerToDefaultOfLocation(string LocationGuid, AjaxControlToolkit.ComboBox cb, string BuyOrSell, ModelTMSContainer ControlObjectContext)
        {
            Location loc=null;

            if (LocationGuid != "")
            {
                loc = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.LocationSet", "Id", new Guid( LocationGuid) )) as Location;
            }

            if (loc != null)
            {
                if ( (loc.RelationBuy != null) && (BuyOrSell == "Buy"))
                {
                    // show the standard purchase cusomer for this location
                    if (cb.Items.FindByValue(loc.RelationBuy.Id.ToString()) != null)
                    {
                        cb.SelectedValue = loc.RelationBuy.Id.ToString();
                    }
                }
                else
                {
                    // show the standard sales customer for this location
                    if ((loc.RelationSale!=null) && (cb.Items.FindByValue(loc.RelationSale.Id.ToString()) != null))
                    {
                        cb.SelectedValue = loc.RelationSale.Id.ToString();
                    }
                }
            }
        }

        public static string TranslateEnumValue(string Value, ListItemCollection lic)
        {
            string result = Value;

            ListItem li = lic.FindByValue(Value);
            if (li != null)
            {
                result = li.Text;
            }

            return result;
        }

        public static InvoiceType DetermineInvoiceType(string InvoiceTypeText, string InvoiceSubTypeText)
        {
            if (InvoiceSubTypeText == "Purchase")
            {
                if (InvoiceTypeText == "Buy")
                {
                    return InvoiceType.Buy;
                }
                else 
                {
                    return InvoiceType.Sell;
                }
            }
            else if (InvoiceSubTypeText == "Rent")
            {
                return InvoiceType.Rent;
            }
            return InvoiceType.Unknown;
        }

        public static string ReturnEntitySQLDateTimeString(DateTime dt)
        {
            string dtFormat = Thread.CurrentThread.CurrentCulture.DateTimeFormat.FullDateTimePattern;
            dtFormat= dtFormat.Replace("ss", "ss.fff");
            return dt.ToString(dtFormat);
        }

        public static DateTime CurrentClientDateTime(HttpSessionState xSession)
        {
            // recalcs the server date & time to the clients date & time
            return ToClientDateTime(xSession, DateTime.Now);
        }

        public static DateTime CurrentClientDate(HttpSessionState xSession)
        {
            // recalcs the server date to the clients date 
            return ToClientDateTime(xSession, DateTime.Now).Date;
        }

        public static DateTime ToClientDateTime(HttpSessionState xSession, DateTime ToCalc)
        {
            // recalcs the offered server date & time and returns the clients server date & time of this server date & time
            int serverUTCOffsetInMin = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours * 60 + TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Minutes;
            int clientUTCOffsetInMin = serverUTCOffsetInMin;

            if (xSession["ClientTimeZoneOffset"] != null)
            {
                clientUTCOffsetInMin = Convert.ToInt32(xSession["ClientTimeZoneOffset"]) + serverUTCOffsetInMin;
            }
            else
            {
                clientUTCOffsetInMin = 0;
            }

            return ToCalc.AddMinutes(clientUTCOffsetInMin);
        }
        public static void AddCountryList( ListItemCollection _CountryList, Boolean ClearListFirst)
            {
                if (ClearListFirst)
                {
                    _CountryList.Clear(); 
                }

                _CountryList.Add(new ListItem("-nog niet bekend-", ""));
                _CountryList.Add( new ListItem("AFGHANISTAN","AF"));
                _CountryList.Add( new ListItem("ÅLAND ISLANDS","AX"));
                _CountryList.Add( new ListItem("ALBANIA","AL"));
                _CountryList.Add( new ListItem("ALGERIA","DZ"));
                _CountryList.Add( new ListItem("AMERICAN SAMOA","AS"));
                _CountryList.Add( new ListItem("ANDORRA","AD"));
                _CountryList.Add( new ListItem("ANGOLA","AO"));
                _CountryList.Add( new ListItem("ANGUILLA","AI"));
                _CountryList.Add( new ListItem("ANTARCTICA","AQ"));
                _CountryList.Add( new ListItem("ANTIGUA AND BARBUDA","AG"));
                _CountryList.Add( new ListItem("ARGENTINA","AR"));
                _CountryList.Add( new ListItem("ARMENIA","AM"));
                _CountryList.Add( new ListItem("ARUBA","AW"));
                _CountryList.Add( new ListItem("AUSTRALIA","AU"));
                _CountryList.Add( new ListItem("AUSTRIA","AT"));
                _CountryList.Add( new ListItem("AZERBAIJAN","AZ"));
                _CountryList.Add( new ListItem("BAHAMAS","BS"));
                _CountryList.Add( new ListItem("BAHRAIN","BH"));
                _CountryList.Add( new ListItem("BANGLADESH","BD"));
                _CountryList.Add( new ListItem("BARBADOS","BB"));
                _CountryList.Add( new ListItem("BELARUS","BY"));
                _CountryList.Add( new ListItem("BELGIUM","BE"));
                _CountryList.Add( new ListItem("BELIZE","BZ"));
                _CountryList.Add( new ListItem("BENIN","BJ"));
                _CountryList.Add( new ListItem("BERMUDA","BM"));
                _CountryList.Add( new ListItem("BHUTAN","BT"));
                _CountryList.Add( new ListItem("BOLIVIA, PLURINATIONAL STATE OF","BO"));
                _CountryList.Add( new ListItem("BOSNIA AND HERZEGOVINA","BA"));
                _CountryList.Add( new ListItem("BOTSWANA","BW"));
                _CountryList.Add( new ListItem("BOUVET ISLAND","BV"));
                _CountryList.Add( new ListItem("BRAZIL","BR"));
                _CountryList.Add( new ListItem("BRITISH INDIAN OCEAN TERRITORY","IO"));
                _CountryList.Add( new ListItem("BRUNEI DARUSSALAM","BN"));
                _CountryList.Add( new ListItem("BULGARIA","BG"));
                _CountryList.Add( new ListItem("BURKINA FASO","BF"));
                _CountryList.Add( new ListItem("BURUNDI","BI"));
                _CountryList.Add( new ListItem("CAMBODIA","KH"));
                _CountryList.Add( new ListItem("CAMEROON","CM"));
                _CountryList.Add( new ListItem("CANADA","CA"));
                _CountryList.Add( new ListItem("CAPE VERDE","CV"));
                _CountryList.Add( new ListItem("CAYMAN ISLANDS","KY"));
                _CountryList.Add( new ListItem("CENTRAL AFRICAN REPUBLIC","CF"));
                _CountryList.Add( new ListItem("CHAD","TD"));
                _CountryList.Add( new ListItem("CHILE","CL"));
                _CountryList.Add( new ListItem("CHINA","CN"));
                _CountryList.Add( new ListItem("CHRISTMAS ISLAND","CX"));
                _CountryList.Add( new ListItem("COCOS (KEELING) ISLANDS","CC"));
                _CountryList.Add( new ListItem("COLOMBIA","CO"));
                _CountryList.Add( new ListItem("COMOROS","KM"));
                _CountryList.Add( new ListItem("CONGO","CG"));
                _CountryList.Add( new ListItem("CONGO, THE DEMOCRATIC REPUBLIC OF THE","CD"));
                _CountryList.Add( new ListItem("COOK ISLANDS","CK"));
                _CountryList.Add( new ListItem("COSTA RICA","CR"));
                _CountryList.Add( new ListItem("CÔTE D'IVOIRE","CI"));
                _CountryList.Add( new ListItem("CROATIA","HR"));
                _CountryList.Add( new ListItem("CUBA","CU"));
                _CountryList.Add( new ListItem("CYPRUS","CY"));
                _CountryList.Add( new ListItem("CZECH REPUBLIC","CZ"));
                _CountryList.Add( new ListItem("DENMARK","DK"));
                _CountryList.Add( new ListItem("DJIBOUTI","DJ"));
                _CountryList.Add( new ListItem("DOMINICA","DM"));
                _CountryList.Add( new ListItem("DOMINICAN REPUBLIC","DO"));
                _CountryList.Add( new ListItem("ECUADOR","EC"));
                _CountryList.Add( new ListItem("EGYPT","EG"));
                _CountryList.Add( new ListItem("EL SALVADOR","SV"));
                _CountryList.Add( new ListItem("EQUATORIAL GUINEA","GQ"));
                _CountryList.Add( new ListItem("ERITREA","ER"));
                _CountryList.Add( new ListItem("ESTONIA","EE"));
                _CountryList.Add( new ListItem("ETHIOPIA","ET"));
                _CountryList.Add( new ListItem("FALKLAND ISLANDS (MALVINAS)","FK"));
                _CountryList.Add( new ListItem("FAROE ISLANDS","FO"));
                _CountryList.Add( new ListItem("FIJI","FJ"));
                _CountryList.Add( new ListItem("FINLAND","FI"));
                _CountryList.Add( new ListItem("FRANCE","FR"));
                _CountryList.Add( new ListItem("FRENCH GUIANA","GF"));
                _CountryList.Add( new ListItem("FRENCH POLYNESIA","PF"));
                _CountryList.Add( new ListItem("FRENCH SOUTHERN TERRITORIES","TF"));
                _CountryList.Add( new ListItem("GABON","GA"));
                _CountryList.Add( new ListItem("GAMBIA","GM"));
                _CountryList.Add( new ListItem("GEORGIA","GE"));
                _CountryList.Add( new ListItem("GERMANY","DE"));
                _CountryList.Add( new ListItem("GHANA","GH"));
                _CountryList.Add( new ListItem("GIBRALTAR","GI"));
                _CountryList.Add( new ListItem("GREECE","GR"));
                _CountryList.Add( new ListItem("GREENLAND","GL"));
                _CountryList.Add( new ListItem("GRENADA","GD"));
                _CountryList.Add( new ListItem("GUADELOUPE","GP"));
                _CountryList.Add( new ListItem("GUAM","GU"));
                _CountryList.Add( new ListItem("GUATEMALA","GT"));
                _CountryList.Add( new ListItem("GUERNSEY","GG"));
                _CountryList.Add( new ListItem("GUINEA","GN"));
                _CountryList.Add( new ListItem("GUINEA-BISSAU","GW"));
                _CountryList.Add( new ListItem("GUYANA","GY"));
                _CountryList.Add( new ListItem("HAITI","HT"));
                _CountryList.Add( new ListItem("HEARD ISLAND AND MCDONALD ISLANDS","HM"));
                _CountryList.Add( new ListItem("HONDURAS","HN"));
                _CountryList.Add( new ListItem("HONG KONG","HK"));
                _CountryList.Add( new ListItem("HUNGARY","HU"));
                _CountryList.Add( new ListItem("ICELAND","IS"));
                _CountryList.Add( new ListItem("INDIA","IN"));
                _CountryList.Add( new ListItem("INDONESIA","ID"));
                _CountryList.Add( new ListItem("IRAN, ISLAMIC REPUBLIC OF","IR"));
                _CountryList.Add( new ListItem("IRAQ","IQ"));
                _CountryList.Add( new ListItem("IRELAND","IE"));
                _CountryList.Add( new ListItem("ISLE OF MAN","IM"));
                _CountryList.Add( new ListItem("ISRAEL","IL"));
                _CountryList.Add( new ListItem("ITALY","IT"));
                _CountryList.Add( new ListItem("JAMAICA","JM"));
                _CountryList.Add( new ListItem("JAPAN","JP"));
                _CountryList.Add( new ListItem("JERSEY","JE"));
                _CountryList.Add( new ListItem("JORDAN","JO"));
                _CountryList.Add( new ListItem("KAZAKHSTAN","KZ"));
                _CountryList.Add( new ListItem("KENYA","KE"));
                _CountryList.Add( new ListItem("KIRIBATI","KI"));
                _CountryList.Add( new ListItem("KOREA, DEMOCRATIC PEOPLE'S REPUBLIC OF","KP"));
                _CountryList.Add( new ListItem("KOREA, REPUBLIC OF","KR"));
                _CountryList.Add( new ListItem("KUWAIT","KW"));
                _CountryList.Add( new ListItem("KYRGYZSTAN","KG"));
                _CountryList.Add( new ListItem("LAO PEOPLE'S DEMOCRATIC REPUBLIC","LA"));
                _CountryList.Add( new ListItem("LATVIA","LV"));
                _CountryList.Add( new ListItem("LEBANON","LB"));
                _CountryList.Add( new ListItem("LESOTHO","LS"));
                _CountryList.Add( new ListItem("LIBERIA","LR"));
                _CountryList.Add( new ListItem("LIBYAN ARAB JAMAHIRIYA","LY"));
                _CountryList.Add( new ListItem("LIECHTENSTEIN","LI"));
                _CountryList.Add( new ListItem("LITHUANIA","LT"));
                _CountryList.Add( new ListItem("LUXEMBOURG","LU"));
                _CountryList.Add( new ListItem("MACAO","MO"));
                _CountryList.Add( new ListItem("MACEDONIA, THE FORMER YUGOSLAV REPUBLIC OF","MK"));
                _CountryList.Add( new ListItem("MADAGASCAR","MG"));
                _CountryList.Add( new ListItem("MALAWI","MW"));
                _CountryList.Add( new ListItem("MALAYSIA","MY"));
                _CountryList.Add( new ListItem("MALDIVES","MV"));
                _CountryList.Add( new ListItem("MALI","ML"));
                _CountryList.Add( new ListItem("MALTA","MT"));
                _CountryList.Add( new ListItem("MARSHALL ISLANDS","MH"));
                _CountryList.Add( new ListItem("MARTINIQUE","MQ"));
                _CountryList.Add( new ListItem("MAURITANIA","MR"));
                _CountryList.Add( new ListItem("MAURITIUS","MU"));
                _CountryList.Add( new ListItem("MAYOTTE","YT"));
                _CountryList.Add( new ListItem("MEXICO","MX"));
                _CountryList.Add( new ListItem("MICRONESIA, FEDERATED STATES OF","FM"));
                _CountryList.Add( new ListItem("MOLDOVA, REPUBLIC OF","MD"));
                _CountryList.Add( new ListItem("MONACO","MC"));
                _CountryList.Add( new ListItem("MONGOLIA","MN"));
                _CountryList.Add( new ListItem("MONTENEGRO","ME"));
                _CountryList.Add( new ListItem("MONTSERRAT","MS"));
                _CountryList.Add( new ListItem("MOROCCO","MA"));
                _CountryList.Add( new ListItem("MOZAMBIQUE","MZ"));
                _CountryList.Add( new ListItem("MYANMAR","MM"));
                _CountryList.Add( new ListItem("NAMIBIA","NA"));
                _CountryList.Add( new ListItem("NAURU","NR"));
                _CountryList.Add( new ListItem("NEPAL","NP"));
                _CountryList.Add( new ListItem("NETHERLANDS","NL"));
                _CountryList.Add( new ListItem("NETHERLANDS ANTILLES","AN"));
                _CountryList.Add( new ListItem("CALEDONIA","NC"));
                _CountryList.Add( new ListItem("ZEALAND","NZ"));
                _CountryList.Add( new ListItem("NICARAGUA","NI"));
                _CountryList.Add( new ListItem("NIGER","NE"));
                _CountryList.Add( new ListItem("NIGERIA","NG"));
                _CountryList.Add( new ListItem("NIUE","NU"));
                _CountryList.Add( new ListItem("NORFOLK ISLAND","NF"));
                _CountryList.Add( new ListItem("NORTHERN MARIANA ISLANDS","MP"));
                _CountryList.Add( new ListItem("NORWAY","NO"));
                _CountryList.Add( new ListItem("OMAN","OM"));
                _CountryList.Add( new ListItem("PAKISTAN","PK"));
                _CountryList.Add( new ListItem("PALAU","PW"));
                _CountryList.Add( new ListItem("PALESTINIAN TERRITORY, OCCUPIED","PS"));
                _CountryList.Add( new ListItem("PANAMA","PA"));
                _CountryList.Add( new ListItem("PAPUA NEW GUINEA","PG"));
                _CountryList.Add( new ListItem("PARAGUAY","PY"));
                _CountryList.Add( new ListItem("PERU","PE"));
                _CountryList.Add( new ListItem("PHILIPPINES","PH"));
                _CountryList.Add( new ListItem("PITCAIRN","PN"));
                _CountryList.Add( new ListItem("POLAND","PL"));
                _CountryList.Add( new ListItem("PORTUGAL","PT"));
                _CountryList.Add( new ListItem("PUERTO RICO","PR"));
                _CountryList.Add( new ListItem("QATAR","QA"));
                _CountryList.Add( new ListItem("RÉUNION","RE"));
                _CountryList.Add( new ListItem("ROMANIA","RO"));
                _CountryList.Add( new ListItem("RUSSIAN FEDERATION","RU"));
                _CountryList.Add( new ListItem("RWANDA","RW"));
                _CountryList.Add( new ListItem("SAINT BARTHÉLEMY","BL"));
                _CountryList.Add( new ListItem("SAINT HELENA, ASCENSION AND TRISTAN DA CUNHA","SH"));
                _CountryList.Add( new ListItem("SAINT KITTS AND NEVIS","KN"));
                _CountryList.Add( new ListItem("SAINT LUCIA","LC"));
                _CountryList.Add( new ListItem("SAINT MARTIN","MF"));
                _CountryList.Add( new ListItem("SAINT PIERRE AND MIQUELON","PM"));
                _CountryList.Add( new ListItem("SAINT VINCENT AND THE GRENADINES","VC"));
                _CountryList.Add( new ListItem("SAMOA","WS"));
                _CountryList.Add( new ListItem("SAN MARINO","SM"));
                _CountryList.Add( new ListItem("SAO TOME AND PRINCIPE","ST"));
                _CountryList.Add( new ListItem("SAUDI ARABIA","SA"));
                _CountryList.Add( new ListItem("SENEGAL","SN"));
                _CountryList.Add( new ListItem("SERBIA","RS"));
                _CountryList.Add( new ListItem("SEYCHELLES","SC"));
                _CountryList.Add( new ListItem("SIERRA LEONE","SL"));
                _CountryList.Add( new ListItem("SINGAPORE","SG"));
                _CountryList.Add( new ListItem("SLOVAKIA","SK"));
                _CountryList.Add( new ListItem("SLOVENIA","SI"));
                _CountryList.Add( new ListItem("SOLOMON ISLANDS","SB"));
                _CountryList.Add( new ListItem("SOMALIA","SO"));
                _CountryList.Add( new ListItem("SOUTH AFRICA","ZA"));
                _CountryList.Add( new ListItem("SOUTH GEORGIA AND THE SOUTH SANDWICH ISLANDS","GS"));
                _CountryList.Add( new ListItem("SPAIN","ES"));
                _CountryList.Add( new ListItem("SRI LANKA","LK"));
                _CountryList.Add( new ListItem("SUDAN","SD"));
                _CountryList.Add( new ListItem("SURINAME","SR"));
                _CountryList.Add( new ListItem("SVALBARD AND JAN MAYEN","SJ"));
                _CountryList.Add( new ListItem("SWAZILAND","SZ"));
                _CountryList.Add( new ListItem("SWEDEN","SE"));
                _CountryList.Add( new ListItem("SWITZERLAND","CH"));
                _CountryList.Add( new ListItem("SYRIAN ARAB REPUBLIC","SY"));
                _CountryList.Add( new ListItem("TAIWAN, PROVINCE OF CHINA","TW"));
                _CountryList.Add( new ListItem("TAJIKISTAN","TJ"));
                _CountryList.Add( new ListItem("TANZANIA, UNITED REPUBLIC OF","TZ"));
                _CountryList.Add( new ListItem("THAILAND","TH"));
                _CountryList.Add( new ListItem("TIMOR-LESTE","TL"));
                _CountryList.Add( new ListItem("TOGO","TG"));
                _CountryList.Add( new ListItem("TOKELAU","TK"));
                _CountryList.Add( new ListItem("TONGA","TO"));
                _CountryList.Add( new ListItem("TRINIDAD AND TOBAGO","TT"));
                _CountryList.Add( new ListItem("TUNISIA","TN"));
                _CountryList.Add( new ListItem("TURKEY","TR"));
                _CountryList.Add( new ListItem("TURKMENISTAN","TM"));
                _CountryList.Add( new ListItem("TURKS AND CAICOS ISLANDS","TC"));
                _CountryList.Add( new ListItem("TUVALU","TV"));
                _CountryList.Add( new ListItem("UGANDA","UG"));
                _CountryList.Add( new ListItem("UKRAINE","UA"));
                _CountryList.Add( new ListItem("UNITED ARAB EMIRATES","AE"));
                _CountryList.Add( new ListItem("UNITED KINGDOM","GB"));
                _CountryList.Add( new ListItem("UNITED STATES","US"));
                _CountryList.Add( new ListItem("UNITED STATES MINOR OUTLYING ISLANDS","UM"));
                _CountryList.Add( new ListItem("URUGUAY","UY"));
                _CountryList.Add( new ListItem("UZBEKISTAN","UZ"));
                _CountryList.Add( new ListItem("VANUATU","VU"));
                _CountryList.Add( new ListItem("VENEZUELA, BOLIVARIAN REPUBLIC OF","VE"));
                _CountryList.Add( new ListItem("VIET NAM","VN"));
                _CountryList.Add( new ListItem("VIRGIN ISLANDS, BRITISH","VG"));
                _CountryList.Add( new ListItem("VIRGIN ISLANDS, U.S.","VI"));
                _CountryList.Add( new ListItem("WALLIS AND FUTUNA","WF"));
                _CountryList.Add( new ListItem("WESTERN SAHARA","EH"));
                _CountryList.Add( new ListItem("YEMEN","YE"));
                _CountryList.Add( new ListItem("ZAMBIA","ZM"));
                _CountryList.Add( new ListItem("ZIMBABWE","ZW"));
            }

        public static void AddIDTypeList(ListItemCollection _List, Boolean ClearListFirst)
        {
            if (ClearListFirst)
            {
                _List.Clear();
            }
            _List.Add( new ListItem("-nog niet bekend-",""));
            _List.Add( new ListItem("Paspoort","Passport"));
            _List.Add( new ListItem("Identificatie-/toeristenkaart","ID card"));
            _List.Add( new ListItem("Rijbewijs","Drivers license"));
        }

        public static void AddRelationTypeList(ListItemCollection _List, Boolean ClearListFirst)
        {
            if (ClearListFirst)
            {
                _List.Clear();
            }
            //_List.Add(new ListItem("-nog niet bekend-", ""));
            _List.Add(new ListItem("Relatie (afnemer en leverancier)", "Both"));
            _List.Add(new ListItem("Leverancier/crediteur", "Creditor"));
            _List.Add(new ListItem("Afnemer/debiteur", "Debtor"));
            _List.Add(new ListItem("Transporteur", "Transport"));
            _List.Add(new ListItem("Anderssoortige relatie", "Other"));
        }

        public static void AddDefaultAllLocationText(ListItemCollection _List, Boolean ClearListFirst)
        {
            if (ClearListFirst)
            {
                _List.Clear();
            }
            _List.Insert(0, new ListItem("-- Bruikbaar voor alle lokaties --", ""));
        }

        public static void AddCurrencyList(ListItemCollection _List, Boolean ClearListFirst)
        {
            if (ClearListFirst)
            {
                _List.Clear();
            }
            ReverseAddListItem( _List, "Eur", "Euro");
/*
            ReverseAddListItem( _List, "GBP", "Brits pond");
            ReverseAddListItem( _List, "USD", "US dollar");
            ReverseAddListItem( _List, "DKK", "Deense kroon");
            ReverseAddListItem( _List, "ALL", "Albanese Lek");
            ReverseAddListItem( _List, "BGL", "Bulgaarse Lev");
            ReverseAddListItem( _List, "HRK", "Croaatse Kuna");
            ReverseAddListItem( _List, "CZK", "Tjechische Kroon");
            ReverseAddListItem( _List, "HUF", "Hongaarse forint");
            ReverseAddListItem( _List, "LVL", "Letse Lat");
            ReverseAddListItem( _List, "LTL", "Litouwse Litas");
            ReverseAddListItem( _List, "PLN", "Poolse Zloty");
            ReverseAddListItem( _List, "ROL", "Roemeense Leu");
            ReverseAddListItem( _List, "RUB", "Roebel (Russische Federatie/Sovjet Unie)");
            ReverseAddListItem( _List, "TRY", "Turkse Lira");
            ReverseAddListItem( _List, "YUN", "Joegoslavische Dinar");
*/
            // check the list for illegal chars 
            foreach (ListItem li in _List)
            {
                if (li.Value.Length > 3)
                {
                    li.Value = li.Value.Substring(0, 3);
                }
            }
        }

        public static void AddLedgerTypeList(ListItemCollection _List, Boolean ClearListFirst)
        {
            if (ClearListFirst)
            {
                _List.Clear();
            }
            _List.Add(new ListItem("Kas", "Cash"));
            _List.Add(new ListItem("Bank", "Bank"));
            //_List.Add(new ListItem("Debug", "Debug"));
        }

        public static void AddLedgerBookingTypeList(ListItemCollection _List, Boolean ClearListFirst)
        {
            AddPriceAgreementTypeList(_List, ClearListFirst);
            //_List.Add(new ListItem("Correctie", "Correction"));
        }

        public static void AddPriceAgreementTypeList(ListItemCollection _List, Boolean ClearListFirst)
        {
            if (ClearListFirst)
            {
                _List.Clear();
            }
            _List.Add(new ListItem("Aankoop", "Buy"));
            _List.Add(new ListItem("Verkoop", "Sell"));
        }

        public static void AddWorkTypeList(ListItemCollection _List, Boolean ClearListFirst)
        {
            if (ClearListFirst)
            {
                _List.Clear();
            }
            _List.Add(new ListItem("Verricht door ons", "ByUs"));
            _List.Add(new ListItem("Verricht door klant (voor ons)", "ByCustomer"));
        }

        public static void AddAdvancePaymentTypeList(ListItemCollection _List, Boolean ClearListFirst)
        {
            if (ClearListFirst)
            {
                _List.Clear();
            }
            _List.Add(new ListItem("Betaald aan relatie", "Paid"));
            _List.Add(new ListItem("Ontvangen van relatie", "Received"));
        }

        public static void AddContractTypeList(ListItemCollection _List, Boolean ClearListFirst)
        {
            AddPriceAgreementTypeList(_List, ClearListFirst);
        }

        public static void AddMaterialCategoryList(ListItemCollection _List, Boolean ClearListFirst)
        {
            if (ClearListFirst)
            {
                _List.Clear();
            }
            _List.Add(new ListItem("Ferro", "Ferro"));
            _List.Add(new ListItem("Non-ferro", "NonFerro"));
            _List.Add(new ListItem("Glas", "Glass"));
            _List.Add(new ListItem("Divers", "Other"));
            _List.Add(new ListItem("", ""));
        }

        public static void AddMaterialInvoiceTypeList(ListItemCollection _List, Boolean ClearListFirst)
        {
            AddPriceAgreementTypeList(_List, ClearListFirst);
            _List.Add(new ListItem("Beide", "Both"));
        }

        public static void AddRelationContractStatusList(ListItemCollection _List, Boolean ClearListFirst)
        {
            if (ClearListFirst)
            {
                _List.Clear();
            }
            _List.Add(new ListItem("Open", "Open"));
            _List.Add(new ListItem("Gesloten", "Closed"));
            _List.Add(new ListItem("Gepauzeerd", "Paused"));
        }

        public static void AddCustomerRelationTypeList(ListItemCollection _List, Boolean ClearListFirst)
        {
            if (ClearListFirst)
            {
                _List.Clear();
            }
            _List.Add(new ListItem("Hoofdcontact", "Primary"));
            _List.Add(new ListItem("Verkoop", "Sales"));
            _List.Add(new ListItem("Inkoop", "Purchase"));
            _List.Add(new ListItem("Administratie", "Administration"));
            _List.Add(new ListItem("Eigenaar", "Owner"));
            _List.Add(new ListItem("Anders", "Other"));
            _List.Add(new ListItem("Onbekend", ""));

        }

        public static void AddCustomerRelationAdressTypeList(ListItemCollection _List, Boolean ClearListFirst)
        {
            if (ClearListFirst)
            {
                _List.Clear();
            }
            _List.Add(new ListItem("Factuuradres", "Invoice"));
            _List.Add(new ListItem("Bezoekadres", "Visiting"));
            _List.Add(new ListItem("Postadres", "Mail"));
            _List.Add(new ListItem("Adres voor ophalen en afleveren goederen", "Delivery"));
            _List.Add(new ListItem("Andersoortig adres", "Other"));
            _List.Add(new ListItem("Onbekend", ""));
        }

        public static void AddRelationContactFollowUpStateList(ListItemCollection _List, Boolean ClearListFirst)
        {
            if (ClearListFirst)
            {
                _List.Clear();
            }
            _List.Add(new ListItem("Open", "Unhandled"));
            _List.Add(new ListItem("Afgehandeld", "Handled"));
            _List.Add(new ListItem("Gepauzeerd", "Paused"));
        }

        public static void AddRelationContactTypeList(ListItemCollection _List, Boolean ClearListFirst)
        {
            if (ClearListFirst)
            {
                _List.Clear();
            }
            _List.Add(new ListItem("E-mail", "E-Mail"));
            _List.Add(new ListItem("Telefoon", "Phone"));
            _List.Add(new ListItem("Bezoek", "Visit"));
            _List.Add(new ListItem("Bezoek aan ons", "OurOffice"));
            _List.Add(new ListItem("Anders", "Other"));
        }

        public static void AddOrderStatusList(ListItemCollection _List, Boolean ClearListFirst)
        {
            if (ClearListFirst)
            {
                _List.Clear();
            }
            _List.Add(new ListItem("Nog te leveren", "Open"));
            _List.Add(new ListItem("Geleverd", "Processed"));
        }

        public static void AddInvoiceStatusList(ListItemCollection _List, Boolean ClearListFirst)
        {
            if (ClearListFirst)
            {
                _List.Clear();
            }
            _List.Add(new ListItem("Nog te betalen", "Open"));
            _List.Add(new ListItem("Gedeeltelijk betaald", "PPaid"));
            _List.Add(new ListItem("Betaald", "Paid"));
        }

        public static void AddFreightTypeList(ListItemCollection _List, Boolean ClearListFirst)
        {
            if (ClearListFirst)
            {
                _List.Clear();
            }
            _List.Add(new ListItem("Transport", "Transport"));
            _List.Add(new ListItem("Weging", "Weighing"));
        }

        public static void AddFreightStatusList(ListItemCollection _List, Boolean ClearListFirst)
        {
            if (ClearListFirst)
            {
                _List.Clear();
            }
            _List.Add(new ListItem("Verwacht", "Expected"));
            _List.Add(new ListItem("Wacht op goedkeuring overheid", "Wait approval"));
            _List.Add(new ListItem("Ingepland", "Planned"));
            _List.Add(new ListItem("In uitvoering", "In progress"));
            _List.Add(new ListItem("Wacht op tweede weging", "2nd weighing"));
            _List.Add(new ListItem("In sorteerproces", "In sorting"));
            _List.Add(new ListItem("Afgeleverd", "Delivered"));
            _List.Add(new ListItem("Wacht op facturering", "To be invoiced"));
            _List.Add(new ListItem("Gereed", "Done"));
        }

        public static void AddFreightDirectionList(ListItemCollection _List, Boolean ClearListFirst)
        {
            if (ClearListFirst)
            {
                _List.Clear();
            }
            _List.Add(new ListItem("Van klant naar ons", "To warehouse"));
            _List.Add(new ListItem("Van ons naar klant", "To customer"));
            _List.Add(new ListItem("Van klant naar klant", "Customer to customer"));
        }

        public static void AddRentLedgerStatusList(ListItemCollection _List, Boolean ClearListFirst)
        {
            if (ClearListFirst)
            {
                _List.Clear();
            }
            _List.Add(new ListItem("Gereserveerd/uitgeleend", "Open"));
            _List.Add(new ListItem("Gefactureerd", "Invoiced"));
            _List.Add(new ListItem("Verloren", "Lost"));
            _List.Add(new ListItem("Teruggebracht met schade", "Damaged"));
            _List.Add(new ListItem("Teruggebracht en OK", "Returned"));
        }

        public static void AddRentalItemStateList(ListItemCollection _List, Boolean ClearListFirst)
        {
            if (ClearListFirst)
            {
                _List.Clear();
            }
            _List.Add(new ListItem("Beschikbaar", "Available"));
            _List.Add(new ListItem("Verhuurd", "Rented"));
            _List.Add(new ListItem("In reparatie", "In repair"));
            _List.Add(new ListItem("Niet beschikbaar (zie toelichting)", "Not available"));
            _List.Add(new ListItem("Afgeschreven", "Retired"));
        }

        #region Freight types
        private static void ReverseAddListItem(ListItemCollection _List, string Value, string Text)
        {
            ListItem li = new ListItem(Text, Value);
            _List.Add(li);
        }

        public static void AddFreightOurRoleTypeList(ListItemCollection _List, Boolean ClearListFirst)
        {
            if (ClearListFirst)
            {
                _List.Clear();
            }
            _List.Add(new ListItem("(Primaire) ontdoener", "Destructor"));
            _List.Add(new ListItem("Ontvanger", "Receiver"));
            _List.Add(new ListItem("Handelaar", "Trader"));
            _List.Add(new ListItem("Bemiddelaar", "Agent"));
        }

        public static void AddFreightTransportNotificationTransportType(ListItemCollection _List, Boolean ClearListFirst)
        {
            if (ClearListFirst)
            {
                _List.Clear();
            }
            _List.Add(new ListItem("Eenmalige overbrenging", "Single"));
            _List.Add(new ListItem("Meervoudige overbrenging", "Multiple"));
        }

        public static void AddFreightTransportNotificationRemovalType(ListItemCollection _List, Boolean ClearListFirst)
        {
            if (ClearListFirst)
            {
                _List.Clear();
            }
            _List.Add(new ListItem("Verwijdering", "Remove"));
            _List.Add(new ListItem("Nuttige toepassing", "Use"));
        }        

        public static void AddWrappingTypeList(ListItemCollection _List, Boolean ClearListFirst)
        {
            if (ClearListFirst)
            {
                _List.Clear();
            }
            ReverseAddListItem(_List,"","-nvt-");
            ReverseAddListItem(_List,"Barrels", "Vaten");
            ReverseAddListItem(_List,"Wooden barrels", "Houten vaten");
            ReverseAddListItem(_List,"Jerrycans", "Blikken/Jerrycans");
            ReverseAddListItem(_List,"Crates", "Kisten");
            ReverseAddListItem(_List,"Bags", "Zakken");
            ReverseAddListItem(_List,"Compound container", "Samengestelde verpakking");
            ReverseAddListItem(_List,"Pressurized containers", "Drukcontainers");
            ReverseAddListItem(_List,"Not wrapped", "Onverpakt");
            ReverseAddListItem(_List,"Other", "Overig (specificeren)");
        }

        public static void AddTransportTypeList(ListItemCollection _List, Boolean ClearListFirst)
        {
            if (ClearListFirst)
            {
                _List.Clear();
            }
            ReverseAddListItem(_List,"","-nvt-");
            ReverseAddListItem(_List,"Road", "Over de weg");
            ReverseAddListItem(_List,"Train/rail", "Per trein");
            ReverseAddListItem(_List,"Sea", "Over zee");
            ReverseAddListItem(_List,"Air", "Per vliegtuig");
            ReverseAddListItem(_List,"inland Waterways", "Over binnenwateren");
        }

        public static void AddPhysicalShapeList(ListItemCollection _List, Boolean ClearListFirst)
        {
            if (ClearListFirst)
            {
                _List.Clear();
            }
            ReverseAddListItem(_List, "", "-nvt-");
            ReverseAddListItem(_List, "Solid", "Vast");
            ReverseAddListItem(_List, "Paste", "Brei/Pasta");
            ReverseAddListItem(_List, "Slurrie", "Slurrie");
            ReverseAddListItem(_List, "Liquid", "Vloeibaar");
            ReverseAddListItem(_List, "Gas", "Gasvormig");
            ReverseAddListItem(_List, "Other", "Anders (specificeer in materiaalomschrijving)");
        }

        public static void AddHCodeList(ListItemCollection _List, Boolean ClearListFirst)
        {
            if (ClearListFirst)
            {
                _List.Clear();
            }
            ReverseAddListItem(_List,"", "-nvt-");
            ReverseAddListItem(_List,"H1", "H1 Ontplofbaar");
            ReverseAddListItem(_List,"H3", "H3 Ontvlambare vloeistoffen");
            ReverseAddListItem(_List,"H4.1", "H4.1 Ontvlambare vast stoffen");
            ReverseAddListItem(_List,"H4.2", "H4.2 Zelfontvlambare stoffen of afvalstoffen");
            ReverseAddListItem(_List,"H4.3", "H4.3 Stoffen of afvalstoffen die bij aanraking met water ontvlambare gassen ontwikkelen");
            ReverseAddListItem(_List,"H5.1", "H5.1 Oxiderend");
            ReverseAddListItem(_List,"H5.2", "H5.2 Organische peroxiden");
            ReverseAddListItem(_List,"H6.1", "H6.1 (Acuut) giftige stoffen");
            ReverseAddListItem(_List,"H6.2", "H6.2 Infectueze stoffen");
            ReverseAddListItem(_List,"H8", "H8 Corrosieve stoffen");
            ReverseAddListItem(_List,"H10", "H10 Afscheiding met giftige gassen bij aanraking met lucht en water");
            ReverseAddListItem(_List,"H11", "H11 Toxisch (vertraagd of chronisch)");
            ReverseAddListItem(_List,"H12", "H12 Ecotoxisch");
            ReverseAddListItem(_List,"H13", "H13 Stoffen die na verwijdering een andere stof doen ontstaan die één van de bovengenoemde eigenschappen bezit");
        }

        public static void AddBaselCodeList(ListItemCollection _List, Boolean ClearListFirst)
        {
            if (ClearListFirst)
            {
                _List.Clear();
            }
             ReverseAddListItem(_List,"", "-nvt-");
             ReverseAddListItem(_List,"A1010","A1010 Metal wastes and waste consisting of alloys of any of the following:• 	Antimony• 	Arsenic• 	Beryllium• 	Cadmium• 	Lead• 	Mercury•	Selenium• 	Tellurium• 	Thallium	but excluding such wastes specifically listed on list B.");
            ReverseAddListItem(_List,"A1020","A1020 Waste having as constituents or contaminants, excluding metal waste in massive form, any of the following:• 	Antimony; antimony compounds• 	Beryllium; beryllium compounds• 	Cadmium; cadmium compounds• 	Lead; lead compounds• 	Selenium; selenium compounds• 	Tellurium; tellurium compounds");
            ReverseAddListItem(_List,"A1030","A1030 Wastes having as constituents or contaminants any of the following:• 	Arsenic; arsenic compounds• 	Mercury; mercury compounds• 	Thallium; thallium compounds");
            ReverseAddListItem(_List,"A1040","A1040 Wastes having as constituents any of the following:• 	Metal carbonyls• 	Hexavalent chromium compounds");
            ReverseAddListItem(_List,"A1050","A1050 Galvanic sludges");
            ReverseAddListItem(_List,"A1060","A1060 Waste liquors from the pickling of metals");
            ReverseAddListItem(_List,"A1070","A1070 Leaching residues from zinc processing, dust and sludges such as jarosite, hematite, etc.");
            ReverseAddListItem(_List,"A1080","A1080 Waste zinc residues not included on list B, containing lead and cadmium in concentrations sufficient to exhibit Annex III characteristics");
            ReverseAddListItem(_List,"A1090","A1090 Ashes from the incineration of insulated copper wire");
            ReverseAddListItem(_List,"A1100","A1100 Dusts and residues from gas cleaning systems of copper smelters");
            ReverseAddListItem(_List,"A1110","A1110 Spent electrolytic solutions from copper electrorefining and electrowinning operations");
            ReverseAddListItem(_List,"A1120","A1120 Waste sludges, excluding anode slimes, from electrolyte purification systems in copper electrorefining and electrowinning operations");
            ReverseAddListItem(_List,"A1130","A1130 Spent etching solutions containing dissolved copper");
            ReverseAddListItem(_List,"A1140","A1140 Waste cupric chloride and copper cyanide catalysts");
            ReverseAddListItem(_List,"A1150","A1150 Precious metal ash from incineration of printed circuit boards not included on list B Note that mirror entry on list B (B1160) does not specify exceptions.");
            ReverseAddListItem(_List,"A1160","A1160 Waste lead-acid batteries, whole or crushed");
            ReverseAddListItem(_List,"A1170","A1170 Unsorted waste batteries excluding mixtures of only list B batteries.  Waste batteries not specified on list B containing Annex I constituents to an extent to render them hazardous");
            ReverseAddListItem(_List,"A1180","A1180 Waste electrical and electronic assemblies or scrap This entry does not include scrap assemblies from electric power generation.  containing components such as accumulators and other batteries included on list A, mercury-switches, glass from cathode-ray tubes and other activated glass and PCB-capacitors, or contaminated with Annex I constituents (e.g., cadmium, mercury, lead, polychlorinated biphenyl) to an extent that they possess any of the characteristics contained in Annex III (note the related entry on list B B1110) PCBs are at a concentration level of 50 mg/kg or more.");
            ReverseAddListItem(_List,"A1190","A1190 Waste metal cables coated or insulated with plastics containing or contaminated with coal tar, PCB PCBs are at a concentration level of 50 mg/kg or more., lead, cadmium, other organohalogen compounds or other Annex I constituents to an extent that they exhibit Annex III characteristics.");
            ReverseAddListItem(_List,"A2010","A2010 Glass waste from cathode-ray tubes and other activated glasses");
            ReverseAddListItem(_List,"A2020","A2020 Waste inorganic fluorine compounds in the form of liquids or sludges but excluding such wastes specified on list B");
            ReverseAddListItem(_List,"A2030","A2030 Waste catalysts but excluding such wastes specified on list B");
            ReverseAddListItem(_List,"A2040","A2040 Waste gypsum arising from chemical industry processes, when containing Annex I constituents to the extent that it exhibits an Annex III hazardous characteristic (note the related entry on list B B2080)");
            ReverseAddListItem(_List,"A2050","A2050 Waste asbestos (dusts and fibres)");
            ReverseAddListItem(_List,"A2060","A2060 Coal-fired power plant fly-ash containing Annex I substances in concentrations sufficient to exhibit Annex III characteristics (note the related entry on list B B2050)");
            ReverseAddListItem(_List,"A3010","A3010 Waste from the production or processing of petroleum coke and bitumen");
            ReverseAddListItem(_List,"A3020","A3020 Waste mineral oils unfit for their originally intended use");
            ReverseAddListItem(_List,"A3030","A3030 Wastes that contain, consist of or are contaminated with leaded anti-knock compound sludges");
            ReverseAddListItem(_List,"A3040","A3040 Waste thermal (heat transfer) fluids");
            ReverseAddListItem(_List,"A3050","A3050 Wastes from production, formulation and use of resins, latex, plasticizers, glues/adhesives excluding such wastes specified on list B (note the related entry on list B B4020)");
            ReverseAddListItem(_List,"A3060","A3060 Waste nitrocellulose");
            ReverseAddListItem(_List,"A3070","A3070 Waste phenols, phenol compounds including chlorophenol in the form of liquids or sludges");
            ReverseAddListItem(_List,"A3080","A3080 Waste ethers not including those specified on list B");
            ReverseAddListItem(_List,"A3090","A3090 Waste leather dust, ash, sludges and flours when containing hexavalent chromium compounds or biocides (note the related entry on list B B3100)");
            ReverseAddListItem(_List,"A3100","A3100 Waste paring and other waste of leather or of composition leather not suitable for the manufacture of leather articles containing hexavalent chromium compounds or biocides (note the related entry on list B B3090)");
            ReverseAddListItem(_List,"A3110","A3110 Fellmongery wastes containing hexavalent chromium compounds or biocides or infectious substances (note the related entry on list B B3110)");
            ReverseAddListItem(_List,"A3120","A3120 Fluff - light fraction from shredding");
            ReverseAddListItem(_List,"A3130","A3130 Waste organic phosphorous compounds");
            ReverseAddListItem(_List,"A3140","A3140 Waste non-halogenated organic solvents but excluding such wastes specified on list B");
            ReverseAddListItem(_List,"A3150","A3150 Waste halogenated organic solvents");
            ReverseAddListItem(_List,"A3160","A3160 Waste halogenated or unhalogenated non-aqueous distillation residues arising from organic solvent recovery operations");
            ReverseAddListItem(_List,"A3170","A3170 Wastes arising from the production of aliphatic halogenated hydrocarbons (such as chloromethane, dichloro-ethane, vinyl chloride, vinylidene chloride, allyl chloride and epichlorhydrin)");
            ReverseAddListItem(_List,"A3180","A3180 Wastes, substances and articles containing, consisting of or contaminated with polychlorinated biphenyl (PCB), polychlorinated terphenyl (PCT), polychlorinated naphthalene (PCN) or polybrominated biphenyl (PBB), or any other polybrominated analogues of these compounds, at a concentration level of 50 mg/kg or more The 50 mg/kg level is considered to be an internationally practical level for all wastes. However, many individual countries have established lower regulatory levels (e.g., 20 mg/kg) for specific wastes.");
            ReverseAddListItem(_List,"A3190","A3190 Waste tarry residues (excluding asphalt cements) arising from refining, distillation and any pyrolitic treatment of organic materials");
            ReverseAddListItem(_List,"A3200","A3200 Bituminous material (asphalt waste) from road construction and maintenance, containing tar (note the related entry on list B, B2130)");
            ReverseAddListItem(_List,"A4010","A4010 Wastes from the production, preparation and use of pharmaceutical products but excluding such wastes specified on list B");
            ReverseAddListItem(_List,"A4020","A4020 Clinical and related wastes; that is wastes arising from medical, nursing, dental, veterinary, or similar practices, and wastes generated in hospitals or other facilities during the investigation or treatment of patients, or research projects");
            ReverseAddListItem(_List,"A4030","A4030 Wastes from the production, formulation and use of biocides and phytopharmaceuticals, including waste pesticides and herbicides which are off-specification, outdated, “Outdated” means unused within the period recommended by the manufacturer. or unfit for their originally intended use");
            ReverseAddListItem(_List,"A4040","A4040 Wastes from the manufacture, formulation and use of wood-preserving chemicals This entry does not include wood treated with wood preserving chemicals. ");
            ReverseAddListItem(_List,"A4050","A4050 Wastes that contain, consist of or are contaminated with any of the following:• 	Inorganic cyanides, excepting precious-metal-bearing residues in solid form containing traces of inorganic cyanides• 	Organic cyanides");
            ReverseAddListItem(_List,"A4060","A4060 Waste oils/water, hydrocarbons/water mixtures, emulsions");
            ReverseAddListItem(_List,"A4070","A4070 Wastes from the production, formulation and use of inks, dyes, pigments, paints, lacquers, varnish excluding any such waste specified on list B (note the related entry on list B B4010)");
            ReverseAddListItem(_List,"A4080","A4080 Wastes of an explosive nature (but excluding such wastes specified on list B)");
            ReverseAddListItem(_List,"A4090","A4090 Waste acidic or basic solutions, other than those specified in the corresponding entry on list B (note the related entry on list B B2120)");
            ReverseAddListItem(_List,"A4100","A4100 Wastes from industrial pollution control devices for cleaning of industrial off-gases but excluding such wastes specified on list B");
            ReverseAddListItem(_List,"A4110","A4110 Wastes that contain, consist of or are contaminated with any of the following:• 	Any congenor of polychlorinated dibenzo-furan• 	Any congenor of polychlorinated dibenzo-dioxin");
            ReverseAddListItem(_List,"A4120","A4120 Wastes that contain, consist of or are contaminated with peroxides");
            ReverseAddListItem(_List,"A4130","A4130 Waste packages and containers containing Annex I substances in concentrations sufficient to exhibit Annex III hazard characteristics");
            ReverseAddListItem(_List,"A4140","A4140 Waste consisting of or containing off specification or outdated “Outdated” means unused within the period recommended by the manufacturer. chemicals corresponding to Annex I categories and exhibiting Annex III hazard characteristics");
            ReverseAddListItem(_List,"A4150","A4150 Waste chemical substances arising from research and development or teaching activities which are not identified and/or are new and whose effects on human health and/or the environment are not known");
            ReverseAddListItem(_List,"A4160","A4160 Spent activated carbon not included on list B (note the related entry on list B B2060)");
            ReverseAddListItem(_List,"B1010","B1010 Metal and metal-alloy wastes in metallic, non-dispersible form:• 	Precious metals (gold, silver, the platinum group, but not mercury)• 	Iron and steel scrap• 	Copper scrap• 	Nickel scrap• 	Aluminium scrap• 	Zinc scrap• 	Tin scrap• 	Tungsten scrap• 	Molybdenum scrap• 	Tantalum scrap• 	Magnesium scrap• 	Cobalt scrap• 	Bismuth scrap• 	Titanium scrap•	Zirconium scrap• 	Manganese scrap• 	Germanium scrap• 	Vanadium scrap• 	Scrap of hafnium, indium, niobium, rhenium and gallium• 	Thorium scrap• 	Rare earths scrap• 	Chromium scrap");
            ReverseAddListItem(_List,"B1020","B1020 Clean, uncontaminated metal scrap, including alloys, in bulk finished form (sheet, plate, beams, rods, etc), of:• 	Antimony scrap• 	Beryllium scrap• 	Cadmium scrap• 	Lead scrap (but excluding lead-acid batteries)• 	Selenium scrap• 	Tellurium scrap");
            ReverseAddListItem(_List,"B1030","B1030 Refractory metals containing residues");
            ReverseAddListItem(_List,"B1031","B1031 Molybdenum, tungsten, titanium, tantalum, niobium and rhenium metal and metal alloy wastes in metallic dispersible form (metal powder), excluding such wastes as specified in list A under entry A1050, Galvanic sludges");
            ReverseAddListItem(_List,"B1040","B1040 Scrap assemblies from electrical power generation not contaminated with lubricating oil, PCB or PCT to an extent to render them hazardous");
            ReverseAddListItem(_List,"B1050","B1050 Mixed non-ferrous metal, heavy fraction scrap, not containing Annex I materials in concentrations sufficient to exhibit Annex III characteristics Note that even where low level contamination with Annex I materials initially exists, subsequent processes, including recycling processes, may result in separated fractions containing significantly enhanced concentrations of those Annex I materials. ");
            ReverseAddListItem(_List,"B1060","B1060 Waste selenium and tellurium in metallic elemental form including powder");
            ReverseAddListItem(_List,"B1070","B1070 Waste of copper and copper alloys in dispersible form, unless they contain Annex I constituents to an extent that they exhibit Annex III characteristics");
            ReverseAddListItem(_List,"B1080","B1080 Zinc ash and residues including zinc alloys residues in dispersible form unless containing Annex I constituents in concentration such as to exhibit Annex III characteristics or exhibiting hazard characteristic H4.3 The status of zinc ash is currently under review and there is a recommendation with the United Nations Conference on Trade and Development (UNCTAD) that zinc ashes should not be dangerous goods.");
            ReverseAddListItem(_List,"B1090","B1090 Waste batteries conforming to a specification, excluding those made with lead, cadmium or mercury");
            ReverseAddListItem(_List,"B1100","B1100 Metal-bearing wastes arising from melting, smelting and refining of metals:• 	Hard zinc spelter• 	Zinc-containing drosses:- 	Galvanizing slab zinc top dross (>90% Zn)- 	Galvanizing slab zinc bottom dross (>92% Zn)- 	Zinc die casting dross (>85% Zn)- 	Hot dip galvanizers slab zinc dross (batch)(>92% Zn)- 	Zinc skimmings• 	Aluminium skimmings (or skims) excluding salt slag• 	Slags from copper processing for further processing or refining not containing arsenic, lead or cadmium to an extent that they exhibit Annex III hazard characteristics• 	Wastes of refractory linings, including crucibles, originating from copper smelting • 	Slags from precious metals processing for further refining• 	Tantalum-bearing tin slags with less than 0.5% tin");
            ReverseAddListItem(_List,"B1110","B1110 Electrical and electronic assemblies:• 	Electronic assemblies consisting only of metals or alloys• 	Waste electrical and electronic assemblies or scrap This entry does not include scrap from electrical power generation. (including printed circuit boards) not containing components such as accumulators and other batteries included on list A, mercury-switches, glass from cathode-ray tubes and other activated glass and PCB-capacitors, or not contaminated with Annex I constituents (e.g., cadmium, mercury, lead, polychlorinated biphenyl) or from which these have been removed, to an extent that they do not possess any of the characteristics contained in Annex III (note the related entry on list A A1180)• 	Electrical and electronic assemblies (including printed circuit boards, electronic components and wires) destined for direct reuse, Reuse can include repair, refurbishment or upgrading, but not major reassembly and not for recycling or final disposal In some countries these materials destined for direct re-use are not considered wastes.");
            ReverseAddListItem(_List,"B2010","B2010 Wastes from mining operations in non-dispersible form:• 	Natural graphite waste• 	Slate waste, whether or not roughly trimmed or merely cut, by sawing or otherwise• 	Mica waste• 	Leucite, nepheline and nepheline syenite waste• 	Feldspar waste• 	Fluorspar waste• 	Silica wastes in solid form excluding those used in foundry operations");
            ReverseAddListItem(_List,"B2020","B2020 Glass waste in non-dispersible form:• 	Cullet and other waste and scrap of glass except for glass from cathode-ray tubes and other activated glasses");
            ReverseAddListItem(_List,"B2030","B2030 Ceramic wastes in non-dispersible form:• 	Cermet wastes and scrap (metal ceramic composites)• 	Ceramic based fibres not elsewhere specified or included");
            ReverseAddListItem(_List,"B2040","B2040 Other wastes containing principally inorganic constituents:• 	Partially refined calcium sulphate produced from flue-gas desulphurization (FGD)• 	Waste gypsum wallboard or plasterboard arising from the demolition of buildings• 	Slag from copper production, chemically stabilized, having a high iron content (above 20%) and processed according to industrial specifications (e.g., DIN 4301 and DIN 8201) mainly for construction and abrasive applications•	Sulphur in solid form• 	Limestone from the production of calcium cyanamide (having a pH less than 9)• 	Sodium, potassium, calcium chlorides• 	Carborundum (silicon carbide)• 	Broken concrete• 	Lithium-tantalum and lithium-niobium containing glass scraps");
            ReverseAddListItem(_List,"B2050","B2050 Coal-fired power plant fly-ash, not included on list A (note the related entry on list A A2060)");
            ReverseAddListItem(_List,"B2060","B2060 Spent activated carbon not containing any Annex I constituents to an extent they exhibit Annex III characteristics, for example, carbon resulting from the treatment of potable water and processes of the food industry and vitamin production (note the related entry on list A, A4160)");
            ReverseAddListItem(_List,"B2070","B2070 Calcium fluoride sludge");
            ReverseAddListItem(_List,"B2080","B2080 Waste gypsum arising from chemical industry processes not included on list A (note the related entry on list A A2040)");
            ReverseAddListItem(_List,"B2090","B2090 Waste anode butts from steel or aluminium production made of petroleum coke or bitumen and cleaned to normal industry specifications (excluding anode butts from chlor alkali electrolyses and from metallurgical industry)");
            ReverseAddListItem(_List,"B2100","B2100 Waste hydrates of aluminium and waste alumina and residues from alumina production excluding such materials used for gas cleaning, flocculation or filtration processes");
            ReverseAddListItem(_List,"B2110","B2110 Bauxite residue (“red mud”) (pH moderated to less than 11.5)");
            ReverseAddListItem(_List,"B2120","B2120 Waste acidic or basic solutions with a pH greater than 2 and less than 11.5, which are not corrosive or otherwise hazardous (note the related entry on list A A4090)");
            ReverseAddListItem(_List,"B2130","B2130 Bituminous material (asphalt waste) from road construction and maintenance, not containing tar The concentration level of Benzol (a) pyrene should not be 50mg/kg or more. (note the related entry on list A, A3200)");
            ReverseAddListItem(_List,"B3010","B3010 Solid plastic waste:The following plastic or mixed plastic materials, provided they are not mixed with other wastes and are prepared to a specification:• 	Scrap plastic of non-halogenated polymers and co-polymers, including but not limited to the following It is understood that such scraps are completely polymerized.- 	ethylene- 	styrene- 	polypropylene- 	polyethylene terephthalate- 	acrylonitrile- 	butadiene- 	polyacetals- 	polyamides- 	polybutylene terephthalate- 	polycarbonates- 	polyethers- 	polyphenylene sulphides- 	acrylic polymers- 	alkanes C10-C13 (plasticiser)- 	polyurethane (not containing CFCs)- 	polysiloxanes- 	polymethyl methacrylate- 	polyvinyl alcohol- 	polyvinyl butyral- 	polyvinyl acetate• 	Cured waste resins or condensation products including the following:- 	urea formaldehyde resins- 	phenol formaldehyde resins- 	melamine formaldehyde resins- 	epoxy resins- 	alkyd resins- 	polyamides• 	The following fluorinated polymer wastes Post-consumer wastes are excluded from this entry:Wastes shall not be mixedProblems arising from open-burning practices to be considered- 	perfluoroethylene/propylene (FEP)- 	perfluoro alkoxyl alkane- tetrafluoroethylene/per fluoro vinyl ether (PFA)- tetrafluoroethylene/per fluoro methylvinyl ether (MFA)- 	polyvinylfluoride (PVF)- 	polyvinylidenefluoride (PVDF)");
            ReverseAddListItem(_List,"B3020","B3020 Paper, paperboard and paper product wastes	The following materials, provided they are not mixed with hazardous wastes: 	Waste and scrap of paper or paperboard of:• 	unbleached paper or paperboard or of corrugated paper or paperboard• 	other paper or paperboard, made mainly of bleached chemical pulp, not coloured in the mass• 	paper or paperboard made mainly of mechanical pulp (for example, newspapers, journals and similar printed matter)• 	other, including but not limited to 1) laminated paperboard 2) unsorted scrap");
            ReverseAddListItem(_List,"B3030","B3030 Textile wastes	The following materials, provided they are not mixed with other wastes and are prepared to a specification:• 	Silk waste (including cocoons unsuitable for reeling, yarn waste and garnetted stock)- 	not carded or combed- 	other• 	Waste of wool or of fine or coarse animal hair, including yarn waste but excluding garnetted stock- 	noils of wool or of fine animal hair- 	other waste of wool or of fine animal hair- 	waste of coarse animal hair• 	Cotton waste (including yarn waste and garnetted stock)- 	yarn waste (including thread waste)-	garnetted stock- 	other• 	Flax tow and waste• 	Tow and waste (including yarn waste and garnetted stock) of true hemp (Cannabis sativa L.)• 	Tow and waste (including yarn waste and garnetted stock) of jute and other textile bast fibres (excluding flax, true hemp and ramie)• 	Tow and waste (including yarn waste and garnetted stock) of sisal and other textile fibres of the genus Agave• 	Tow, noils and waste (including yarn waste and garnetted stock) of coconut• 	Tow, noils and waste (including yarn waste and garnetted stock) of abaca (Manila hemp or Musa textilis Nee)• 	Tow, noils and waste (including yarn waste and garnetted stock) of ramie and other vegetable textile fibres, not elsewhere specified or included• 	Waste (including noils, yarn waste and garnetted stock) of man-made fibres- 	of synthetic fibres- 	of artificial fibres• 	Worn clothing and other worn textile articles• 	Used rags, scrap twine, cordage, rope and cables and worn out articles of twine, cordage, rope or cables of textile materials- 	sorted- 	other");
            ReverseAddListItem(_List,"B3035","B3035 Waste textile floor coverings, carpets");
            ReverseAddListItem(_List,"B3040","B3040 Rubber wastes	The following materials, provided they are not mixed with other wastes:• 	Waste and scrap of hard rubber (e.g., ebonite)• 	Other rubber wastes (excluding such wastes specified elsewhere)");
            ReverseAddListItem(_List,"B3050","B3050 Untreated cork and wood waste:• 	Wood waste and scrap, whether or not agglomerated in logs, briquettes, pellets or similar forms• 	Cork waste: crushed, granulated or ground cork");
            ReverseAddListItem(_List,"B3060","B3060 Wastes arising from agro-food industries provided it is not infectious:• 	Wine lees• 	Dried and sterilized vegetable waste, residues and byproducts, whether or not in the form of pellets, of a kind used in animal feeding, not elsewhere specified or included• 	Degras: residues resulting from the treatment of fatty substances or animal or vegetable waxes• 	Waste of bones and horn-cores, unworked, defatted, simply prepared (but not cut to shape), treated with acid or degelatinised• 	Fish waste• 	Cocoa shells, husks, skins and other cocoa waste• 	Other wastes from the agro-food industry excluding by-products which meet national and international requirements and standards for human or animal consumption");
            ReverseAddListItem(_List,"B3065","B3065 Waste edible fats and oils of animal or vegetable origin (e.g. frying oils), provided they do not exhibit an Annex III characteristic");
            ReverseAddListItem(_List,"B3070","B3070 The following wastes: • 	Waste of human hair• 	Waste straw• 	Deactivated fungus mycelium from penicillin production to be used as animal feed");
            ReverseAddListItem(_List,"B3080","B3080 Waste parings and scrap of rubber");
            ReverseAddListItem(_List,"B3090","B3090 Paring and other wastes of leather or of composition leather not suitable for the manufacture of leather articles, excluding leather sludges, not containing hexavalent chromium compounds and biocides (note the related entry on list A A3100)");
            ReverseAddListItem(_List,"B3100","B3100 Leather dust, ash, sludges or flours not containing hexavalent chromium compounds or biocides (note the related entry on list A A3090)");
            ReverseAddListItem(_List,"B3110","B3110 Fellmongery wastes not containing hexavalent chromium compounds or biocides or infectious substances (note the related entry on list A A3110)");
            ReverseAddListItem(_List,"B3120","B3120 Wastes consisting of food dyes");
            ReverseAddListItem(_List,"B3130","B3130 Waste polymer ethers and waste non-hazardous monomer ethers incapable of forming peroxides");
            ReverseAddListItem(_List,"B3140","B3140 Waste pneumatic tyres, excluding those destined for Annex IVA operations");
            ReverseAddListItem(_List,"B4010","B4010 Wastes consisting mainly of water-based/latex paints, inks and hardened varnishes not containing organic solvents, heavy metals or biocides to an extent to render them hazardous (note the related entry on list A A4070)");
            ReverseAddListItem(_List,"B4020","B4020 Wastes from production, formulation and use of resins, latex, plasticizers, glues/adhesives, not listed on list A, free of solvents and other contaminants to an extent that they do not exhibit Annex III characteristics, e.g., water-based, or glues based on casein starch, dextrin, cellulose ethers, polyvinyl alcohols (note the related entry on list A A3050)");
            ReverseAddListItem(_List,"B4030","B4030 Used single-use cameras, with batteries not included on list A");
            ReverseAddListItem(_List,"AA010","AA010 Dross, scalings and other wastes from the manufacture of iron and steel (3)");
            ReverseAddListItem(_List,"AA060","AA060 Vanadium ashes and residues (3)");
            ReverseAddListItem(_List,"AA190","AA190 Magnesium waste and scrap that is flammable, pyrophoric or emits, upon contact with water,flammable gases in dangerous quantities");
            ReverseAddListItem(_List,"AB030","AB030 Wastes from non-cyanide based systems which arise from surface treatment of metals");
            ReverseAddListItem(_List,"AB070","AB070 Sands used in foundry operations");
            ReverseAddListItem(_List,"AB120","AB120 Inorganic halide compounds, not elsewhere specified or included");
            ReverseAddListItem(_List,"AB130","AB130 Used blasting grit");
            ReverseAddListItem(_List,"AB150","AB150 Unrefined calcium sulphite and calcium sulphate from flue gas desulphurisation (FGD)");
            ReverseAddListItem(_List,"AC060","AC060 Hydraulic fluids");
            ReverseAddListItem(_List,"AC070","AC070 Brake fluids");
            ReverseAddListItem(_List,"AC080","AC080 Antifreeze fluids");
            ReverseAddListItem(_List,"AC150","AC150 Chlorofluorocarbons");
            ReverseAddListItem(_List,"AC160","AC160 Halons");
            ReverseAddListItem(_List,"AC170","AC170 Treated cork and wood wastes");
            ReverseAddListItem(_List,"AC250","AC250 Surface active agents (surfactants)");
            ReverseAddListItem(_List,"AC260","AC260 Liquid pig manure; faeces");
            ReverseAddListItem(_List,"AC270","AC270 Sewage sludge");
            ReverseAddListItem(_List,"AD090","AD090 Wastes from production, formulation and use of reprographic and photographic chemicals and materials not elsewhere specified or included");
            ReverseAddListItem(_List,"AD100","AD100 Wastes from non-cyanide based systems which arise from surface treatment of plastics");
            ReverseAddListItem(_List,"AD120","AD120 Ion exchange resins");
            ReverseAddListItem(_List,"AD150","AD150 Naturally occurring organic material used as a filter medium (such as bio-filters)");
            ReverseAddListItem(_List,"RB020","RB020 Ceramic based fibres of physico-chemical characteristics similar to those of asbestos");
            ReverseAddListItem(_List,"GB040","GB040 Slags from precious metals and copper processing for further refining");
            ReverseAddListItem(_List,"GC010","GC010 Electrical assemblies consisting only of metals or alloys");
            ReverseAddListItem(_List,"GC020","GC020 Electronic scrap (e.g. printed circuit boards, electronic components, wire, etc.) and reclaimed electronic components suitable for base and precious metal recovery");
            ReverseAddListItem(_List,"GC030","GC030 Vessels and other floating structures for breaking up, properly emptied of any cargo and other materials arising from the operation of the vessel which may have been classified as adangerous substance or waste");
            ReverseAddListItem(_List,"GC050","GC050 Spent fluid catalytic cracking (FCC) catalysts (e.g. aluminium oxide, zeolites) ");
            ReverseAddListItem(_List,"GE020","GE020 Glass fibre waste");
            ReverseAddListItem(_List,"GF010","GF010 Ceramic wastes which have been fired after shaping, including ceramic vessels (before and/or after use)");
            ReverseAddListItem(_List,"GG030","GG030 Bottom ash and slag tap from coal fired power plants");
            ReverseAddListItem(_List,"GG040","GG040 Coal fired power plants fly ash");
            ReverseAddListItem(_List,"GH013","GH013  Polymers of vinyl chloride");
            ReverseAddListItem(_List,"GN010","GN010 Waste of pigs', hogs' or boars' bristles and hair or of badger hair and other brush making hair");
            ReverseAddListItem(_List,"GN020","GN020 Horsehair waste, whether or not put up as a layer with or without supporting material");
            ReverseAddListItem(_List,"GN030","GN030 Waste of skins and other parts of birds, with their feathers or down, of feathers and parts of feathers (whether or not with trimmed edges) and down, not further worked than cleaned,disinfected or treated for preservation");
            ReverseAddListItem(_List,"B1115","B1115 Waste metal cables coated or insulated with plastics, not included in list A1190, excluding those destined for Annex IVA operations or any other disposal operations involving, at any stage, uncontrolled thermal processes, such as open-burning.	");
            ReverseAddListItem(_List,"B1120","B1120 Spent catalysts excluding liquids used as catalysts, containing any of: Transition metals, excluding waste catalysts (spent catalysts, liquid used catalysts or other catalysts) on list A: Scandium  Vanadium Manganese Cobalt Copper Yttrium Niobium Hafnium Tungsten Titanium  Chromium Iron Nickel Zinc Zirconium Molybdenum Tantalum Rhenium  Lanthanides (rare earth metals): Lanthanum  Praseodymium Samarium  Gadolinium Dysprosium Erbium Ytterbium Cerium  Neody Europium Terbium Holmium Thulium Lutetium                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              ");
            ReverseAddListItem(_List,"B1130","B1130 Cleaned spent precious-metal-bearing catalysts																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																				");
            ReverseAddListItem(_List,"B1140","B1140 Precious-metal-bearing residues in solid form which contain traces of inorganic cyanides	");
            ReverseAddListItem(_List,"B1150","B1150 Precious metals and alloy wastes (gold, silver, the platinum group, but not mercury) in a dispersible, non-liquid form with appropriate packaging and labelling");
            ReverseAddListItem(_List,"B1160","B1160 Precious-metal ash from the incineration of printed circuit boards (note the related entry on list A A1150)");
            ReverseAddListItem(_List,"B1170","B1170 Precious-metal ash from the incineration of photographic film");
            ReverseAddListItem(_List,"B1180","B1180 Waste photographic film containing silver halides and metallic silver	");
            ReverseAddListItem(_List,"B1190","B1190 Waste photographic paper containing silver halides and metallic silver");
            ReverseAddListItem(_List,"B1200","B1200 Granulated slag arising from the manufacture of iron and steel");
            ReverseAddListItem(_List,"B1210","B1210 Slag arising from the manufacture of iron and steel including slags as a source of TiO2 and vanadium");
            ReverseAddListItem(_List,"B1220","B1220 Slag from zinc production, chemically stabilized, having a high iron content (above 20%) and processed according to industrial specifications (e.g., DIN 4301) mainly for construction");
            ReverseAddListItem(_List,"B1230","B1230 Mill scaling arising from the manufacture of iron and steel");
            ReverseAddListItem(_List,"B1240","B1240 Copper oxide mill-scale");
            ReverseAddListItem(_List,"B1250","B1250 Waste end-of-life motor vehicles, containing neither liquids nor other hazardous components");

        }

        public static void AddDCodeList(ListItemCollection _List, Boolean ClearListFirst, Boolean IncludeDummy)
        {
            if (ClearListFirst)
            {
                _List.Clear();
            }
            if (IncludeDummy) { ReverseAddListItem(_List, "", "-nvt-"); }
            ReverseAddListItem(_List,"D1", "D1 Storten op of in de bodem (bijvoorbeeld op een vuilstortplaats enzovoorts).");
            ReverseAddListItem(_List,"D2", "D2 Uitrijden (bijvoorbeeld biodegradatie van vloeibaar of slibachtig afval in de bodem enzovoorts).");
            ReverseAddListItem(_List,"D3", "D3 Injectie in de diepe ondergrond (bijvoorbeeld injectie van verpompbare afvalstoffen in putten, zoutkoepels of natuurlijk gevormde holten enzovoorts).");
            ReverseAddListItem(_List,"D4", "D4 Opslag in waterbekkens (bijvoorbeeld het lozen van vloeibaar of slibachtig afval in putten, vijvers of lagunes, enzovoorts).");
            ReverseAddListItem(_List,"D5", "D5 Verwijderen op speciaal ingerichte locaties (bijvoorbeeld in afzonderlijke beklede, afgedekte cellen die onderling en van de omgeving afgeschermd zijn, enzovoorts).");
            ReverseAddListItem(_List,"D6", "D6 Lozen in wateren, behalve zeeën en oceanen.");
            ReverseAddListItem(_List,"D7", "D7 Verwijderen in zeeën en oceanen, inclusief inbrengen in de bodem.");
            ReverseAddListItem(_List,"D8", "D8 Biologische behandeling op een niet elders in deze bijlage aangegeven wijze waardoor verbindingen of mengsels ontstaan die worden verwijderd op een van de onder D1 tot en met D12 vermelde methodes.");
            ReverseAddListItem(_List,"D9", "D9 Fysisch-chemische behandeling op een niet elders in deze bijlage aangegeven wijze, waardoor verbindingen of mengsels ontstaan die worden verwijderd op een van de onder D1 tot en met D12 vermelde methodes (bijvoorbeeld verdampen, drogen, calcineren, enzovoorts).");
            ReverseAddListItem(_List,"D10", "D10 Verbranding op het land.");
            ReverseAddListItem(_List,"D11", "D11 Verbranding op zee.");
            ReverseAddListItem(_List,"D12", "D12 Permanente opslag (bijvoorbeeld plaatsen van houders in mijnen, enzovoorts).");
            ReverseAddListItem(_List,"D13", "D13 Vermengen vóór een van de onder D1 tot en met D12 vermelde behandelingen.");
            ReverseAddListItem(_List,"D14", "D14 Herverpakken vóór een van de onder D1 tot en met D13 vermelde behandelingen.");
            ReverseAddListItem(_List,"D15", "D15 Opslag in afwachting van een van de onder D1 tot en met D14 vermelde behandelingen (met uitsluiting van voorlopige opslag voorafgaande aan inzameling op de plaats van productie).");

        }

        public static void AddRCodeList(ListItemCollection _List, Boolean ClearListFirst, Boolean IncludeDummy)
        {
            if (ClearListFirst)
            {
                _List.Clear();
            }

            if (IncludeDummy) { ReverseAddListItem(_List, "", "-nvt-"); }
            ReverseAddListItem(_List, "R1", "R1 Hoofdgebruik als brandstof of een andere wijze van energieopwekking.");
            ReverseAddListItem(_List,"R2", "R2 Terugwinning van oplosmiddelen.");
            ReverseAddListItem(_List,"R3", "R3 Recycling/terugwinning van organische stoffen die niet als oplosmiddel worden gebruikt (met inbegrip van compostbemesting en bemesting met andere biologisch omgezette stoffen).");
            ReverseAddListItem(_List,"R4", "R4 Recycling/terugwinning van metalen en metaalverbindingen.");
            ReverseAddListItem(_List,"R5", "R5 Recycling/terugwinning van andere anorganische stoffen.");
            ReverseAddListItem(_List,"R6", "R6 Terugwinning van zuren of basen.");
            ReverseAddListItem(_List,"R7", "R7 Terugwinning van bestanddelen die worden gebruikt om vervuiling tegen te gaan.");
            ReverseAddListItem(_List,"R8", "R8 Terugwinning van bestanddelen uit katalysatoren.");
            ReverseAddListItem(_List,"R9", "R9 Herraffinage van olie en ander hergebruik van olie.");
            ReverseAddListItem(_List,"R10", "R10 Uitrijden voor landbouwkundige of ecologische verbetering.");
            ReverseAddListItem(_List,"R11", "R11 Gebruik van afvalstoffen die bij een van de onder R1 tot en met R10 genoemde behandelingen vrijkomen.");
            ReverseAddListItem(_List,"R12", "R12 Uitwisseling van afvalstoffen voor een van de onder R1 tot en met R11 genoemde behandelingen.");
            ReverseAddListItem(_List,"R13", "R13 Opslag van afvalstoffen bestemd voor een van de onder R1 tot en met R12 genoemde behandelingen (met uitsluiting van voorlopige opslag voorafgaande aan inzameling op de plaats van productie).");

        }
        #endregion

        public static PropertyInfo GetPropertyFromObject(object Data, string PropertyName)
        {
            PropertyInfo ToReturn = null;
            PropertyName = PropertyName.ToUpper();
            PropertyInfo[] TempPropData = Data.GetType().GetProperties();
            foreach (PropertyInfo pd in TempPropData)
            {
                if (pd.Name.ToUpper() == PropertyName)
                {
                    ToReturn = pd;
                    break;
                }
            }
            return ToReturn;
        }

        public static byte[] ConvertImageToByteArray(System.Drawing.Image imageToConvert,
                                              System.Drawing.Imaging.ImageFormat formatOfImage)
        {
            byte[] Ret;
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    imageToConvert.Save(ms, formatOfImage);
                    Ret = ms.ToArray();
                }
            }
            catch (Exception) { throw; }
            return Ret;
        }

        public static string DateTimeToString(DateTime x)
        {
            return x.Year + "," + x.Month + "," + x.Day + "," + x.Hour + "," + x.Minute + "," + x.Second;
        }

        public static DateTime StringToDateTime(string s)
        {
            if (s.Trim() != "")
            {
                string[] d = s.Split(',');
                return new DateTime(Convert.ToInt32(d[0]), Convert.ToInt32(d[1]), Convert.ToInt32(d[2]), Convert.ToInt32(d[3]), Convert.ToInt32(d[4]), Convert.ToInt32(d[5]));
            }
            else
            {
                return new DateTime(2100, 1, 1);
            }
        }

        public static string DateToString(DateTime x)
        {
            return x.Year + "," + x.Month + "," + x.Day;
        }

        public static DateTime StringToDate(string s)
        {
            if (s.Trim() != "")
            {
                string[] d = s.Split(',');
                return new DateTime(Convert.ToInt32(d[0]), Convert.ToInt32(d[1]), Convert.ToInt32(d[2]));
            }
            else
            {
                return new DateTime(2100, 1, 1);
            }
        }
    }
}