using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AddCampaignB2BApi.Models
{
    public class AGENT_CAMP
    {

        public int AGENT_CAMP_ID { get; set; }

        public string COMMUNICATOR { get; set; }

        public string CAMP_ABBR { get; set; }

        public bool? ACTIVE_SW { get; set; }

        public DateTime? START_DATE { get; set; }

        public DateTime? END_DATE { get; set; }

        public int? LAST_UPDATED_BY { get; set; }

        public DateTime? LAST_UPDATED_DATE { get; set; }

        public byte[] LAST_TIMESTAMP { get; set; }

        public int? MAX_RECORDS { get; set; }

        public int? AGENT_PRIORITY { get; set; }

        public bool? ALWAYS_FILL_BOOK_OF_BUSINESS_SW { get; set; }

        public int? EMPLOYEE_ID { get; set; }

        public string CALLER_ID { get; set; }

        public string REGION { get; set; }

        public int? REGION_ID { get; set; }

        public DateTime? PROGRAM_START_DATE { get; set; }

        public string DNIS { get; set; }

        public string RECORD_TYPE { get; set; }

        public int? ORIG_AGENT_ID { get; set; }

        public int? ORDER_MANAGER_SW { get; set; }

        public string Port_Number { get; set; }

        public string Phone_Number { get; set; }

        public string GK_Owned { get; set; }

        public string workgroup { get; set; }

        public string BAM_EMAIL { get; set; }

        public string NT_LOGIN_NAME { get; set; }

        public bool? TEST_AGENT { get; set; }

        public int? MIN_RECORDS { get; set; }
    }
}