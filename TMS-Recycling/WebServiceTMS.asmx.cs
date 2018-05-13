using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.Objects;

namespace TMS_Recycling
{
    /// <summary>
    /// WebServiceTMS. Contains all generic webmethods required for AJAX components
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebServiceTMS : System.Web.Services.WebService
    {
        public WebServiceTMS()
        {
            // constructor
            
        }

        [System.Web.Script.Services.ScriptMethod]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public string[] GetRelationList(string prefixText, int count, string contextKey)
        {
            // execute object query
            List<string> Result = new List<string>();
            ModelTMSContainer ControlObjectContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);

            ObjectQuery QueryResult;
            QueryResult = ControlObjectContext.CreateQuery<Relation>("select value rs from RelationSet as rs where rs.Description like @prefixText and rs.PreferredLocation.Id=@LocId order by rs.description LIMIT(@Limit)",
                new ObjectParameter("prefixText", "%" + prefixText + "%"),
                new ObjectParameter("Limit", count),
                new ObjectParameter("LocId", new Guid(contextKey))
                );
            foreach (Relation rel in QueryResult)
            {
                Result.Add(rel.Description);
            }

            return Result.ToArray<string>();
        }

        [System.Web.Script.Services.ScriptMethod]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public string[] GetRelationList(string prefixText, int count)
        {
            // execute object query
            List<string> Result = new List<string>();
            ModelTMSContainer ControlObjectContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);

            ObjectQuery QueryResult;
            QueryResult = ControlObjectContext.CreateQuery<Relation>("select value rs from RelationSet as rs where rs.Description like @prefixText order by rs.description LIMIT(@Limit)",
                new ObjectParameter("prefixText", "%" + prefixText + "%"),
                new ObjectParameter("Limit", count)
                );
            foreach (Relation rel in QueryResult)
            {
                Result.Add(rel.Description);
            }

            return Result.ToArray<string>();
        }

        
        [System.Web.Script.Services.ScriptMethod]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public string[] GetContactList(string prefixText, int count)
        {
            // execute object query
            List<string> Result = new List<string>();
            ModelTMSContainer ControlObjectContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);

            ObjectQuery QueryResult;
            QueryResult = ControlObjectContext.CreateQuery<RelationContact>("select value rs from RelationContactSet as rs where rs.Description like @prefixText order by rs.description LIMIT(@Limit)",
                new ObjectParameter("prefixText", "%" + prefixText + "%"),
                new ObjectParameter("Limit", count)
                );

            foreach (RelationContact rel in QueryResult)
            {
                Result.Add(rel.Description);
            }

            return Result.ToArray<string>();
        }
    }
}
