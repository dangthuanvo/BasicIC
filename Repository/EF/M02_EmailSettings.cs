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
    
    public partial class M02_EmailSettings
    {
        public System.Guid id { get; set; }
        public string address { get; set; }
        public string pass { get; set; }
        public string smtp_host { get; set; }
        public Nullable<int> smtp_port { get; set; }
        public Nullable<bool> enable_ssl { get; set; }
        public Nullable<System.DateTime> create_time { get; set; }
        public string create_by { get; set; }
        public Nullable<System.DateTime> modify_time { get; set; }
        public string modify_by { get; set; }
        public Nullable<System.Guid> tenant_id { get; set; }
    }
}
