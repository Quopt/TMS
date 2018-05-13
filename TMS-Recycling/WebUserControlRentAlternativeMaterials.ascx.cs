using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace TMS_Recycling
{
    public partial class WebUserControlRentAlternativeMaterials : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EntityDataSourceMaterials.CommandParameters["BorderEndDate"].DefaultValue = Common.ReturnEntitySQLDateTimeString(new DateTime(2100, 1, 1));
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ConstructQuery();
        }

        public void ConstructQuery()
        {
            RentalType rt = ControlObjectContext.GetObjectByKey( new System.Data.EntityKey("ModelTMSContainer.RentalTypeSet","Id", new Guid( Request.Params["RentalType"] ) ) ) as RentalType;

            StringBuilder Query = new StringBuilder();
            Query.Append(   "select distinct it.Id, it.ItemNumber, it.Description, it.Description, it.BaseRentalPrice, it.RentPerDay, it.RentPerWeek, it.RentPerMonth, it.BailPrice, it.Location.Description as LocationDescription " +
                            "from RentalItemSet as it  "+
                            "where  "+
                            "it.IsActive and  "+
                            "( (it.ItemState = \"Available\") or (it.ItemState = \"Rented\") ) and "+
                            "( it not in ( "+
                            "	select value itx2.RentalItem  "+
                            "	from RentalItemActivitySet as itx2  "+
                            "	where itx2.RentalItem.Id = it.Id and "+
                            "	 (  "+
                            "	   ((@StartDate <= itx2.RentStartDateTime) and (@EndDate >= itx2.RentStartDateTime))  "+
                            "	 ) or "+
                            "	 ( "+
                            "	   ((@StartDate >= itx2.RentStartDateTime) and (@StartDate <= itx2.RentEndStartDateTime)) "+
                            "	 ) "+
                            "  ) " +
                            ") and  ( cast(it.RentalType.Id as System.String) in { ");
            // insert the alternative rental type ID's
            foreach(RentalType rtAlt in rt.AlternativeRentalTypes) {
               Query.Append( "'" + rtAlt.Id.ToString() + "'");
            }
            Query.Append ("} ) " );

            // insert the location ID if required
            if (CheckBoxIncludeAlternativeLocations.Checked)
            {
                if (ComboBoxLocations.SelectedValue != "")
                {
                    Query.Append(" and (it.Location.Id = @LocationId) ");
                    EntityDataSourceMaterials.CommandParameters["LocationId"].DefaultValue = ComboBoxLocations.SelectedValue;
                }
            }
            else
            {
                Query.Append(" and (it.Location.Id = @LocationId) ");
                EntityDataSourceMaterials.CommandParameters["LocationId"].DefaultValue = Request.Params["LocationId"];
            }
            
            // add the ordering
            Query.Append("order by Description, BaseRentalPrice");

            // set as commandtext
            EntityDataSourceMaterials.CommandText = Query.ToString();
        }
    }
}