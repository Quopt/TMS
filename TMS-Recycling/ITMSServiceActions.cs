using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// ITMSServiceActions
// exposes serve side actions which can be performed on various objects of the TMS model
// the server side actions are characterised by the fact that they require 
// - direct interaction with the database 
// - ???
namespace TMS_Recycling
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITMSServiceActions" in both code and config file together.
    [ServiceContract]
    public interface ITMSServiceActions
    {
        // update high frequency changed objects with a special query (at the end of the transaction) instead of the entire object
        [OperationContract]
        void LedgerUpdateLedgerLevel(System.Guid ID, Double UpdateAmount);
        [OperationContract]
        void MaterialUpdateCurrentStockLevel(System.Guid ID, String SaleOrPurchase, Double UpdateStockLevel, Double UpdateAmount);
    }
}
