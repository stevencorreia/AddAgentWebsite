using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AddCampaignB2BApi.Models
{
    public class AGENT_CAMP_CRITERIA
    {


        public int AGENT_CAMP_ID { get; set; }

        public int PRIORITY { get; set; }

        public int PERCENT_APPLIED { get; set; }

        public string QUERY { get; set; }

        public string DESCRIPTION { get; set; }

        public int AGENT_CAMP_CRITERIA_ID { get; set; }

        public DateTime? LAST_UPDATED_DATE { get; set; }

        public int? LAST_UPDATED_BY { get; set; }

        public byte[] LAST_TIMESTAMP { get; set; }


        public virtual AGENT_CAMP AGENT_CAMP { get; set; }
    }
}