using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AddCampaignB2b.Models
{
    [MetadataType(typeof(AGENT_CAMPMetaData))]
    public partial class AGENT_CAMP
    {

    }

    public class AGENT_CAMPMetaData
    {
        [Required(ErrorMessage = "(BAM initials required.)")]
        [Display(Name = "BAM")] //or DisplayName
        public string COMMUNICATOR { get; set; }

        [Required(ErrorMessage = "(Employee ID is required.)")]
        [Display(Name = "Employee ID")] //or DisplayName
        public string EMPLOYEE_ID { get; set; }

        
        [Display(Name = "Max Records ")] //or DisplayName
        public string MAX_RECORDS { get; set; }

        [Display(Name = "Min Records")] //or DisplayName
        public string MIN_RECORDS { get; set; }

        [Display(Name = "Is order manager")] //or DisplayName
        public string ORDER_MANAGER_SW { get; set; }

        [Display(Name = "Phone")] //or DisplayName
        public string Phone_Number { get; set; }

        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[\\w-]+(\\.[\\w-]+)*@([a-z0-9-]+(\\.[a-z0-9-]+)*?\\.[a-z]{2,6}|(\\d{1,3}\\.){3}\\d{1,3})(:\\d{4})?$", ErrorMessage = "Email address should be in proper format (e.g. username@domain.com)")]
        [Display(Name = "Email")] //or DisplayName
        public string BAM_EMAIL { get; set; }

        [Display(Name = "NT Login")] //or DisplayName
        public string NT_LOGIN_NAME { get; set; }

        [Display(Name = "Caller ID")] //or DisplayName
        public string CALLER_ID { get; set; }

        [Display(Name = "Start Date")] //or DisplayName
        public string START_DATE { get; set; }
    }
}