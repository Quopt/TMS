using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using System.ServiceModel.Activation;
using System.Data.SqlClient;
using System.Data.Objects;
using System.Data.Metadata.Edm;
using System.Web.SessionState;

namespace TMS_Recycling
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class TMSService : DataService<ModelTMSContainer>
    {
        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config)
        {
            // TODO: set rules to indicate which entity sets and service operations are visible, updatable, etc.
            // Examples:
            // config.SetEntitySetAccessRule("MyEntityset", EntitySetRights.AllRead);
            // config.SetServiceOperationAccessRule("MyServiceOperation", ServiceOperationRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
            config.DataServiceBehavior.AcceptProjectionRequests = true;
            config.DataServiceBehavior.AcceptCountRequests = true;

            // paging at the specified amount of records
            config.MaxResultsPerCollection = 100;

            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            config.SetServiceOperationAccessRule("*", ServiceOperationRights.All);

            config.UseVerboseErrors = true;
        }

        public TMSService()
        {
        }

        protected override void OnStartProcessingRequest(ProcessRequestArgs args)
        {
            // set the current user name
            //base.CurrentDataSource.ExecuteStoreCommand("SET CONTEXT_INFO [@ID]",
            //    new SqlParameter("@ID", args.RequestUri.UserInfo));
            string CurrentUser = HttpContext.Current.User.Identity.Name;

            // check database
            CheckDatabase(HttpContext.Current.Session);

            // call inherited
            base.OnStartProcessingRequest(args);
        }

        public static void CheckDatabase(HttpSessionState Session)
        {
            
            ModelTMSContainer ThisContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);

            if (!ThisContext.DatabaseExists())
            {
                ThisContext.CreateDatabase();
                //System.Data.Metadata.Edm.
            }

            // check database version
            CheckDatabaseVersion(ThisContext);

            // database fillings have to be checked client side
        }

        public static SystemSetting DBVersion(ModelTMSContainer Context)
        {
            SystemSetting TempSetting;

            ObjectQuery<SystemSetting> TempQry = Context.SystemSettingSet.Where("it.Description = 'DBVERSION'");
            if (TempQry.Count() == 1)
            {
                TempSetting = TempQry.First();
            }
            else
            {
                TempSetting = new SystemSetting();
                Context.SystemSettingSet.AddObject(TempSetting);
                TempSetting.Description = "DBVERSION";
                TempSetting.Value = "0";
                Context.SaveChanges();
            }

            return TempSetting; 
        }

        public static int DBVersionNr(ModelTMSContainer Context)
        {
            return System.Convert.ToInt32(DBVersion(Context).Value);
        }

        public static void SetDBVersion(ModelTMSContainer Context, int NewVersion)
        {
            SystemSetting TempSetting = DBVersion (Context);

            TempSetting.Value = NewVersion.ToString();
        }

        public static void CheckDatabaseVersion(ModelTMSContainer Context)
        {
            // check the database version
            int DBVersion = DBVersionNr(Context);
            if (DBVersion == 0)
            {
                SetDBVersion(Context, 1);
                DBVersion = 1;
            }

            if (DBVersion == 1)
            {
                MigrateDBFromVersion1To2(Context);

                SetDBVersion(Context, 2);
                DBVersion = 2;
            }

            Context.SaveChanges(SaveOptions.DetectChangesBeforeSave);
            //MigrateDBAuto(Context);
        }

        private static void MigrateDBAuto(ModelTMSContainer Context)
            // under construction!!!
        {
            string Test = "";
            System.Data.Metadata.Edm.MetadataWorkspace CheckDB = Context.MetadataWorkspace;
            System.Data.Metadata.Edm.ItemCollection EntColl = CheckDB.GetItemCollection(System.Data.Metadata.Edm.DataSpace.SSpace);
            foreach (EntityType Ent in EntColl.OfType<EntityType>())
            {
                // check if all properties are present
                foreach (EdmProperty Mem in Ent.Members.OfType<EdmProperty>())
                {
                    Test = Test + Ent.Name + " - " + Mem.Name + "\r\n";

                }
            }

            System.Data.Metadata.Edm.ItemCollection KeyColl = CheckDB.GetItemCollection(System.Data.Metadata.Edm.DataSpace.CSpace);
            foreach (EntityType Ent in KeyColl.OfType<EntityType>())
            {
                // check if all properties are present
                foreach (NavigationProperty Nav in Ent.Members.OfType<NavigationProperty>())
                {
                    Test = Test + Ent.Name + " - " + Nav.Name + "\r\n";

                }
            }
        }

        private static void AddDefaultIndexes(ModelTMSContainer Context, string SetName)
        {
            Context.ExecuteStoreCommand("Create index IX_" + SetName + "Description on " + SetName + " (Description)");
            Context.ExecuteStoreCommand("Create index IX_" + SetName + "CreateDateTime on " + SetName + " (CreateDateTime)");
            Context.ExecuteStoreCommand("Create index IX_" + SetName + "ModifyDateTime on " + SetName + " (ModifyDateTime)");
            Context.ExecuteStoreCommand("Create index IX_" + SetName + "CreateUser on " + SetName + " (CreateUser)");
            Context.ExecuteStoreCommand("Create index IX_" + SetName + "ModifyUser on " + SetName + " (ModifyUser)");

            // these fields may or may not exits on all sets
            try { Context.ExecuteStoreCommand("Create index IX_" + SetName + "IsActive on " + SetName + " (IsActive)"); } catch { }

        }

        private static void AddIndex(ModelTMSContainer Context, string SetName, string FieldName)
        {
            Context.ExecuteStoreCommand("Create index IX_" + SetName + FieldName + " on [" + SetName + "] ([" + FieldName + "])");
        }

        private static void MigrateDBFromVersion1To2(ModelTMSContainer Context)
        {
            Context.ExecuteStoreCommand("EXEC [sp_fulltext_database] @action = 'enable'");

            // add indexes to all fields
            AddDefaultIndexes(Context, "FreightSet");
            AddDefaultIndexes(Context, "FreightSortingMaterialSet");
            AddDefaultIndexes(Context, "FreightWeighingMaterialSet");
            AddDefaultIndexes(Context, "FreightWeighingSet");
            AddDefaultIndexes(Context, "InvoiceLineSet");
            AddDefaultIndexes(Context, "InvoiceSet");
            AddDefaultIndexes(Context, "LedgerBookingCodeSet");
            AddDefaultIndexes(Context, "LedgerCheckSet");
            AddDefaultIndexes(Context, "LedgerClosureSet");
            AddDefaultIndexes(Context, "LedgerMutationSet");
            AddDefaultIndexes(Context, "LedgerSet");
            AddDefaultIndexes(Context, "LocationSet");
            AddDefaultIndexes(Context, "MaterialClosureSet");
            AddDefaultIndexes(Context, "MaterialMutationSet");
            AddDefaultIndexes(Context, "MaterialSet");
            AddDefaultIndexes(Context, "MaterialUnitSet");
            AddDefaultIndexes(Context, "OrderLineSet");
            AddDefaultIndexes(Context, "OrderSet");
            AddDefaultIndexes(Context, "RelationAddressSet");
            AddDefaultIndexes(Context, "RelationAdvancePaymentSet");
            AddDefaultIndexes(Context, "RelationContactLogSet");
            AddDefaultIndexes(Context, "RelationContactSet");
            AddDefaultIndexes(Context, "RelationContractMaterialSet");
            AddDefaultIndexes(Context, "RelationContractSet");
            AddDefaultIndexes(Context, "RelationLocationSet");
            AddDefaultIndexes(Context, "RelationMaterialSet");
            AddDefaultIndexes(Context, "RelationPriceAgreementSet");
            AddDefaultIndexes(Context, "RelationProjectSet");
            AddDefaultIndexes(Context, "RelationSet");
            AddDefaultIndexes(Context, "RelationWorkSet");
            AddDefaultIndexes(Context, "RentLedgerSet");
            AddDefaultIndexes(Context, "RentalItemActivitySet");
            AddDefaultIndexes(Context, "RentalItemSet");
            AddDefaultIndexes(Context, "RentalTypeSet");
            AddDefaultIndexes(Context, "RentalTypeVATSet");
            AddDefaultIndexes(Context, "SecurityRoleObjectAccessSet");
            AddDefaultIndexes(Context, "SecurityRoleSet");
            AddDefaultIndexes(Context, "StaffMemberAdvancePaymentSet");
            AddDefaultIndexes(Context, "StaffMemberPaymentDeclarationSet");
            AddDefaultIndexes(Context, "StaffMemberPaymentSet");
            AddDefaultIndexes(Context, "StaffMemberSet");
            AddDefaultIndexes(Context, "StaffMemberTimeRegistrationSet");
            AddDefaultIndexes(Context, "StaffTimeRegistrationActivitySet");
            AddDefaultIndexes(Context, "SystemSettingSet");
            AddDefaultIndexes(Context, "TruckSet");

            AddIndex(Context, "RentalTypeRentalType", "IsAlternativeRentalTypeFor_Id");
            AddIndex(Context, "RentalTypeRentalType", "AlternativeRentalTypes_Id");

            AddIndex(Context, "FreightSet", "YourReference");
            AddIndex(Context, "FreightSet", "FreightDateTime");
            AddIndex(Context, "FreightSet", "RequestedFreightEndDateTime");
            AddIndex(Context, "FreightSet", "RequestedFreightStartDateTime");
            AddIndex(Context, "FreightSet", "FreightStatus");
            AddIndex(Context, "FreightSet", "FreightType");
            AddIndex(Context, "FreightSet", "OurDriverID");
            AddIndex(Context, "FreightSet", "YourTruckPlate");
            AddIndex(Context, "FreightSet", "YourDriverName");
            AddIndex(Context, "FreightSet", "FreightDirection");
            AddIndex(Context, "FreightSet", "OurReference");

            AddIndex(Context, "FreightWeighingSet", "WeighingDateTime1");
            AddIndex(Context, "FreightWeighingSet", "WeighingDateTime2");
            AddIndex(Context, "FreightWeighingSet", "WeighingNumber");
            AddIndex(Context, "FreightWeighingSet", "Key1");
            AddIndex(Context, "FreightWeighingSet", "Key2");
            AddIndex(Context, "FreightWeighingSet", "IsDriverInTruck");

            AddIndex(Context, "InvoiceLineSet", "LineNumber");

            AddIndex(Context, "InvoiceSet", "InvoiceNumber");
            AddIndex(Context, "InvoiceSet", "InvoiceStatus");
            AddIndex(Context, "InvoiceSet", "InvoiceType");
            AddIndex(Context, "InvoiceSet", "BookingDateTime");
            AddIndex(Context, "InvoiceSet", "YourReference");
            AddIndex(Context, "InvoiceSet", "IsCorrected");
            AddIndex(Context, "InvoiceSet", "GroupCode");
            AddIndex(Context, "InvoiceSet", "AlreadyPaid");
            AddIndex(Context, "InvoiceSet", "InvoiceSubType");
            AddIndex(Context, "InvoiceSet", "DiscountPercentage");
            AddIndex(Context, "InvoiceSet", "Price");

            AddIndex(Context, "LedgerBookingCodeSet", "IsDebugLedgerCode");

            AddIndex(Context, "LedgerCheckSet", "CorrectionAmount");
            AddIndex(Context, "LedgerCheckSet", "IsLedgerCorrected");
            AddIndex(Context, "LedgerCheckSet", "CheckDate");

            AddIndex(Context, "LedgerClosureSet", "ClosureDate");
            AddIndex(Context, "LedgerClosureSet", "IsCorrection");
            AddIndex(Context, "LedgerClosureSet", "LedgerDelta");

            AddIndex(Context, "LedgerMutationSet", "IsEditable");
            AddIndex(Context, "LedgerMutationSet", "BookingDateTime");
            AddIndex(Context, "LedgerMutationSet", "BookingType");
            AddIndex(Context, "LedgerMutationSet", "IsCorrection");
            AddIndex(Context, "LedgerMutationSet", "GroupCode");

            AddIndex(Context, "LedgerSet", "LedgerCurrency");
            AddIndex(Context, "LedgerSet", "LedgerType");
            AddIndex(Context, "LedgerSet", "IsDebugLedger");

            AddIndex(Context, "LocationSet", "ZIPcode");
            AddIndex(Context, "LocationSet", "City");
            AddIndex(Context, "LocationSet", "Country");
            AddIndex(Context, "LocationSet", "VATNumber");
            AddIndex(Context, "LocationSet", "EMail");
            AddIndex(Context, "LocationSet", "PreferredCurrency");
            AddIndex(Context, "LocationSet", "LocationNumber");
            AddIndex(Context, "LocationSet", "VIHBCode");

            AddIndex(Context, "MaterialClosureSet", "ClosureDateTime");
            AddIndex(Context, "MaterialClosureSet", "IsCorrected");
            AddIndex(Context, "MaterialClosureSet", "MaterialStockLevel");
            AddIndex(Context, "MaterialClosureSet", "MaterialTotalBought");
            AddIndex(Context, "MaterialClosureSet", "MaterialTotalSold");
            AddIndex(Context, "MaterialClosureSet", "MaterialTotalBoughtPrice");
            AddIndex(Context, "MaterialClosureSet", "MaterialTotalSoldPrice");
            AddIndex(Context, "MaterialClosureSet", "MaterialStockPrice");
            AddIndex(Context, "MaterialClosureSet", "MaterialTotalBoughtDay");
            AddIndex(Context, "MaterialClosureSet", "MaterialTotalSoldDay");
            AddIndex(Context, "MaterialClosureSet", "MaterialTotalBoughtPriceDay");
            AddIndex(Context, "MaterialClosureSet", "MaterialTotalSoldPriceDay");

            AddIndex(Context, "MaterialMutationSet", "MutationDateTime");
            AddIndex(Context, "MaterialMutationSet", "Amount");
            AddIndex(Context, "MaterialMutationSet", "TotalPrice");
            AddIndex(Context, "MaterialMutationSet", "MutationType");
            AddIndex(Context, "MaterialMutationSet", "AmountInKg");
            AddIndex(Context, "MaterialMutationSet", "IsCorrection");
            AddIndex(Context, "MaterialMutationSet", "GroupCode");
            AddIndex(Context, "MaterialMutationSet", "PricePerUnit");
            AddIndex(Context, "MaterialMutationSet", "MutationNumber");

            AddIndex(Context, "MaterialSet", "MaterialNumber");
            AddIndex(Context, "MaterialSet", "Category");
            AddIndex(Context, "MaterialSet", "Group");
            AddIndex(Context, "MaterialSet", "StorageCode");
            AddIndex(Context, "MaterialSet", "LMECode");
            AddIndex(Context, "MaterialSet", "InvoiceType");
            AddIndex(Context, "MaterialSet", "MaterialId");
            AddIndex(Context, "MaterialSet", "HCode");
            AddIndex(Context, "MaterialSet", "BaselCode");
            AddIndex(Context, "MaterialSet", "PhysicalShape");

            AddIndex(Context, "MaterialUnitSet", "StockUnit");

            AddIndex(Context, "OrderSet", "OrderNumber");
            AddIndex(Context, "OrderSet", "OrderType");
            AddIndex(Context, "OrderSet", "BookingDateTime");
            AddIndex(Context, "OrderSet", "OrderStatus");
            AddIndex(Context, "OrderSet", "YourTruckPlate");
            AddIndex(Context, "OrderSet", "YourDriverName");
            AddIndex(Context, "OrderSet", "DeterminePriceDuringInvoicing");
            AddIndex(Context, "OrderSet", "IsCorrected");
            AddIndex(Context, "OrderSet", "GroupCode");

            AddIndex(Context, "RelationAddressSet", "AdressType");
            AddIndex(Context, "RelationAddressSet", "ZIPcode");
            AddIndex(Context, "RelationAddressSet", "City");
            AddIndex(Context, "RelationAddressSet", "Country");

            AddIndex(Context, "RelationAdvancePaymentSet", "IsPaidBack");
            AddIndex(Context, "RelationAdvancePaymentSet", "IsPaidOut");
            AddIndex(Context, "RelationAdvancePaymentSet", "PaymentType");
            AddIndex(Context, "RelationAdvancePaymentSet", "PaymentDateTime");

            AddIndex(Context, "RelationContactLogSet", "ContactDateTime");
            AddIndex(Context, "RelationContactLogSet", "ContactType");
            AddIndex(Context, "RelationContactLogSet", "FollowUpDateTime");
            AddIndex(Context, "RelationContactLogSet", "FollowUpState");
            AddIndex(Context, "RelationContactLogSet", "PausedUntilDateTime");
            AddIndex(Context, "RelationContactLogSet", "HandledBy");
            AddIndex(Context, "RelationContactLogSet", "Handler");

            AddIndex(Context, "RelationContactSet", "RelationType");
            AddIndex(Context, "RelationContactSet", "PhoneNumber");
            AddIndex(Context, "RelationContactSet", "MobilePhone");
            AddIndex(Context, "RelationContactSet", "HomePhone");
            AddIndex(Context, "RelationContactSet", "PrivateMobilePhone");
            AddIndex(Context, "RelationContactSet", "EMail");
            AddIndex(Context, "RelationContactSet", "PrivateEMail");

            AddIndex(Context, "RelationContractMaterialSet", "AvgRequiredProfitPerUnit");
            AddIndex(Context, "RelationContractMaterialSet", "PricePerUnit");
            AddIndex(Context, "RelationContractMaterialSet", "MinAmount");
            AddIndex(Context, "RelationContractMaterialSet", "MaxAmount");
            AddIndex(Context, "RelationContractMaterialSet", "AvgStockPrice");
            AddIndex(Context, "RelationContractMaterialSet", "AvgStockUnits");
            AddIndex(Context, "RelationContractMaterialSet", "DeliveredAmount");
            AddIndex(Context, "RelationContractMaterialSet", "DeliveredTotalPrice");

            AddIndex(Context, "RelationContractSet", "YourReference");
            AddIndex(Context, "RelationContractSet", "OurReference");
            AddIndex(Context, "RelationContractSet", "ContractDate");
            AddIndex(Context, "RelationContractSet", "ContractStartDate");
            AddIndex(Context, "RelationContractSet", "ContractEndDate");
            AddIndex(Context, "RelationContractSet", "ContractType");
            AddIndex(Context, "RelationContractSet", "HasContractGuidance");
            AddIndex(Context, "RelationContractSet", "ContractPriority");
            AddIndex(Context, "RelationContractSet", "ContractStatus");

            AddIndex(Context, "RelationMaterialSet", "LMECode");

            AddIndex(Context, "RelationPriceAgreementSet", "StartDateTime");
            AddIndex(Context, "RelationPriceAgreementSet", "EndDateTime");
            AddIndex(Context, "RelationPriceAgreementSet", "AgreementType");
            AddIndex(Context, "RelationPriceAgreementSet", "PricePerUnit");

            AddIndex(Context, "RelationProjectSet", "ProjectNumber");

            AddIndex(Context, "RelationSet", "VATNumber");
            AddIndex(Context, "RelationSet", "PhoneNumber");
            AddIndex(Context, "RelationSet", "EMail");
            AddIndex(Context, "RelationSet", "YourReference");
            AddIndex(Context, "RelationSet", "CustomerNumber");
            AddIndex(Context, "RelationSet", "CustomerType");
            AddIndex(Context, "RelationSet", "IsSystemUser");
            AddIndex(Context, "RelationSet", "Country");

            AddIndex(Context, "RelationWorkSet", "AmountPaidBack");
            AddIndex(Context, "RelationWorkSet", "AgreementDateTime");
            AddIndex(Context, "RelationWorkSet", "IsVATApplicable");
            AddIndex(Context, "RelationWorkSet", "IsTreatedAsAdvancePayment");
            AddIndex(Context, "RelationWorkSet", "WorkType");
            AddIndex(Context, "RelationWorkSet", "AmountEXVat");
            AddIndex(Context, "RelationWorkSet", "VATAmount");
            AddIndex(Context, "RelationWorkSet", "TotalAmount");
            AddIndex(Context, "RelationWorkSet", "VATPercentage");

            AddIndex(Context, "RentLedgerSet", "InitialRentStartDateTime");
            AddIndex(Context, "RentLedgerSet", "InitialRentEndStartDateTime");
            AddIndex(Context, "RentLedgerSet", "VATRentPrice");
            AddIndex(Context, "RentLedgerSet", "TotalRentPrice");
            AddIndex(Context, "RentLedgerSet", "BaseRentPrice");
            AddIndex(Context, "RentLedgerSet", "RentNumber");

            AddIndex(Context, "RentalItemActivitySet", "IsTreatedAsAdvancePayment");
            AddIndex(Context, "RentalItemActivitySet", "RentStartDateTime");
            AddIndex(Context, "RentalItemActivitySet", "RentEndStartDateTime");
            AddIndex(Context, "RentalItemActivitySet", "CalculatedRentPrice");
            AddIndex(Context, "RentalItemActivitySet", "DiscountPercentage");
            AddIndex(Context, "RentalItemActivitySet", "BaseRentPrice");
            AddIndex(Context, "RentalItemActivitySet", "VATRentPrice");
            AddIndex(Context, "RentalItemActivitySet", "TotalRentPrice");
            AddIndex(Context, "RentalItemActivitySet", "InvoiceStatus");

            AddIndex(Context, "RentalItemSet", "ItemState");
            AddIndex(Context, "RentalItemSet", "ItemNumber");
            AddIndex(Context, "RentalItemSet", "BailPrice");
            AddIndex(Context, "RentalItemSet", "BaseRentalPrice");
            AddIndex(Context, "RentalItemSet", "RentPerDay");
            AddIndex(Context, "RentalItemSet", "RentPerWeek");
            AddIndex(Context, "RentalItemSet", "RentPerMonth");

            AddIndex(Context, "RentalTypeVATSet", "VATPercentage");

            AddIndex(Context, "SecurityRoleObjectAccessSet", "ObjectName");
            AddIndex(Context, "SecurityRoleObjectAccessSet", "HasCreateAccess");
            AddIndex(Context, "SecurityRoleObjectAccessSet", "HasReadAccess");
            AddIndex(Context, "SecurityRoleObjectAccessSet", "HasUpdateAccess");
            AddIndex(Context, "SecurityRoleObjectAccessSet", "HasDeleteAccess");
            AddIndex(Context, "SecurityRoleObjectAccessSet", "HasExecuteAccess");
            AddIndex(Context, "SecurityRoleObjectAccessSet", "SettableAccessTypes");

            AddIndex(Context, "SecurityRoleSet", "IsRoleTemplate");
            AddIndex(Context, "SecurityRoleSet", "HasUnlimitedAccess");

            AddIndex(Context, "StaffMemberAdvancePaymentSet", "AdvancePaymentNumber");
            AddIndex(Context, "StaffMemberAdvancePaymentSet", "Amount");
            AddIndex(Context, "StaffMemberAdvancePaymentSet", "PaymentDate");
            AddIndex(Context, "StaffMemberAdvancePaymentSet", "AlreadyPaidBackAmount");
            AddIndex(Context, "StaffMemberAdvancePaymentSet", "IsPaidBack");

            AddIndex(Context, "StaffMemberPaymentDeclarationSet", "DeclarationNumber");
            AddIndex(Context, "StaffMemberPaymentDeclarationSet", "IsCheckedOK");
            AddIndex(Context, "StaffMemberPaymentDeclarationSet", "DeclarationDateTime");

            AddIndex(Context, "StaffMemberPaymentSet", "PaymentDate");
            AddIndex(Context, "StaffMemberPaymentSet", "StartPaymentPeriod");
            AddIndex(Context, "StaffMemberPaymentSet", "EndPaymentPeriod");
            AddIndex(Context, "StaffMemberPaymentSet", "TotalPaymentAmount");
            AddIndex(Context, "StaffMemberPaymentSet", "DeductedAdvancePayments");
            AddIndex(Context, "StaffMemberPaymentSet", "PaymentAmountTime");
            AddIndex(Context, "StaffMemberPaymentSet", "PaymentAmountDeclarations");

            AddIndex(Context, "StaffMemberSecurityRole", "StaffMember_Id");
            AddIndex(Context, "StaffMemberSecurityRole", "SecurityRole_Id");

            AddIndex(Context, "StaffMemberSet", "IsDriver");
            AddIndex(Context, "StaffMemberSet", "ContractHoursPerWeek");
            AddIndex(Context, "StaffMemberSet", "SocialSecurityNumber");
            AddIndex(Context, "StaffMemberSet", "InServiceDate");
            AddIndex(Context, "StaffMemberSet", "OutOfServiceDate");
            AddIndex(Context, "StaffMemberSet", "StaffMemberNumber");
            AddIndex(Context, "StaffMemberSet", "IDNumber");
            AddIndex(Context, "StaffMemberSet", "IDExpirationDate");
            AddIndex(Context, "StaffMemberSet", "IDNationality");
            AddIndex(Context, "StaffMemberSet", "IDType");
            AddIndex(Context, "StaffMemberSet", "NetHourlyRate");
            AddIndex(Context, "StaffMemberSet", "HasVMSAccount");
            AddIndex(Context, "StaffMemberSet", "Password");
            AddIndex(Context, "StaffMemberSet", "AccountName");

            AddIndex(Context, "StaffMemberTimeRegistrationSet", "RegistrationDay");
            AddIndex(Context, "StaffMemberTimeRegistrationSet", "Hours");
            AddIndex(Context, "StaffMemberTimeRegistrationSet", "BaseHourlyRate");
            AddIndex(Context, "StaffMemberTimeRegistrationSet", "TotalHoursPayment");
            AddIndex(Context, "StaffMemberTimeRegistrationSet", "IsCheckedOK");

            AddIndex(Context, "StaffTimeRegistrationActivitySet", "AdditionalPayPercentage");
            AddIndex(Context, "StaffTimeRegistrationActivitySet", "HasToBePaid");
            AddIndex(Context, "StaffTimeRegistrationActivitySet", "IsStandardActivity");

            AddIndex(Context, "TruckSet", "TruckPlate");

        }
    }

}
