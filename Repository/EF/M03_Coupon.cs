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
    
    public partial class M03_Coupon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public M03_Coupon()
        {
            this.M03_CouponOrder = new HashSet<M03_CouponOrder>();
        }
    
        public System.Guid id { get; set; }
        public string coupon_name { get; set; }
        public string coupon_code { get; set; }
        public Nullable<int> is_active { get; set; }
        public Nullable<int> discount_type { get; set; }
        public Nullable<decimal> discount_amount { get; set; }
        public Nullable<System.DateTime> create_time { get; set; }
        public string create_by { get; set; }
        public Nullable<System.DateTime> modify_time { get; set; }
        public string modify_by { get; set; }
        public Nullable<System.Guid> tenant_id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<M03_CouponOrder> M03_CouponOrder { get; set; }
    }
}
