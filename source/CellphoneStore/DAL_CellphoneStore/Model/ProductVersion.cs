//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL_CellPhoneStore.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductVersion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductVersion()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
            this.Customers = new HashSet<Customer>();
        }
    
        public string ProductVersionID { get; set; }
        public string ProductID { get; set; }
        public string ProductVersionName { get; set; }
        public string RAM { get; set; }
        public string ROM { get; set; }
        public string Color { get; set; }
        public int ListPrice { get; set; }
        public int Price { get; set; }
        public int QuantityInStock { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<bool> HotSale { get; set; }
        public string Image { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual Product Product { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
