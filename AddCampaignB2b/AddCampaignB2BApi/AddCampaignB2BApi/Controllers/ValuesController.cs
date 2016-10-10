using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using AddCampaignB2BApi.Models;
using Newtonsoft.Json;


namespace AddCampaignB2BApi.Controllers
{
    [EnableCors("*", "*", "*", PreflightMaxAge = 6000)]
    public class ValuesController : ApiController
    {
        // GET api/values
        public HttpResponseMessage Get(string dataBase = "DefaultConnection")
        {
            HttpResponseMessage response;
            try
            {
                using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings[dataBase].ConnectionString))
                {
                    var agentList = new List<AGENT_CAMP>();
                    var cmd = new SqlCommand("SELECT * FROM AGENT_CAMP where ACTIVE_SW = 1 and END_DATE is null", db);
                    db.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            agentList.Add(BuildAgentObject(rdr));
                        }
                    }
                    var testResult = new SampleReturn { data = agentList };
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

        // GET api/values/5
        public HttpResponseMessage Get(int id, string dataBase = "DefaultConnection")
        {
            HttpResponseMessage response;
            try
            {
                using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings[dataBase].ConnectionString))
                {
                    var cmd = new SqlCommand("SELECT * FROM AGENT_CAMP where ACTIVE_SW = 1 and END_DATE is null and AGENT_CAMP_ID = " + id, db);
                    var agent = new AGENT_CAMP();
                    db.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            agent = BuildAgentObject(rdr);
                        }
                    }
                    var result = agent;
                    response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(JsonConvert.SerializeObject(result));
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                response.Content = new StringContent(JsonConvert.SerializeObject(ex));
            }
            return response;
        }

        // POST api/values
        public void Post([FromBody]AGENT_CAMP value)
        {
        }

        // PUT api/values/5
        public HttpResponseMessage Put(int id, [FromBody]AGENT_CAMP value, string dataBase = "DefaultConnection")
        {
            HttpResponseMessage response;
            try
            {
                using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings[dataBase].ConnectionString))
                using(var command = db.CreateCommand())
                {

                    command.CommandText = "UPDATE a SET " + 
                                            (value.END_DATE != null ? "END_DATE = '" + value.END_DATE + "', ACTIVE_SW =0, " : "") +
                                            (value.MIN_RECORDS != null ? "MIN_RECORDS = " + value.MIN_RECORDS + "," : "") +
                                            (value.MAX_RECORDS != null ? "MAX_RECORDS = " + value.MAX_RECORDS + "," : "") +
                                            (value.workgroup != null ? "workgroup = '" + value.workgroup + "'," : "") +
                                            (value.Phone_Number != null ? "Phone_Number = '" + value.Phone_Number + "'," : "") +
                                            (value.BAM_EMAIL != null ? "BAM_EMAIL = '" + value.BAM_EMAIL + "'," : "") +
                                            (value.DNIS != null ? "DNIS = '" + value.DNIS + "'," : "") +
                                            (value.COMMUNICATOR != null ? "COMMUNICATOR = '" + value.COMMUNICATOR + "'," : "") +
                                            (value.CALLER_ID != null ? "CALLER_ID = '" + value.CALLER_ID + "'," : "") +
                                            (value.ORDER_MANAGER_SW != null ? "ORDER_MANAGER_SW = " + value.ORDER_MANAGER_SW + "," : "") +
                                            (value.NT_LOGIN_NAME != null ? "NT_LOGIN_NAME = '" + value.NT_LOGIN_NAME + "'" : "") + 
                                            " FROM AGENT_CAMP a WHERE AGENT_CAMP_ID = " + id;
                    //command.Parameters.AddWithValue("@END", value.END_DATE);
                    //command.Parameters.AddWithValue("@Min", value.MIN_RECORDS);
                    //command.Parameters.AddWithValue("@Max", value.MAX_RECORDS);
                    //command.Parameters.AddWithValue("@workgroup", value.workgroup);
                    //command.Parameters.AddWithValue("@Phone", value.Phone_Number);
                    //command.Parameters.AddWithValue("@Email", value.BAM_EMAIL);
                    //command.Parameters.AddWithValue("@DNIS", value.DNIS);
                    //command.Parameters.AddWithValue("@Comm", value.COMMUNICATOR);
                    //command.Parameters.AddWithValue("@CallerId", value.CALLER_ID);
                    //command.Parameters.AddWithValue("@OM", value.ORDER_MANAGER_SW);
                    //command.Parameters.AddWithValue("@NT", value.NT_LOGIN_NAME);

                    db.Open();
                    var testResult = command.ExecuteNonQuery();
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

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        private AGENT_CAMP BuildAgentObject(SqlDataReader rdr)
        {
            return new AGENT_CAMP
            {
                AGENT_CAMP_ID = Convert.ToInt32(rdr["AGENT_CAMP_ID"]),
                COMMUNICATOR = rdr["COMMUNICATOR"] != DBNull.Value ? rdr["COMMUNICATOR"].ToString() : "",
                CAMP_ABBR = rdr["CAMP_ABBR"] != DBNull.Value ? rdr["CAMP_ABBR"].ToString() : "",
                DNIS = rdr["DNIS"] != DBNull.Value ? rdr["DNIS"].ToString() : "",
                END_DATE = rdr["END_DATE"] != DBNull.Value ? Convert.ToDateTime(rdr["END_DATE"]) : (DateTime?)null,
                START_DATE =rdr["START_DATE"] != DBNull.Value ? Convert.ToDateTime(rdr["START_DATE"]) : (DateTime?)null,
                BAM_EMAIL = rdr["BAM_EMAIL"] != DBNull.Value ? rdr["BAM_EMAIL"].ToString() : "",
                Phone_Number = rdr["Phone_Number"] != DBNull.Value ? rdr["Phone_Number"].ToString() : "",
                CALLER_ID = rdr["CALLER_ID"] != DBNull.Value ? rdr["CALLER_ID"].ToString() : "",
                workgroup = rdr["workgroup"] != DBNull.Value ? rdr["workgroup"].ToString() : "",
                NT_LOGIN_NAME = rdr["NT_LOGIN_NAME"] != DBNull.Value ? rdr["NT_LOGIN_NAME"].ToString() : "",
                MAX_RECORDS = rdr["MAX_RECORDS"] != DBNull.Value ? Convert.ToInt32(rdr["MAX_RECORDS"]) : (int?) null,
                MIN_RECORDS = rdr["MIN_RECORDS"] != DBNull.Value ? Convert.ToInt32(rdr["MIN_RECORDS"]) : (int?) null,
                ORDER_MANAGER_SW = rdr["ORDER_MANAGER_SW"] != DBNull.Value ? Convert.ToInt32(rdr["ORDER_MANAGER_SW"]) : (int?)null
            };
        }
    }

    public class SampleReturn
    {
        public List<AGENT_CAMP> data { get; set; }
    }
}
