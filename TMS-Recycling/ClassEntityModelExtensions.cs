using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects;
using System.Data;
using System.Data.Objects.DataClasses;
using System.Reflection;
using System.Runtime.Serialization;
using System.Collections;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data.EntityClient;
using System.Web.SessionState;

namespace TMS_Recycling
{

    public partial class ModelTMSContainer : ObjectContext
    {
        public HttpSessionState Session = null;
        public string connectString = ""; 

        public ModelTMSContainer(string connectionString, HttpSessionState xSession)
            : base(connectionString, "ModelTMSContainer")
        {
            this.Session = xSession;
            this.connectString = connectionString;
            this.ContextOptions.LazyLoadingEnabled = true;

            OnContextCreated();
        }

        #region Generic update logic
        // context created
        partial void OnContextCreated()
        {
            // Register the handler for the SavingChanges event.
            this.SavingChanges
                += new EventHandler(context_SavingChanges);
        }

        // SavingChanges event handler.
        private static void context_SavingChanges(object sender, EventArgs e)
        {
            // Ensure that we are passed an ObjectContext
            ModelTMSContainer context = sender as ModelTMSContainer;
            if (context != null)
            {

                // Validate the state of each entity in the context
                // before SaveChanges can succeed.
                foreach (ObjectStateEntry entry in
                    context.ObjectStateManager.GetObjectStateEntries(
                    EntityState.Added | EntityState.Modified))
                {
                    // Find an object state entry for a SalesOrderHeader object. 
                    if (!entry.IsRelationship)
                    {
                        EntityObject ObjectToCheck = entry.Entity as EntityObject;

                        // change the MdifyDateTime and ModifyUser property
                        PropertyInfo ModDT = ClassCustomBinding.GetProperty(ObjectToCheck, "ModifyDateTime");
                        PropertyInfo ModUser = ClassCustomBinding.GetProperty(ObjectToCheck, "ModifyUser");
                        PropertyInfo CreaUser = ClassCustomBinding.GetProperty(ObjectToCheck, "CreateUser");

                        if (ModDT != null)
                        {
                            ModDT.SetValue(ObjectToCheck, System.DateTime.Now, null);
                        }
                        if (context.Session != null)
                        {
                            ModDT.SetValue(ObjectToCheck, Common.CurrentClientDateTime(context.Session), null);
                        }

                        if ((ModUser != null) && (context.Session != null))
                        {
                            ModUser.SetValue(ObjectToCheck, context.Session["CurrentUserID"], null);
                        }

                        if ((CreaUser != null) && (context.Session != null))
                        {
                            string CheckString = CreaUser.GetValue(ObjectToCheck, null).ToString();
                            if (CheckString == Guid.Empty.ToString())
                            {
                                CreaUser.SetValue(ObjectToCheck, context.Session["CurrentUserID"], null);
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }

    #region ContractGuidanceMaterialMutation
    public partial class ContractGuidanceMaterialMutation
    {
        #region Constructors
        public ContractGuidanceMaterialMutation()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";

            IsCorrected = false;
        }
        #endregion
    }
    #endregion

    #region Freight
    public partial class Freight
    {
        #region Constructors
        public Freight()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";

            RequestedFreightEndDateTime = new DateTime(2000, 1, 1);
            RequestedFreightStartDateTime = new DateTime(2000, 1, 1);
            FreightDateTime = DateTime.Now;
            YourReference = "";
            FreightStatus = "Planned";
            FreightType = "Transport";
            OurDriverID = Guid.Empty;
            YourTruckPlate = "";
            YourDriverName = "";
            FreightDirection = "To warehouse";
            IsActive = true;

            TransportRole = "Trader";
            TransportNotificationTransportType = "Single";
            TransportNotificationRemovalType = "Use";
            TransportNotificationApprovedDestructor = false;
            TransportPlannedTransports = 1;
            TransportWrapping = "Not wrapped";
            TransportSpecialTreatment = "";
            TransportDRCode = "R4";
            TransportUsedTechnology = "";
            TransportReasonForExport = "";
            TransportType = "Road";
            TransportDestructionAction = "R4";
        }
        #endregion

        #region Support functions

        public void AssignFreightNumber(ModelTMSContainer _ControlObjectContext)
        {
            if (OurReference == 0)
            {
                OurReference = SystemSettingSet.GetNextOrderNumber(_ControlObjectContext);
            }
        }

        public static Freight SelectFreightByFreightId(Guid FreightId, ModelTMSContainer ControlObjectContext)
        {
            Freight frg = null;

            ObjectQuery<Freight> oqsm = ControlObjectContext.FreightSet.Where("it.Id=@id", new ObjectParameter("id", FreightId));
            if (oqsm.Count() > 0)
            {
                frg = oqsm.First();
            }

            return frg;
        }

        public static Freight SelectFreightByFreightNr(long FreightNr, ModelTMSContainer ControlObjectContext)
        {
            Freight frg = null;

            ObjectQuery<Freight> oqsm = ControlObjectContext.FreightSet.Where("it.OurReference=@id", new ObjectParameter("id", FreightNr));
            if (oqsm.Count() > 0)
            {
                frg = oqsm.First();
            }

            return frg;
        }

        public string FirstFreightWeighingDescription()
        {
            string Result = "";

            if (FreightWeighing.Count > 0)
            {
                FreightWeighing fw = FreightWeighing.ElementAt<FreightWeighing>(0);
                Result = fw.Description;
            }

            return Result;
        }

        public FreightSortingMaterial CheckForSortingMaterial(Material Mat, ModelTMSContainer _ControlObjectContext)
        {
            FreightSortingMaterial fsm = null;

            foreach (FreightSortingMaterial freightsm in FreightSortingMaterial)
            {
                if (freightsm.Material == Mat)
                {
                    fsm = freightsm;
                    break;
                }
            }

            return fsm;
        }

        #endregion

        #region Totals functions
        public void RecalcTotalWeightFromSorting ()
        {
            TotalNetWeight = 0;
            foreach (FreightSortingMaterial fsm in FreightSortingMaterial)
            {
                TotalNetWeight = TotalNetWeight + fsm.Weight;
            }
        }

        public void RecalcTotalWeightFromWeighing()
        {
            TotalNetWeight = 0;
            foreach (FreightWeighing fw in FreightWeighing)
            {
                TotalNetWeight = TotalNetWeight + fw.TotalNetWeight();
            }
        }
        #endregion
    }
    #endregion

    #region FreightSortingMaterial
    public partial class FreightSortingMaterial
    {
        #region Constructors
        public FreightSortingMaterial()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";
        }
        #endregion

        #region Totals functions
        public void RecalcTotals()
        {
            Weight = Math.Abs(GrossWeight - TarraWeight);
        }
        #endregion

        #region Support functions
        public bool RelevantLine()
        {
            // determines if this line is relevant for this weighing
            return ((TarraWeight != 0) || (GrossWeight != 0) || (Weight != 0));
        }
        #endregion
    }
    #endregion

    #region FreightWeighing
    public partial class FreightWeighing
    {
        #region Constructors
        public FreightWeighing()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";

            WeighingDateTime1 = new DateTime(2000, 1, 1);
            WeighingDateTime2 = new DateTime(2000, 1, 1);
            Key1 = "";
            Key2 = "";
        }
        #endregion

        #region Totals functions 
        public double TotalNetWeight()
        {
            double Total = 0;
            foreach (FreightWeighingMaterial fwm in FreightWeighingMaterial)
            {
                fwm.RecalcTotals();
                Total = Total + fwm.NetWeight;
            }
            return Total;
        }
        #endregion
    }
    #endregion

    #region FreightWeighingMaterial
    public partial class FreightWeighingMaterial
    {
        #region Constructors
        public FreightWeighingMaterial()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";
        }
        #endregion

        #region Totals functions
        public void RecalcTotals()
        {
            NetWeight = GrossWeight - TarraWeight;
        }
        #endregion
    }
    #endregion

    #region Invoice
    public partial class Invoice
    {
        #region Constructors
        public Invoice()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";

            InvoiceNote = "";
            YourReference = "";

            InvoiceType = "Sell";
            InvoiceStatus = "Open";
            InvoiceSubType = "Purchase";

            BookingDateTime = DateTime.Now;
            IsCorrected = false;

            GroupCode = Guid.NewGuid();
        }
        #endregion

        #region Clone functions
        public Invoice CloneToNew(ModelTMSContainer _ControlObjectContext, Boolean IsCorrectedSetting, Hashtable ReplugList, DateTime NewBookingDateTime)
        {
            Invoice NewInvoice = new Invoice();

            // create a replug list which allows for replugging of old links to new links
            Hashtable _ReplugList = null;
            if (ReplugList == null)
            {
                _ReplugList = new Hashtable();
            }
            else{
                _ReplugList = ReplugList;
            }

            Common.CloneProperties(this, NewInvoice);
            NewInvoice.IsCorrected = IsCorrectedSetting;
            NewInvoice.InvoiceNumber = SystemSettingSet.GetNextInvoiceNumber(_ControlObjectContext);
            NewInvoice.Ledger = Ledger;
            NewInvoice.Relation = Relation;
            NewInvoice.Location = Location;
            NewInvoice.BookingDateTime = NewBookingDateTime;
            foreach (StaffMemberPayment staffmp in StaffMemberPayment.ToArray()) { NewInvoice.StaffMemberPayment.Add(staffmp); }
            foreach (StaffMemberAdvancePayment CopyObj in StaffMemberAdvancePayment.ToArray()) { NewInvoice.StaffMemberAdvancePayment.Add(CopyObj); }
            _ControlObjectContext.AddToInvoiceSet(NewInvoice);
            _ReplugList.Add(this, NewInvoice);

            // clone the invoice lines and add them to the New invoice
            if (this.InvoiceLine.Count > 0)
            {
                foreach(InvoiceLine il in this.InvoiceLine.ToArray()) 
                {
                    InvoiceLine NL = il.CloneToNew(_ControlObjectContext, _ReplugList);
                    NL.Invoice = NewInvoice;
                    NewInvoice.InvoiceLine.Add(NL);
                }
            }

            // clone the orders and add them to the New invoice
            if (this.Order.Count > 0)
            {
                foreach (Order OldOrder in this.Order.ToArray())
                {
                    Order NewOrder = OldOrder.CloneToNew(_ControlObjectContext, IsCorrectedSetting, _ReplugList, NewBookingDateTime);

                    NewOrder.Invoice = NewInvoice;
                    NewInvoice.Order.Add(NewOrder);
                }
            }

            return NewInvoice;
        }

        #endregion

        #region Totals functions
        public void RecalcTotals()
        {
            double LinePrice = 0, LineVATPercentage = 0;
            int i = 1;

            Price = 0;
            VATPrice = 0;
            TotalPrice = 0;
            PriceWithoutDiscount = 0;
            foreach (InvoiceLine il in InvoiceLine)
            {

                il.CalcLinePriceAndVATPercentageWithInvoiceDiscount(DiscountPercentage, out LinePrice, out LineVATPercentage);
                il.LineNumber = i;
                i = i + 1;

                PriceWithoutDiscount = PriceWithoutDiscount + il.PriceWithDiscount; // but incuding the line discount !!!
                Price = LinePrice + Price;
                VATPrice = LineVATPercentage + VATPrice;
            }
            TotalPrice = Price + VATPrice;

            PriceWithoutDiscount = Math.Round(PriceWithoutDiscount, 2);
            Price = Math.Round(Price, 2);
            VATPrice = Math.Round(VATPrice, 2);
            TotalPrice = Math.Round( TotalPrice, 2);
        }

        #endregion

        #region Add/remove order from invoice
        public void AddOrderToInvoice(ModelTMSContainer _ControlObjectContext, Order OrderToAdd)
        {
            if (Order.Contains(OrderToAdd))
            {
                RemoveOrderFromInvoice(_ControlObjectContext, OrderToAdd);
            }

            foreach (OrderLine ol in OrderToAdd.OrderLine)
            {
                InvoiceLine il = new InvoiceLine();
                il.Invoice = this;
                il.OrderLine = ol;
                il.Description = ol.Description;
                il.OriginalPrice = ol.PriceExVAT;
                //il.VATPercentage = ol.Material.VATPercentage;
                il.Amount = ol.Amount;
                il.PricePerUnit = ol.PricePerUnit;
                il.OrderLine = ol;
                il.Material = ol.Material;
                il.Ledger = Ledger;

                il.VATPercentage = 0;
                if (this.Relation.MustPayVat())
                {
                    il.VATPercentage = il.Material.VATPercentage;
                }

                if (OrderToAdd.OrderType=="Buy") 
                {
                    il.LedgerBookingCode = ol.Material.PurchaseLedgerBookingCode;
                }
                else
                {
                    il.LedgerBookingCode = ol.Material.SalesLedgerBookingCode;
                }

                il.RecalcAmounts();
                _ControlObjectContext.AddToInvoiceLineSet(il);
                il.LineNumber = InvoiceLine.Count;
            }
            Order.Add(OrderToAdd);

            RecalcTotals();
        }

        public void RemoveOrderFromInvoice(ModelTMSContainer _ControlObjectContext, Order OrderToRemove)
        {
            InvoiceLine[] InvoiceLines = InvoiceLine.ToArray<InvoiceLine>();

            foreach (InvoiceLine il in InvoiceLines)
            {
                if (il.OrderLine.Order == OrderToRemove)
                {
                    il.OrderLine.Order.Invoice = null;
                    _ControlObjectContext.DeleteObject(il);
                }
            }

            RecalcTotals();
        }

        public void RemoveAllOrdersFromInvoice(ModelTMSContainer _ControlObjectContext)
        {
            InvoiceLine[] InvoiceLines = InvoiceLine.ToArray<InvoiceLine>();

            foreach (InvoiceLine il in InvoiceLines)
            {
                if ((il.OrderLine != null) && (il.OrderLine.Order != null) && (il.OrderLine.Order.Invoice != null))
                {
                    il.OrderLine.Order.Invoice = null;
                }
                _ControlObjectContext.DeleteObject(il);
            }

            RecalcTotals();
        }

        #endregion

        #region Rent functions in invoices
        public void UnlinkRentalItemActivities(ModelTMSContainer ControlObjectContext)
        {
            foreach (InvoiceLine il in InvoiceLine)
            {
                il.RentalItemActivity = null;
            }
        }

        #endregion

        #region Process invoice methods
        public void UnlinkFreights()
        {
            // unlink all freights from the orders in this invoice
            foreach (Order or in Order)
            {
                or.UnlinkFreights();
            }
        }

        private void ProcessInvoiceWithCorrection(ModelTMSContainer _ControlObjectContext, Boolean IsCorrection, System.Guid LedgerMutationGroupCode, DateTime LedgerMutDateTime)
        {
            // set the multiplier (1=normal processing, -1= correction)
            int Multiplier = 1;
            if (IsCorrection) { Multiplier = -1; }

            // recalc the totals on this invoice
            RecalcTotals();

            // check if we may proceed
            Boolean Proceed = false;
            if ((IsCorrection) && (InvoiceStatus == "Paid")) { Proceed = true; }
            if ((!IsCorrection) && ((InvoiceStatus == "Open") || (InvoiceStatus == "PPaid")) ) { Proceed = true; }

            // is there a order linked to this invoice ? Then check if this is processed
            if (Proceed)
            {
                if (Order.Count != 0)
                {
                    foreach (Order or in Order)
                    {
                        or.IsCorrected = this.IsCorrected;

                        if (!IsCorrection)
                        {
                            or.ProcessOrder(_ControlObjectContext, LedgerMutationGroupCode, LedgerMutDateTime);
                        }
                        else
                        {
                            or.UnprocessOrder(_ControlObjectContext, LedgerMutationGroupCode, LedgerMutDateTime);
                        }
                    }
                }
            }

            if (Proceed)
            {
                // check invoice number
                GenerateInvoiceNumber(_ControlObjectContext);

                // hide invoice from user if it is corrected
                IsCorrected = IsCorrection;
                GroupCode = LedgerMutationGroupCode;

                // group the amount of money per ledger and per booking code to process, do this by iterating over the invoice lines
                Hashtable LedgerMutationList = new Hashtable(); // contains a list of all new ledgermutations
                Hashtable TotalLedgerMutationList = new Hashtable(); // contains a list of ledger ids and the total amounts of money that have to be corrected
                Hashtable TotalLedgerBookingCodeMutationList = new Hashtable(); // ditto for ledger booking codes
                Hashtable TotalRelationAdvancePaymentList = new Hashtable(); // ditto for advance payments for a relation
                Hashtable TotalRelationWorkList = new Hashtable(); // ditto for work for a relation
                ArrayList LedgerList = new ArrayList();
                ArrayList LedgerBookingCodeList = new ArrayList();
                foreach (InvoiceLine il in InvoiceLine)
                {
                    // locate the ledger and ledger booking code for this line. 
                    Ledger lg = il.Ledger;
                    if (lg == null)
                    {
                        lg = il.Invoice.Location.BankLedger; 
                    }
                    if (LedgerList.IndexOf(lg) < 0)
                    {
                        LedgerList.Add(lg);
                    }
                    LedgerBookingCode lbc = il.LedgerBookingCode;
                    if (lbc == null) // if there is no ledgerbooking code set then select the first one (at random)
                    {
                        lbc = _ControlObjectContext.LedgerBookingCodeSet.First();
                    }
                    if (LedgerBookingCodeList.IndexOf(lbc) < 0)
                    {
                        LedgerBookingCodeList.Add(lbc);
                    }

                    // create the mutation if not there
                    if (!LedgerMutationList.ContainsKey(lg.Id + "." + lbc.Id))
                    {
                        LedgerMutation NewMutation = new LedgerMutation();
                        LedgerMutationList.Add(lg.Id + "." + lbc.Id, NewMutation);
                        if (IsCorrection)
                        {
                            NewMutation.Description = "INVCOR/" + InvoiceNumber + " / " + Description;
                        }
                        else
                        {
                            NewMutation.Description = "INV/" + InvoiceNumber + " / " + Description;
                        }
                        NewMutation.GroupCode = Id;
                        NewMutation.IsCorrection = IsCorrection;
                        NewMutation.BookingType = InvoiceType;
                        NewMutation.IsEditable = false;
                        NewMutation.BookingDateTime = LedgerMutDateTime;
                        NewMutation.AmountEXVat = 0;
                        NewMutation.VATAmount = 0;
                        NewMutation.TotalAmount = 0;
                        NewMutation.Ledger = lg;
                        NewMutation.Relation = Relation;
                        NewMutation.Location = Location;
                        NewMutation.LedgerBookingCode = lbc;
                        NewMutation.Ledger = lg;
                        NewMutation.GroupCode = GroupCode;
                        _ControlObjectContext.AddToLedgerMutationSet(NewMutation);
                    }

                    // add the amounts to this mutations
                    LedgerMutation CurrMutation = LedgerMutationList[lg.Id + "." + lbc.Id] as LedgerMutation;
                    // Recalc the amounts for this invoiceline with an optional invoice discount (which may or may not be applicable to this invoiceline)
                    double TempExVATAmount, TempVATAmount;
                    il.CalcLinePriceAndVATPercentageWithInvoiceDiscount(DiscountPercentage, out TempExVATAmount, out TempVATAmount);
                    // add amounts to mutation
                    double TotalMutationAmountExVat = (TempExVATAmount * Multiplier);
                    CurrMutation.AmountEXVat = CurrMutation.AmountEXVat + TotalMutationAmountExVat;
                    CurrMutation.VATAmount = CurrMutation.VATAmount + (TempVATAmount * Multiplier);
                    double TotalMutationAmount = ((TempVATAmount + TempExVATAmount) * Multiplier);
                    CurrMutation.TotalAmount = CurrMutation.TotalAmount + TotalMutationAmount; 
                    CurrMutation.RoundNumbers();

                    // update the relation advance payments, relation work, ledgerbookingcode and ledger totals
                    if (!TotalLedgerMutationList.Contains(lg.Id)) { TotalLedgerMutationList.Add(lg.Id, "0"); }
                    if (!TotalLedgerBookingCodeMutationList.Contains(lbc.Id)) { TotalLedgerBookingCodeMutationList.Add(lbc.Id, "0"); }

                    // ledger totals
                    double TempDouble = Convert.ToDouble(TotalLedgerMutationList[lg.Id] as String);
                    if (InvoiceType == "Buy") { TempDouble = TempDouble - TotalMutationAmount; } else { TempDouble = TempDouble + TotalMutationAmount; }
                    TotalLedgerMutationList[lg.Id] = TempDouble.ToString();

                    // ledgerbooking code totals
                    TempDouble = Convert.ToDouble(TotalLedgerBookingCodeMutationList[lbc.Id] as String);
                    if (InvoiceType == "Buy") { TempDouble = TempDouble - TotalMutationAmountExVat; } else { TempDouble = TempDouble + TotalMutationAmountExVat; }
                    TotalLedgerBookingCodeMutationList[lbc.Id] = TempDouble.ToString();

                    // relation advance payment
                    if (il.RelationAdvancePayment != null)
                    {
                        if (!TotalRelationAdvancePaymentList.Contains(il.RelationAdvancePayment.Id)) { TotalRelationAdvancePaymentList.Add(il.RelationAdvancePayment.Id, "0"); }
                        TempDouble = Convert.ToDouble(TotalRelationAdvancePaymentList[il.RelationAdvancePayment.Id] as String);
                        if (InvoiceType == "Buy") { TempDouble = TempDouble - il.TotalPrice; } else { TempDouble = TempDouble  + il.TotalPrice; }
                        TotalRelationAdvancePaymentList[il.RelationAdvancePayment.Id] = TempDouble.ToString();
                        if (IsCorrection) { il.RelationAdvancePayment.IsPaidBack = false; }
                    }

                    // relation work
                    if (il.RelationWork != null)
                    {
                        if (!TotalRelationWorkList.Contains(il.RelationWork.Id)) { TotalRelationWorkList.Add(il.RelationWork.Id, "0"); }
                        TempDouble = Convert.ToDouble(TotalRelationWorkList[il.RelationWork.Id] as String);
                        if (InvoiceType == "Buy") { TempDouble = TempDouble - il.TotalPrice; } else { TempDouble = TempDouble + il.TotalPrice; }
                        TotalRelationWorkList[il.RelationWork.Id] = TempDouble.ToString();
                        if (IsCorrection) { il.RelationWork.IsActive = true; }
                    }

                }

                // all ledgermutations are now known
                // update the totals
                String SQLquery = "";
                foreach (DictionaryEntry de in TotalLedgerMutationList)
                {
                    SqlParameter UpdateAmount = new SqlParameter("updateamount", Convert.ToDouble(de.Value) );
                    UpdateAmount.DbType = DbType.Double;
                    SQLquery = "update LedgerSet set ledgerlevel = ROUND(ledgerlevel + @updateamount,2) where Id = @LedgerID";
                    _ControlObjectContext.ExecuteStoreCommand(SQLquery, UpdateAmount, new SqlParameter("LedgerID", de.Key));
                }
                foreach (DictionaryEntry de in TotalLedgerBookingCodeMutationList)
                {
                    SqlParameter UpdateAmount = new SqlParameter("updateamount", Convert.ToDouble(de.Value) );
                    UpdateAmount.DbType = DbType.Double;
                    SQLquery = "update LedgerBookingCodeSet set ledgerlevel = ROUND(ledgerlevel + @updateamount,2) where Id = @LedgerID";
                    _ControlObjectContext.ExecuteStoreCommand(SQLquery, UpdateAmount, new SqlParameter("LedgerID", de.Key));
                }
                foreach (DictionaryEntry de in TotalRelationAdvancePaymentList)
                {
                    int APMultiplier = IsCorrection ? -1 : 1;
                    SqlParameter UpdateAmount = new SqlParameter("updateamount", Math.Abs(Convert.ToDouble(de.Value)) * APMultiplier);
                    UpdateAmount.DbType = DbType.Double;
                    SQLquery = "update RelationAdvancePaymentSet set AmountPaidBack = AmountPaidBack + @updateamount where Id = @LedgerID";
                    _ControlObjectContext.ExecuteStoreCommand(SQLquery, UpdateAmount, new SqlParameter("LedgerID", de.Key));
                    SQLquery = "update RelationAdvancePaymentSet set IsPaidBack = 1 where (Id = @LedgerID) and (AmountPaidBack >= Amount)";
                    _ControlObjectContext.ExecuteStoreCommand(SQLquery, new SqlParameter("LedgerID", de.Key));
                }
                foreach (DictionaryEntry de in TotalRelationWorkList)
                {
                    int WorkMultiplier = IsCorrection ? -1 : 1;
                    SqlParameter UpdateAmount = new SqlParameter("updateamount", Math.Abs(Convert.ToDouble(de.Value)) * WorkMultiplier);
                    UpdateAmount.DbType = DbType.Double;
                    SQLquery = "update RelationWorkSet set AmountPaidBack = AmountPaidBack + @updateamount where Id = @LedgerID";
                    _ControlObjectContext.ExecuteStoreCommand(SQLquery, UpdateAmount, new SqlParameter("LedgerID", de.Key));
                    SQLquery = "update RelationWorkSet set IsActive = 0 where (Id = @LedgerID) and (AmountPaidBack >= TotalAmount)";
                    _ControlObjectContext.ExecuteStoreCommand(SQLquery, new SqlParameter("LedgerID", de.Key));
                }

                // if a part of this invoice was already partially paid then correct this, since we have processed the invoice at full amounts
                if (AlreadyPaid != 0)
                {
                    LedgerMutation lm = new LedgerMutation();
                    _ControlObjectContext.AddToLedgerMutationSet(lm);
                    lm.Ledger = Ledger;
                    lm.Description = "INV / PPaid / Correctie reeds betaald bedrag factuur " + InvoiceNumber.ToString();
                    lm.GroupCode = GroupCode;
                    lm.BookingType = InvoiceType;
                    lm.AmountEXVat = (-AlreadyPaid) * Multiplier;
                    lm.VATAmount = 0;
                    lm.TotalAmount = lm.AmountEXVat;
                    lm.Process(_ControlObjectContext);
                }

                // reset the invoice status
                if (IsCorrection)
                {
                    InvoiceStatus = "Open";
                }
                else
                {
                    InvoiceStatus = "Paid";
                }
            }

            // make sure that we got the right corrected status
            IsCorrected = IsCorrection;

            // recalc the totals on this invoice to make sure any changes in the invoiceline prices are reflected in the invoice totals
            RecalcTotals();
        }

        public void GenerateInvoiceNumber(ModelTMSContainer _ControlObjectContext)
        {
            if (InvoiceNumber <= 0)
            {
                InvoiceNumber = SystemSettingSet.GetNextInvoiceNumber(_ControlObjectContext);
            }
        }

        public void ProcessInvoice(ModelTMSContainer _ControlObjectContext, System.Guid LedgerMutationGroupCode, bool AllInvoiceLinesToInvoiceLedger, DateTime LedgerMutDateTime )
        {
            if (AllInvoiceLinesToInvoiceLedger) { AllInvoiceLinesToSameLedger(Ledger); }
            ProcessInvoiceWithCorrection(_ControlObjectContext, false, LedgerMutationGroupCode, LedgerMutDateTime);
        }

        public void UnprocessInvoice(ModelTMSContainer _ControlObjectContext, System.Guid LedgerMutationGroupCode, DateTime LedgerMutDateTime)
        {
            // unlink rental item activiteis belonging to this invoice orderlines and delete them from the database
            UnlinkRentalItemActivities(_ControlObjectContext);

            ProcessInvoiceWithCorrection(_ControlObjectContext, true, LedgerMutationGroupCode, LedgerMutDateTime);
        }

        public void UnprocessInvoiceWithOrdersAsOptional(ModelTMSContainer _ControlObjectContext, System.Guid LedgerMutationGroupCode, DateTime LedgerMutDateTime)
        {
            if (InvoiceStatus != "Open")
            { // this invoice is already processed. Unprocess and unprocess the orders.
                ProcessInvoiceWithCorrection(_ControlObjectContext, true, LedgerMutationGroupCode, LedgerMutDateTime);
            }
            else
            { // this invoice is not processed yet, so just correct it and unlink the orders so they can be linked to other invoices.
                UnlinkOrdersFromInvoice();
            }
        }

        public void AllInvoiceLinesToSameLedger(Ledger ledg)
        {
            foreach (InvoiceLine il in InvoiceLine)
            {
                il.Ledger = ledg;
            }
        }

        private void UnlinkOrdersFromInvoice()
        {
            if (Order.Count != 0)
            {
                Order[] Orders = Order.ToArray<Order>();
                foreach (Order or in Orders)
                {
                    or.Invoice = null;
                }
            }
        }

        public void AddAdvancePaymentCorrection(ModelTMSContainer _ControlObjectContext, RelationAdvancePayment rap, double CorrectionAmount, string InvoiceDescription)
        {
            InvoiceLine NewInvoiceLine = new InvoiceLine();

            NewInvoiceLine.Invoice = this;
            NewInvoiceLine.OrderLine = null;
            NewInvoiceLine.Material = null;
            NewInvoiceLine.Description = InvoiceDescription;
            NewInvoiceLine.OriginalPrice = CorrectionAmount;
            NewInvoiceLine.AllowDiscount = false;
            NewInvoiceLine.RelationAdvancePayment = rap;
            NewInvoiceLine.Ledger = rap.Ledger;
            NewInvoiceLine.LedgerBookingCode = rap.LedgerBookingCode;
            NewInvoiceLine.RecalcAmounts();

            _ControlObjectContext.AddToInvoiceLineSet(NewInvoiceLine);
            NewInvoiceLine.LineNumber = InvoiceLine.Count;
            RecalcTotals();
        }

        public void AddWorkCorrection(ModelTMSContainer _ControlObjectContext, RelationWork rap, double CorrectionAmount, string InvoiceDescription)
        {
            InvoiceLine NewInvoiceLine = new InvoiceLine();

            NewInvoiceLine.Invoice = this;
            NewInvoiceLine.OrderLine = null;
            NewInvoiceLine.Material = null;
            NewInvoiceLine.Description = InvoiceDescription;
            NewInvoiceLine.OriginalPrice = CorrectionAmount;
            NewInvoiceLine.AllowDiscount = false;
            NewInvoiceLine.RelationWork = rap;
            NewInvoiceLine.Ledger = Ledger;
            NewInvoiceLine.VATPercentage = 0;
            if (rap.IsVATApplicable) { NewInvoiceLine.VATPercentage = rap.VATPercentage; }
            NewInvoiceLine.LedgerBookingCode = rap.LedgerBookingCode;
            NewInvoiceLine.RecalcAmounts();

            _ControlObjectContext.AddToInvoiceLineSet(NewInvoiceLine);
            NewInvoiceLine.LineNumber = InvoiceLine.Count;
            RecalcTotals();
        }

        #endregion

    }
    #endregion

    #region InvoiceLine
    public partial class InvoiceLine
    {
        #region Constructors
        public InvoiceLine()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";

            AllowDiscount = true;
        }
        #endregion

        #region Clone functions
        public InvoiceLine CloneToNew(ModelTMSContainer _ControlObjectContext, Hashtable ReplugList)
        {
            InvoiceLine New = new InvoiceLine();

            Common.CloneProperties(this, New);
            New.LedgerBookingCode = LedgerBookingCode;
            New.Material = Material;
            New.RelationAdvancePayment = RelationAdvancePayment;
            New.Ledger = Ledger;
            New.RelationWork = RelationWork;

            if (ReplugList != null)
            {
                ReplugList.Add(this, New);
            }

            return New;
        }
        #endregion

        #region Totals functions
        public void RecalcAmounts()
        {
            double DiscountedPrice ;

            if (Amount != 0)
            {
                OriginalPrice = Math.Round(Amount * PricePerUnit, 2);
                
            }
            PriceWithDiscount = OriginalPrice;

            DiscountedPrice = PriceWithDiscount;

            if ((DiscountPercentage != 0) && AllowDiscount)
            {
                DiscountedPrice = PriceWithDiscount - (PriceWithDiscount * (DiscountPercentage / 100));
            }

            PriceWithDiscount = DiscountedPrice; ;
            VATPrice = Math.Round(DiscountedPrice * (VATPercentage / 100), 2);
            TotalPrice = Math.Round(DiscountedPrice + VATPrice, 2);
        }

        public void CalcLinePriceAndVATPercentageWithInvoiceDiscount(double InvoiceDiscount, out double NewLinePrice, out double NewLineVATPrice)
        {
            double DiscountedPrice;

            RecalcAmounts();
            DiscountedPrice = PriceWithDiscount;

            // and now the additional invoice discount
            if ((InvoiceDiscount != 0) && AllowDiscount)
            {
                DiscountedPrice = DiscountedPrice - (DiscountedPrice * (InvoiceDiscount / 100));
            }

            NewLineVATPrice = Math.Round(DiscountedPrice * (VATPercentage / 100), 2);
            NewLinePrice = Math.Round(DiscountedPrice, 2);
        }

        #endregion
    }
    #endregion

    #region Ledger
    public partial class Ledger
    {
        #region Constructors
        public Ledger()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";

            Bank = "";
            BankAccount = "";
            BankBIC = "";
            BankIBAN = "";

            LedgerType = "Bank";
            LedgerCurrency = "Eur";

            IsDebugLedger = false;
        }
        #endregion
    }
    #endregion

    #region LedgerBookingCode
    public partial class LedgerBookingCode
    {
        #region Constructors
        public LedgerBookingCode()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";

            LedgerCurrency = "Eur";
            IsDebugLedgerCode = false;
            IsActive = true;
        }
        #endregion

        #region Rounding
        public void RoundNumbers()
        {
            LedgerLevel = Math.Round(LedgerLevel, 2);
        }
        #endregion
    }
    #endregion

    #region LedgerCheck
    public partial class LedgerCheck
    {
        #region Constructors
        public LedgerCheck()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";

            CheckDate = DateTime.Now;
            IsLedgerCorrected = false;
            CorrectionAmount = 0;
        }
        #endregion
    }
    #endregion

    #region LedgerClosure
    public partial class LedgerClosure
    {
        #region Constructors
        public LedgerClosure()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";

            ClosureDate = DateTime.Now;
            IsCorrection = false;
        }
        #endregion

        #region Recalc functions
        public void RecalcTotals(ModelTMSContainer Context, bool AutoCloseYear, bool WithoutVat)
        {
            DateTime StartDate, EndDate;
            EndDate = ClosureDate;
            StartDate = ClosureDate.AddDays(-1);

            // read the Buy totals
            ObjectParameter Par1 = new ObjectParameter("StartDate", StartDate);
            ObjectParameter Par2 = new ObjectParameter("EndDate", EndDate);
            string LinkType = Ledger != null ? "Ledger" : "LedgerBookingCode";
            ObjectParameter Par3;
            if (LinkType == "Ledger")
            {
                Par3 = new ObjectParameter("Id", Ledger.Id);
            }
            else
            {
                Par3 = new ObjectParameter("Id", LedgerBookingCode.Id);
            }

            ObjectQuery<LedgerMutation> qry = Context.LedgerMutationSet.Where("(it.BookingDateTime between @StartDate and @EndDate) and (it." + LinkType + 
                                              ".Id = @Id)", Par1, Par2, Par3);

            // calc
            LedgerLevel = 0;
            foreach (LedgerMutation mm in qry)
            {
                if (mm.BookingType == "Buy")
                {
                    if (WithoutVat)
                    {
                        LedgerLevel = LedgerLevel - mm.AmountEXVat;
                    }
                    else
                    {
                        LedgerLevel = LedgerLevel - mm.TotalAmount;
                    }
                }
                else //Sell
                {
                    if (WithoutVat)
                    {
                        LedgerLevel = LedgerLevel + mm.AmountEXVat;
                    }
                    else
                    {
                        LedgerLevel = LedgerLevel + mm.TotalAmount;
                    }
                }
            }
            LedgerDelta = LedgerLevel;

            // locate the previous closure (if any) and update this mutation record
            ObjectQuery<LedgerClosure> mcQry = Context.LedgerClosureSet.Where("(it.ClosureDate = @StartDate) and (it."+LinkType+".Id = @Id)", Par1, Par3);
            if (mcQry.Count() > 0)
            {
                LedgerClosure mc = mcQry.First<LedgerClosure>();
                LedgerLevel = LedgerLevel + mc.LedgerLevel;
            }

            // round everything
            LedgerLevel = Math.Round(LedgerLevel, 4);
            LedgerDelta = Math.Round(LedgerDelta, 4);

            // auto close year
            if (AutoCloseYear)
            {
                if ((ClosureDate.Month == 12) && (ClosureDate.Day == 31))
                {
                    IsCorrection = true;
                    OriginalLedgerLevel = LedgerLevel;
                    LedgerLevel = 0;
                }
            }
        }
        #endregion
    
    }
    #endregion

    #region LedgerMutation
    public partial class LedgerMutation
    {
        #region Constructors
        public LedgerMutation()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";

            IsEditable = false;
            IsCorrection = false;
            GroupCode = Guid.Empty;
            BookingDateTime = DateTime.Now;

        }
        #endregion

        public void RoundNumbers()
        {
            AmountEXVat = Math.Round(AmountEXVat,2);
            VATAmount = Math.Round(VATAmount, 2);
            TotalAmount = Math.Round(TotalAmount, 2);
        }

        public void Process(ModelTMSContainer _ControlObjectContext)
        {
            RoundNumbers();

            double NewAmountExVat = AmountEXVat;
            String SQLquery = "";

            if (Ledger != null)
            {
                SQLquery = "update LedgerSet set ledgerlevel = ROUND(ledgerlevel + @updateamount,2) where Id = @LedgerID";
                _ControlObjectContext.ExecuteStoreCommand(SQLquery, new SqlParameter("updateamount", NewAmountExVat), new SqlParameter("LedgerID", Ledger.Id));
            }

            if (LedgerBookingCode != null)
            {
                SQLquery = "update LedgerBookingCodeSet set ledgerlevel = ROUND(ledgerlevel + @updateamount,2) where Id = @LedgerID";
                _ControlObjectContext.ExecuteStoreCommand(SQLquery, new SqlParameter("updateamount", NewAmountExVat), new SqlParameter("LedgerID", LedgerBookingCode.Id));
            }
        }

    }
    #endregion

    #region Location
    public partial class Location
    {
        #region Constructors
        public Location()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";

            AdressLine1 = "";
            AdressLine2 = "";
            AdressLine3 = "";
            InvoiceAddress = "";
            InvoiceFooter = "";
            InvoiceFooterPerPage = "";
            InvoicePrintLogo = true;
            City = "";
            ZIPcode = "";
            Country = "";
            PreferredCurrency = "Eur";
            EMail = "";
            PhoneNumber = "";
            VATNumber = "";
            VIHBCode = "";
            ContactPerson = "";

            IsActive = true;
        }
        #endregion
    }
    #endregion

    #region Material
    public partial class Material
    {
        #region Constructors
        public Material()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";


            AvgPurchasePrice = 0;
            AvgPurchasePriceTotalPrice = 0;
            AvgPurchasePriceTotalWeight = 0;
            AvgSalesPrice = 0;
            AvgSalesPriceTotalPrice = 0;
            AvgSalesPriceTotalWeight = 0;
            SalesPrice = 0;
            PurchasePrice = 0;
            UseAvgPurchasePriceAsActualPrice = false;
            UseAvgSalesPriceAsActualPrice = false;
            IsActive = true;
            Category = "";
            CurrentStockLevel = 0;
            StockMayBeNegative = false;
            IsWorkInsteadOfMaterial = false;
            Group = "";
            StorageCode = "";
            LMECode = "";
            InvoiceType = "Both";

            BaselCode = "";
            HCode = "";
            PhysicalShape = "";

            MaterialId = Id;
        }
        #endregion

        #region Service functions
        public void RoundNumbers()
        {
            CurrentStockLevel = Math.Round(CurrentStockLevel, 4);
            //if (MaterialStockPosition != this)
            //{
            //    MaterialStockPosition.RoundNumbers();
            //}
        }
        #endregion

        #region Material stock position
        public Material GetMaterialStockPosition(ModelTMSContainer ControlObjectContext)
        {
            return ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.MaterialSet", "Id", MaterialId)) as Material;
        }
        #endregion

    }
    #endregion

    #region MaterialClosure
    public partial class MaterialClosure
    {
        #region Constructors
        public MaterialClosure()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";
            ClosureDateTime = DateTime.Today;

            IsCorrected = false;
            OriginalValues = "";
        }
        #endregion

        #region Recalc functions
        public void RecalcTotals(ModelTMSContainer Context)
        {
            DateTime StartDate, EndDate; 
            EndDate = ClosureDateTime;
            StartDate = ClosureDateTime.AddDays(-1);

            // read the Buy totals
            ObjectParameter Par1 = new ObjectParameter("StartDate", StartDate);
            ObjectParameter Par2 = new ObjectParameter("EndDate", EndDate);
            ObjectParameter Par3 = new ObjectParameter("Material_Id", Material.Id);
            ObjectQuery<MaterialMutation> qry = Context.MaterialMutationSet.Where("(it.MutationDateTime between @StartDate and @EndDate) and (it.Material.Id = @Material_Id)", Par1, Par2, Par3);

            // calc
            MaterialTotalBoughtDay = 0;
            MaterialTotalBoughtPriceDay = 0;
            MaterialTotalSoldDay = 0;
            MaterialTotalSoldPriceDay = 0;
            foreach (MaterialMutation mm in qry)
            {
                if (mm.MutationType == "Buy")
                {
                    MaterialTotalBoughtDay = MaterialTotalBoughtDay + mm.AmountInKg;
                    MaterialTotalBoughtPriceDay = MaterialTotalBoughtPriceDay + mm.TotalPrice;
                }
                else //Sell
                {
                    MaterialTotalSoldDay = MaterialTotalSoldDay + mm.AmountInKg;
                    MaterialTotalSoldPriceDay = MaterialTotalSoldPriceDay + mm.TotalPrice;
                }
            }


            // locate the previous closure (if any) and update this mutation record
            ObjectQuery<MaterialClosure> mcQry = Context.MaterialClosureSet.Where("(it.ClosureDateTime = @StartDate) and (it.Material.Id = @Material_Id)", Par1, Par3);
            if (mcQry.Count() > 0)
            {
                MaterialClosure mc = mcQry.First<MaterialClosure>();
                MaterialTotalBought = MaterialTotalBoughtDay + mc.MaterialTotalBought;
                MaterialTotalBoughtPrice = MaterialTotalBoughtPriceDay + mc.MaterialTotalBoughtPrice;
                MaterialTotalSold = MaterialTotalSoldDay + mc.MaterialTotalSold;
                MaterialTotalSoldPrice = MaterialTotalSoldPriceDay + mc.MaterialTotalSoldPrice;

                MaterialStockPrice = mc.MaterialStockPrice + MaterialTotalBoughtPriceDay + MaterialTotalSoldPriceDay;
                MaterialStockLevel = mc.MaterialStockLevel + MaterialTotalBoughtDay + MaterialTotalSoldDay;

                PurchasePrice = mc.PurchasePrice;
                SalesPrice = mc.SalesPrice;
            }
            else
            {
                MaterialTotalBought = MaterialTotalBoughtDay;
                MaterialTotalBoughtPrice = MaterialTotalBoughtPriceDay;
                MaterialTotalSold = MaterialTotalSoldDay;
                MaterialTotalSoldPrice = MaterialTotalSoldPriceDay;

                MaterialStockLevel = MaterialTotalBoughtDay + MaterialTotalSoldDay;
                MaterialStockPrice = MaterialTotalBoughtPriceDay + MaterialTotalSoldPriceDay;
            }


            // calc average purchase & sales prices if there were sales/purchases
            if ((MaterialTotalBoughtPriceDay != 0) && (MaterialTotalBoughtDay != 0))
            {
                PurchasePrice = (MaterialTotalBoughtDay / MaterialTotalBoughtPriceDay);
            }
            if ((MaterialTotalSoldPriceDay != 0) && (MaterialTotalSoldDay != 0))
            {
                SalesPrice = (MaterialTotalSoldDay / MaterialTotalSoldPriceDay);
            }

            // round everything
            MaterialTotalBought = Math.Round(MaterialTotalBought, 4);
            MaterialTotalBoughtPrice = Math.Round(MaterialTotalBoughtPrice, 4);
            MaterialTotalSold = Math.Round(MaterialTotalSold, 4);
            MaterialTotalSoldPrice = Math.Round(MaterialTotalSoldPrice, 4);
            MaterialTotalBoughtDay = Math.Round(MaterialTotalBoughtDay, 4);
            MaterialTotalBoughtPriceDay = Math.Round(MaterialTotalBoughtPriceDay, 4);
            MaterialTotalSoldDay = Math.Round(MaterialTotalSoldDay, 4);
            MaterialTotalSoldPriceDay = Math.Round(MaterialTotalSoldPriceDay, 4);
            MaterialStockLevel = Math.Round(MaterialStockLevel, 4);
            MaterialStockPrice = Math.Round(MaterialStockPrice, 4);

            // save in original values
            OriginalValues = "MaterialTotalBought = " + MaterialTotalBought.ToString() + "\n" +
                            "MaterialTotalBoughtPrice = " + MaterialTotalBoughtPrice.ToString() + "\n" +
                            "MaterialTotalSold = " + MaterialTotalSold.ToString() + "\n" +
                            "MaterialTotalSoldPrice = " + MaterialTotalSoldPrice.ToString() + "\n" +
                            "MaterialStockLevel = " + MaterialStockLevel.ToString() + "\n" +
                            "MaterialStockPrice = " + MaterialStockPrice.ToString();

            // auto close year
            if ((ClosureDateTime.Month == 12) && (ClosureDateTime.Day == 31))
            {
                IsCorrected = true;
                MaterialTotalBought = 0;
                MaterialTotalBoughtPrice = 0;
                MaterialTotalSold = 0;
                MaterialTotalSoldPrice = 0;
            }
        }
        #endregion
    }
    #endregion

    #region MaterialMutation
    public partial class MaterialMutation
    {
        #region Constructors
        public MaterialMutation()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";

            MutationDateTime = DateTime.Now;
            MutationType = "";
            IsCorrection = false;
            MutationNumber = 0;
        }
        #endregion

        #region Generate mutation number
        public void GenerateMutationNumber(ModelTMSContainer ControlObjectContext)
        {
            if (MutationNumber <= 0)
            {
                MutationNumber = SystemSettingSet.GetNextMaterialMutationNumber(ControlObjectContext);
            }
        }

        #endregion

        #region Recalc & process functions
        public void RecalcTotals()
        {
            AmountInKg = Amount * Material.MaterialUnit.StockKgMultiplier;
        }

        public void ProcessToStockLevel(bool UpdateRunningTotals)
        {
            Material.CurrentStockLevel = Material.CurrentStockLevel + Amount;

            if (UpdateRunningTotals)
            {
                if (MutationType == "Buy")
                {
                    Material.AvgPurchasePriceTotalWeight = Material.AvgPurchasePriceTotalWeight + Amount;
                    Material.AvgPurchasePriceTotalPrice = Material.AvgPurchasePriceTotalPrice + TotalPrice;
                    if (Material.UseAvgPurchasePriceAsActualPrice)
                    {
                        Material.AvgPurchasePrice = Material.AvgPurchasePriceTotalPrice / Material.AvgPurchasePriceTotalWeight;
                    }
                }
                else
                {
                    Material.AvgSalesPriceTotalWeight = Material.AvgSalesPriceTotalWeight - Amount; // amount & price are always negative for sales
                    Material.AvgSalesPriceTotalPrice = Material.AvgSalesPriceTotalPrice - TotalPrice;
                    if (Material.UseAvgSalesPriceAsActualPrice)
                    {
                        Material.AvgSalesPrice = Material.AvgSalesPriceTotalPrice / Material.AvgSalesPriceTotalWeight;
                    }
                }
            }
        }

        public void ProcessToLedgerMutation(ModelTMSContainer ControlObjectContext)
        {
            LedgerMutation lm = new LedgerMutation();
            ControlObjectContext.AddToLedgerMutationSet(lm);


            lm.AmountEXVat = TotalPrice;
            lm.Description = Description + " (" + MutationNumber.ToString() + ")";
            lm.VATAmount = 0;
            lm.BookingType = MutationType;
            lm.BookingDateTime = MutationDateTime;
            lm.Comments = Comments;
            lm.GroupCode = GroupCode;
            lm.IsEditable = false;
            lm.IsCorrection = true;

            if (MutationType == "Buy")
            {
                lm.LedgerBookingCode = Material.PurchaseLedgerBookingCode;
            }
            else
            {
                lm.LedgerBookingCode = Material.SalesLedgerBookingCode;
                lm.AmountEXVat = -lm.AmountEXVat; // sales are negative before processing
            }
            lm.TotalAmount = lm.AmountEXVat;

            lm.Process(ControlObjectContext);

            // buys are negative after processing
            if (MutationType == "Buy")
            {
                lm.AmountEXVat = -lm.AmountEXVat;
                lm.TotalAmount = lm.AmountEXVat;
            }
        }

        #endregion
    }
    #endregion

    #region MaterialUnit
    public partial class MaterialUnit
    {
        #region Constructors
        public MaterialUnit()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";

            IsActive = true;
            StockKgMultiplier = 1;
            StockUnit = "";
        }
        #endregion
    }
    #endregion

    #region Order
    public partial class Order
    {
        #region Constructors
        public Order()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";
            OrderNote = "";

            OrderStatus = "Open";
            OrderType = "Sell";
        }
        #endregion

        #region Clone functions
        public Order CloneToNew(ModelTMSContainer _ControlObjectContext, Boolean IsCorrectedSetting, Hashtable ReplugList, DateTime NewBookingDateTime)
        {
            Order NewOrder = new Order();

            Common.CloneProperties(this, NewOrder);
            NewOrder.IsCorrected = IsCorrectedSetting;
            NewOrder.OrderNumber = SystemSettingSet.GetNextOrderNumber(_ControlObjectContext);
            NewOrder.Relation = Relation;
            NewOrder.RelationLocation = RelationLocation;
            NewOrder.RelationProject = RelationProject;
            NewOrder.RelationContact = RelationContact;
            NewOrder.Location = Location;
            NewOrder.BookingDateTime = NewBookingDateTime;
            NewOrder.StaffMemberPurchaser = StaffMemberPurchaser;
            foreach (Freight CopyObj in Freight.ToArray()) { NewOrder.Freight.Add(CopyObj); }
            _ControlObjectContext.AddToOrderSet(NewOrder);

            if (ReplugList != null)
            {
                ReplugList.Add(this, NewOrder);
            }

            // clone order lines
            if (this.OrderLine.Count > 0)
            {
                foreach (OrderLine OldLine in this.OrderLine)
                {
                    OrderLine NewLine = OldLine.CloneToNew(_ControlObjectContext, ReplugList);
                    NewLine.Order = NewOrder;
                }
            }

            // relink freights to this order
            foreach (Freight frg in Freight.ToArray<Freight>())
            {
                Freight.Remove(frg);
                frg.Order = NewOrder;
            }

            return NewOrder;
        }
        #endregion

        #region Totals functions
        public void RecalcTotals()
        {
            TotalAmount = 0;
            TotalPrice = 0;
            foreach (OrderLine il in OrderLine)
            {
                il.RecalcTotals();
                TotalPrice = il.PriceExVAT + TotalPrice;
                TotalAmount = il.AmountInKgs + TotalAmount;
            }

            TotalPrice = Math.Round(TotalPrice, 2);
            TotalAmount = Math.Round(TotalAmount, 8);
        }
        #endregion

        #region Informative functions
        public Boolean IsInvoiced()
        {
            return (Invoice != null);
        }
        #endregion

        #region Process order methods
        public void LinkToSingleFreight(Freight frg)
        {
            foreach (Freight frgDel in Freight.ToArray<Freight>())
            {
                Freight.Remove(frgDel);
                frgDel.Order = null;
            }
            if (frg != null)
            {
                Freight.Add(frg);
            }
        }

        public void UnlinkFreights()
        {
            foreach (Freight frgDel in Freight.ToArray<Freight>())
            {
                Freight.Remove(frgDel);
                frgDel.Order = null;
            }
        }

        private void ProcessOrderWithCorrection(ModelTMSContainer _ControlObjectContext, Boolean IsCorrection, System.Guid LedgerMutationGroupCode, DateTime MatMutDateTime)
        {
            string SQLquery; 
            // set the multiplier (1=normal processing, -1= correction)
            int Multiplier = 1;
            if (OrderType == "Sell") { Multiplier = -1; }
            if (IsCorrection) { Multiplier = - Multiplier; }

            // check if we may proceed
            Boolean Proceed = false;
            if ((IsCorrection) && (OrderStatus == "Processed")) { Proceed = true; }
            if ((!IsCorrection) && (OrderStatus == "Open")) { Proceed = true; }

            if (Proceed)
            {
                // check order number
                if (OrderNumber <= 0)
                {
                    OrderNumber = SystemSettingSet.GetNextOrderNumber(_ControlObjectContext);
                }

                // check if there is a freight linked to this order
                foreach (Freight frg in Freight.ToArray<Freight>())
                {
                    if (IsCorrection) {
                        frg.FreightStatus = "To be invoiced";
                    }
                    else 
                    {
                        frg.FreightStatus = "Done";
                    }
                }

                // hide from user if this is a correction
                IsCorrected = IsCorrection;
                GroupCode = LedgerMutationGroupCode;

                // if this order is going to be corrected then check if the material mutations have been placed on a contract guidance material contract. If so, subtract !
                if (IsCorrection)
                {
                    foreach (MaterialMutation mm in MaterialMutation)
                    {
                        foreach (ContractGuidanceMaterialMutation cgmm in mm.ContractGuidanceMaterialMutation)
                        {
                            string SQLqueryUpdate = "update RelationContractMaterialSet set AvgStockUnits = AvgStockUnits - @AvgStockUnits, AvgStockPrice = AvgStockPrice - @AvgStockPrice where Id = @Id";
                            _ControlObjectContext.ExecuteStoreCommand(SQLqueryUpdate,
                                new SqlParameter("AvgStockUnits", cgmm.MutationAmount),
                                new SqlParameter("AvgStockPrice", cgmm.MutationAmount * mm.PricePerUnit),
                                new SqlParameter("Id", cgmm.RelationContractMaterial.Id));

                            cgmm.IsCorrected = true;
                        }

                    }
                }

                // create materialmutation
                Hashtable OrderLineList = new Hashtable();
                foreach (OrderLine ol in OrderLine)
                {
                    if (!OrderLineList.Contains(ol.Material.Id)) 
                    { 
                        MaterialMutation NewMut = new MaterialMutation ();
                        OrderLineList.Add(ol.Material.Id, NewMut);
                        if (IsCorrection)
                        {
                            NewMut.Description = "ORDCOR/" + OrderNumber + " / " + Description;
                        }
                        else
                        {
                            NewMut.Description = "ORD/" + OrderNumber + " / " + Description;
                        }
                        NewMut.IsCorrection = IsCorrection;
                        NewMut.GroupCode = GroupCode;
                        NewMut.MutationType = OrderType;
                        NewMut.MutationDateTime = MatMutDateTime;
                        NewMut.Material = ol.Material; 
                        NewMut.Order = this;
                        NewMut.GenerateMutationNumber(_ControlObjectContext);

                        // if this order line is on a contract then add this to the contract (DeliveredAmount, DeliveredTotalPrice) 
                        if (ol.RelationContractMaterial != null) 
                        {
                            // add the amounts to the contract
                            int ContractMultiplier = IsCorrection ? -1 : 1;
                            SQLquery = "update RelationContractMaterialSet set DeliveredAmount = DeliveredAmount + @DeliveredAmount, DeliveredTotalPrice = DeliveredTotalPrice + @DeliveredTotalPrice where Id = @Id";
                            _ControlObjectContext.ExecuteStoreCommand(SQLquery, 
                                new SqlParameter("DeliveredAmount", ol.Amount * ContractMultiplier), 
                                new SqlParameter("DeliveredTotalPrice", ol.PriceExVAT * ContractMultiplier),
                                new SqlParameter("Id", ol.RelationContractMaterial.Id));
                            // check if this contract may be closed
                            ol.RelationContractMaterial.RelationContract.CheckIfContractMayBeClosed(_ControlObjectContext);
                        }
                    }
                    MaterialMutation MatMut = OrderLineList[ol.Material.Id] as MaterialMutation;
                    MatMut.Amount = MatMut.Amount + (ol.Amount * Multiplier);
                    MatMut.TotalPrice = MatMut.TotalPrice + (ol.PriceExVAT * Multiplier);
                    MatMut.PricePerUnit = MatMut.Amount != 0 ? MatMut.TotalPrice / MatMut.Amount : MatMut.TotalPrice;
                    MatMut.AmountInKg = MatMut.Amount * MatMut.Material.MaterialUnit.StockKgMultiplier;
                }

                // process material prices
                foreach (DictionaryEntry de in OrderLineList)
                {
                    // get the material mutation to process
                    MaterialMutation mm = de.Value as MaterialMutation;
                    // update total in store (CurrentStockLevel)
                    SQLquery = "update [MaterialSet] set [CurrentStockLevel] = ([CurrentStockLevel] + @Amount) where [Id] = @Id";
                    _ControlObjectContext.ExecuteStoreCommand(SQLquery, new SqlParameter("Amount", mm.Amount), new SqlParameter("Id", mm.Material.GetMaterialStockPosition(_ControlObjectContext).Id));

                    // update material with actual stock level
                    Material mmStockCheck = mm.Material;
                    if (mm.Material.GetMaterialStockPosition(_ControlObjectContext) == mm.Material)
                    { 
                        _ControlObjectContext.Refresh(RefreshMode.StoreWins, mmStockCheck);
                    }

                    if (OrderType == "Sell")
                    {
                        // check for negative stock
                        if ((mmStockCheck.GetMaterialStockPosition(_ControlObjectContext).CurrentStockLevel < 0) && (!mmStockCheck.GetMaterialStockPosition(_ControlObjectContext).StockMayBeNegative) && (!IsCorrection))
                        {
                            throw new Exception( String.Format("De voorraad van materiaal {0} wordt negatief door deze verkoping. Het is niet toegestaan dat de voorraad van dit materiaal negatief wordt.", mmStockCheck.Description) );
                        }

                        // update material prices
                        SQLquery = "update MaterialSet set AvgSalesPriceTotalPrice = AvgSalesPriceTotalPrice + @TotalPrice" +
                                          ", AvgSalesPriceTotalWeight  = AvgSalesPriceTotalWeight + @Amount" +
                                          " where Id = @Id";
                        _ControlObjectContext.ExecuteStoreCommand(SQLquery, new SqlParameter("Amount", mm.Amount), new SqlParameter("TotalPrice", mm.TotalPrice), new SqlParameter("Id", mm.Material.Id));

                        if ((mm.Material.AvgSalesPriceTotalWeight + mm.Amount) != 0)
                        {
                            SQLquery = "update MaterialSet set " +
                                          " AvgSalesPrice = AvgSalesPriceTotalPrice / AvgSalesPriceTotalWeight " +
                                          " where Id = @Id";
                            _ControlObjectContext.ExecuteStoreCommand(SQLquery, new SqlParameter("Id", mm.Material.Id));

                            if (mm.Material.UseAvgSalesPriceAsActualPrice)
                            {
                                SQLquery = "update MaterialSet set SalesPrice = round(AvgSalesPriceTotalPrice / AvgSalesPriceTotalWeight,4) " +
                                              " where Id = @Id";// and (AvgSalesPriceTotalWeight > 0) and (AvgSalesPriceTotalPrice > 0)";
                                _ControlObjectContext.ExecuteStoreCommand(SQLquery, new SqlParameter("Id", mm.Material.Id));
                            }
                        }
                    }
                    else
                    {
                        SQLquery = "update MaterialSet set AvgPurchasePriceTotalPrice = AvgPurchasePriceTotalPrice + @TotalPrice " +
                                          ", AvgPurchasePriceTotalWeight  = AvgPurchasePriceTotalWeight + @Amount" +
                                          " where Id = @Id";
                        _ControlObjectContext.ExecuteStoreCommand(SQLquery, new SqlParameter("Amount", mm.Amount), new SqlParameter("TotalPrice", mm.TotalPrice), new SqlParameter("Id", mm.Material.Id));

                        if ((mm.Material.AvgPurchasePriceTotalWeight + mm.Amount) != 0)
                        {
                            SQLquery = "update MaterialSet set " +
                                              " AvgPurchasePrice = AvgPurchasePriceTotalPrice / AvgPurchasePriceTotalWeight " +
                                              " where Id = @Id";
                            _ControlObjectContext.ExecuteStoreCommand(SQLquery, new SqlParameter("Id", mm.Material.Id));

                            if (mm.Material.UseAvgPurchasePriceAsActualPrice)
                            {
                                SQLquery = "update MaterialSet set PurchasePrice = round(AvgPurchasePriceTotalPrice / AvgPurchasePriceTotalWeight,4) " +
                                              " where Id = @Id";    // and (AvgPurchasePriceTotalWeight > 0) and (AvgPurchasePriceTotalPrice > 0)";
                                _ControlObjectContext.ExecuteStoreCommand(SQLquery, new SqlParameter("Id", mm.Material.Id));
                            }
                        }
                    }

                    // if this order has any material at a certain price then check if there is contract guidance for this material so this material can be placed on a contract
                    if ((mm.Amount != 0) && (mm.TotalPrice != 0) && (!IsCorrection))
                    {
                        // check if we can link up this mutation to a contract material for contract guidance
                        string Query = "select value it from RelationContractMaterialSet as it inner join RelationContractSet as rcs on rcs.Id = it.RelationContract.Id "+
                            "where rcs.ContractType = \"" + (  OrderType == "Sell" ? "Buy" : "Sell" ) + "\" " +
                            "and rcs.ContractStatus = \"Open\" " +
                            "and (CurrentDateTime() between rcs.ContractStartDate and rcs.ContractEndDate) " +
                            "and rcs.HasContractGuidance " +
                            "and it.AvgStockUnits < it.MaxAmount " +
                            "and (it.PricePerUnit " + (OrderType == "Sell" ? "+" : "-") + // als het een verkooporder is trek de marge dan af van de prijs, tel hem bij een inkooporder erbij op
                            " it.AvgRequiredProfitPerUnit) " + (OrderType == "Sell" ? "<" : ">") + "= @AvgPrice " +// (kleiner of groter afhankelijk van order type [inkoop=groter,verkoop=kleiner]) 
                            "and it.Material.Id = @MaterialId " + 
                            "and rcs.Relation.Id != @RelationId " +
                            "order by rcs.ContractPriority desc, rcs.ContractStartDate "; 
                        ObjectQuery<RelationContractMaterial> query = new ObjectQuery<RelationContractMaterial>(Query, _ControlObjectContext);
                        query.Parameters.Add(new ObjectParameter("MaterialId", mm.Material.Id));
                        query.Parameters.Add(new ObjectParameter("RelationId", Relation.Id));
                        query.Parameters.Add(new ObjectParameter("AvgPrice", mm.TotalPrice / (mm.Amount != 0 ? mm.Amount : 1)));
                        ObjectResult<RelationContractMaterial> ilines = query.Execute(MergeOption.AppendOnly);

                        IEnumerator ienum = ilines.GetEnumerator();
                        double AmountToAssign = Math.Abs( mm.Amount);
                        while (ienum.MoveNext())
                        {
                            if (AmountToAssign > 0)
                            {
                                RelationContractMaterial TempContractMat = ienum.Current as RelationContractMaterial;

                                // add the contract guidance material to that specific contract (AvgStockPrice, AvgStockUnits which contain the total weight and total price) 
                                ContractGuidanceMaterialMutation cgmm = new ContractGuidanceMaterialMutation();

                                cgmm.Description = TempContractMat.Description;
                                cgmm.MaterialMutation = mm;
                                cgmm.RelationContractMaterial = TempContractMat;
                                double AmountToFillUp = TempContractMat.MaxAmount - Math.Abs(TempContractMat.AvgStockUnits);
                                if (AmountToFillUp > AmountToAssign)
                                {
                                    cgmm.MutationAmount = AmountToAssign;
                                }
                                else
                                {
                                    cgmm.MutationAmount = AmountToFillUp;
                                }

                                //mm.RelationContractMaterialForContractGuidance = TempContractMat;

                                string SQLqueryUpdate = "update RelationContractMaterialSet set AvgStockUnits = AvgStockUnits + @AvgStockUnits, AvgStockPrice = AvgStockPrice + @AvgStockPrice where Id = @Id";
                                _ControlObjectContext.ExecuteStoreCommand(SQLqueryUpdate,
                                    new SqlParameter("AvgStockUnits", cgmm.MutationAmount),
                                    new SqlParameter("AvgStockPrice", cgmm.MutationAmount * mm.PricePerUnit),
                                    new SqlParameter("Id", TempContractMat.Id));

                                AmountToAssign = AmountToAssign - cgmm.MutationAmount;
                            }
                        }
                    }
                }

                // set the order to the correct status
                if (IsCorrection)
                {
                    OrderStatus = "Open";
                }
                else
                {
                    OrderStatus = "Processed";
                }

                RecalcTotals();
            }
        }

        public void ProcessOrder(ModelTMSContainer _ControlObjectContext, System.Guid LedgerMutationGroupCode, DateTime MatMutDateTime)
        {
            ProcessOrderWithCorrection(_ControlObjectContext, false, LedgerMutationGroupCode, MatMutDateTime);
        }

        public void UnprocessOrder(ModelTMSContainer _ControlObjectContext, System.Guid LedgerMutationGroupCode, DateTime MatMutDateTime)
        {
            // unlink any freights in the orders 
            UnlinkFreights(); 
            
            ProcessOrderWithCorrection(_ControlObjectContext, true, LedgerMutationGroupCode, MatMutDateTime);

            IsCorrected = true;
        }

        public void AssignOrderNumber(ModelTMSContainer _ControlObjectContext)
        {
            // check order number
            if (OrderNumber <= 0)
            {
                OrderNumber = SystemSettingSet.GetNextOrderNumber(_ControlObjectContext);
            }
        }
        #endregion

    }
    #endregion

    #region OrderLine
    public partial class OrderLine
    {
        public void RecalcTotals()
        {
            PriceExVAT = Math.Round(PricePerUnit * Amount, 2);

            if (Material != null)
            {
                Material.RoundNumbers();
            }
        }

        public double AmountInKgs
        {
            get
            {
                return Amount * Material.MaterialUnit.StockKgMultiplier;
            }
        }

        public double PriceWithVAT
        {
            get
            {
                return PriceExVAT * (1 + (Material.VATPercentage/100) );
            }
        }

        #region Clone functions
        public OrderLine CloneToNew(ModelTMSContainer _ControlObjectContext, Hashtable ReplugList)
        {
            OrderLine NL = new OrderLine();

            Common.CloneProperties(this, NL);
            NL.Material = Material;
            NL.RelationContractMaterial = RelationContractMaterial;
            NL.RelationPriceAgreement = RelationPriceAgreement;
            _ControlObjectContext.AddToOrderLineSet(NL);

            if (ReplugList != null)
            {
                ReplugList.Add(this, NL);

                // replug this orderline to the new invoiceline if this is known. Otherwise leave it empty.
                if (InvoiceLine != null)
                {
                    InvoiceLine il = ReplugList[InvoiceLine] as InvoiceLine;
                    if (il != null)
                    {
                        NL.InvoiceLine = il;
                    }
                }
            }

            return NL;
        }
        #endregion

        #region Constructors
        public OrderLine()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";
        }
        #endregion
    }
    #endregion

    #region Relation
    public partial class Relation
    {
        #region Constructors
        public Relation()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";

            IsActive = true;
            VATNumber = "";
            PhoneNumber = "";
            EMail = "";
            YourReference = "";
            CustomerType = "Both";
            PreferredCurrency = "Eur";
            Country = "NL";

            IsSystemUser = false;

            FaxNumber= "";
            TransportContact = "";
            TransportVIHB   = "";
            TransportAddressLine = "";
        }
        #endregion

        #region VAT helpers
        public bool MustPayVat()
        {
            return (VATNumber.Trim() == "");
        }
        #endregion
    }
    #endregion

    #region RelationMaterial
    public partial class RelationMaterial
    {
        #region Constructors
        public RelationMaterial()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";
            LMECode = "";
        }
        #endregion
    }
    #endregion

    #region RelationAddress
    public partial class RelationAddress
    {
        #region Constructors
        public RelationAddress()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";
        }
        #endregion
    }
    #endregion

    #region RelationAdvancePayment
    public partial class RelationAdvancePayment
    {
        #region Constructors
        public RelationAdvancePayment()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";

            PaymentType = "Paid";
            PaymentDateTime = DateTime.Now;
            IsPaidOut = false;
            IsPaidBack = false;
        }
        #endregion

        #region Payment methods
        public void PayOut(ModelTMSContainer _ControlObjectContext, DateTime PayDateTime)
        {
            PayMe(_ControlObjectContext, false, PayDateTime);
        }

        public void PayBack(ModelTMSContainer _ControlObjectContext, DateTime PayDateTime)
        {
            PayMe(_ControlObjectContext, true, PayDateTime);
        }

        protected void PayMe(ModelTMSContainer _ControlObjectContext, Boolean IsCorrection, DateTime BookingDateTime)
        {
            // create a ledger mutation
            LedgerMutation NewMut = new LedgerMutation();

            NewMut.Ledger = Ledger;
            NewMut.LedgerBookingCode = LedgerBookingCode;
            NewMut.Relation = Relation;
            NewMut.Location = Ledger.LimitToLocation;

            string Acronym = "APM/";
            Double TempAmount = - Amount;
            NewMut.BookingType = "Buy";
            if (IsCorrection)
            {
                Acronym = "CORAPM/";
                TempAmount = Amount;
            }            
            if (PaymentType == "Received")
            {
                NewMut.BookingType = "Sell";
                TempAmount = -TempAmount;
            }

            NewMut.GroupCode = Id;
            NewMut.IsCorrection = IsCorrection;
            NewMut.AmountEXVat = TempAmount;
            NewMut.VATAmount = 0;
            NewMut.TotalAmount = TempAmount;
            NewMut.Description = Acronym + Description + "/" + PaymentDateTime.ToString();
            NewMut.Relation = Relation;
            NewMut.BookingDateTime = BookingDateTime;
            NewMut.IsEditable = false;

            _ControlObjectContext.AddToLedgerMutationSet(NewMut);

            NewMut.Process(_ControlObjectContext);


            if (!IsCorrection)
            {
                IsPaidOut = true;
            }
            else
            {
                IsPaidOut = false;
            }
        }
        #endregion
    }
    #endregion

    #region RelationContact
    public partial class RelationContact
    {
        #region Constructors
        public RelationContact()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";

            PhoneNumber = "";
            MobilePhone = "";
            HomePhone = "";

            PrivateEMail = "";
            PrivateMobilePhone = "";
            EMail = "";

            IsActive = true;
            RelationType = "Other";
        }
        #endregion
    }
    #endregion

    #region RelationContactLog
    public partial class RelationContactLog
    {
        #region Constructors
        public RelationContactLog()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";
        }
        #endregion
    }
    #endregion


    #region RelationContract
    public enum ContractGuidanceFillType { NotEnoughToReachMin=0, BetweenMinAndMax=1, Filled=2 };

    public partial class RelationContract
    {
        #region Constructors
        public RelationContract()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";
        }
        #endregion

        #region Service functions
        public void CheckIfContractMayBeClosed(ModelTMSContainer ControlObjectContext)
        {
            bool ContractClosure = true;

            ControlObjectContext.Refresh(RefreshMode.StoreWins, RelationContractMaterial);
            foreach (RelationContractMaterial rcm in RelationContractMaterial)
            {
                ContractClosure = rcm.DeliveredAmount >= rcm.MaxAmount;
                if (!ContractClosure) { break; } 
            }

            if (ContractClosure)
            {
                ContractStatus = "Closed";
            }
            else
            {
                ContractStatus = "Open";
            }
        }
        #endregion

        #region Informative contract functions
        public ContractGuidanceFillType ContractGuidanceFilled()
        {
            ContractGuidanceFillType Check = ContractGuidanceFillType.Filled;
            double TempAvgStockUnits;

            foreach (RelationContractMaterial rcm in RelationContractMaterial)
            {
                TempAvgStockUnits = rcm.AvgStockUnits;

//                if (ContractType == "Buy")
//                {
//                    TempAvgStockUnits = -TempAvgStockUnits;
//                }

                if ((TempAvgStockUnits >= rcm.MinAmount) && (TempAvgStockUnits < rcm.MaxAmount) && (Check >= ContractGuidanceFillType.Filled))
                {
                    Check = ContractGuidanceFillType.BetweenMinAndMax;
                }
                if ((TempAvgStockUnits < rcm.MinAmount) && (Check >= ContractGuidanceFillType.BetweenMinAndMax))
                {
                    Check = ContractGuidanceFillType.NotEnoughToReachMin;
                }
            }
            return Check;
        }
        #endregion
    }
    #endregion

    #region RelationContractMaterial
    public partial class RelationContractMaterial
    {
        #region Constructors
        public RelationContractMaterial()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";
        }
        #endregion

        #region Informative contract functions
        public ContractGuidanceFillType ContractGuidanceFilled()
        {
            ContractGuidanceFillType Check = ContractGuidanceFillType.Filled;

            if ((AvgStockUnits >= MinAmount) && (AvgStockUnits < MaxAmount) && (Check >= ContractGuidanceFillType.Filled))
            {
                Check = ContractGuidanceFillType.BetweenMinAndMax;
            }
            if ((AvgStockUnits < MinAmount) && (Check >= ContractGuidanceFillType.BetweenMinAndMax))
            {
                Check = ContractGuidanceFillType.NotEnoughToReachMin;
            }

            return Check;
        }

        public double AvgStockUnitPrice()
        {
            double AvgPrice = 0;

            if (RelationContract.HasContractGuidance)
            {
                AvgPrice = (AvgStockPrice / (AvgStockUnits != 0 ? AvgStockUnits : 1));
                AvgPrice = Math.Round(AvgPrice, 4);
            }

            return AvgPrice;
        }
        #endregion

        #region Custom properties
        public global::System.Double AlreadyDeliveredAmount
        {
            get
            {
                Double CalcAmount = 0;

                foreach (OrderLine ol in OrderLine)
                {
                    CalcAmount = CalcAmount + ol.Amount;
                }

                return CalcAmount;
            }
        }

        public global::System.Double AlreadyDeliveredTotalPrice
        {
            get
            {
                Double CalcAmount = 0;

                foreach (OrderLine ol in OrderLine)
                {
                    CalcAmount = CalcAmount + ol.PriceExVAT;
                }

                return CalcAmount;
            }
        }

        #endregion
    }
    #endregion

    #region RelationLocation
    public partial class RelationLocation
    {
        #region Constructors
        public RelationLocation()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";
        }
        #endregion
    }
    #endregion

    #region RelationPriceAgreement
    public partial class RelationPriceAgreement
    {
        #region Constructors
        public RelationPriceAgreement()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";
        }
        #endregion
    }
    #endregion

    #region RelationProject
    public partial class RelationProject
    {
        #region Constructors
        public RelationProject()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";
        }
        #endregion
    }
    #endregion

    #region RelationWork
    public partial class RelationWork
    {
        #region Constructors
        public RelationWork()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";
            WorkType = "ByUs";
        }
        #endregion

        #region derived properties
        private void RecalcTotals()
        {
            AmountEXVat = Hours * HourlyRate;
            VATAmount = 0;
            if (IsVATApplicable) { VATAmount = AmountEXVat * (VATPercentage / 100); }
            TotalAmount = Math.Round(AmountEXVat + VATAmount, 2);
        }

        partial void OnHourlyRateChanged()
        {
            RecalcTotals();
        }
        partial void OnHoursChanged()
        {
            RecalcTotals();
        }
        partial void OnIsVATApplicableChanged()
        {
            RecalcTotals();
        }
        partial void OnVATPercentageChanged()
        {
            RecalcTotals();
        }

        #endregion
    }
    #endregion

    #region RentLedger
    public partial class RentLedger
    {
        #region Constructors
        public RentLedger()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";

            RentNumber = 0;
        }
        #endregion

        #region Invoicing functions
        public void AddRentMaterialsToInvoice(ModelTMSContainer ControlObjectContext, Invoice inv)
        {
            // add each rental item line to the invoice as an invoiceline
            foreach (RentalItemActivity ria in RentalItemActivity)
            {
                ria.AddRentToInvoice(ControlObjectContext, inv);
            }
            Invoice.Add(inv);
        }

        public void DestroyRentalItemActivities(ModelTMSContainer ControlObjectContext)
        {
            foreach (RentalItemActivity ria in RentalItemActivity.ToArray<RentalItemActivity>())
            {
                ControlObjectContext.DeleteObject(ria);
            }
        }

        public void CheckRentLedgerBeforeSave(ModelTMSContainer ControlObjectContext)
        {
            if (RentNumber == 0) { RentNumber = SystemSettingSet.GetNextRentNumber(ControlObjectContext); }

            InitialRentEndStartDateTime = new DateTime(2000, 1, 1);
            InitialRentStartDateTime = new DateTime(2100, 1, 1);
            BaseRentPrice = 0; 
            TotalRentPrice = 0;
            VATRentPrice = 0; 
            foreach (RentalItemActivity ria in RentalItemActivity)
            {
                if (ria.RentStartDateTime < InitialRentStartDateTime)
                {
                    InitialRentStartDateTime = ria.RentStartDateTime;
                }
                if (ria.RentEndStartDateTime > InitialRentEndStartDateTime)
                {
                    InitialRentEndStartDateTime = ria.RentEndStartDateTime;
                }

                BaseRentPrice = BaseRentPrice + ria.BaseRentPrice;
                TotalRentPrice = TotalRentPrice + ria.TotalRentPrice;
                VATRentPrice = VATRentPrice + ria.VATRentPrice;
            }
        }
        #endregion
    }
    #endregion

    #region RentalItem
    public partial class RentalItem
    {
        #region Constructors
        public RentalItem()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";

            ItemState = "Available";
        }
        #endregion

        #region Calculation functions
        public void CalculateRentForPeriod(DateTime StartPeriod, DateTime EndPeriod, out double Price, out double Vat, out double TotalPrice)
        {
            Price = 0;
            Vat = 0;

            if (StartPeriod > EndPeriod) 
            {
                DateTime TempPeriod = StartPeriod; 
                StartPeriod = EndPeriod;
                EndPeriod = TempPeriod;
            }

            if (EndPeriod > new DateTime(2099, 1, 1))
            {
                Price = RentPerDay;
            }
            else
            {
                int Years=0, Months=0, Weeks=0, Days=0;

                Years = EndPeriod.Year - StartPeriod.Year;
                Months = Math.Abs( EndPeriod.Month - StartPeriod.Month );
                if ((Years>0) && (EndPeriod.Month < StartPeriod.Month)) 
                { 
                    Years--;
                    Months = 12 - Months;
                }
                DateTime TempStart = new DateTime(StartPeriod.Year, StartPeriod.Month,1);
                DateTime TempEnd = new DateTime(EndPeriod.Year, EndPeriod.Month, 1);
                Days = Convert.ToInt32(Math.Abs(TempEnd.Subtract(TempStart).TotalDays - EndPeriod.Subtract(StartPeriod).TotalDays));
                Weeks = Convert.ToInt32(Days / 7);
                Days = Days - (Weeks * 7);

                if (RentPerMonth != 0)
                {
                    Price = (Years * RentPerMonth * 12) + (Months * RentPerMonth) + (Weeks * RentPerWeek) + (Days * RentPerDay);
                }
                else if (RentPerWeek != 0)
                {
                    Weeks = Convert.ToInt32(EndPeriod.Subtract(StartPeriod).TotalDays % 7);
                    Days = Convert.ToInt32(EndPeriod.Subtract(StartPeriod).TotalDays - (Weeks * 7));
                    Price = (Weeks * RentPerWeek) + (Days * RentPerDay);
                }
                else if (RentPerDay != 0)
                {
                    Price = EndPeriod.Subtract(StartPeriod).TotalDays * RentPerDay;
                }
                else
                {
                    Price = EndPeriod.Subtract(StartPeriod).TotalDays * BaseRentalPrice;
                }

                // lookup the vat for this location
                double VATPercentage = VATPercentageForCurrentLocation();
                Vat = Price * (VATPercentage / 100);
            }

            Price = Math.Round(Price, 2);
            Vat = Math.Round(Vat, 2);
            TotalPrice = Price + Vat;
        }

        public double VATPercentageForCurrentLocation()
        {
            IEnumerator VATEnum = RentalType.RentalTypeVAT.GetEnumerator();
            while (VATEnum.MoveNext())
            {
                RentalTypeVAT tempVAT = VATEnum.Current as RentalTypeVAT;
                if (tempVAT.Location == Location)
                {
                    return tempVAT.VATPercentage;
                }
            }
            return 0;
        }
        #endregion

        #region Numbering
        public void AssignRentalItemNumber(ModelTMSContainer _ControlObjectContext)
        {
            if (ItemNumber <= 0)
            {
                ItemNumber = SystemSettingSet.GetNextRentalItemNumber(_ControlObjectContext);
            }
        }
        #endregion

        #region Service functions
        public void DisableItem()
        {
            this.ItemState = "Not available";
        }

        public void SendDisabledEMail(bool DisableItemFirst)
        {
            if (DisableItemFirst) { DisableItem(); }

            TMSMail Mail = new TMSMail();

            Mail.To.Add(this.Location.EMail);
            Mail.Subject = string.Format("Materiaal {0} uit verhuur genomen", this.Description);
            Mail.Body = "Het genoemde materiaal is uit de verhuur genomen omdat het beschadigd of verloren is geraakt. Controleer de status van het materiaal en herplan eventuele openstaande verhuringen van dit materiaal. \n\r\n\rUw TMS team." ;

            Mail.Send();
        }
        #endregion
    }
    #endregion

    #region RentalItemActivity
    public partial class RentalItemActivity
    {
        #region Constructors
        public RentalItemActivity()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";
            RentStartDateTime = System.DateTime.Now;
            RentEndStartDateTime = System.DateTime.Now.AddDays(1);
        }
        #endregion

        #region Recalc functions
        private void CalcDiscount()
        {
            BaseRentPrice = Math.Round(BaseRentPrice * ((100 - DiscountPercentage) / 100) ,2);
            VATRentPrice = Math.Round(VATRentPrice * ((100 - DiscountPercentage) / 100) ,2);

            // check if there has to be a rent price for this line
            if (InvoiceLine != null)
            {
                if ((InvoiceLine.Invoice != null) && (InvoiceLine.Invoice.Relation != null))
                {
                    if (!InvoiceLine.Invoice.Relation.MustPayVat())
                    {
                        VATRentPrice = 0;
                    }
                }
            }
        }

        public void RecalcRentPrice(bool ForceRecalc=false)
        {
            if (ForceRecalc)
            {
                double xCalculatedRentPrice, xVATRentPrice, xTotalRentPrice;
                RentalItem.CalculateRentForPeriod(RentStartDateTime, RentEndStartDateTime, out xCalculatedRentPrice, out xVATRentPrice, out xTotalRentPrice);
                CalculatedRentPrice = xCalculatedRentPrice;
                BaseRentPrice = CalculatedRentPrice;
                VATRentPrice = xVATRentPrice;
                CalcDiscount();
                TotalRentPrice = BaseRentPrice + VATRentPrice;
            }
            else
            {
                double VATPercentage = RentalItem.VATPercentageForCurrentLocation();
                BaseRentPrice = CalculatedRentPrice;
                VATRentPrice = Math.Round(BaseRentPrice * (VATPercentage / 100), 2);
                CalcDiscount();
                TotalRentPrice = BaseRentPrice + VATRentPrice;
            }
        }

        public void UpdateLinkedInvoiceLine()
        {
            if (InvoiceLine!=null) 
            {
                if (InvoiceLine.Invoice.InvoiceStatus != "Paid")
                {
                    InvoiceLine.OriginalPrice = BaseRentPrice;
                    InvoiceLine.VATPrice = VATRentPrice;
                    if (InvoiceLine.Invoice.Relation.MustPayVat())
                    {
                        InvoiceLine.VATPercentage = RentalItem.VATPercentageForCurrentLocation();
                    }
                    else
                    {
                        InvoiceLine.VATPercentage = 0;
                    }
                    InvoiceLine.TotalPrice = TotalRentPrice;
                    InvoiceLine.Invoice.RecalcTotals();
                    InvoiceLine.Description = SuggestedDescription();
                }
            }
        }

        public void UpdateAdvancePaymentStatus(ModelTMSContainer ControlObjectContext, bool UpdateAmount, bool AdvancePaymentActive)
        {
            if (IsTreatedAsAdvancePayment)
            {
                if (GeneratedRelationAdvancePayment == null)
                {
                    GeneratedRelationAdvancePayment = new RelationAdvancePayment();

                    ControlObjectContext.AddToRelationAdvancePaymentSet(GeneratedRelationAdvancePayment);
                }

                GeneratedRelationAdvancePayment.Relation = InvoiceLine.Invoice.Relation;
                GeneratedRelationAdvancePayment.Ledger = InvoiceLine.Invoice.Ledger;
                GeneratedRelationAdvancePayment.LedgerBookingCode = InvoiceLine.LedgerBookingCode;
                GeneratedRelationAdvancePayment.Description = Description;
                if (UpdateAmount) { GeneratedRelationAdvancePayment.Amount = TotalRentPrice; }
                GeneratedRelationAdvancePayment.IsPaidBack = !AdvancePaymentActive;
            }
            else
            {
                if (GeneratedRelationAdvancePayment != null)
                {
                    GeneratedRelationAdvancePayment.IsPaidBack = true;
                }
            }
        }

        public void CheckInvoiceStatus()
        {
            this.InvoiceStatus = "Open";

            if (this.InvoiceLine != null)
            {
                this.InvoiceStatus = "Invoiced";
            }
        }
        #endregion

        #region Attribute update functions
        public void GenerateDescription()
        {
            Description = SuggestedDescription();
        }

        public string SuggestedDescription()
        {
            if (RentStartDateTime.Year > 2099)
            {
                return "Huur " +  RentalItem.Description + " " + RentStartDateTime.ToLongDateString() + " tot nader te bepalen datum.";
            }
            else
            {
                return "Huur " + RentalItem.Description + " " + RentStartDateTime.ToLongDateString() + " tot en met " + RentEndStartDateTime.ToLongDateString();
            }
        }
        #endregion

        #region Invoicing functions
        public void AddRentToInvoice(ModelTMSContainer ControlObjectContext, Invoice inv)
        {
            // add this rental item line to the invoice as an invoiceline
            InvoiceLine iline = new InvoiceLine();

            // hookup items
            iline.Invoice = inv;
            iline.RentalItemActivity = this;

            // create invoiceline
            iline.Description = Description;
            iline.LineNumber = iline.Invoice.InvoiceLine.Count;
            iline.OriginalPrice = BaseRentPrice;
            iline.VATPrice = VATRentPrice;
            iline.VATPercentage = RentalItem.VATPercentageForCurrentLocation();
            iline.TotalPrice = TotalRentPrice;
            iline.LedgerBookingCode = RentalItem.RentalType.LedgerBookingCode;
            iline.Ledger = inv.Ledger;

            inv.InvoiceLine.Add(iline);

            InvoiceStatus = "Invoiced";

            UpdateAdvancePaymentStatus(ControlObjectContext, true, IsTreatedAsAdvancePayment);
        }
        #endregion

        #region Clone functions
        public RentalItemActivity CloneToNew(ModelTMSContainer ControlObjectContext)
        {
            RentalItemActivity ria = new RentalItemActivity();

            Common.CloneProperties(this, ria);
            ria.RentalItem = RentalItem;
            ria.InvoiceLine = null; // this rental item activity cannot be placed on the same invoice line
            ria.RentLedger = RentLedger;
            ria.GeneratedRelationAdvancePayment = null; // this rental item activity cannot be placed on the same advance payment

            // generate relation advance payment if the cloned line was also treated as advance payment
            ria.UpdateAdvancePaymentStatus(ControlObjectContext, true, ria.IsTreatedAsAdvancePayment);
            ria.InvoiceStatus = "Open"; // a cloned invoice line is always uninvoiced

            return ria;
        }
        #endregion
    }
    #endregion

    #region RentalType
    public partial class RentalType
    {
        #region Constructors
        public RentalType()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";

            RentalConditions = "";
        }
        #endregion
    }
    #endregion


    #region RentalTypeVAT
    public partial class RentalTypeVAT
    {
        #region Constructors
        public RentalTypeVAT()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";

            IsActive = true;
        }
        #endregion
    }
    #endregion

    #region SecurityRole
    public partial class SecurityRole
    {
        #region Constructors
        public SecurityRole()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";

            IsRoleTemplate = false;
            HasUnlimitedAccess = false;
            IsActive = true;
        }
        #endregion

        #region Security functions
        public Boolean CheckAccessToElement(ModelTMSContainer ControlObjectContext, string FormName, string ElementName, string Description, AccessType at)
        {
            Boolean Result = HasUnlimitedAccess;
            Boolean SettingPresent = false;
            Boolean NewAccessType = false;
            Boolean NewAccessTypeRequired = false;


            if ((Description == "...") || (ElementName.ToLower().IndexOf("buttonclosepopup") >= 0) || (ElementName.ToLower().IndexOf("buttonsearch") >= 0))
            {
                Result = true;
                return Result;
            }

            if (!Result)
            {
                SecurityRoleObjectAccess.Load();
                foreach (SecurityRoleObjectAccess smr in SecurityRoleObjectAccess)
                {
                    switch (smr.CheckAccessToElement(FormName, ElementName, Description, at, out NewAccessType))
                    {
                        case AccessElementCheck.NotPresent:
                            break;
                        case AccessElementCheck.FoundAndAccess:
                            SettingPresent = true;
                            NewAccessTypeRequired = NewAccessType;
                            Result = true;
                            break;
                        case AccessElementCheck.FoundNoAccess:
                            SettingPresent = true;
                            NewAccessTypeRequired = NewAccessType;
                            Result = false;
                            break;
                    }
                    if (SettingPresent) { break; }
                }

                if ((!SettingPresent) || (NewAccessTypeRequired))
                {
                    if (!SettingPresent)
                    {
                        // add this setting to this role 
                        SecurityRoleObjectAccess sroa = new SecurityRoleObjectAccess();

                        sroa.ObjectName = FormName + "." + ElementName;
                        sroa.Description = Description;
                        sroa.SecurityRole = this; 

                        ControlObjectContext.AddToSecurityRoleObjectAccessSet(sroa);
                    }

                    UpdateRoleTemplate(ControlObjectContext, FormName, ElementName, Description, at);
                }
            }
            else
            {
                UpdateRoleTemplate(ControlObjectContext, FormName, ElementName, Description, at);
            }

            return Result;
        }

        private void UpdateRoleTemplate(ModelTMSContainer ControlObjectContext, string FormName, string ElementName, string Description, AccessType at)
        {
            // add this setting or access type to the template if required
            SecurityRole Template = GetSecurityTemplateRole(ControlObjectContext);
            if (Template != this) // if we are not in the template right now
            {
                // add the setting
                Template.CheckAccessToElement(ControlObjectContext, FormName, ElementName, Description, at);
            }
        }

        public SecurityRole GetSecurityTemplateRole(ModelTMSContainer ControlObjectContext)
        {
            SecurityRole SR = null;
            IQueryable<SecurityRole> SRs = ControlObjectContext.SecurityRoleSet.Where(m => m.IsRoleTemplate);

            if (SRs.Count() == 0)
            {
                SR = new SecurityRole();
                SR.Description = "Template role";
                SR.IsRoleTemplate = true;
                SR.HasUnlimitedAccess = false;
                ControlObjectContext.AddToSecurityRoleSet(SR);

                // unfortunately a save is required here ...
                ControlObjectContext.SaveChanges();
            }
            else
            {
                SR = SRs.First<SecurityRole>();
            }

            return SR;
        }

        public void CopyObjectAccessFromTemplateRole(ModelTMSContainer ControlObjectContext)
        {
            SecurityRole sr = GetSecurityTemplateRole(ControlObjectContext);
            bool Exists = false;

            foreach (SecurityRoleObjectAccess sroa in sr.SecurityRoleObjectAccess)
            {
                // check for existence
                Exists = false;
                foreach (SecurityRoleObjectAccess CheckSroa in SecurityRoleObjectAccess)
                {
                    if (sroa.ObjectName == CheckSroa.ObjectName)
                    {
                        Exists = true;
                        CheckSroa.SettableAccessTypes = sroa.SettableAccessTypes;
                        break;
                    }
                }

                if (!Exists)
                {
                    // add if not exists
                    SecurityRoleObjectAccess NewSr = new SecurityRoleObjectAccess();

                    Common.CloneProperties(sroa, NewSr);
                    NewSr.SecurityRole = this;

                    ControlObjectContext.AddToSecurityRoleObjectAccessSet(NewSr);
                }
            }
        }

        public void CheckSecurityRolObjectAccess(ModelTMSContainer ControlObjectContext)
        {
            CopyObjectAccessFromTemplateRole(ControlObjectContext);
        }
        #endregion
    }
    #endregion

    #region SecurityRoleObjectAccess
    public enum AccessElementCheck { NotPresent, FoundAndAccess, FoundNoAccess };
    public partial class SecurityRoleObjectAccess
    {
        #region Constructors
        public SecurityRoleObjectAccess()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";

            SettableAccessTypes = "X";

            HasCreateAccess = false;
            HasReadAccess = false;
            HasUpdateAccess = false;
            HasDeleteAccess = false;
            HasExecuteAccess = false;
        }
        #endregion

        #region Security functions
        public AccessElementCheck CheckAccessToElement(string FormName, string ElementName, string Description, AccessType at, out Boolean NewAccessType)
        {
            Boolean Result = false;
            AccessElementCheck aec = AccessElementCheck.NotPresent;
            NewAccessType = false;

            if ( (ObjectName == FormName + "." + ElementName) && (!Result))
            {
                aec = AccessElementCheck.FoundNoAccess;
                switch (at)
                {
                    case AccessType.Create:
                        Result = HasCreateAccess;
                        if (SettableAccessTypes.IndexOf('C') < 0) { SettableAccessTypes = SettableAccessTypes + "C"; NewAccessType = true;  }
                        break;
                    case AccessType.Read:
                        Result = HasReadAccess;
                        if (SettableAccessTypes.IndexOf('R') < 0) { SettableAccessTypes = SettableAccessTypes + "R"; NewAccessType = true; }
                        break;
                    case AccessType.Update:
                        Result = HasUpdateAccess;
                        if (SettableAccessTypes.IndexOf('U') < 0) { SettableAccessTypes = SettableAccessTypes + "U"; NewAccessType = true; }
                        break;
                    case AccessType.Delete:
                        Result = HasDeleteAccess;
                        if (SettableAccessTypes.IndexOf('D') < 0) { SettableAccessTypes = SettableAccessTypes + "D"; NewAccessType = true; }
                        break;
                    case AccessType.Execute:
                        Result = HasExecuteAccess;
                        if (SettableAccessTypes.IndexOf('X') < 0) { SettableAccessTypes = SettableAccessTypes + "X"; NewAccessType = true; }
                        break;
                }
            }

            if (Result) { aec = AccessElementCheck.FoundAndAccess; }
            return aec;
        }
        #endregion
    }
    #endregion

    #region StaffMember
    public partial class StaffMember
    {
        #region Constructors
        public StaffMember()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";

            IsActive = true;
            IsDriver = true;
            ContractHoursPerWeek = 40;
            SocialSecurityNumber = "";
            InServiceDate = System.DateTime.Now;
            OutOfServiceDate = new System.DateTime(2100, 1, 1);
            IDExpirationDate = new System.DateTime(2000, 1, 1);
            IDNationality = "";
            IDType = "";
            IDNumber = "";
            LivingAddress = "";
            HomeAddress = "";

            HasVMSAccount = false;
            AccountName = "";
            Password = "";

            NetHourlyRate = 10;

        }
        #endregion

        #region Security functions
        public Boolean CheckAccessToElement(ModelTMSContainer ControlObjectContext, string FormName, string ElementName, string Description, AccessType at)
        {
            Boolean Result = false;

            foreach (SecurityRole smr in SecurityRole)
            {
                Result = smr.CheckAccessToElement(ControlObjectContext, FormName, ElementName, Description, at);
                if (Result) { break; }
            }

            return Result;
        }
        #endregion

    }
    #endregion

    #region StaffMemberAdvancePayment
    public partial class StaffMemberAdvancePayment
    {
        #region Constructors
        public StaffMemberAdvancePayment()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";
        }
        #endregion
    }
    #endregion

    #region StaffMemberPayment
    public partial class StaffMemberPayment
    {
        #region Constructors
        public StaffMemberPayment()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";
        }
        #endregion
    }
    #endregion

    #region StaffMemberPaymentDeclaration
    public partial class StaffMemberPaymentDeclaration
    {
        #region Constructors
        public StaffMemberPaymentDeclaration()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";
        }
        #endregion
    }
    #endregion

    #region StaffMemberTimeRegistration
    public partial class StaffMemberTimeRegistration
    {
        #region Constructors
        public StaffMemberTimeRegistration()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";
        }
        #endregion
    }
    #endregion

    #region StaffTimeRegistrationActivity
    public partial class StaffTimeRegistrationActivity
    {
        #region Constructors
        public StaffTimeRegistrationActivity()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";
        }
        #endregion
    }
    #endregion

    #region SystemSetting
    public partial class SystemSetting
    {
        #region Constructors
        public SystemSetting()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";

            //StaffMember = null;
            //Location = null;
        }
        #endregion

    }
    #endregion

    #region Truck
    public partial class Truck
    {
        #region Constructors
        public Truck()
        {
            Id = System.Guid.NewGuid();
            CreateDateTime = System.DateTime.Now;
            ModifyDateTime = System.DateTime.Now;
            CreateUser = System.Guid.Empty;
            ModifyUser = System.Guid.Empty;
            Description = "";
            Comments = "";

            TruckPlate = "";
            IsActive = true;
        }
        #endregion
    }
    #endregion

}