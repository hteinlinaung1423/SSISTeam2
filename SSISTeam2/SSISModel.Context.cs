﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SSISTeam2
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SSISEntities : DbContext
    {
        public SSISEntities()
            : base("name=SSISEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Adjustment_Details> Adjustment_Details { get; set; }
        public virtual DbSet<Alert> Alerts { get; set; }

        internal object SearchCatagories(string text)
        {
            throw new NotImplementedException();
        }

        public virtual DbSet<Approval_Duties> Approval_Duties { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Collection_Point> Collection_Point { get; set; }
        public virtual DbSet<Delivery_Details> Delivery_Details { get; set; }
        public virtual DbSet<Delivery_Orders> Delivery_Orders { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Dept_Registry> Dept_Registry { get; set; }
        public virtual DbSet<Favourite> Favourites { get; set; }
        public virtual DbSet<Inventory_Adjustment> Inventory_Adjustment { get; set; }
        public virtual DbSet<Monthly_Check_Records> Monthly_Check_Records { get; set; }
        public virtual DbSet<Purchase_Order> Purchase_Order { get; set; }
        public virtual DbSet<Purchase_Order_Details> Purchase_Order_Details { get; set; }
        public virtual DbSet<Request_Details> Request_Details { get; set; }
        public virtual DbSet<Request_Event> Request_Event { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Stock_Inventory> Stock_Inventory { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Tender_List> Tender_List { get; set; }
        public virtual DbSet<Tender_List_Details> Tender_List_Details { get; set; }

        internal static object SearchCatagories(object text)
        {
            throw new NotImplementedException();
        }
    }
}
