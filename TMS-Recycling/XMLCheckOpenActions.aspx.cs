using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects;
using System.Data.EntityClient;
using System.Data.Common;

namespace TMS_Recycling
{
    public partial class XMLCheckOpenActions : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Response.Clear();
            Response.CacheControl = "no-cache";
            Response.Expires = -1;

            Response.Write("<actioncounter>\n\r<openactions>\n\r");

            ModelTMSContainer ControlObjectContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);

            ObjectQuery oq = ControlObjectContext.CreateQuery<DbDataRecord>(@"select count(0) 
                from RelationContactLogSet as it inner join RelationContactSet as rcs on it.RelationContact.Id = rcs.Id 
                inner join RelationSet as rs on rcs.Relation.Id = rs.Id
                where ((it.FollowUpState = ""Unhandled"" and it.FollowUpDateTime <= @ClientDateTime) or 
                    (it.FollowUpState = ""Paused"" and it.PausedUntilDateTime <= @ClientDateTime))
                    and (it.Handler = @Handler) ",
                  new ObjectParameter("ClientDateTime", Common.CurrentClientDateTime(Session)),
                  new ObjectParameter("Handler", Session["CurrentUserID"]));

            int Counter = 0;
            foreach (DbDataRecord rec in oq)
            {
                Counter = rec.GetInt32(0);
            }
            Response.Write(Counter.ToString());

            Response.Write("\n\r</openactions>\n\r</actioncounter>");
            Response.End();
        }
    }
}