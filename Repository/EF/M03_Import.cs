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
    
    public partial class M03_Import
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public M03_Import()
        {
            this.M03_ImportDetail = new HashSet<M03_ImportDetail>();
        }
    
        public System.Guid id { get; set; }
        public Nullable<System.Guid> supplier_id { get; set; }
        public Nullable<System.DateTime> arrive_date { get; set; }
        public Nullable<decimal> total_price { get; set; }
        public Nullable<System.DateTime> create_time { get; set; }
        public string create_by { get; set; }
        public Nullable<System.DateTime> modify_time { get; set; }
        public string modify_by { get; set; }
        public Nullable<System.Guid> tenant_id { get; set; }
    
        public virtual M03_Supplier M03_Supplier { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<M03_ImportDetail> M03_ImportDetail { get; set; }
    }
}
