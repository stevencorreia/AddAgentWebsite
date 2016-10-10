using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using AddCampaignB2BApi.Models;
using Newtonsoft.Json;

namespace AddCampaignB2BApi.Controllers
{
    [EnableCors("*", "*", "*", PreflightMaxAge = 6000)]
    public class CriteriaController : ApiController
    {
        public HttpResponseMessage Get(int agentCampId,string dataBase = "DefaultConnection")
        {
            HttpResponseMessage response;
            try
            {
                using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings[dataBase].ConnectionString))
                {
                    var agentCampCriteria = new List<AGENT_CAMP_CRITERIA>();
                    var cmd = new SqlCommand("SELECT * FROM AGENT_CAMP_CRITERIA where AGENT_CAMP_ID = " + agentCampId, db);
                    db.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            agentCampCriteria.Add(new AGENT_CAMP_CRITERIA()
                            {
                                AGENT_CAMP_CRITERIA_ID = (int) rdr["AGENT_CAMP_CRITERIA_ID"],
                                AGENT_CAMP_ID = (int) rdr["AGENT_CAMP_ID"],
                                PERCENT_APPLIED = (int) rdr["PERCENT_APPLIED"],
                                QUERY = rdr["QUERY"].ToString()
                            });
                        }
                    }
                    var result = agentCampCriteria;
                    var testResult = new SampleReturn {data = agentCampCriteria};
                    response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(JsonConvert.SerializeObject(testResult));
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                response.Content = new StringContent(JsonConvert.SerializeObject(ex));
            }
            return response;
        }

        public class SampleReturn
        {
            public List<AGENT_CAMP_CRITERIA> data { get; set; }
        }
    }
}
