//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Repository.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class M03_UserLog
    {
        public System.Guid id { get; set; }
        public Nullable<System.Guid> user_id { get; set; }
        public Nullable<int> user_type { get; set; }
        public string activity_type { get; set; }
        public Nullable<System.DateTime> execute_date { get; set; }
        public Nullable<System.DateTime> create_time { get; set; }
        public string create_by { get; set; }
        public Nullable<System.DateTime> modify_time { get; set; }
        public string modify_by { get; set; }
        public Nullable<System.Guid> tenant_id { get; set; }
    }
}