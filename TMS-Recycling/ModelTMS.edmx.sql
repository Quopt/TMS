
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 05/03/2011 09:07:04
-- Generated from EDMX file: D:\Mijn documenten\Quantitative Options\Software projects\TMS\TMS-Recycling\ModelTMS.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [TMS2];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_RelationRelationAdress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RelationAddressSet] DROP CONSTRAINT [FK_RelationRelationAdress];
GO
IF OBJECT_ID(N'[dbo].[FK_RelationRelationLocation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RelationLocationSet] DROP CONSTRAINT [FK_RelationRelationLocation];
GO
IF OBJECT_ID(N'[dbo].[FK_RelationAdressRelationLocation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RelationLocationSet] DROP CONSTRAINT [FK_RelationAdressRelationLocation];
GO
IF OBJECT_ID(N'[dbo].[FK_RelationRelationProject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RelationProjectSet] DROP CONSTRAINT [FK_RelationRelationProject];
GO
IF OBJECT_ID(N'[dbo].[FK_RelationRelationAdvancePayment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RelationAdvancePaymentSet] DROP CONSTRAINT [FK_RelationRelationAdvancePayment];
GO
IF OBJECT_ID(N'[dbo].[FK_RelationRelationPriceAgreement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RelationPriceAgreementSet] DROP CONSTRAINT [FK_RelationRelationPriceAgreement];
GO
IF OBJECT_ID(N'[dbo].[FK_RelationRelationWork]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RelationWorkSet] DROP CONSTRAINT [FK_RelationRelationWork];
GO
IF OBJECT_ID(N'[dbo].[FK_RelationRelationContract]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RelationContractSet] DROP CONSTRAINT [FK_RelationRelationContract];
GO
IF OBJECT_ID(N'[dbo].[FK_RelationContractRelationContractMaterial]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RelationContractMaterialSet] DROP CONSTRAINT [FK_RelationContractRelationContractMaterial];
GO
IF OBJECT_ID(N'[dbo].[FK_FreightFreightSortingMaterial]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FreightSortingMaterialSet] DROP CONSTRAINT [FK_FreightFreightSortingMaterial];
GO
IF OBJECT_ID(N'[dbo].[FK_TruckFreight]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FreightSet] DROP CONSTRAINT [FK_TruckFreight];
GO
IF OBJECT_ID(N'[dbo].[FK_FreightFreightWeighing]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FreightWeighingSet] DROP CONSTRAINT [FK_FreightFreightWeighing];
GO
IF OBJECT_ID(N'[dbo].[FK_FreightWeighingFreightWeigingMaterial]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FreightWeighingMaterialSet] DROP CONSTRAINT [FK_FreightWeighingFreightWeigingMaterial];
GO
IF OBJECT_ID(N'[dbo].[FK_RelationRelationContact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RelationContactSet] DROP CONSTRAINT [FK_RelationRelationContact];
GO
IF OBJECT_ID(N'[dbo].[FK_RelationContactRelationContactLog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RelationContactLogSet] DROP CONSTRAINT [FK_RelationContactRelationContactLog];
GO
IF OBJECT_ID(N'[dbo].[FK_LedgerLedgerClosure]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LedgerClosureSet] DROP CONSTRAINT [FK_LedgerLedgerClosure];
GO
IF OBJECT_ID(N'[dbo].[FK_LedgerLedgerChecks]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LedgerCheckSet] DROP CONSTRAINT [FK_LedgerLedgerChecks];
GO
IF OBJECT_ID(N'[dbo].[FK_LedgerChecksLedgerBookingCode]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LedgerCheckSet] DROP CONSTRAINT [FK_LedgerChecksLedgerBookingCode];
GO
IF OBJECT_ID(N'[dbo].[FK_LedgerLedgerMutation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LedgerMutationSet] DROP CONSTRAINT [FK_LedgerLedgerMutation];
GO
IF OBJECT_ID(N'[dbo].[FK_LedgerBookingCodeLedgerMutation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LedgerMutationSet] DROP CONSTRAINT [FK_LedgerBookingCodeLedgerMutation];
GO
IF OBJECT_ID(N'[dbo].[FK_LedgerMutationRelation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LedgerMutationSet] DROP CONSTRAINT [FK_LedgerMutationRelation];
GO
IF OBJECT_ID(N'[dbo].[FK_RelationWorkLedgerBookingCode]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RelationWorkSet] DROP CONSTRAINT [FK_RelationWorkLedgerBookingCode];
GO
IF OBJECT_ID(N'[dbo].[FK_RelationAdvancePaymentLedger]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RelationAdvancePaymentSet] DROP CONSTRAINT [FK_RelationAdvancePaymentLedger];
GO
IF OBJECT_ID(N'[dbo].[FK_RelationAdvancePaymentLedgerBookingCode]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RelationAdvancePaymentSet] DROP CONSTRAINT [FK_RelationAdvancePaymentLedgerBookingCode];
GO
IF OBJECT_ID(N'[dbo].[FK_RelationLocationFreight]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FreightSet] DROP CONSTRAINT [FK_RelationLocationFreight];
GO
IF OBJECT_ID(N'[dbo].[FK_RelationLocationFreight1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FreightSet] DROP CONSTRAINT [FK_RelationLocationFreight1];
GO
IF OBJECT_ID(N'[dbo].[FK_RelationFreight]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FreightSet] DROP CONSTRAINT [FK_RelationFreight];
GO
IF OBJECT_ID(N'[dbo].[FK_RelationFreight1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FreightSet] DROP CONSTRAINT [FK_RelationFreight1];
GO
IF OBJECT_ID(N'[dbo].[FK_LocationTruck]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TruckSet] DROP CONSTRAINT [FK_LocationTruck];
GO
IF OBJECT_ID(N'[dbo].[FK_TruckLocation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TruckSet] DROP CONSTRAINT [FK_TruckLocation];
GO
IF OBJECT_ID(N'[dbo].[FK_FreightLocation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FreightSet] DROP CONSTRAINT [FK_FreightLocation];
GO
IF OBJECT_ID(N'[dbo].[FK_LedgerBookingCodeMaterial]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MaterialSet] DROP CONSTRAINT [FK_LedgerBookingCodeMaterial];
GO
IF OBJECT_ID(N'[dbo].[FK_LedgerBookingCodeMaterial1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MaterialSet] DROP CONSTRAINT [FK_LedgerBookingCodeMaterial1];
GO
IF OBJECT_ID(N'[dbo].[FK_MaterialMaterialMutation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MaterialMutationSet] DROP CONSTRAINT [FK_MaterialMaterialMutation];
GO
IF OBJECT_ID(N'[dbo].[FK_MaterialMaterialClosure]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MaterialClosureSet] DROP CONSTRAINT [FK_MaterialMaterialClosure];
GO
IF OBJECT_ID(N'[dbo].[FK_RelationOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderSet] DROP CONSTRAINT [FK_RelationOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_MaterialUnitMaterial]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MaterialSet] DROP CONSTRAINT [FK_MaterialUnitMaterial];
GO
IF OBJECT_ID(N'[dbo].[FK_InvoiceOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderSet] DROP CONSTRAINT [FK_InvoiceOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_RelationLocationOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderSet] DROP CONSTRAINT [FK_RelationLocationOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_RelationProjectOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderSet] DROP CONSTRAINT [FK_RelationProjectOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_RelationContactOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderSet] DROP CONSTRAINT [FK_RelationContactOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_LocationOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderSet] DROP CONSTRAINT [FK_LocationOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderOrderLine]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderLineSet] DROP CONSTRAINT [FK_OrderOrderLine];
GO
IF OBJECT_ID(N'[dbo].[FK_MaterialOrderLine]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderLineSet] DROP CONSTRAINT [FK_MaterialOrderLine];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderFreight]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FreightSet] DROP CONSTRAINT [FK_OrderFreight];
GO
IF OBJECT_ID(N'[dbo].[FK_RelationPriceAgreementOrderLine]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderLineSet] DROP CONSTRAINT [FK_RelationPriceAgreementOrderLine];
GO
IF OBJECT_ID(N'[dbo].[FK_RelationContractMaterialOrderLine]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderLineSet] DROP CONSTRAINT [FK_RelationContractMaterialOrderLine];
GO
IF OBJECT_ID(N'[dbo].[FK_LedgerInvoice]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InvoiceSet] DROP CONSTRAINT [FK_LedgerInvoice];
GO
IF OBJECT_ID(N'[dbo].[FK_RelationInvoice]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InvoiceSet] DROP CONSTRAINT [FK_RelationInvoice];
GO
IF OBJECT_ID(N'[dbo].[FK_LocationInvoice]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InvoiceSet] DROP CONSTRAINT [FK_LocationInvoice];
GO
IF OBJECT_ID(N'[dbo].[FK_LedgerBookingCodeInvoiceLine]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InvoiceLineSet] DROP CONSTRAINT [FK_LedgerBookingCodeInvoiceLine];
GO
IF OBJECT_ID(N'[dbo].[FK_MaterialInvoiceLine]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InvoiceLineSet] DROP CONSTRAINT [FK_MaterialInvoiceLine];
GO
IF OBJECT_ID(N'[dbo].[FK_RelationAdvancePaymentInvoiceLine]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InvoiceLineSet] DROP CONSTRAINT [FK_RelationAdvancePaymentInvoiceLine];
GO
IF OBJECT_ID(N'[dbo].[FK_LocationLedgerMutation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LedgerMutationSet] DROP CONSTRAINT [FK_LocationLedgerMutation];
GO
IF OBJECT_ID(N'[dbo].[FK_LedgerBookingCodeRentalType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RentalTypeSet] DROP CONSTRAINT [FK_LedgerBookingCodeRentalType];
GO
IF OBJECT_ID(N'[dbo].[FK_RentalTypeRentalItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RentalItemSet] DROP CONSTRAINT [FK_RentalTypeRentalItem];
GO
IF OBJECT_ID(N'[dbo].[FK_RentalTypeRentalTypeVAT]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RentalTypeVATSet] DROP CONSTRAINT [FK_RentalTypeRentalTypeVAT];
GO
IF OBJECT_ID(N'[dbo].[FK_LocationRentalTypeVAT]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RentalTypeVATSet] DROP CONSTRAINT [FK_LocationRentalTypeVAT];
GO
IF OBJECT_ID(N'[dbo].[FK_RentalItemRentalItemActivity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RentalItemActivitySet] DROP CONSTRAINT [FK_RentalItemRentalItemActivity];
GO
IF OBJECT_ID(N'[dbo].[FK_LocationStaffMember]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StaffMemberSet] DROP CONSTRAINT [FK_LocationStaffMember];
GO
IF OBJECT_ID(N'[dbo].[FK_LocationRentalItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RentalItemSet] DROP CONSTRAINT [FK_LocationRentalItem];
GO
IF OBJECT_ID(N'[dbo].[FK_LocationStaffMember1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StaffMemberSet] DROP CONSTRAINT [FK_LocationStaffMember1];
GO
IF OBJECT_ID(N'[dbo].[FK_SecurityRoleSecurityRoleObjectAccess]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SecurityRoleObjectAccessSet] DROP CONSTRAINT [FK_SecurityRoleSecurityRoleObjectAccess];
GO
IF OBJECT_ID(N'[dbo].[FK_StaffMemberSystemSettings]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SystemSettingSet] DROP CONSTRAINT [FK_StaffMemberSystemSettings];
GO
IF OBJECT_ID(N'[dbo].[FK_LocationSystemSettings]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SystemSettingSet] DROP CONSTRAINT [FK_LocationSystemSettings];
GO
IF OBJECT_ID(N'[dbo].[FK_StaffMemberStaffMemberPayment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StaffMemberPaymentSet] DROP CONSTRAINT [FK_StaffMemberStaffMemberPayment];
GO
IF OBJECT_ID(N'[dbo].[FK_LedgerStaffMemberPayment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StaffMemberPaymentSet] DROP CONSTRAINT [FK_LedgerStaffMemberPayment];
GO
IF OBJECT_ID(N'[dbo].[FK_InvoiceStaffMemberPayment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StaffMemberPaymentSet] DROP CONSTRAINT [FK_InvoiceStaffMemberPayment];
GO
IF OBJECT_ID(N'[dbo].[FK_StaffMemberPaymentStaffMemberPaymentDeclaration]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StaffMemberPaymentDeclarationSet] DROP CONSTRAINT [FK_StaffMemberPaymentStaffMemberPaymentDeclaration];
GO
IF OBJECT_ID(N'[dbo].[FK_LedgerBookingCodeStaffMemberPaymentDeclaration]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StaffMemberPaymentDeclarationSet] DROP CONSTRAINT [FK_LedgerBookingCodeStaffMemberPaymentDeclaration];
GO
IF OBJECT_ID(N'[dbo].[FK_StaffMemberPaymentStaffMemberTimeRegistration]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StaffMemberTimeRegistrationSet] DROP CONSTRAINT [FK_StaffMemberPaymentStaffMemberTimeRegistration];
GO
IF OBJECT_ID(N'[dbo].[FK_StaffTimeRegistrationActivityStaffMemberTimeRegistration]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StaffMemberTimeRegistrationSet] DROP CONSTRAINT [FK_StaffTimeRegistrationActivityStaffMemberTimeRegistration];
GO
IF OBJECT_ID(N'[dbo].[FK_StaffMemberPaymentStaffMemberAdvancePayment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StaffMemberAdvancePaymentSet] DROP CONSTRAINT [FK_StaffMemberPaymentStaffMemberAdvancePayment];
GO
IF OBJECT_ID(N'[dbo].[FK_LedgerStaffMemberAdvancePayment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StaffMemberAdvancePaymentSet] DROP CONSTRAINT [FK_LedgerStaffMemberAdvancePayment];
GO
IF OBJECT_ID(N'[dbo].[FK_InvoiceStaffMemberAdvancePayment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StaffMemberAdvancePaymentSet] DROP CONSTRAINT [FK_InvoiceStaffMemberAdvancePayment];
GO
IF OBJECT_ID(N'[dbo].[FK_MaterialFreightSortingMaterial]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FreightSortingMaterialSet] DROP CONSTRAINT [FK_MaterialFreightSortingMaterial];
GO
IF OBJECT_ID(N'[dbo].[FK_MaterialFreightWeigingMaterial]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FreightWeighingMaterialSet] DROP CONSTRAINT [FK_MaterialFreightWeigingMaterial];
GO
IF OBJECT_ID(N'[dbo].[FK_MaterialRelationContractMaterial]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RelationContractMaterialSet] DROP CONSTRAINT [FK_MaterialRelationContractMaterial];
GO
IF OBJECT_ID(N'[dbo].[FK_MaterialRelationPriceAgreement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RelationPriceAgreementSet] DROP CONSTRAINT [FK_MaterialRelationPriceAgreement];
GO
IF OBJECT_ID(N'[dbo].[FK_StaffMemberOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderSet] DROP CONSTRAINT [FK_StaffMemberOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_MaterialLocation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MaterialSet] DROP CONSTRAINT [FK_MaterialLocation];
GO
IF OBJECT_ID(N'[dbo].[FK_InvoiceLineLedger]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InvoiceLineSet] DROP CONSTRAINT [FK_InvoiceLineLedger];
GO
IF OBJECT_ID(N'[dbo].[FK_RelationWorkInvoiceLine]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InvoiceLineSet] DROP CONSTRAINT [FK_RelationWorkInvoiceLine];
GO
IF OBJECT_ID(N'[dbo].[FK_InvoiceLineOrderLine]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderLineSet] DROP CONSTRAINT [FK_InvoiceLineOrderLine];
GO
IF OBJECT_ID(N'[dbo].[FK_InvoiceLineInvoice]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InvoiceLineSet] DROP CONSTRAINT [FK_InvoiceLineInvoice];
GO
IF OBJECT_ID(N'[dbo].[FK_RelationRelationMaterials]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RelationMaterialSet] DROP CONSTRAINT [FK_RelationRelationMaterials];
GO
IF OBJECT_ID(N'[dbo].[FK_MaterialRelationMaterials]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RelationMaterialSet] DROP CONSTRAINT [FK_MaterialRelationMaterials];
GO
IF OBJECT_ID(N'[dbo].[FK_LocationLedger]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LedgerSet] DROP CONSTRAINT [FK_LocationLedger];
GO
IF OBJECT_ID(N'[dbo].[FK_LocationMaterialDirt]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LocationSet] DROP CONSTRAINT [FK_LocationMaterialDirt];
GO
IF OBJECT_ID(N'[dbo].[FK_LocationMaterialWork]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LocationSet] DROP CONSTRAINT [FK_LocationMaterialWork];
GO
IF OBJECT_ID(N'[dbo].[FK_LocationLedger1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LocationSet] DROP CONSTRAINT [FK_LocationLedger1];
GO
IF OBJECT_ID(N'[dbo].[FK_LocationBankLedger]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LocationSet] DROP CONSTRAINT [FK_LocationBankLedger];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderMaterialMutation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MaterialMutationSet] DROP CONSTRAINT [FK_OrderMaterialMutation];
GO
IF OBJECT_ID(N'[dbo].[FK_LocationLedgerBookingCode]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LocationSet] DROP CONSTRAINT [FK_LocationLedgerBookingCode];
GO
IF OBJECT_ID(N'[dbo].[FK_RentalItemActivityInvoiceLine]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InvoiceLineSet] DROP CONSTRAINT [FK_RentalItemActivityInvoiceLine];
GO
IF OBJECT_ID(N'[dbo].[FK_LocationRelation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LocationSet] DROP CONSTRAINT [FK_LocationRelation];
GO
IF OBJECT_ID(N'[dbo].[FK_LocationRelation1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LocationSet] DROP CONSTRAINT [FK_LocationRelation1];
GO
IF OBJECT_ID(N'[dbo].[FK_LocationLedgerBookingCode1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LocationSet] DROP CONSTRAINT [FK_LocationLedgerBookingCode1];
GO
IF OBJECT_ID(N'[dbo].[FK_RentalItemActivityRelationAdvancePayment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RelationAdvancePaymentSet] DROP CONSTRAINT [FK_RentalItemActivityRelationAdvancePayment];
GO
IF OBJECT_ID(N'[dbo].[FK_RentalTypeRentalType_RentalType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RentalTypeRentalType] DROP CONSTRAINT [FK_RentalTypeRentalType_RentalType];
GO
IF OBJECT_ID(N'[dbo].[FK_RentalTypeRentalType_RentalType1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RentalTypeRentalType] DROP CONSTRAINT [FK_RentalTypeRentalType_RentalType1];
GO
IF OBJECT_ID(N'[dbo].[FK_LedgerBookingCodeLedgerClosure]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LedgerClosureSet] DROP CONSTRAINT [FK_LedgerBookingCodeLedgerClosure];
GO
IF OBJECT_ID(N'[dbo].[FK_StaffMemberSecurityRole_StaffMember]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StaffMemberSecurityRole] DROP CONSTRAINT [FK_StaffMemberSecurityRole_StaffMember];
GO
IF OBJECT_ID(N'[dbo].[FK_StaffMemberSecurityRole_SecurityRole]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StaffMemberSecurityRole] DROP CONSTRAINT [FK_StaffMemberSecurityRole_SecurityRole];
GO
IF OBJECT_ID(N'[dbo].[FK_FreightTransportUltimateSourceCustomer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FreightSet] DROP CONSTRAINT [FK_FreightTransportUltimateSourceCustomer];
GO
IF OBJECT_ID(N'[dbo].[FK_FreightTransportSourceCustomer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FreightSet] DROP CONSTRAINT [FK_FreightTransportSourceCustomer];
GO
IF OBJECT_ID(N'[dbo].[FK_FreightTransportCompanyCustomer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FreightSet] DROP CONSTRAINT [FK_FreightTransportCompanyCustomer];
GO
IF OBJECT_ID(N'[dbo].[FK_FreightTransportDestinationCustomer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FreightSet] DROP CONSTRAINT [FK_FreightTransportDestinationCustomer];
GO
IF OBJECT_ID(N'[dbo].[FK_FreightTransportDestructorCustomer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FreightSet] DROP CONSTRAINT [FK_FreightTransportDestructorCustomer];
GO
IF OBJECT_ID(N'[dbo].[FK_RelationPreferredLocation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RelationSet] DROP CONSTRAINT [FK_RelationPreferredLocation];
GO
IF OBJECT_ID(N'[dbo].[FK_ContractGuidanceMaterialMutationRelationContractMaterial]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContractGuidanceMaterialMutationSet] DROP CONSTRAINT [FK_ContractGuidanceMaterialMutationRelationContractMaterial];
GO
IF OBJECT_ID(N'[dbo].[FK_MaterialMutationContractGuidanceMaterialMutation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContractGuidanceMaterialMutationSet] DROP CONSTRAINT [FK_MaterialMutationContractGuidanceMaterialMutation];
GO
IF OBJECT_ID(N'[dbo].[FK_RentLedgerRentalItemActivity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RentalItemActivitySet] DROP CONSTRAINT [FK_RentLedgerRentalItemActivity];
GO
IF OBJECT_ID(N'[dbo].[FK_RentLedgerInvoice]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InvoiceSet] DROP CONSTRAINT [FK_RentLedgerInvoice];
GO
IF OBJECT_ID(N'[dbo].[FK_RentLedgerRelation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RentLedgerSet] DROP CONSTRAINT [FK_RentLedgerRelation];
GO
IF OBJECT_ID(N'[dbo].[FK_RentLedgerLocation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RentLedgerSet] DROP CONSTRAINT [FK_RentLedgerLocation];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[RelationSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RelationSet];
GO
IF OBJECT_ID(N'[dbo].[RelationAddressSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RelationAddressSet];
GO
IF OBJECT_ID(N'[dbo].[RelationContactSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RelationContactSet];
GO
IF OBJECT_ID(N'[dbo].[RelationContactLogSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RelationContactLogSet];
GO
IF OBJECT_ID(N'[dbo].[RelationLocationSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RelationLocationSet];
GO
IF OBJECT_ID(N'[dbo].[RelationProjectSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RelationProjectSet];
GO
IF OBJECT_ID(N'[dbo].[RelationAdvancePaymentSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RelationAdvancePaymentSet];
GO
IF OBJECT_ID(N'[dbo].[RelationPriceAgreementSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RelationPriceAgreementSet];
GO
IF OBJECT_ID(N'[dbo].[RelationWorkSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RelationWorkSet];
GO
IF OBJECT_ID(N'[dbo].[RelationContractSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RelationContractSet];
GO
IF OBJECT_ID(N'[dbo].[RelationContractMaterialSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RelationContractMaterialSet];
GO
IF OBJECT_ID(N'[dbo].[FreightSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FreightSet];
GO
IF OBJECT_ID(N'[dbo].[FreightSortingMaterialSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FreightSortingMaterialSet];
GO
IF OBJECT_ID(N'[dbo].[TruckSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TruckSet];
GO
IF OBJECT_ID(N'[dbo].[FreightWeighingSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FreightWeighingSet];
GO
IF OBJECT_ID(N'[dbo].[FreightWeighingMaterialSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FreightWeighingMaterialSet];
GO
IF OBJECT_ID(N'[dbo].[LedgerSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LedgerSet];
GO
IF OBJECT_ID(N'[dbo].[LedgerClosureSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LedgerClosureSet];
GO
IF OBJECT_ID(N'[dbo].[LedgerBookingCodeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LedgerBookingCodeSet];
GO
IF OBJECT_ID(N'[dbo].[LedgerCheckSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LedgerCheckSet];
GO
IF OBJECT_ID(N'[dbo].[LedgerMutationSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LedgerMutationSet];
GO
IF OBJECT_ID(N'[dbo].[LocationSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LocationSet];
GO
IF OBJECT_ID(N'[dbo].[MaterialSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MaterialSet];
GO
IF OBJECT_ID(N'[dbo].[MaterialMutationSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MaterialMutationSet];
GO
IF OBJECT_ID(N'[dbo].[MaterialClosureSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MaterialClosureSet];
GO
IF OBJECT_ID(N'[dbo].[OrderSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderSet];
GO
IF OBJECT_ID(N'[dbo].[MaterialUnitSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MaterialUnitSet];
GO
IF OBJECT_ID(N'[dbo].[InvoiceSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InvoiceSet];
GO
IF OBJECT_ID(N'[dbo].[OrderLineSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderLineSet];
GO
IF OBJECT_ID(N'[dbo].[InvoiceLineSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InvoiceLineSet];
GO
IF OBJECT_ID(N'[dbo].[RentalTypeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RentalTypeSet];
GO
IF OBJECT_ID(N'[dbo].[RentalItemSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RentalItemSet];
GO
IF OBJECT_ID(N'[dbo].[RentalTypeVATSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RentalTypeVATSet];
GO
IF OBJECT_ID(N'[dbo].[RentalItemActivitySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RentalItemActivitySet];
GO
IF OBJECT_ID(N'[dbo].[StaffMemberSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StaffMemberSet];
GO
IF OBJECT_ID(N'[dbo].[SecurityRoleSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SecurityRoleSet];
GO
IF OBJECT_ID(N'[dbo].[SecurityRoleObjectAccessSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SecurityRoleObjectAccessSet];
GO
IF OBJECT_ID(N'[dbo].[SystemSettingSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SystemSettingSet];
GO
IF OBJECT_ID(N'[dbo].[StaffMemberPaymentSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StaffMemberPaymentSet];
GO
IF OBJECT_ID(N'[dbo].[StaffMemberPaymentDeclarationSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StaffMemberPaymentDeclarationSet];
GO
IF OBJECT_ID(N'[dbo].[StaffMemberTimeRegistrationSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StaffMemberTimeRegistrationSet];
GO
IF OBJECT_ID(N'[dbo].[StaffTimeRegistrationActivitySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StaffTimeRegistrationActivitySet];
GO
IF OBJECT_ID(N'[dbo].[StaffMemberAdvancePaymentSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StaffMemberAdvancePaymentSet];
GO
IF OBJECT_ID(N'[dbo].[RelationMaterialSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RelationMaterialSet];
GO
IF OBJECT_ID(N'[dbo].[ContractGuidanceMaterialMutationSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ContractGuidanceMaterialMutationSet];
GO
IF OBJECT_ID(N'[dbo].[RentLedgerSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RentLedgerSet];
GO
IF OBJECT_ID(N'[dbo].[RentalTypeRentalType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RentalTypeRentalType];
GO
IF OBJECT_ID(N'[dbo].[StaffMemberSecurityRole]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StaffMemberSecurityRole];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'RelationSet'
CREATE TABLE [dbo].[RelationSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [IsActive] bit  NOT NULL,
    [VATNumber] nvarchar(40)  NOT NULL,
    [PhoneNumber] nvarchar(40)  NOT NULL,
    [EMail] nvarchar(40)  NOT NULL,
    [YourReference] nvarchar(40)  NOT NULL,
    [CustomerNumber] bigint IDENTITY(1,1) NOT NULL,
    [CustomerType] varchar(15)  NOT NULL,
    [PreferredCurrency] nvarchar(3)  NOT NULL,
    [IsSystemUser] bit  NOT NULL,
    [FaxNumber] nvarchar(40)  NOT NULL,
    [TransportContact] nvarchar(40)  NOT NULL,
    [TransportVIHB] nvarchar(40)  NOT NULL,
    [TransportAddressLine] nvarchar(250)  NOT NULL,
    [Country] nvarchar(100)  NOT NULL,
    [PreferredLocation_Id] uniqueidentifier  NULL
);
GO

-- Creating table 'RelationAddressSet'
CREATE TABLE [dbo].[RelationAddressSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [AdressType] varchar(10)  NOT NULL,
    [AdressLine1] nvarchar(250)  NOT NULL,
    [AdressLine2] nvarchar(250)  NOT NULL,
    [AdressLine3] nvarchar(250)  NOT NULL,
    [ZIPcode] nvarchar(20)  NOT NULL,
    [City] nvarchar(100)  NOT NULL,
    [Country] nvarchar(100)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [Relation_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'RelationContactSet'
CREATE TABLE [dbo].[RelationContactSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [PhoneNumber] nvarchar(20)  NOT NULL,
    [MobilePhone] nvarchar(20)  NOT NULL,
    [HomePhone] nvarchar(20)  NOT NULL,
    [PrivateMobilePhone] nvarchar(20)  NOT NULL,
    [EMail] nvarchar(40)  NOT NULL,
    [PrivateEMail] nvarchar(40)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [RelationType] varchar(15)  NOT NULL,
    [Relation_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'RelationContactLogSet'
CREATE TABLE [dbo].[RelationContactLogSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [ContactDateTime] datetime  NOT NULL,
    [ContactType] varchar(15)  NOT NULL,
    [FollowUpDateTime] datetime  NOT NULL,
    [FollowUpState] varchar(15)  NOT NULL,
    [PausedUntilDateTime] datetime  NOT NULL,
    [HandledBy] uniqueidentifier  NOT NULL,
    [Handler] uniqueidentifier  NOT NULL,
    [RelationContact_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'RelationLocationSet'
CREATE TABLE [dbo].[RelationLocationSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [IsActive] bit  NOT NULL,
    [Relation_Id] uniqueidentifier  NOT NULL,
    [RelationAdress_Id] uniqueidentifier  NULL
);
GO

-- Creating table 'RelationProjectSet'
CREATE TABLE [dbo].[RelationProjectSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [IsActive] bit  NOT NULL,
    [ProjectNumber] bigint IDENTITY(1,1) NOT NULL,
    [Relation_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'RelationAdvancePaymentSet'
CREATE TABLE [dbo].[RelationAdvancePaymentSet] (
    [Id] uniqueidentifier  NOT NULL,
    [IsPaidBack] bit  NOT NULL,
    [Amount] float  NOT NULL,
    [AmountPaidBack] float  NOT NULL,
    [PaymentDateTime] datetime  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [IsPaidOut] bit  NOT NULL,
    [PaymentType] varchar(10)  NOT NULL,
    [Relation_Id] uniqueidentifier  NOT NULL,
    [Ledger_Id] uniqueidentifier  NOT NULL,
    [LedgerBookingCode_Id] uniqueidentifier  NOT NULL,
    [RentalItemActivity_Id] uniqueidentifier  NULL
);
GO

-- Creating table 'RelationPriceAgreementSet'
CREATE TABLE [dbo].[RelationPriceAgreementSet] (
    [Id] uniqueidentifier  NOT NULL,
    [IsActive] bit  NOT NULL,
    [StartDateTime] datetime  NOT NULL,
    [EndDateTime] datetime  NOT NULL,
    [PricePerUnit] float  NOT NULL,
    [AgreementType] varchar(5)  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [Relation_Id] uniqueidentifier  NOT NULL,
    [Material_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'RelationWorkSet'
CREATE TABLE [dbo].[RelationWorkSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [HourlyRate] float  NOT NULL,
    [Hours] float  NOT NULL,
    [AmountPaidBack] float  NOT NULL,
    [AmountEXVat] float  NOT NULL,
    [VATAmount] float  NOT NULL,
    [TotalAmount] float  NOT NULL,
    [AgreementDateTime] datetime  NOT NULL,
    [IsVATApplicable] bit  NOT NULL,
    [IsTreatedAsAdvancePayment] bit  NOT NULL,
    [IsActive] bit  NOT NULL,
    [VATPercentage] float  NOT NULL,
    [WorkType] varchar(10)  NOT NULL,
    [Relation_Id] uniqueidentifier  NOT NULL,
    [LedgerBookingCode_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'RelationContractSet'
CREATE TABLE [dbo].[RelationContractSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [YourReference] nvarchar(40)  NOT NULL,
    [OurReference] bigint IDENTITY(1,1) NOT NULL,
    [PaymentConditions] nvarchar(500)  NOT NULL,
    [DeliveryConditions] nvarchar(500)  NOT NULL,
    [ContractDate] datetime  NOT NULL,
    [ContractStartDate] datetime  NOT NULL,
    [ContractEndDate] datetime  NOT NULL,
    [ContractType] varchar(5)  NOT NULL,
    [HasContractGuidance] bit  NOT NULL,
    [ContractPriority] tinyint  NOT NULL,
    [ContractStatus] varchar(10)  NULL,
    [Relation_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'RelationContractMaterialSet'
CREATE TABLE [dbo].[RelationContractMaterialSet] (
    [Id] uniqueidentifier  NOT NULL,
    [MinAmount] float  NOT NULL,
    [MaxAmount] float  NOT NULL,
    [PricePerUnit] float  NOT NULL,
    [AvgStockPrice] float  NOT NULL,
    [AvgStockUnits] float  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [DeliveredAmount] float  NOT NULL,
    [DeliveredTotalPrice] float  NOT NULL,
    [AvgRequiredProfitPerUnit] float  NOT NULL,
    [RelationContract_Id] uniqueidentifier  NOT NULL,
    [Material_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'FreightSet'
CREATE TABLE [dbo].[FreightSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [OurReference] bigint  NOT NULL,
    [YourReference] nvarchar(40)  NOT NULL,
    [FreightDateTime] datetime  NOT NULL,
    [RequestedFreightEndDateTime] datetime  NOT NULL,
    [RequestedFreightStartDateTime] datetime  NOT NULL,
    [FreightStatus] varchar(15)  NOT NULL,
    [FreightType] varchar(15)  NOT NULL,
    [TotalNetWeight] float  NOT NULL,
    [OurDriverID] uniqueidentifier  NOT NULL,
    [YourTruckPlate] nvarchar(40)  NOT NULL,
    [YourDriverName] nvarchar(40)  NOT NULL,
    [FreightDirection] varchar(25)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [TransportRole] nvarchar(20)  NOT NULL,
    [TransportNotificationTransportType] nvarchar(20)  NOT NULL,
    [TransportNotificationRemovalType] nvarchar(20)  NOT NULL,
    [TransportNotificationApprovedDestructor] bit  NOT NULL,
    [TransportPlannedTransports] bigint  NOT NULL,
    [TransportWrapping] nvarchar(20)  NOT NULL,
    [TransportSpecialTreatment] nvarchar(40)  NOT NULL,
    [TransportDRCode] nvarchar(10)  NOT NULL,
    [TransportUsedTechnology] nvarchar(40)  NOT NULL,
    [TransportReasonForExport] nvarchar(40)  NOT NULL,
    [TransportType] nvarchar(20)  NOT NULL,
    [TransportDestructionAction] nvarchar(20)  NOT NULL,
    [OurTruck_Id] uniqueidentifier  NULL,
    [FromRelationLocation_Id] uniqueidentifier  NULL,
    [ToRelationLocation_Id] uniqueidentifier  NULL,
    [FromRelation_Id] uniqueidentifier  NULL,
    [ToRelation_Id] uniqueidentifier  NULL,
    [SourceOrDestinationLocation_Id] uniqueidentifier  NULL,
    [Order_Id] uniqueidentifier  NULL,
    [TransportUltimateSourceCustomer_Id] uniqueidentifier  NULL,
    [TransportSourceCustomer_Id] uniqueidentifier  NULL,
    [TransportCompanyCustomer_Id] uniqueidentifier  NULL,
    [TransportDestinationCustomer_Id] uniqueidentifier  NULL,
    [TransportDestructorCustomer_Id] uniqueidentifier  NULL
);
GO

-- Creating table 'FreightSortingMaterialSet'
CREATE TABLE [dbo].[FreightSortingMaterialSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [Weight] float  NOT NULL,
    [GrossWeight] float  NOT NULL,
    [TarraWeight] float  NOT NULL,
    [Freight_Id] uniqueidentifier  NOT NULL,
    [Material_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'TruckSet'
CREATE TABLE [dbo].[TruckSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [TruckPlate] nvarchar(40)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [CurrentTruckLocation_Id] uniqueidentifier  NULL,
    [HomeTruckLocation_Id] uniqueidentifier  NULL
);
GO

-- Creating table 'FreightWeighingSet'
CREATE TABLE [dbo].[FreightWeighingSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [WeighingDateTime1] datetime  NOT NULL,
    [Weight1] float  NOT NULL,
    [WeighingDateTime2] datetime  NOT NULL,
    [Weight2] float  NOT NULL,
    [IsDriverInTruck] bit  NOT NULL,
    [WeighingNumber] bigint IDENTITY(1,1) NOT NULL,
    [Key1] nvarchar(40)  NOT NULL,
    [Key2] nvarchar(40)  NOT NULL,
    [Freight_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'FreightWeighingMaterialSet'
CREATE TABLE [dbo].[FreightWeighingMaterialSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [GrossWeight] float  NOT NULL,
    [NetWeight] float  NOT NULL,
    [TarraWeight] float  NOT NULL,
    [FreightWeighing_Id] uniqueidentifier  NOT NULL,
    [Material_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'LedgerSet'
CREATE TABLE [dbo].[LedgerSet] (
    [Id] uniqueidentifier  NOT NULL,
    [IsActive] bit  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [LedgerCurrency] nvarchar(3)  NOT NULL,
    [LedgerLevel] float  NOT NULL,
    [Bank] nvarchar(40)  NOT NULL,
    [BankAccount] nvarchar(40)  NOT NULL,
    [BankIBAN] nvarchar(40)  NOT NULL,
    [BankBIC] nvarchar(40)  NOT NULL,
    [LedgerType] varchar(5)  NOT NULL,
    [LimitToLocationId] uniqueidentifier  NULL,
    [IsDebugLedger] bit  NOT NULL
);
GO

-- Creating table 'LedgerClosureSet'
CREATE TABLE [dbo].[LedgerClosureSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [ClosureDate] datetime  NOT NULL,
    [LedgerLevel] float  NOT NULL,
    [OriginalLedgerLevel] float  NOT NULL,
    [IsCorrection] bit  NOT NULL,
    [LedgerDelta] float  NOT NULL,
    [Ledger_Id] uniqueidentifier  NULL,
    [LedgerBookingCode_Id] uniqueidentifier  NULL
);
GO

-- Creating table 'LedgerBookingCodeSet'
CREATE TABLE [dbo].[LedgerBookingCodeSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [IsActive] bit  NOT NULL,
    [IsDebugLedgerCode] bit  NOT NULL,
    [LedgerLevel] float  NOT NULL,
    [LedgerCurrency] nvarchar(3)  NOT NULL
);
GO

-- Creating table 'LedgerCheckSet'
CREATE TABLE [dbo].[LedgerCheckSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [IsLedgerCorrected] bit  NOT NULL,
    [CorrectionAmount] float  NOT NULL,
    [CheckDate] datetime  NOT NULL,
    [Ledger_Id] uniqueidentifier  NULL,
    [LedgerBookingCode_Id] uniqueidentifier  NULL
);
GO

-- Creating table 'LedgerMutationSet'
CREATE TABLE [dbo].[LedgerMutationSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [AmountEXVat] float  NOT NULL,
    [VATAmount] float  NOT NULL,
    [TotalAmount] float  NOT NULL,
    [IsEditable] bit  NOT NULL,
    [BookingDateTime] datetime  NOT NULL,
    [BookingType] varchar(20)  NOT NULL,
    [IsCorrection] bit  NOT NULL,
    [GroupCode] uniqueidentifier  NULL,
    [Ledger_Id] uniqueidentifier  NULL,
    [LedgerBookingCode_Id] uniqueidentifier  NULL,
    [Relation_Id] uniqueidentifier  NULL,
    [Location_Id] uniqueidentifier  NULL
);
GO

-- Creating table 'LocationSet'
CREATE TABLE [dbo].[LocationSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [IsActive] bit  NOT NULL,
    [ZIPcode] nvarchar(20)  NOT NULL,
    [City] nvarchar(100)  NOT NULL,
    [Country] nvarchar(100)  NOT NULL,
    [VATNumber] nvarchar(40)  NOT NULL,
    [PhoneNumber] nvarchar(40)  NOT NULL,
    [EMail] nvarchar(40)  NOT NULL,
    [PreferredCurrency] nvarchar(3)  NOT NULL,
    [LocationNumber] bigint IDENTITY(1,1) NOT NULL,
    [AdressLine1] nvarchar(250)  NOT NULL,
    [AdressLine2] nvarchar(250)  NOT NULL,
    [AdressLine3] nvarchar(250)  NOT NULL,
    [InvoiceAddress] nvarchar(250)  NOT NULL,
    [InvoiceFooter] nvarchar(250)  NOT NULL,
    [InvoicePrintLogo] bit  NOT NULL,
    [DefaultVATPercentage] float  NOT NULL,
    [DefaultHourlyRate] float  NOT NULL,
    [DefaultWeighingTariff] float  NOT NULL,
    [MaterialForDirtId] uniqueidentifier  NULL,
    [MaterialForWorkId] uniqueidentifier  NULL,
    [CashLedgerId] uniqueidentifier  NULL,
    [BankLedgerId] uniqueidentifier  NULL,
    [InvoiceFooterPerPage] nvarchar(250)  NOT NULL,
    [CompanyLogoImage] varbinary(max)  NULL,
    [CompanyMembershipsLogo] varbinary(max)  NULL,
    [VIHBCode] nvarchar(20)  NOT NULL,
    [ContactPerson] nvarchar(40)  NOT NULL,
    [DefaultWeighingTariffBookingCode_Id] uniqueidentifier  NULL,
    [RelationSale_Id] uniqueidentifier  NULL,
    [RelationBuy_Id] uniqueidentifier  NULL,
    [DefaultBailPriceBookingCode_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'MaterialSet'
CREATE TABLE [dbo].[MaterialSet] (
    [Id] uniqueidentifier  NOT NULL,
    [IsActive] bit  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [MaterialNumber] bigint IDENTITY(1,1) NOT NULL,
    [Category] nvarchar(40)  NOT NULL,
    [Group] nvarchar(40)  NOT NULL,
    [CurrentStockLevel] float  NOT NULL,
    [StockMayBeNegative] bit  NOT NULL,
    [IsWorkInsteadOfMaterial] bit  NOT NULL,
    [PurchasePrice] float  NOT NULL,
    [SalesPrice] float  NOT NULL,
    [AvgPurchasePrice] float  NOT NULL,
    [AvgSalesPrice] float  NOT NULL,
    [AvgPurchasePriceTotalWeight] float  NOT NULL,
    [AvgSalesPriceTotalWeight] float  NOT NULL,
    [AvgPurchasePriceTotalPrice] float  NOT NULL,
    [AvgSalesPriceTotalPrice] float  NOT NULL,
    [UseAvgSalesPriceAsActualPrice] bit  NOT NULL,
    [UseAvgPurchasePriceAsActualPrice] bit  NOT NULL,
    [VATPercentage] float  NOT NULL,
    [StorageCode] nvarchar(250)  NOT NULL,
    [LMECode] nvarchar(40)  NOT NULL,
    [InvoiceType] varchar(5)  NOT NULL,
    [MaterialId] uniqueidentifier  NOT NULL,
    [HCode] nvarchar(10)  NOT NULL,
    [BaselCode] nvarchar(10)  NOT NULL,
    [PhysicalShape] nvarchar(20)  NOT NULL,
    [PurchaseLedgerBookingCode_Id] uniqueidentifier  NOT NULL,
    [SalesLedgerBookingCode_Id] uniqueidentifier  NOT NULL,
    [MaterialUnit_Id] uniqueidentifier  NOT NULL,
    [Location_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'MaterialMutationSet'
CREATE TABLE [dbo].[MaterialMutationSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [MutationDateTime] datetime  NOT NULL,
    [Amount] float  NOT NULL,
    [TotalPrice] float  NOT NULL,
    [MutationType] varchar(10)  NOT NULL,
    [AmountInKg] float  NOT NULL,
    [IsCorrection] bit  NOT NULL,
    [GroupCode] uniqueidentifier  NULL,
    [MutationNumber] bigint  NOT NULL,
    [PricePerUnit] float  NOT NULL,
    [Material_Id] uniqueidentifier  NOT NULL,
    [Order_Id] uniqueidentifier  NULL
);
GO

-- Creating table 'MaterialClosureSet'
CREATE TABLE [dbo].[MaterialClosureSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [ClosureDateTime] datetime  NOT NULL,
    [MaterialStockLevel] float  NOT NULL,
    [MaterialTotalBought] float  NOT NULL,
    [MaterialTotalSold] float  NOT NULL,
    [MaterialTotalBoughtPrice] float  NOT NULL,
    [MaterialTotalSoldPrice] float  NOT NULL,
    [MaterialStockPrice] float  NOT NULL,
    [IsCorrected] bit  NOT NULL,
    [OriginalValues] nvarchar(600)  NOT NULL,
    [MaterialTotalBoughtDay] float  NOT NULL,
    [MaterialTotalSoldDay] float  NOT NULL,
    [MaterialTotalBoughtPriceDay] float  NOT NULL,
    [MaterialTotalSoldPriceDay] float  NOT NULL,
    [PurchasePrice] float  NOT NULL,
    [SalesPrice] float  NOT NULL,
    [Material_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'OrderSet'
CREATE TABLE [dbo].[OrderSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [OrderNumber] bigint  NOT NULL,
    [OrderType] varchar(5)  NOT NULL,
    [BookingDateTime] datetime  NOT NULL,
    [OrderStatus] varchar(20)  NOT NULL,
    [TotalPrice] float  NOT NULL,
    [TotalAmount] float  NOT NULL,
    [YourTruckPlate] nvarchar(40)  NOT NULL,
    [YourDriverName] nvarchar(40)  NOT NULL,
    [DeterminePriceDuringInvoicing] bit  NOT NULL,
    [IsCorrected] bit  NOT NULL,
    [GroupCode] uniqueidentifier  NOT NULL,
    [FreightID] nvarchar(40)  NOT NULL,
    [OrderNote] nvarchar(1000)  NOT NULL,
    [Relation_Id] uniqueidentifier  NOT NULL,
    [Invoice_Id] uniqueidentifier  NULL,
    [RelationLocation_Id] uniqueidentifier  NULL,
    [RelationProject_Id] uniqueidentifier  NULL,
    [RelationContact_Id] uniqueidentifier  NULL,
    [Location_Id] uniqueidentifier  NOT NULL,
    [StaffMemberPurchaser_Id] uniqueidentifier  NULL
);
GO

-- Creating table 'MaterialUnitSet'
CREATE TABLE [dbo].[MaterialUnitSet] (
    [Id] uniqueidentifier  NOT NULL,
    [IsActive] bit  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [StockUnit] nvarchar(10)  NOT NULL,
    [StockKgMultiplier] float  NOT NULL
);
GO

-- Creating table 'InvoiceSet'
CREATE TABLE [dbo].[InvoiceSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [InvoiceNumber] bigint  NOT NULL,
    [InvoiceStatus] varchar(5)  NOT NULL,
    [InvoiceType] varchar(5)  NOT NULL,
    [BookingDateTime] datetime  NOT NULL,
    [YourReference] nvarchar(40)  NOT NULL,
    [Price] float  NOT NULL,
    [VATPrice] float  NOT NULL,
    [TotalPrice] float  NOT NULL,
    [InvoiceNote] nvarchar(1000)  NOT NULL,
    [IsCorrected] bit  NOT NULL,
    [GroupCode] uniqueidentifier  NOT NULL,
    [AlreadyPaid] float  NOT NULL,
    [DiscountPercentage] float  NOT NULL,
    [InvoiceSubType] varchar(10)  NOT NULL,
    [PriceWithoutDiscount] float  NOT NULL,
    [Ledger_Id] uniqueidentifier  NOT NULL,
    [Relation_Id] uniqueidentifier  NULL,
    [Location_Id] uniqueidentifier  NULL,
    [RentLedger_Id] uniqueidentifier  NULL
);
GO

-- Creating table 'OrderLineSet'
CREATE TABLE [dbo].[OrderLineSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [PriceExVAT] float  NOT NULL,
    [Amount] float  NOT NULL,
    [PricePerUnit] float  NOT NULL,
    [AlreadyDeliveredAmount] float  NOT NULL,
    [Order_Id] uniqueidentifier  NOT NULL,
    [Material_Id] uniqueidentifier  NOT NULL,
    [RelationPriceAgreement_Id] uniqueidentifier  NULL,
    [RelationContractMaterial_Id] uniqueidentifier  NULL,
    [InvoiceLine_Id] uniqueidentifier  NULL
);
GO

-- Creating table 'InvoiceLineSet'
CREATE TABLE [dbo].[InvoiceLineSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [LineNumber] bigint  NOT NULL,
    [PriceWithDiscount] float  NOT NULL,
    [VATPrice] float  NOT NULL,
    [TotalPrice] float  NOT NULL,
    [Amount] float  NOT NULL,
    [PricePerUnit] float  NOT NULL,
    [VATPercentage] float  NOT NULL,
    [InvoiceId] uniqueidentifier  NOT NULL,
    [LedgerId] uniqueidentifier  NOT NULL,
    [RelationWorkId] uniqueidentifier  NULL,
    [DiscountPercentage] float  NOT NULL,
    [OriginalPrice] float  NOT NULL,
    [AllowDiscount] bit  NOT NULL,
    [LedgerBookingCode_Id] uniqueidentifier  NOT NULL,
    [Material_Id] uniqueidentifier  NULL,
    [RelationAdvancePayment_Id] uniqueidentifier  NULL,
    [RentalItemActivity_Id] uniqueidentifier  NULL
);
GO

-- Creating table 'RentalTypeSet'
CREATE TABLE [dbo].[RentalTypeSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [RentalConditions] nvarchar(max)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [LedgerBookingCode_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'RentalItemSet'
CREATE TABLE [dbo].[RentalItemSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [IsActive] bit  NOT NULL,
    [BaseRentalPrice] float  NOT NULL,
    [RentPerDay] float  NOT NULL,
    [RentPerWeek] float  NOT NULL,
    [RentPerMonth] float  NOT NULL,
    [ItemState] varchar(15)  NOT NULL,
    [ItemNumber] bigint  NOT NULL,
    [BailPrice] float  NOT NULL,
    [RentalType_Id] uniqueidentifier  NOT NULL,
    [Location_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'RentalTypeVATSet'
CREATE TABLE [dbo].[RentalTypeVATSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [VATPercentage] float  NOT NULL,
    [IsActive] bit  NOT NULL,
    [RentalType_Id] uniqueidentifier  NOT NULL,
    [Location_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'RentalItemActivitySet'
CREATE TABLE [dbo].[RentalItemActivitySet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [IsTreatedAsAdvancePayment] bit  NOT NULL,
    [RentStartDateTime] datetime  NOT NULL,
    [RentEndStartDateTime] datetime  NOT NULL,
    [CalculatedRentPrice] float  NOT NULL,
    [DiscountPercentage] float  NOT NULL,
    [BaseRentPrice] float  NOT NULL,
    [VATRentPrice] float  NOT NULL,
    [TotalRentPrice] float  NOT NULL,
    [InvoiceStatus] nvarchar(10)  NOT NULL,
    [RentalItem_Id] uniqueidentifier  NOT NULL,
    [RentLedger_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'StaffMemberSet'
CREATE TABLE [dbo].[StaffMemberSet] (
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [Id] uniqueidentifier  NOT NULL,
    [IsActive] bit  NOT NULL,
    [IsDriver] bit  NOT NULL,
    [ContractHoursPerWeek] bigint  NOT NULL,
    [SocialSecurityNumber] nvarchar(40)  NOT NULL,
    [InServiceDate] datetime  NOT NULL,
    [OutOfServiceDate] datetime  NOT NULL,
    [StaffMemberNumber] bigint IDENTITY(1,1) NOT NULL,
    [IDNumber] nvarchar(40)  NOT NULL,
    [IDExpirationDate] datetime  NOT NULL,
    [IDNationality] nvarchar(40)  NOT NULL,
    [IDType] nvarchar(20)  NOT NULL,
    [LivingAddress] nvarchar(2000)  NOT NULL,
    [HomeAddress] nvarchar(2000)  NOT NULL,
    [NetHourlyRate] float  NOT NULL,
    [HasVMSAccount] bit  NOT NULL,
    [AccountName] nvarchar(50)  NOT NULL,
    [Password] nvarchar(50)  NOT NULL,
    [HomeLocation_Id] uniqueidentifier  NOT NULL,
    [LimitAccessToThisLocation_Id] uniqueidentifier  NULL
);
GO

-- Creating table 'SecurityRoleSet'
CREATE TABLE [dbo].[SecurityRoleSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [HasUnlimitedAccess] bit  NOT NULL,
    [IsRoleTemplate] bit  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'SecurityRoleObjectAccessSet'
CREATE TABLE [dbo].[SecurityRoleObjectAccessSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [ObjectName] nvarchar(250)  NOT NULL,
    [HasCreateAccess] bit  NOT NULL,
    [HasReadAccess] bit  NOT NULL,
    [HasUpdateAccess] bit  NOT NULL,
    [HasDeleteAccess] bit  NOT NULL,
    [HasExecuteAccess] bit  NOT NULL,
    [SettableAccessTypes] varchar(5)  NOT NULL,
    [SecurityRole_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'SystemSettingSet'
CREATE TABLE [dbo].[SystemSettingSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [Value] nvarchar(4000)  NOT NULL,
    [StaffMember_Id] uniqueidentifier  NULL,
    [Location_Id] uniqueidentifier  NULL
);
GO

-- Creating table 'StaffMemberPaymentSet'
CREATE TABLE [dbo].[StaffMemberPaymentSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [PaymentDate] datetime  NOT NULL,
    [StartPaymentPeriod] datetime  NOT NULL,
    [EndPaymentPeriod] datetime  NOT NULL,
    [TotalPaymentAmount] float  NOT NULL,
    [DeductedAdvancePayments] float  NOT NULL,
    [PaymentAmountTime] float  NOT NULL,
    [PaymentAmountDeclarations] float  NOT NULL,
    [StaffMember_Id] uniqueidentifier  NOT NULL,
    [Ledger_Id] uniqueidentifier  NOT NULL,
    [Invoice_Id] uniqueidentifier  NULL
);
GO

-- Creating table 'StaffMemberPaymentDeclarationSet'
CREATE TABLE [dbo].[StaffMemberPaymentDeclarationSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [DeclarationNumber] bigint IDENTITY(1,1) NOT NULL,
    [IsCheckedOK] bit  NOT NULL,
    [DeclarationDateTime] datetime  NOT NULL,
    [StaffMemberPayment_Id] uniqueidentifier  NOT NULL,
    [LedgerBookingCode_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'StaffMemberTimeRegistrationSet'
CREATE TABLE [dbo].[StaffMemberTimeRegistrationSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [RegistrationDay] datetime  NOT NULL,
    [Hours] float  NOT NULL,
    [BaseHourlyRate] float  NOT NULL,
    [TotalHoursPayment] float  NOT NULL,
    [IsCheckedOK] bit  NOT NULL,
    [StaffMemberPayment_Id] uniqueidentifier  NOT NULL,
    [StaffTimeRegistrationActivity_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'StaffTimeRegistrationActivitySet'
CREATE TABLE [dbo].[StaffTimeRegistrationActivitySet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [IsActive] bit  NOT NULL,
    [AdditionalPayPercentage] float  NOT NULL,
    [HasToBePaid] bit  NOT NULL,
    [IsStandardActivity] bit  NOT NULL
);
GO

-- Creating table 'StaffMemberAdvancePaymentSet'
CREATE TABLE [dbo].[StaffMemberAdvancePaymentSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [IsActive] bit  NOT NULL,
    [AdvancePaymentNumber] bigint IDENTITY(1,1) NOT NULL,
    [Amount] float  NOT NULL,
    [PaymentDate] datetime  NOT NULL,
    [AlreadyPaidBackAmount] float  NOT NULL,
    [IsPaidBack] bit  NOT NULL,
    [StaffMemberPayment_Id] uniqueidentifier  NOT NULL,
    [Ledger_Id] uniqueidentifier  NOT NULL,
    [Invoice_Id] uniqueidentifier  NULL
);
GO

-- Creating table 'RelationMaterialSet'
CREATE TABLE [dbo].[RelationMaterialSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [RelationId] uniqueidentifier  NOT NULL,
    [MaterialId] uniqueidentifier  NOT NULL,
    [LMECode] nvarchar(40)  NOT NULL
);
GO

-- Creating table 'ContractGuidanceMaterialMutationSet'
CREATE TABLE [dbo].[ContractGuidanceMaterialMutationSet] (
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [MutationAmount] float  NOT NULL,
    [IsCorrected] bit  NOT NULL,
    [Id] uniqueidentifier  NOT NULL,
    [RelationContractMaterial_Id] uniqueidentifier  NULL,
    [MaterialMutation_Id] uniqueidentifier  NULL
);
GO

-- Creating table 'RentLedgerSet'
CREATE TABLE [dbo].[RentLedgerSet] (
    [Id] uniqueidentifier  NOT NULL,
    [CreateDateTime] datetime  NOT NULL,
    [ModifyDateTime] datetime  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(4000)  NOT NULL,
    [CreateUser] uniqueidentifier  NOT NULL,
    [ModifyUser] uniqueidentifier  NOT NULL,
    [InitialRentStartDateTime] datetime  NOT NULL,
    [InitialRentEndStartDateTime] datetime  NOT NULL,
    [VATRentPrice] float  NOT NULL,
    [TotalRentPrice] float  NOT NULL,
    [BaseRentPrice] float  NOT NULL,
    [RentNumber] bigint  NOT NULL,
    [Relation_Id] uniqueidentifier  NOT NULL,
    [Location_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'RentalTypeRentalType'
CREATE TABLE [dbo].[RentalTypeRentalType] (
    [IsAlternativeRentalTypeFor_Id] uniqueidentifier  NOT NULL,
    [AlternativeRentalTypes_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'StaffMemberSecurityRole'
CREATE TABLE [dbo].[StaffMemberSecurityRole] (
    [StaffMember_Id] uniqueidentifier  NOT NULL,
    [SecurityRole_Id] uniqueidentifier  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'RelationSet'
ALTER TABLE [dbo].[RelationSet]
ADD CONSTRAINT [PK_RelationSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RelationAddressSet'
ALTER TABLE [dbo].[RelationAddressSet]
ADD CONSTRAINT [PK_RelationAddressSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RelationContactSet'
ALTER TABLE [dbo].[RelationContactSet]
ADD CONSTRAINT [PK_RelationContactSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RelationContactLogSet'
ALTER TABLE [dbo].[RelationContactLogSet]
ADD CONSTRAINT [PK_RelationContactLogSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RelationLocationSet'
ALTER TABLE [dbo].[RelationLocationSet]
ADD CONSTRAINT [PK_RelationLocationSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RelationProjectSet'
ALTER TABLE [dbo].[RelationProjectSet]
ADD CONSTRAINT [PK_RelationProjectSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RelationAdvancePaymentSet'
ALTER TABLE [dbo].[RelationAdvancePaymentSet]
ADD CONSTRAINT [PK_RelationAdvancePaymentSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RelationPriceAgreementSet'
ALTER TABLE [dbo].[RelationPriceAgreementSet]
ADD CONSTRAINT [PK_RelationPriceAgreementSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RelationWorkSet'
ALTER TABLE [dbo].[RelationWorkSet]
ADD CONSTRAINT [PK_RelationWorkSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RelationContractSet'
ALTER TABLE [dbo].[RelationContractSet]
ADD CONSTRAINT [PK_RelationContractSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RelationContractMaterialSet'
ALTER TABLE [dbo].[RelationContractMaterialSet]
ADD CONSTRAINT [PK_RelationContractMaterialSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FreightSet'
ALTER TABLE [dbo].[FreightSet]
ADD CONSTRAINT [PK_FreightSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FreightSortingMaterialSet'
ALTER TABLE [dbo].[FreightSortingMaterialSet]
ADD CONSTRAINT [PK_FreightSortingMaterialSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TruckSet'
ALTER TABLE [dbo].[TruckSet]
ADD CONSTRAINT [PK_TruckSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FreightWeighingSet'
ALTER TABLE [dbo].[FreightWeighingSet]
ADD CONSTRAINT [PK_FreightWeighingSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FreightWeighingMaterialSet'
ALTER TABLE [dbo].[FreightWeighingMaterialSet]
ADD CONSTRAINT [PK_FreightWeighingMaterialSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LedgerSet'
ALTER TABLE [dbo].[LedgerSet]
ADD CONSTRAINT [PK_LedgerSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LedgerClosureSet'
ALTER TABLE [dbo].[LedgerClosureSet]
ADD CONSTRAINT [PK_LedgerClosureSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LedgerBookingCodeSet'
ALTER TABLE [dbo].[LedgerBookingCodeSet]
ADD CONSTRAINT [PK_LedgerBookingCodeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LedgerCheckSet'
ALTER TABLE [dbo].[LedgerCheckSet]
ADD CONSTRAINT [PK_LedgerCheckSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LedgerMutationSet'
ALTER TABLE [dbo].[LedgerMutationSet]
ADD CONSTRAINT [PK_LedgerMutationSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LocationSet'
ALTER TABLE [dbo].[LocationSet]
ADD CONSTRAINT [PK_LocationSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MaterialSet'
ALTER TABLE [dbo].[MaterialSet]
ADD CONSTRAINT [PK_MaterialSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MaterialMutationSet'
ALTER TABLE [dbo].[MaterialMutationSet]
ADD CONSTRAINT [PK_MaterialMutationSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MaterialClosureSet'
ALTER TABLE [dbo].[MaterialClosureSet]
ADD CONSTRAINT [PK_MaterialClosureSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [PK_OrderSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MaterialUnitSet'
ALTER TABLE [dbo].[MaterialUnitSet]
ADD CONSTRAINT [PK_MaterialUnitSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'InvoiceSet'
ALTER TABLE [dbo].[InvoiceSet]
ADD CONSTRAINT [PK_InvoiceSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderLineSet'
ALTER TABLE [dbo].[OrderLineSet]
ADD CONSTRAINT [PK_OrderLineSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'InvoiceLineSet'
ALTER TABLE [dbo].[InvoiceLineSet]
ADD CONSTRAINT [PK_InvoiceLineSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RentalTypeSet'
ALTER TABLE [dbo].[RentalTypeSet]
ADD CONSTRAINT [PK_RentalTypeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RentalItemSet'
ALTER TABLE [dbo].[RentalItemSet]
ADD CONSTRAINT [PK_RentalItemSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RentalTypeVATSet'
ALTER TABLE [dbo].[RentalTypeVATSet]
ADD CONSTRAINT [PK_RentalTypeVATSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RentalItemActivitySet'
ALTER TABLE [dbo].[RentalItemActivitySet]
ADD CONSTRAINT [PK_RentalItemActivitySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StaffMemberSet'
ALTER TABLE [dbo].[StaffMemberSet]
ADD CONSTRAINT [PK_StaffMemberSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SecurityRoleSet'
ALTER TABLE [dbo].[SecurityRoleSet]
ADD CONSTRAINT [PK_SecurityRoleSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SecurityRoleObjectAccessSet'
ALTER TABLE [dbo].[SecurityRoleObjectAccessSet]
ADD CONSTRAINT [PK_SecurityRoleObjectAccessSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SystemSettingSet'
ALTER TABLE [dbo].[SystemSettingSet]
ADD CONSTRAINT [PK_SystemSettingSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StaffMemberPaymentSet'
ALTER TABLE [dbo].[StaffMemberPaymentSet]
ADD CONSTRAINT [PK_StaffMemberPaymentSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StaffMemberPaymentDeclarationSet'
ALTER TABLE [dbo].[StaffMemberPaymentDeclarationSet]
ADD CONSTRAINT [PK_StaffMemberPaymentDeclarationSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StaffMemberTimeRegistrationSet'
ALTER TABLE [dbo].[StaffMemberTimeRegistrationSet]
ADD CONSTRAINT [PK_StaffMemberTimeRegistrationSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StaffTimeRegistrationActivitySet'
ALTER TABLE [dbo].[StaffTimeRegistrationActivitySet]
ADD CONSTRAINT [PK_StaffTimeRegistrationActivitySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StaffMemberAdvancePaymentSet'
ALTER TABLE [dbo].[StaffMemberAdvancePaymentSet]
ADD CONSTRAINT [PK_StaffMemberAdvancePaymentSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RelationMaterialSet'
ALTER TABLE [dbo].[RelationMaterialSet]
ADD CONSTRAINT [PK_RelationMaterialSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ContractGuidanceMaterialMutationSet'
ALTER TABLE [dbo].[ContractGuidanceMaterialMutationSet]
ADD CONSTRAINT [PK_ContractGuidanceMaterialMutationSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RentLedgerSet'
ALTER TABLE [dbo].[RentLedgerSet]
ADD CONSTRAINT [PK_RentLedgerSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [IsAlternativeRentalTypeFor_Id], [AlternativeRentalTypes_Id] in table 'RentalTypeRentalType'
ALTER TABLE [dbo].[RentalTypeRentalType]
ADD CONSTRAINT [PK_RentalTypeRentalType]
    PRIMARY KEY NONCLUSTERED ([IsAlternativeRentalTypeFor_Id], [AlternativeRentalTypes_Id] ASC);
GO

-- Creating primary key on [StaffMember_Id], [SecurityRole_Id] in table 'StaffMemberSecurityRole'
ALTER TABLE [dbo].[StaffMemberSecurityRole]
ADD CONSTRAINT [PK_StaffMemberSecurityRole]
    PRIMARY KEY NONCLUSTERED ([StaffMember_Id], [SecurityRole_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Relation_Id] in table 'RelationAddressSet'
ALTER TABLE [dbo].[RelationAddressSet]
ADD CONSTRAINT [FK_RelationRelationAdress]
    FOREIGN KEY ([Relation_Id])
    REFERENCES [dbo].[RelationSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RelationRelationAdress'
CREATE INDEX [IX_FK_RelationRelationAdress]
ON [dbo].[RelationAddressSet]
    ([Relation_Id]);
GO

-- Creating foreign key on [Relation_Id] in table 'RelationLocationSet'
ALTER TABLE [dbo].[RelationLocationSet]
ADD CONSTRAINT [FK_RelationRelationLocation]
    FOREIGN KEY ([Relation_Id])
    REFERENCES [dbo].[RelationSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RelationRelationLocation'
CREATE INDEX [IX_FK_RelationRelationLocation]
ON [dbo].[RelationLocationSet]
    ([Relation_Id]);
GO

-- Creating foreign key on [RelationAdress_Id] in table 'RelationLocationSet'
ALTER TABLE [dbo].[RelationLocationSet]
ADD CONSTRAINT [FK_RelationAdressRelationLocation]
    FOREIGN KEY ([RelationAdress_Id])
    REFERENCES [dbo].[RelationAddressSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RelationAdressRelationLocation'
CREATE INDEX [IX_FK_RelationAdressRelationLocation]
ON [dbo].[RelationLocationSet]
    ([RelationAdress_Id]);
GO

-- Creating foreign key on [Relation_Id] in table 'RelationProjectSet'
ALTER TABLE [dbo].[RelationProjectSet]
ADD CONSTRAINT [FK_RelationRelationProject]
    FOREIGN KEY ([Relation_Id])
    REFERENCES [dbo].[RelationSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RelationRelationProject'
CREATE INDEX [IX_FK_RelationRelationProject]
ON [dbo].[RelationProjectSet]
    ([Relation_Id]);
GO

-- Creating foreign key on [Relation_Id] in table 'RelationAdvancePaymentSet'
ALTER TABLE [dbo].[RelationAdvancePaymentSet]
ADD CONSTRAINT [FK_RelationRelationAdvancePayment]
    FOREIGN KEY ([Relation_Id])
    REFERENCES [dbo].[RelationSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RelationRelationAdvancePayment'
CREATE INDEX [IX_FK_RelationRelationAdvancePayment]
ON [dbo].[RelationAdvancePaymentSet]
    ([Relation_Id]);
GO

-- Creating foreign key on [Relation_Id] in table 'RelationPriceAgreementSet'
ALTER TABLE [dbo].[RelationPriceAgreementSet]
ADD CONSTRAINT [FK_RelationRelationPriceAgreement]
    FOREIGN KEY ([Relation_Id])
    REFERENCES [dbo].[RelationSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RelationRelationPriceAgreement'
CREATE INDEX [IX_FK_RelationRelationPriceAgreement]
ON [dbo].[RelationPriceAgreementSet]
    ([Relation_Id]);
GO

-- Creating foreign key on [Relation_Id] in table 'RelationWorkSet'
ALTER TABLE [dbo].[RelationWorkSet]
ADD CONSTRAINT [FK_RelationRelationWork]
    FOREIGN KEY ([Relation_Id])
    REFERENCES [dbo].[RelationSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RelationRelationWork'
CREATE INDEX [IX_FK_RelationRelationWork]
ON [dbo].[RelationWorkSet]
    ([Relation_Id]);
GO

-- Creating foreign key on [Relation_Id] in table 'RelationContractSet'
ALTER TABLE [dbo].[RelationContractSet]
ADD CONSTRAINT [FK_RelationRelationContract]
    FOREIGN KEY ([Relation_Id])
    REFERENCES [dbo].[RelationSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RelationRelationContract'
CREATE INDEX [IX_FK_RelationRelationContract]
ON [dbo].[RelationContractSet]
    ([Relation_Id]);
GO

-- Creating foreign key on [RelationContract_Id] in table 'RelationContractMaterialSet'
ALTER TABLE [dbo].[RelationContractMaterialSet]
ADD CONSTRAINT [FK_RelationContractRelationContractMaterial]
    FOREIGN KEY ([RelationContract_Id])
    REFERENCES [dbo].[RelationContractSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RelationContractRelationContractMaterial'
CREATE INDEX [IX_FK_RelationContractRelationContractMaterial]
ON [dbo].[RelationContractMaterialSet]
    ([RelationContract_Id]);
GO

-- Creating foreign key on [Freight_Id] in table 'FreightSortingMaterialSet'
ALTER TABLE [dbo].[FreightSortingMaterialSet]
ADD CONSTRAINT [FK_FreightFreightSortingMaterial]
    FOREIGN KEY ([Freight_Id])
    REFERENCES [dbo].[FreightSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FreightFreightSortingMaterial'
CREATE INDEX [IX_FK_FreightFreightSortingMaterial]
ON [dbo].[FreightSortingMaterialSet]
    ([Freight_Id]);
GO

-- Creating foreign key on [OurTruck_Id] in table 'FreightSet'
ALTER TABLE [dbo].[FreightSet]
ADD CONSTRAINT [FK_TruckFreight]
    FOREIGN KEY ([OurTruck_Id])
    REFERENCES [dbo].[TruckSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TruckFreight'
CREATE INDEX [IX_FK_TruckFreight]
ON [dbo].[FreightSet]
    ([OurTruck_Id]);
GO

-- Creating foreign key on [Freight_Id] in table 'FreightWeighingSet'
ALTER TABLE [dbo].[FreightWeighingSet]
ADD CONSTRAINT [FK_FreightFreightWeighing]
    FOREIGN KEY ([Freight_Id])
    REFERENCES [dbo].[FreightSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FreightFreightWeighing'
CREATE INDEX [IX_FK_FreightFreightWeighing]
ON [dbo].[FreightWeighingSet]
    ([Freight_Id]);
GO

-- Creating foreign key on [FreightWeighing_Id] in table 'FreightWeighingMaterialSet'
ALTER TABLE [dbo].[FreightWeighingMaterialSet]
ADD CONSTRAINT [FK_FreightWeighingFreightWeigingMaterial]
    FOREIGN KEY ([FreightWeighing_Id])
    REFERENCES [dbo].[FreightWeighingSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FreightWeighingFreightWeigingMaterial'
CREATE INDEX [IX_FK_FreightWeighingFreightWeigingMaterial]
ON [dbo].[FreightWeighingMaterialSet]
    ([FreightWeighing_Id]);
GO

-- Creating foreign key on [Relation_Id] in table 'RelationContactSet'
ALTER TABLE [dbo].[RelationContactSet]
ADD CONSTRAINT [FK_RelationRelationContact]
    FOREIGN KEY ([Relation_Id])
    REFERENCES [dbo].[RelationSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RelationRelationContact'
CREATE INDEX [IX_FK_RelationRelationContact]
ON [dbo].[RelationContactSet]
    ([Relation_Id]);
GO

-- Creating foreign key on [RelationContact_Id] in table 'RelationContactLogSet'
ALTER TABLE [dbo].[RelationContactLogSet]
ADD CONSTRAINT [FK_RelationContactRelationContactLog]
    FOREIGN KEY ([RelationContact_Id])
    REFERENCES [dbo].[RelationContactSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RelationContactRelationContactLog'
CREATE INDEX [IX_FK_RelationContactRelationContactLog]
ON [dbo].[RelationContactLogSet]
    ([RelationContact_Id]);
GO

-- Creating foreign key on [Ledger_Id] in table 'LedgerClosureSet'
ALTER TABLE [dbo].[LedgerClosureSet]
ADD CONSTRAINT [FK_LedgerLedgerClosure]
    FOREIGN KEY ([Ledger_Id])
    REFERENCES [dbo].[LedgerSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LedgerLedgerClosure'
CREATE INDEX [IX_FK_LedgerLedgerClosure]
ON [dbo].[LedgerClosureSet]
    ([Ledger_Id]);
GO

-- Creating foreign key on [Ledger_Id] in table 'LedgerCheckSet'
ALTER TABLE [dbo].[LedgerCheckSet]
ADD CONSTRAINT [FK_LedgerLedgerChecks]
    FOREIGN KEY ([Ledger_Id])
    REFERENCES [dbo].[LedgerSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LedgerLedgerChecks'
CREATE INDEX [IX_FK_LedgerLedgerChecks]
ON [dbo].[LedgerCheckSet]
    ([Ledger_Id]);
GO

-- Creating foreign key on [LedgerBookingCode_Id] in table 'LedgerCheckSet'
ALTER TABLE [dbo].[LedgerCheckSet]
ADD CONSTRAINT [FK_LedgerChecksLedgerBookingCode]
    FOREIGN KEY ([LedgerBookingCode_Id])
    REFERENCES [dbo].[LedgerBookingCodeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LedgerChecksLedgerBookingCode'
CREATE INDEX [IX_FK_LedgerChecksLedgerBookingCode]
ON [dbo].[LedgerCheckSet]
    ([LedgerBookingCode_Id]);
GO

-- Creating foreign key on [Ledger_Id] in table 'LedgerMutationSet'
ALTER TABLE [dbo].[LedgerMutationSet]
ADD CONSTRAINT [FK_LedgerLedgerMutation]
    FOREIGN KEY ([Ledger_Id])
    REFERENCES [dbo].[LedgerSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LedgerLedgerMutation'
CREATE INDEX [IX_FK_LedgerLedgerMutation]
ON [dbo].[LedgerMutationSet]
    ([Ledger_Id]);
GO

-- Creating foreign key on [LedgerBookingCode_Id] in table 'LedgerMutationSet'
ALTER TABLE [dbo].[LedgerMutationSet]
ADD CONSTRAINT [FK_LedgerBookingCodeLedgerMutation]
    FOREIGN KEY ([LedgerBookingCode_Id])
    REFERENCES [dbo].[LedgerBookingCodeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LedgerBookingCodeLedgerMutation'
CREATE INDEX [IX_FK_LedgerBookingCodeLedgerMutation]
ON [dbo].[LedgerMutationSet]
    ([LedgerBookingCode_Id]);
GO

-- Creating foreign key on [Relation_Id] in table 'LedgerMutationSet'
ALTER TABLE [dbo].[LedgerMutationSet]
ADD CONSTRAINT [FK_LedgerMutationRelation]
    FOREIGN KEY ([Relation_Id])
    REFERENCES [dbo].[RelationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LedgerMutationRelation'
CREATE INDEX [IX_FK_LedgerMutationRelation]
ON [dbo].[LedgerMutationSet]
    ([Relation_Id]);
GO

-- Creating foreign key on [LedgerBookingCode_Id] in table 'RelationWorkSet'
ALTER TABLE [dbo].[RelationWorkSet]
ADD CONSTRAINT [FK_RelationWorkLedgerBookingCode]
    FOREIGN KEY ([LedgerBookingCode_Id])
    REFERENCES [dbo].[LedgerBookingCodeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RelationWorkLedgerBookingCode'
CREATE INDEX [IX_FK_RelationWorkLedgerBookingCode]
ON [dbo].[RelationWorkSet]
    ([LedgerBookingCode_Id]);
GO

-- Creating foreign key on [Ledger_Id] in table 'RelationAdvancePaymentSet'
ALTER TABLE [dbo].[RelationAdvancePaymentSet]
ADD CONSTRAINT [FK_RelationAdvancePaymentLedger]
    FOREIGN KEY ([Ledger_Id])
    REFERENCES [dbo].[LedgerSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RelationAdvancePaymentLedger'
CREATE INDEX [IX_FK_RelationAdvancePaymentLedger]
ON [dbo].[RelationAdvancePaymentSet]
    ([Ledger_Id]);
GO

-- Creating foreign key on [LedgerBookingCode_Id] in table 'RelationAdvancePaymentSet'
ALTER TABLE [dbo].[RelationAdvancePaymentSet]
ADD CONSTRAINT [FK_RelationAdvancePaymentLedgerBookingCode]
    FOREIGN KEY ([LedgerBookingCode_Id])
    REFERENCES [dbo].[LedgerBookingCodeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RelationAdvancePaymentLedgerBookingCode'
CREATE INDEX [IX_FK_RelationAdvancePaymentLedgerBookingCode]
ON [dbo].[RelationAdvancePaymentSet]
    ([LedgerBookingCode_Id]);
GO

-- Creating foreign key on [FromRelationLocation_Id] in table 'FreightSet'
ALTER TABLE [dbo].[FreightSet]
ADD CONSTRAINT [FK_RelationLocationFreight]
    FOREIGN KEY ([FromRelationLocation_Id])
    REFERENCES [dbo].[RelationLocationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RelationLocationFreight'
CREATE INDEX [IX_FK_RelationLocationFreight]
ON [dbo].[FreightSet]
    ([FromRelationLocation_Id]);
GO

-- Creating foreign key on [ToRelationLocation_Id] in table 'FreightSet'
ALTER TABLE [dbo].[FreightSet]
ADD CONSTRAINT [FK_RelationLocationFreight1]
    FOREIGN KEY ([ToRelationLocation_Id])
    REFERENCES [dbo].[RelationLocationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RelationLocationFreight1'
CREATE INDEX [IX_FK_RelationLocationFreight1]
ON [dbo].[FreightSet]
    ([ToRelationLocation_Id]);
GO

-- Creating foreign key on [FromRelation_Id] in table 'FreightSet'
ALTER TABLE [dbo].[FreightSet]
ADD CONSTRAINT [FK_RelationFreight]
    FOREIGN KEY ([FromRelation_Id])
    REFERENCES [dbo].[RelationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RelationFreight'
CREATE INDEX [IX_FK_RelationFreight]
ON [dbo].[FreightSet]
    ([FromRelation_Id]);
GO

-- Creating foreign key on [ToRelation_Id] in table 'FreightSet'
ALTER TABLE [dbo].[FreightSet]
ADD CONSTRAINT [FK_RelationFreight1]
    FOREIGN KEY ([ToRelation_Id])
    REFERENCES [dbo].[RelationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RelationFreight1'
CREATE INDEX [IX_FK_RelationFreight1]
ON [dbo].[FreightSet]
    ([ToRelation_Id]);
GO

-- Creating foreign key on [CurrentTruckLocation_Id] in table 'TruckSet'
ALTER TABLE [dbo].[TruckSet]
ADD CONSTRAINT [FK_LocationTruck]
    FOREIGN KEY ([CurrentTruckLocation_Id])
    REFERENCES [dbo].[LocationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LocationTruck'
CREATE INDEX [IX_FK_LocationTruck]
ON [dbo].[TruckSet]
    ([CurrentTruckLocation_Id]);
GO

-- Creating foreign key on [HomeTruckLocation_Id] in table 'TruckSet'
ALTER TABLE [dbo].[TruckSet]
ADD CONSTRAINT [FK_TruckLocation]
    FOREIGN KEY ([HomeTruckLocation_Id])
    REFERENCES [dbo].[LocationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TruckLocation'
CREATE INDEX [IX_FK_TruckLocation]
ON [dbo].[TruckSet]
    ([HomeTruckLocation_Id]);
GO

-- Creating foreign key on [SourceOrDestinationLocation_Id] in table 'FreightSet'
ALTER TABLE [dbo].[FreightSet]
ADD CONSTRAINT [FK_FreightLocation]
    FOREIGN KEY ([SourceOrDestinationLocation_Id])
    REFERENCES [dbo].[LocationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FreightLocation'
CREATE INDEX [IX_FK_FreightLocation]
ON [dbo].[FreightSet]
    ([SourceOrDestinationLocation_Id]);
GO

-- Creating foreign key on [PurchaseLedgerBookingCode_Id] in table 'MaterialSet'
ALTER TABLE [dbo].[MaterialSet]
ADD CONSTRAINT [FK_LedgerBookingCodeMaterial]
    FOREIGN KEY ([PurchaseLedgerBookingCode_Id])
    REFERENCES [dbo].[LedgerBookingCodeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LedgerBookingCodeMaterial'
CREATE INDEX [IX_FK_LedgerBookingCodeMaterial]
ON [dbo].[MaterialSet]
    ([PurchaseLedgerBookingCode_Id]);
GO

-- Creating foreign key on [SalesLedgerBookingCode_Id] in table 'MaterialSet'
ALTER TABLE [dbo].[MaterialSet]
ADD CONSTRAINT [FK_LedgerBookingCodeMaterial1]
    FOREIGN KEY ([SalesLedgerBookingCode_Id])
    REFERENCES [dbo].[LedgerBookingCodeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LedgerBookingCodeMaterial1'
CREATE INDEX [IX_FK_LedgerBookingCodeMaterial1]
ON [dbo].[MaterialSet]
    ([SalesLedgerBookingCode_Id]);
GO

-- Creating foreign key on [Material_Id] in table 'MaterialMutationSet'
ALTER TABLE [dbo].[MaterialMutationSet]
ADD CONSTRAINT [FK_MaterialMaterialMutation]
    FOREIGN KEY ([Material_Id])
    REFERENCES [dbo].[MaterialSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MaterialMaterialMutation'
CREATE INDEX [IX_FK_MaterialMaterialMutation]
ON [dbo].[MaterialMutationSet]
    ([Material_Id]);
GO

-- Creating foreign key on [Material_Id] in table 'MaterialClosureSet'
ALTER TABLE [dbo].[MaterialClosureSet]
ADD CONSTRAINT [FK_MaterialMaterialClosure]
    FOREIGN KEY ([Material_Id])
    REFERENCES [dbo].[MaterialSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MaterialMaterialClosure'
CREATE INDEX [IX_FK_MaterialMaterialClosure]
ON [dbo].[MaterialClosureSet]
    ([Material_Id]);
GO

-- Creating foreign key on [Relation_Id] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [FK_RelationOrder]
    FOREIGN KEY ([Relation_Id])
    REFERENCES [dbo].[RelationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RelationOrder'
CREATE INDEX [IX_FK_RelationOrder]
ON [dbo].[OrderSet]
    ([Relation_Id]);
GO

-- Creating foreign key on [MaterialUnit_Id] in table 'MaterialSet'
ALTER TABLE [dbo].[MaterialSet]
ADD CONSTRAINT [FK_MaterialUnitMaterial]
    FOREIGN KEY ([MaterialUnit_Id])
    REFERENCES [dbo].[MaterialUnitSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MaterialUnitMaterial'
CREATE INDEX [IX_FK_MaterialUnitMaterial]
ON [dbo].[MaterialSet]
    ([MaterialUnit_Id]);
GO

-- Creating foreign key on [Invoice_Id] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [FK_InvoiceOrder]
    FOREIGN KEY ([Invoice_Id])
    REFERENCES [dbo].[InvoiceSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_InvoiceOrder'
CREATE INDEX [IX_FK_InvoiceOrder]
ON [dbo].[OrderSet]
    ([Invoice_Id]);
GO

-- Creating foreign key on [RelationLocation_Id] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [FK_RelationLocationOrder]
    FOREIGN KEY ([RelationLocation_Id])
    REFERENCES [dbo].[RelationLocationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RelationLocationOrder'
CREATE INDEX [IX_FK_RelationLocationOrder]
ON [dbo].[OrderSet]
    ([RelationLocation_Id]);
GO

-- Creating foreign key on [RelationProject_Id] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [FK_RelationProjectOrder]
    FOREIGN KEY ([RelationProject_Id])
    REFERENCES [dbo].[RelationProjectSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RelationProjectOrder'
CREATE INDEX [IX_FK_RelationProjectOrder]
ON [dbo].[OrderSet]
    ([RelationProject_Id]);
GO

-- Creating foreign key on [RelationContact_Id] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [FK_RelationContactOrder]
    FOREIGN KEY ([RelationContact_Id])
    REFERENCES [dbo].[RelationContactSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RelationContactOrder'
CREATE INDEX [IX_FK_RelationContactOrder]
ON [dbo].[OrderSet]
    ([RelationContact_Id]);
GO

-- Creating foreign key on [Location_Id] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [FK_LocationOrder]
    FOREIGN KEY ([Location_Id])
    REFERENCES [dbo].[LocationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LocationOrder'
CREATE INDEX [IX_FK_LocationOrder]
ON [dbo].[OrderSet]
    ([Location_Id]);
GO

-- Creating foreign key on [Order_Id] in table 'OrderLineSet'
ALTER TABLE [dbo].[OrderLineSet]
ADD CONSTRAINT [FK_OrderOrderLine]
    FOREIGN KEY ([Order_Id])
    REFERENCES [dbo].[OrderSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderOrderLine'
CREATE INDEX [IX_FK_OrderOrderLine]
ON [dbo].[OrderLineSet]
    ([Order_Id]);
GO

-- Creating foreign key on [Material_Id] in table 'OrderLineSet'
ALTER TABLE [dbo].[OrderLineSet]
ADD CONSTRAINT [FK_MaterialOrderLine]
    FOREIGN KEY ([Material_Id])
    REFERENCES [dbo].[MaterialSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MaterialOrderLine'
CREATE INDEX [IX_FK_MaterialOrderLine]
ON [dbo].[OrderLineSet]
    ([Material_Id]);
GO

-- Creating foreign key on [Order_Id] in table 'FreightSet'
ALTER TABLE [dbo].[FreightSet]
ADD CONSTRAINT [FK_OrderFreight]
    FOREIGN KEY ([Order_Id])
    REFERENCES [dbo].[OrderSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderFreight'
CREATE INDEX [IX_FK_OrderFreight]
ON [dbo].[FreightSet]
    ([Order_Id]);
GO

-- Creating foreign key on [RelationPriceAgreement_Id] in table 'OrderLineSet'
ALTER TABLE [dbo].[OrderLineSet]
ADD CONSTRAINT [FK_RelationPriceAgreementOrderLine]
    FOREIGN KEY ([RelationPriceAgreement_Id])
    REFERENCES [dbo].[RelationPriceAgreementSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RelationPriceAgreementOrderLine'
CREATE INDEX [IX_FK_RelationPriceAgreementOrderLine]
ON [dbo].[OrderLineSet]
    ([RelationPriceAgreement_Id]);
GO

-- Creating foreign key on [RelationContractMaterial_Id] in table 'OrderLineSet'
ALTER TABLE [dbo].[OrderLineSet]
ADD CONSTRAINT [FK_RelationContractMaterialOrderLine]
    FOREIGN KEY ([RelationContractMaterial_Id])
    REFERENCES [dbo].[RelationContractMaterialSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RelationContractMaterialOrderLine'
CREATE INDEX [IX_FK_RelationContractMaterialOrderLine]
ON [dbo].[OrderLineSet]
    ([RelationContractMaterial_Id]);
GO

-- Creating foreign key on [Ledger_Id] in table 'InvoiceSet'
ALTER TABLE [dbo].[InvoiceSet]
ADD CONSTRAINT [FK_LedgerInvoice]
    FOREIGN KEY ([Ledger_Id])
    REFERENCES [dbo].[LedgerSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LedgerInvoice'
CREATE INDEX [IX_FK_LedgerInvoice]
ON [dbo].[InvoiceSet]
    ([Ledger_Id]);
GO

-- Creating foreign key on [Relation_Id] in table 'InvoiceSet'
ALTER TABLE [dbo].[InvoiceSet]
ADD CONSTRAINT [FK_RelationInvoice]
    FOREIGN KEY ([Relation_Id])
    REFERENCES [dbo].[RelationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RelationInvoice'
CREATE INDEX [IX_FK_RelationInvoice]
ON [dbo].[InvoiceSet]
    ([Relation_Id]);
GO

-- Creating foreign key on [Location_Id] in table 'InvoiceSet'
ALTER TABLE [dbo].[InvoiceSet]
ADD CONSTRAINT [FK_LocationInvoice]
    FOREIGN KEY ([Location_Id])
    REFERENCES [dbo].[LocationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LocationInvoice'
CREATE INDEX [IX_FK_LocationInvoice]
ON [dbo].[InvoiceSet]
    ([Location_Id]);
GO

-- Creating foreign key on [LedgerBookingCode_Id] in table 'InvoiceLineSet'
ALTER TABLE [dbo].[InvoiceLineSet]
ADD CONSTRAINT [FK_LedgerBookingCodeInvoiceLine]
    FOREIGN KEY ([LedgerBookingCode_Id])
    REFERENCES [dbo].[LedgerBookingCodeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LedgerBookingCodeInvoiceLine'
CREATE INDEX [IX_FK_LedgerBookingCodeInvoiceLine]
ON [dbo].[InvoiceLineSet]
    ([LedgerBookingCode_Id]);
GO

-- Creating foreign key on [Material_Id] in table 'InvoiceLineSet'
ALTER TABLE [dbo].[InvoiceLineSet]
ADD CONSTRAINT [FK_MaterialInvoiceLine]
    FOREIGN KEY ([Material_Id])
    REFERENCES [dbo].[MaterialSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MaterialInvoiceLine'
CREATE INDEX [IX_FK_MaterialInvoiceLine]
ON [dbo].[InvoiceLineSet]
    ([Material_Id]);
GO

-- Creating foreign key on [RelationAdvancePayment_Id] in table 'InvoiceLineSet'
ALTER TABLE [dbo].[InvoiceLineSet]
ADD CONSTRAINT [FK_RelationAdvancePaymentInvoiceLine]
    FOREIGN KEY ([RelationAdvancePayment_Id])
    REFERENCES [dbo].[RelationAdvancePaymentSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RelationAdvancePaymentInvoiceLine'
CREATE INDEX [IX_FK_RelationAdvancePaymentInvoiceLine]
ON [dbo].[InvoiceLineSet]
    ([RelationAdvancePayment_Id]);
GO

-- Creating foreign key on [Location_Id] in table 'LedgerMutationSet'
ALTER TABLE [dbo].[LedgerMutationSet]
ADD CONSTRAINT [FK_LocationLedgerMutation]
    FOREIGN KEY ([Location_Id])
    REFERENCES [dbo].[LocationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LocationLedgerMutation'
CREATE INDEX [IX_FK_LocationLedgerMutation]
ON [dbo].[LedgerMutationSet]
    ([Location_Id]);
GO

-- Creating foreign key on [LedgerBookingCode_Id] in table 'RentalTypeSet'
ALTER TABLE [dbo].[RentalTypeSet]
ADD CONSTRAINT [FK_LedgerBookingCodeRentalType]
    FOREIGN KEY ([LedgerBookingCode_Id])
    REFERENCES [dbo].[LedgerBookingCodeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LedgerBookingCodeRentalType'
CREATE INDEX [IX_FK_LedgerBookingCodeRentalType]
ON [dbo].[RentalTypeSet]
    ([LedgerBookingCode_Id]);
GO

-- Creating foreign key on [RentalType_Id] in table 'RentalItemSet'
ALTER TABLE [dbo].[RentalItemSet]
ADD CONSTRAINT [FK_RentalTypeRentalItem]
    FOREIGN KEY ([RentalType_Id])
    REFERENCES [dbo].[RentalTypeSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RentalTypeRentalItem'
CREATE INDEX [IX_FK_RentalTypeRentalItem]
ON [dbo].[RentalItemSet]
    ([RentalType_Id]);
GO

-- Creating foreign key on [RentalType_Id] in table 'RentalTypeVATSet'
ALTER TABLE [dbo].[RentalTypeVATSet]
ADD CONSTRAINT [FK_RentalTypeRentalTypeVAT]
    FOREIGN KEY ([RentalType_Id])
    REFERENCES [dbo].[RentalTypeSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RentalTypeRentalTypeVAT'
CREATE INDEX [IX_FK_RentalTypeRentalTypeVAT]
ON [dbo].[RentalTypeVATSet]
    ([RentalType_Id]);
GO

-- Creating foreign key on [Location_Id] in table 'RentalTypeVATSet'
ALTER TABLE [dbo].[RentalTypeVATSet]
ADD CONSTRAINT [FK_LocationRentalTypeVAT]
    FOREIGN KEY ([Location_Id])
    REFERENCES [dbo].[LocationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LocationRentalTypeVAT'
CREATE INDEX [IX_FK_LocationRentalTypeVAT]
ON [dbo].[RentalTypeVATSet]
    ([Location_Id]);
GO

-- Creating foreign key on [RentalItem_Id] in table 'RentalItemActivitySet'
ALTER TABLE [dbo].[RentalItemActivitySet]
ADD CONSTRAINT [FK_RentalItemRentalItemActivity]
    FOREIGN KEY ([RentalItem_Id])
    REFERENCES [dbo].[RentalItemSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RentalItemRentalItemActivity'
CREATE INDEX [IX_FK_RentalItemRentalItemActivity]
ON [dbo].[RentalItemActivitySet]
    ([RentalItem_Id]);
GO

-- Creating foreign key on [HomeLocation_Id] in table 'StaffMemberSet'
ALTER TABLE [dbo].[StaffMemberSet]
ADD CONSTRAINT [FK_LocationStaffMember]
    FOREIGN KEY ([HomeLocation_Id])
    REFERENCES [dbo].[LocationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LocationStaffMember'
CREATE INDEX [IX_FK_LocationStaffMember]
ON [dbo].[StaffMemberSet]
    ([HomeLocation_Id]);
GO

-- Creating foreign key on [Location_Id] in table 'RentalItemSet'
ALTER TABLE [dbo].[RentalItemSet]
ADD CONSTRAINT [FK_LocationRentalItem]
    FOREIGN KEY ([Location_Id])
    REFERENCES [dbo].[LocationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LocationRentalItem'
CREATE INDEX [IX_FK_LocationRentalItem]
ON [dbo].[RentalItemSet]
    ([Location_Id]);
GO

-- Creating foreign key on [LimitAccessToThisLocation_Id] in table 'StaffMemberSet'
ALTER TABLE [dbo].[StaffMemberSet]
ADD CONSTRAINT [FK_LocationStaffMember1]
    FOREIGN KEY ([LimitAccessToThisLocation_Id])
    REFERENCES [dbo].[LocationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LocationStaffMember1'
CREATE INDEX [IX_FK_LocationStaffMember1]
ON [dbo].[StaffMemberSet]
    ([LimitAccessToThisLocation_Id]);
GO

-- Creating foreign key on [SecurityRole_Id] in table 'SecurityRoleObjectAccessSet'
ALTER TABLE [dbo].[SecurityRoleObjectAccessSet]
ADD CONSTRAINT [FK_SecurityRoleSecurityRoleObjectAccess]
    FOREIGN KEY ([SecurityRole_Id])
    REFERENCES [dbo].[SecurityRoleSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SecurityRoleSecurityRoleObjectAccess'
CREATE INDEX [IX_FK_SecurityRoleSecurityRoleObjectAccess]
ON [dbo].[SecurityRoleObjectAccessSet]
    ([SecurityRole_Id]);
GO

-- Creating foreign key on [StaffMember_Id] in table 'SystemSettingSet'
ALTER TABLE [dbo].[SystemSettingSet]
ADD CONSTRAINT [FK_StaffMemberSystemSettings]
    FOREIGN KEY ([StaffMember_Id])
    REFERENCES [dbo].[StaffMemberSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StaffMemberSystemSettings'
CREATE INDEX [IX_FK_StaffMemberSystemSettings]
ON [dbo].[SystemSettingSet]
    ([StaffMember_Id]);
GO

-- Creating foreign key on [Location_Id] in table 'SystemSettingSet'
ALTER TABLE [dbo].[SystemSettingSet]
ADD CONSTRAINT [FK_LocationSystemSettings]
    FOREIGN KEY ([Location_Id])
    REFERENCES [dbo].[LocationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LocationSystemSettings'
CREATE INDEX [IX_FK_LocationSystemSettings]
ON [dbo].[SystemSettingSet]
    ([Location_Id]);
GO

-- Creating foreign key on [StaffMember_Id] in table 'StaffMemberPaymentSet'
ALTER TABLE [dbo].[StaffMemberPaymentSet]
ADD CONSTRAINT [FK_StaffMemberStaffMemberPayment]
    FOREIGN KEY ([StaffMember_Id])
    REFERENCES [dbo].[StaffMemberSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StaffMemberStaffMemberPayment'
CREATE INDEX [IX_FK_StaffMemberStaffMemberPayment]
ON [dbo].[StaffMemberPaymentSet]
    ([StaffMember_Id]);
GO

-- Creating foreign key on [Ledger_Id] in table 'StaffMemberPaymentSet'
ALTER TABLE [dbo].[StaffMemberPaymentSet]
ADD CONSTRAINT [FK_LedgerStaffMemberPayment]
    FOREIGN KEY ([Ledger_Id])
    REFERENCES [dbo].[LedgerSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LedgerStaffMemberPayment'
CREATE INDEX [IX_FK_LedgerStaffMemberPayment]
ON [dbo].[StaffMemberPaymentSet]
    ([Ledger_Id]);
GO

-- Creating foreign key on [Invoice_Id] in table 'StaffMemberPaymentSet'
ALTER TABLE [dbo].[StaffMemberPaymentSet]
ADD CONSTRAINT [FK_InvoiceStaffMemberPayment]
    FOREIGN KEY ([Invoice_Id])
    REFERENCES [dbo].[InvoiceSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_InvoiceStaffMemberPayment'
CREATE INDEX [IX_FK_InvoiceStaffMemberPayment]
ON [dbo].[StaffMemberPaymentSet]
    ([Invoice_Id]);
GO

-- Creating foreign key on [StaffMemberPayment_Id] in table 'StaffMemberPaymentDeclarationSet'
ALTER TABLE [dbo].[StaffMemberPaymentDeclarationSet]
ADD CONSTRAINT [FK_StaffMemberPaymentStaffMemberPaymentDeclaration]
    FOREIGN KEY ([StaffMemberPayment_Id])
    REFERENCES [dbo].[StaffMemberPaymentSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StaffMemberPaymentStaffMemberPaymentDeclaration'
CREATE INDEX [IX_FK_StaffMemberPaymentStaffMemberPaymentDeclaration]
ON [dbo].[StaffMemberPaymentDeclarationSet]
    ([StaffMemberPayment_Id]);
GO

-- Creating foreign key on [LedgerBookingCode_Id] in table 'StaffMemberPaymentDeclarationSet'
ALTER TABLE [dbo].[StaffMemberPaymentDeclarationSet]
ADD CONSTRAINT [FK_LedgerBookingCodeStaffMemberPaymentDeclaration]
    FOREIGN KEY ([LedgerBookingCode_Id])
    REFERENCES [dbo].[LedgerBookingCodeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LedgerBookingCodeStaffMemberPaymentDeclaration'
CREATE INDEX [IX_FK_LedgerBookingCodeStaffMemberPaymentDeclaration]
ON [dbo].[StaffMemberPaymentDeclarationSet]
    ([LedgerBookingCode_Id]);
GO

-- Creating foreign key on [StaffMemberPayment_Id] in table 'StaffMemberTimeRegistrationSet'
ALTER TABLE [dbo].[StaffMemberTimeRegistrationSet]
ADD CONSTRAINT [FK_StaffMemberPaymentStaffMemberTimeRegistration]
    FOREIGN KEY ([StaffMemberPayment_Id])
    REFERENCES [dbo].[StaffMemberPaymentSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StaffMemberPaymentStaffMemberTimeRegistration'
CREATE INDEX [IX_FK_StaffMemberPaymentStaffMemberTimeRegistration]
ON [dbo].[StaffMemberTimeRegistrationSet]
    ([StaffMemberPayment_Id]);
GO

-- Creating foreign key on [StaffTimeRegistrationActivity_Id] in table 'StaffMemberTimeRegistrationSet'
ALTER TABLE [dbo].[StaffMemberTimeRegistrationSet]
ADD CONSTRAINT [FK_StaffTimeRegistrationActivityStaffMemberTimeRegistration]
    FOREIGN KEY ([StaffTimeRegistrationActivity_Id])
    REFERENCES [dbo].[StaffTimeRegistrationActivitySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StaffTimeRegistrationActivityStaffMemberTimeRegistration'
CREATE INDEX [IX_FK_StaffTimeRegistrationActivityStaffMemberTimeRegistration]
ON [dbo].[StaffMemberTimeRegistrationSet]
    ([StaffTimeRegistrationActivity_Id]);
GO

-- Creating foreign key on [StaffMemberPayment_Id] in table 'StaffMemberAdvancePaymentSet'
ALTER TABLE [dbo].[StaffMemberAdvancePaymentSet]
ADD CONSTRAINT [FK_StaffMemberPaymentStaffMemberAdvancePayment]
    FOREIGN KEY ([StaffMemberPayment_Id])
    REFERENCES [dbo].[StaffMemberPaymentSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StaffMemberPaymentStaffMemberAdvancePayment'
CREATE INDEX [IX_FK_StaffMemberPaymentStaffMemberAdvancePayment]
ON [dbo].[StaffMemberAdvancePaymentSet]
    ([StaffMemberPayment_Id]);
GO

-- Creating foreign key on [Ledger_Id] in table 'StaffMemberAdvancePaymentSet'
ALTER TABLE [dbo].[StaffMemberAdvancePaymentSet]
ADD CONSTRAINT [FK_LedgerStaffMemberAdvancePayment]
    FOREIGN KEY ([Ledger_Id])
    REFERENCES [dbo].[LedgerSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LedgerStaffMemberAdvancePayment'
CREATE INDEX [IX_FK_LedgerStaffMemberAdvancePayment]
ON [dbo].[StaffMemberAdvancePaymentSet]
    ([Ledger_Id]);
GO

-- Creating foreign key on [Invoice_Id] in table 'StaffMemberAdvancePaymentSet'
ALTER TABLE [dbo].[StaffMemberAdvancePaymentSet]
ADD CONSTRAINT [FK_InvoiceStaffMemberAdvancePayment]
    FOREIGN KEY ([Invoice_Id])
    REFERENCES [dbo].[InvoiceSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_InvoiceStaffMemberAdvancePayment'
CREATE INDEX [IX_FK_InvoiceStaffMemberAdvancePayment]
ON [dbo].[StaffMemberAdvancePaymentSet]
    ([Invoice_Id]);
GO

-- Creating foreign key on [Material_Id] in table 'FreightSortingMaterialSet'
ALTER TABLE [dbo].[FreightSortingMaterialSet]
ADD CONSTRAINT [FK_MaterialFreightSortingMaterial]
    FOREIGN KEY ([Material_Id])
    REFERENCES [dbo].[MaterialSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MaterialFreightSortingMaterial'
CREATE INDEX [IX_FK_MaterialFreightSortingMaterial]
ON [dbo].[FreightSortingMaterialSet]
    ([Material_Id]);
GO

-- Creating foreign key on [Material_Id] in table 'FreightWeighingMaterialSet'
ALTER TABLE [dbo].[FreightWeighingMaterialSet]
ADD CONSTRAINT [FK_MaterialFreightWeigingMaterial]
    FOREIGN KEY ([Material_Id])
    REFERENCES [dbo].[MaterialSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MaterialFreightWeigingMaterial'
CREATE INDEX [IX_FK_MaterialFreightWeigingMaterial]
ON [dbo].[FreightWeighingMaterialSet]
    ([Material_Id]);
GO

-- Creating foreign key on [Material_Id] in table 'RelationContractMaterialSet'
ALTER TABLE [dbo].[RelationContractMaterialSet]
ADD CONSTRAINT [FK_MaterialRelationContractMaterial]
    FOREIGN KEY ([Material_Id])
    REFERENCES [dbo].[MaterialSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MaterialRelationContractMaterial'
CREATE INDEX [IX_FK_MaterialRelationContractMaterial]
ON [dbo].[RelationContractMaterialSet]
    ([Material_Id]);
GO

-- Creating foreign key on [Material_Id] in table 'RelationPriceAgreementSet'
ALTER TABLE [dbo].[RelationPriceAgreementSet]
ADD CONSTRAINT [FK_MaterialRelationPriceAgreement]
    FOREIGN KEY ([Material_Id])
    REFERENCES [dbo].[MaterialSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MaterialRelationPriceAgreement'
CREATE INDEX [IX_FK_MaterialRelationPriceAgreement]
ON [dbo].[RelationPriceAgreementSet]
    ([Material_Id]);
GO

-- Creating foreign key on [StaffMemberPurchaser_Id] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [FK_StaffMemberOrder]
    FOREIGN KEY ([StaffMemberPurchaser_Id])
    REFERENCES [dbo].[StaffMemberSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StaffMemberOrder'
CREATE INDEX [IX_FK_StaffMemberOrder]
ON [dbo].[OrderSet]
    ([StaffMemberPurchaser_Id]);
GO

-- Creating foreign key on [Location_Id] in table 'MaterialSet'
ALTER TABLE [dbo].[MaterialSet]
ADD CONSTRAINT [FK_MaterialLocation]
    FOREIGN KEY ([Location_Id])
    REFERENCES [dbo].[LocationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MaterialLocation'
CREATE INDEX [IX_FK_MaterialLocation]
ON [dbo].[MaterialSet]
    ([Location_Id]);
GO

-- Creating foreign key on [LedgerId] in table 'InvoiceLineSet'
ALTER TABLE [dbo].[InvoiceLineSet]
ADD CONSTRAINT [FK_InvoiceLineLedger]
    FOREIGN KEY ([LedgerId])
    REFERENCES [dbo].[LedgerSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_InvoiceLineLedger'
CREATE INDEX [IX_FK_InvoiceLineLedger]
ON [dbo].[InvoiceLineSet]
    ([LedgerId]);
GO

-- Creating foreign key on [RelationWorkId] in table 'InvoiceLineSet'
ALTER TABLE [dbo].[InvoiceLineSet]
ADD CONSTRAINT [FK_RelationWorkInvoiceLine]
    FOREIGN KEY ([RelationWorkId])
    REFERENCES [dbo].[RelationWorkSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RelationWorkInvoiceLine'
CREATE INDEX [IX_FK_RelationWorkInvoiceLine]
ON [dbo].[InvoiceLineSet]
    ([RelationWorkId]);
GO

-- Creating foreign key on [InvoiceLine_Id] in table 'OrderLineSet'
ALTER TABLE [dbo].[OrderLineSet]
ADD CONSTRAINT [FK_InvoiceLineOrderLine]
    FOREIGN KEY ([InvoiceLine_Id])
    REFERENCES [dbo].[InvoiceLineSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_InvoiceLineOrderLine'
CREATE INDEX [IX_FK_InvoiceLineOrderLine]
ON [dbo].[OrderLineSet]
    ([InvoiceLine_Id]);
GO

-- Creating foreign key on [InvoiceId] in table 'InvoiceLineSet'
ALTER TABLE [dbo].[InvoiceLineSet]
ADD CONSTRAINT [FK_InvoiceLineInvoice]
    FOREIGN KEY ([InvoiceId])
    REFERENCES [dbo].[InvoiceSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_InvoiceLineInvoice'
CREATE INDEX [IX_FK_InvoiceLineInvoice]
ON [dbo].[InvoiceLineSet]
    ([InvoiceId]);
GO

-- Creating foreign key on [RelationId] in table 'RelationMaterialSet'
ALTER TABLE [dbo].[RelationMaterialSet]
ADD CONSTRAINT [FK_RelationRelationMaterials]
    FOREIGN KEY ([RelationId])
    REFERENCES [dbo].[RelationSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RelationRelationMaterials'
CREATE INDEX [IX_FK_RelationRelationMaterials]
ON [dbo].[RelationMaterialSet]
    ([RelationId]);
GO

-- Creating foreign key on [MaterialId] in table 'RelationMaterialSet'
ALTER TABLE [dbo].[RelationMaterialSet]
ADD CONSTRAINT [FK_MaterialRelationMaterials]
    FOREIGN KEY ([MaterialId])
    REFERENCES [dbo].[MaterialSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MaterialRelationMaterials'
CREATE INDEX [IX_FK_MaterialRelationMaterials]
ON [dbo].[RelationMaterialSet]
    ([MaterialId]);
GO

-- Creating foreign key on [LimitToLocationId] in table 'LedgerSet'
ALTER TABLE [dbo].[LedgerSet]
ADD CONSTRAINT [FK_LocationLedger]
    FOREIGN KEY ([LimitToLocationId])
    REFERENCES [dbo].[LocationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LocationLedger'
CREATE INDEX [IX_FK_LocationLedger]
ON [dbo].[LedgerSet]
    ([LimitToLocationId]);
GO

-- Creating foreign key on [MaterialForDirtId] in table 'LocationSet'
ALTER TABLE [dbo].[LocationSet]
ADD CONSTRAINT [FK_LocationMaterialDirt]
    FOREIGN KEY ([MaterialForDirtId])
    REFERENCES [dbo].[MaterialSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LocationMaterialDirt'
CREATE INDEX [IX_FK_LocationMaterialDirt]
ON [dbo].[LocationSet]
    ([MaterialForDirtId]);
GO

-- Creating foreign key on [MaterialForWorkId] in table 'LocationSet'
ALTER TABLE [dbo].[LocationSet]
ADD CONSTRAINT [FK_LocationMaterialWork]
    FOREIGN KEY ([MaterialForWorkId])
    REFERENCES [dbo].[MaterialSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LocationMaterialWork'
CREATE INDEX [IX_FK_LocationMaterialWork]
ON [dbo].[LocationSet]
    ([MaterialForWorkId]);
GO

-- Creating foreign key on [CashLedgerId] in table 'LocationSet'
ALTER TABLE [dbo].[LocationSet]
ADD CONSTRAINT [FK_LocationLedger1]
    FOREIGN KEY ([CashLedgerId])
    REFERENCES [dbo].[LedgerSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LocationLedger1'
CREATE INDEX [IX_FK_LocationLedger1]
ON [dbo].[LocationSet]
    ([CashLedgerId]);
GO

-- Creating foreign key on [BankLedgerId] in table 'LocationSet'
ALTER TABLE [dbo].[LocationSet]
ADD CONSTRAINT [FK_LocationBankLedger]
    FOREIGN KEY ([BankLedgerId])
    REFERENCES [dbo].[LedgerSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LocationBankLedger'
CREATE INDEX [IX_FK_LocationBankLedger]
ON [dbo].[LocationSet]
    ([BankLedgerId]);
GO

-- Creating foreign key on [Order_Id] in table 'MaterialMutationSet'
ALTER TABLE [dbo].[MaterialMutationSet]
ADD CONSTRAINT [FK_OrderMaterialMutation]
    FOREIGN KEY ([Order_Id])
    REFERENCES [dbo].[OrderSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderMaterialMutation'
CREATE INDEX [IX_FK_OrderMaterialMutation]
ON [dbo].[MaterialMutationSet]
    ([Order_Id]);
GO

-- Creating foreign key on [DefaultWeighingTariffBookingCode_Id] in table 'LocationSet'
ALTER TABLE [dbo].[LocationSet]
ADD CONSTRAINT [FK_LocationLedgerBookingCode]
    FOREIGN KEY ([DefaultWeighingTariffBookingCode_Id])
    REFERENCES [dbo].[LedgerBookingCodeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LocationLedgerBookingCode'
CREATE INDEX [IX_FK_LocationLedgerBookingCode]
ON [dbo].[LocationSet]
    ([DefaultWeighingTariffBookingCode_Id]);
GO

-- Creating foreign key on [RentalItemActivity_Id] in table 'InvoiceLineSet'
ALTER TABLE [dbo].[InvoiceLineSet]
ADD CONSTRAINT [FK_RentalItemActivityInvoiceLine]
    FOREIGN KEY ([RentalItemActivity_Id])
    REFERENCES [dbo].[RentalItemActivitySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RentalItemActivityInvoiceLine'
CREATE INDEX [IX_FK_RentalItemActivityInvoiceLine]
ON [dbo].[InvoiceLineSet]
    ([RentalItemActivity_Id]);
GO

-- Creating foreign key on [RelationSale_Id] in table 'LocationSet'
ALTER TABLE [dbo].[LocationSet]
ADD CONSTRAINT [FK_LocationRelation]
    FOREIGN KEY ([RelationSale_Id])
    REFERENCES [dbo].[RelationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LocationRelation'
CREATE INDEX [IX_FK_LocationRelation]
ON [dbo].[LocationSet]
    ([RelationSale_Id]);
GO

-- Creating foreign key on [RelationBuy_Id] in table 'LocationSet'
ALTER TABLE [dbo].[LocationSet]
ADD CONSTRAINT [FK_LocationRelation1]
    FOREIGN KEY ([RelationBuy_Id])
    REFERENCES [dbo].[RelationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LocationRelation1'
CREATE INDEX [IX_FK_LocationRelation1]
ON [dbo].[LocationSet]
    ([RelationBuy_Id]);
GO

-- Creating foreign key on [DefaultBailPriceBookingCode_Id] in table 'LocationSet'
ALTER TABLE [dbo].[LocationSet]
ADD CONSTRAINT [FK_LocationLedgerBookingCode1]
    FOREIGN KEY ([DefaultBailPriceBookingCode_Id])
    REFERENCES [dbo].[LedgerBookingCodeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LocationLedgerBookingCode1'
CREATE INDEX [IX_FK_LocationLedgerBookingCode1]
ON [dbo].[LocationSet]
    ([DefaultBailPriceBookingCode_Id]);
GO

-- Creating foreign key on [RentalItemActivity_Id] in table 'RelationAdvancePaymentSet'
ALTER TABLE [dbo].[RelationAdvancePaymentSet]
ADD CONSTRAINT [FK_RentalItemActivityRelationAdvancePayment]
    FOREIGN KEY ([RentalItemActivity_Id])
    REFERENCES [dbo].[RentalItemActivitySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RentalItemActivityRelationAdvancePayment'
CREATE INDEX [IX_FK_RentalItemActivityRelationAdvancePayment]
ON [dbo].[RelationAdvancePaymentSet]
    ([RentalItemActivity_Id]);
GO

-- Creating foreign key on [IsAlternativeRentalTypeFor_Id] in table 'RentalTypeRentalType'
ALTER TABLE [dbo].[RentalTypeRentalType]
ADD CONSTRAINT [FK_RentalTypeRentalType_RentalType]
    FOREIGN KEY ([IsAlternativeRentalTypeFor_Id])
    REFERENCES [dbo].[RentalTypeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AlternativeRentalTypes_Id] in table 'RentalTypeRentalType'
ALTER TABLE [dbo].[RentalTypeRentalType]
ADD CONSTRAINT [FK_RentalTypeRentalType_RentalType1]
    FOREIGN KEY ([AlternativeRentalTypes_Id])
    REFERENCES [dbo].[RentalTypeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RentalTypeRentalType_RentalType1'
CREATE INDEX [IX_FK_RentalTypeRentalType_RentalType1]
ON [dbo].[RentalTypeRentalType]
    ([AlternativeRentalTypes_Id]);
GO

-- Creating foreign key on [LedgerBookingCode_Id] in table 'LedgerClosureSet'
ALTER TABLE [dbo].[LedgerClosureSet]
ADD CONSTRAINT [FK_LedgerBookingCodeLedgerClosure]
    FOREIGN KEY ([LedgerBookingCode_Id])
    REFERENCES [dbo].[LedgerBookingCodeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LedgerBookingCodeLedgerClosure'
CREATE INDEX [IX_FK_LedgerBookingCodeLedgerClosure]
ON [dbo].[LedgerClosureSet]
    ([LedgerBookingCode_Id]);
GO

-- Creating foreign key on [StaffMember_Id] in table 'StaffMemberSecurityRole'
ALTER TABLE [dbo].[StaffMemberSecurityRole]
ADD CONSTRAINT [FK_StaffMemberSecurityRole_StaffMember]
    FOREIGN KEY ([StaffMember_Id])
    REFERENCES [dbo].[StaffMemberSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [SecurityRole_Id] in table 'StaffMemberSecurityRole'
ALTER TABLE [dbo].[StaffMemberSecurityRole]
ADD CONSTRAINT [FK_StaffMemberSecurityRole_SecurityRole]
    FOREIGN KEY ([SecurityRole_Id])
    REFERENCES [dbo].[SecurityRoleSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StaffMemberSecurityRole_SecurityRole'
CREATE INDEX [IX_FK_StaffMemberSecurityRole_SecurityRole]
ON [dbo].[StaffMemberSecurityRole]
    ([SecurityRole_Id]);
GO

-- Creating foreign key on [TransportUltimateSourceCustomer_Id] in table 'FreightSet'
ALTER TABLE [dbo].[FreightSet]
ADD CONSTRAINT [FK_FreightTransportUltimateSourceCustomer]
    FOREIGN KEY ([TransportUltimateSourceCustomer_Id])
    REFERENCES [dbo].[RelationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FreightTransportUltimateSourceCustomer'
CREATE INDEX [IX_FK_FreightTransportUltimateSourceCustomer]
ON [dbo].[FreightSet]
    ([TransportUltimateSourceCustomer_Id]);
GO

-- Creating foreign key on [TransportSourceCustomer_Id] in table 'FreightSet'
ALTER TABLE [dbo].[FreightSet]
ADD CONSTRAINT [FK_FreightTransportSourceCustomer]
    FOREIGN KEY ([TransportSourceCustomer_Id])
    REFERENCES [dbo].[RelationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FreightTransportSourceCustomer'
CREATE INDEX [IX_FK_FreightTransportSourceCustomer]
ON [dbo].[FreightSet]
    ([TransportSourceCustomer_Id]);
GO

-- Creating foreign key on [TransportCompanyCustomer_Id] in table 'FreightSet'
ALTER TABLE [dbo].[FreightSet]
ADD CONSTRAINT [FK_FreightTransportCompanyCustomer]
    FOREIGN KEY ([TransportCompanyCustomer_Id])
    REFERENCES [dbo].[RelationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FreightTransportCompanyCustomer'
CREATE INDEX [IX_FK_FreightTransportCompanyCustomer]
ON [dbo].[FreightSet]
    ([TransportCompanyCustomer_Id]);
GO

-- Creating foreign key on [TransportDestinationCustomer_Id] in table 'FreightSet'
ALTER TABLE [dbo].[FreightSet]
ADD CONSTRAINT [FK_FreightTransportDestinationCustomer]
    FOREIGN KEY ([TransportDestinationCustomer_Id])
    REFERENCES [dbo].[RelationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FreightTransportDestinationCustomer'
CREATE INDEX [IX_FK_FreightTransportDestinationCustomer]
ON [dbo].[FreightSet]
    ([TransportDestinationCustomer_Id]);
GO

-- Creating foreign key on [TransportDestructorCustomer_Id] in table 'FreightSet'
ALTER TABLE [dbo].[FreightSet]
ADD CONSTRAINT [FK_FreightTransportDestructorCustomer]
    FOREIGN KEY ([TransportDestructorCustomer_Id])
    REFERENCES [dbo].[RelationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FreightTransportDestructorCustomer'
CREATE INDEX [IX_FK_FreightTransportDestructorCustomer]
ON [dbo].[FreightSet]
    ([TransportDestructorCustomer_Id]);
GO

-- Creating foreign key on [PreferredLocation_Id] in table 'RelationSet'
ALTER TABLE [dbo].[RelationSet]
ADD CONSTRAINT [FK_RelationPreferredLocation]
    FOREIGN KEY ([PreferredLocation_Id])
    REFERENCES [dbo].[LocationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RelationPreferredLocation'
CREATE INDEX [IX_FK_RelationPreferredLocation]
ON [dbo].[RelationSet]
    ([PreferredLocation_Id]);
GO

-- Creating foreign key on [RelationContractMaterial_Id] in table 'ContractGuidanceMaterialMutationSet'
ALTER TABLE [dbo].[ContractGuidanceMaterialMutationSet]
ADD CONSTRAINT [FK_ContractGuidanceMaterialMutationRelationContractMaterial]
    FOREIGN KEY ([RelationContractMaterial_Id])
    REFERENCES [dbo].[RelationContractMaterialSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContractGuidanceMaterialMutationRelationContractMaterial'
CREATE INDEX [IX_FK_ContractGuidanceMaterialMutationRelationContractMaterial]
ON [dbo].[ContractGuidanceMaterialMutationSet]
    ([RelationContractMaterial_Id]);
GO

-- Creating foreign key on [MaterialMutation_Id] in table 'ContractGuidanceMaterialMutationSet'
ALTER TABLE [dbo].[ContractGuidanceMaterialMutationSet]
ADD CONSTRAINT [FK_MaterialMutationContractGuidanceMaterialMutation]
    FOREIGN KEY ([MaterialMutation_Id])
    REFERENCES [dbo].[MaterialMutationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MaterialMutationContractGuidanceMaterialMutation'
CREATE INDEX [IX_FK_MaterialMutationContractGuidanceMaterialMutation]
ON [dbo].[ContractGuidanceMaterialMutationSet]
    ([MaterialMutation_Id]);
GO

-- Creating foreign key on [RentLedger_Id] in table 'RentalItemActivitySet'
ALTER TABLE [dbo].[RentalItemActivitySet]
ADD CONSTRAINT [FK_RentLedgerRentalItemActivity]
    FOREIGN KEY ([RentLedger_Id])
    REFERENCES [dbo].[RentLedgerSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RentLedgerRentalItemActivity'
CREATE INDEX [IX_FK_RentLedgerRentalItemActivity]
ON [dbo].[RentalItemActivitySet]
    ([RentLedger_Id]);
GO

-- Creating foreign key on [RentLedger_Id] in table 'InvoiceSet'
ALTER TABLE [dbo].[InvoiceSet]
ADD CONSTRAINT [FK_RentLedgerInvoice]
    FOREIGN KEY ([RentLedger_Id])
    REFERENCES [dbo].[RentLedgerSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RentLedgerInvoice'
CREATE INDEX [IX_FK_RentLedgerInvoice]
ON [dbo].[InvoiceSet]
    ([RentLedger_Id]);
GO

-- Creating foreign key on [Relation_Id] in table 'RentLedgerSet'
ALTER TABLE [dbo].[RentLedgerSet]
ADD CONSTRAINT [FK_RentLedgerRelation]
    FOREIGN KEY ([Relation_Id])
    REFERENCES [dbo].[RelationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RentLedgerRelation'
CREATE INDEX [IX_FK_RentLedgerRelation]
ON [dbo].[RentLedgerSet]
    ([Relation_Id]);
GO

-- Creating foreign key on [Location_Id] in table 'RentLedgerSet'
ALTER TABLE [dbo].[RentLedgerSet]
ADD CONSTRAINT [FK_RentLedgerLocation]
    FOREIGN KEY ([Location_Id])
    REFERENCES [dbo].[LocationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RentLedgerLocation'
CREATE INDEX [IX_FK_RentLedgerLocation]
ON [dbo].[RentLedgerSet]
    ([Location_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------