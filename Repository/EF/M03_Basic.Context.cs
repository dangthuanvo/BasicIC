﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class M03_BasicEntities : DbContext
    {
        public M03_BasicEntities()
            : base("name=M03_BasicEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<M03_Address> M03_Address { get; set; }
        public virtual DbSet<M03_Attribute> M03_Attribute { get; set; }
        public virtual DbSet<M03_Cart> M03_Cart { get; set; }
        public virtual DbSet<M03_CartDetail> M03_CartDetail { get; set; }
        public virtual DbSet<M03_Coupon> M03_Coupon { get; set; }
        public virtual DbSet<M03_CouponOrder> M03_CouponOrder { get; set; }
        public virtual DbSet<M03_Customer> M03_Customer { get; set; }
        public virtual DbSet<M03_Employee> M03_Employee { get; set; }
        public virtual DbSet<M03_Image> M03_Image { get; set; }
        public virtual DbSet<M03_Import> M03_Import { get; set; }
        public virtual DbSet<M03_ImportDetail> M03_ImportDetail { get; set; }
        public virtual DbSet<M03_Menu> M03_Menu { get; set; }
        public virtual DbSet<M03_Order> M03_Order { get; set; }
        public virtual DbSet<M03_OrderDetail> M03_OrderDetail { get; set; }
        public virtual DbSet<M03_Product> M03_Product { get; set; }
        public virtual DbSet<M03_ProductAttribute> M03_ProductAttribute { get; set; }
        public virtual DbSet<M03_ProductMenu> M03_ProductMenu { get; set; }
        public virtual DbSet<M03_Rating> M03_Rating { get; set; }
        public virtual DbSet<M03_Role> M03_Role { get; set; }
        public virtual DbSet<M03_RoleService> M03_RoleService { get; set; }
        public virtual DbSet<M03_RoleServiceEmployee> M03_RoleServiceEmployee { get; set; }
        public virtual DbSet<M03_Service> M03_Service { get; set; }
        public virtual DbSet<M03_Supplier> M03_Supplier { get; set; }
        public virtual DbSet<M03_UserLog> M03_UserLog { get; set; }
        public virtual DbSet<M03_WishList> M03_WishList { get; set; }
    }
}