//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AddCampaignB2b.Models
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    
    [Serializable, DataContract(IsReference = true)]
    public partial class AGENT_CAMP
    {
        public AGENT_CAMP()
        {
            this.AGENT_CAMP_CRITERIA = new HashSet<AGENT_CAMP_CRITERIA>();
        }
    
        [DataMember] public int AGENT_CAMP_ID { get; set; }
        [DataMember] public string COMMUNICATOR { get; set; }
        [DataMember] public string CAMP_ABBR { get; set; }
        [DataMember] public Nullable<bool> ACTIVE_SW { get; set; }
        [DataMember] public Nullable<System.DateTime> START_DATE { get; set; }
        [DataMember] public Nullable<System.DateTime> END_DATE { get; set; }
        [DataMember] public Nullable<int> LAST_UPDATED_BY { get; set; }
        [DataMember] public Nullable<System.DateTime> LAST_UPDATED_DATE { get; set; }
        [DataMember] public byte[] LAST_TIMESTAMP { get; set; }
        [DataMember] public Nullable<int> MAX_RECORDS { get; set; }
        [DataMember] public Nullable<int> AGENT_PRIORITY { get; set; }
        [DataMember] public Nullable<bool> ALWAYS_FILL_BOOK_OF_BUSINESS_SW { get; set; }
        [DataMember] public Nullable<int> EMPLOYEE_ID { get; set; }
        [DataMember] public string CALLER_ID { get; set; }
        [DataMember] public string REGION { get; set; }
        [DataMember] public Nullable<int> REGION_ID { get; set; }
        [DataMember] public Nullable<System.DateTime> PROGRAM_START_DATE { get; set; }
        [DataMember] public string DNIS { get; set; }
        [DataMember] public string RECORD_TYPE { get; set; }
        [DataMember] public Nullable<int> ORIG_AGENT_ID { get; set; }
        [DataMember] public Nullable<int> ORDER_MANAGER_SW { get; set; }
        [DataMember] public string Port_Number { get; set; }
        [DataMember] public string Phone_Number { get; set; }
        [DataMember] public string GK_Owned { get; set; }
        [DataMember] public string workgroup { get; set; }
        [DataMember] public string BAM_EMAIL { get; set; }
        [DataMember] public string NT_LOGIN_NAME { get; set; }
        [DataMember] public Nullable<bool> TEST_AGENT { get; set; }
        [DataMember] public Nullable<int> MIN_RECORDS { get; set; }
    
        [DataMember] public virtual ICollection<AGENT_CAMP_CRITERIA> AGENT_CAMP_CRITERIA { get; set; }
    }
}
