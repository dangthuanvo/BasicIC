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
    
    public partial class M03_Supplier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public M03_Supplier()
        {
            this.M03_Import = new HashSet<M03_Import>();
        }
    
        public System.Guid id { get; set; }
        public string supplier_name { get; set; }
        public string supplier_email { get; set; }
        public string phone_number { get; set; }
        public string address { get; set; }
        public Nullable<System.DateTime> create_time { get; set; }
        public string create_by { get; set; }
        public Nullable<System.DateTime> modify_time { get; set; }
        public string modify_by { get; set; }
        public Nullable<System.Guid> tenant_id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<M03_Import> M03_Import { get; set; }
    }
}
