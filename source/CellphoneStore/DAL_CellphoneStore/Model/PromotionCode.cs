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
    
    public partial class PromotionCode
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PromotionCode()
        {
            this.PromotionCodeUseds = new HashSet<PromotionCodeUsed>();
        }
    
        public string Code { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public int Maximum { get; set; }
        public int Require { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime ExpiryDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PromotionCodeUsed> PromotionCodeUseds { get; set; }
    }
}
