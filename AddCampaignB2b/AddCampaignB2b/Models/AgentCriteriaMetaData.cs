using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AddCampaignB2b.Models
{
    [MetadataType(typeof(AgentCriteriaMetaData))]
    public partial class AGENT_CAMP_CRITERIA
    {
        public string SelectedCRITERIAValue1 { get; set; }
        public string SelectedCRITERIAValue2 { get; set; }
        public string SelectedCRITERIAValue3 { get; set; }
        public string selectedListID { get; set; }

        [Display(Name = "List")]
        public List<SelectListItem> LIST { get; set; }

        public string selectedSegmentCode { get; set; }

        [Display(Name = "Segment")]
        public List<SelectListItem> SEGMENT_CODE { get; set; }

        public string SelectedCRITERIA1 { get; set; }
        
        [Display(Name = "Criteria1")]
        public List<SelectListItem> CRITERIA1 { get; set; }

        public string SelectedCRITERIA2 { get; set; }
        [Display(Name = "Criteria2")]
        public List<SelectListItem> CRITERIA2 { get; set; }

        public string SelectedCRITERIA3 { get; set; }
        [Display(Name = "Criteria3")]
        public List<SelectListItem> CRITERIA3 { get; set; }
    }

    public class AgentCriteriaMetaData 
    {
        [Range(0, 100.00, ErrorMessage = "Please enter valid Percentage")]
        [Display(Name = "Percentage")]
        [Remote("GetCurrentPercentage", "AddAgentCriteria", ErrorMessage = "Please make sure the total % is not greater than 100.", AdditionalFields = "AGENT_CAMP_ID,AGENT_CAMP_CRITERIA_ID")]
        public string PERCENT_APPLIED { get; set; }

        [Display(Name = "Criteria")]
        public string QUERY { get; set; }

        //[RegularExpression("([1-9][0-9]*)")] 
        //[Display(Name = "Priority")]
        //public string PRIORITY;

    }
}