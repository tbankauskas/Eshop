//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Eshop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Colors
    {
        public Colors()
        {
            this.ProductSpecifications = new HashSet<ProductSpecifications>();
        }
    
        public int ColorId { get; set; }
        public string Color { get; set; }
    
        public virtual ICollection<ProductSpecifications> ProductSpecifications { get; set; }
    }
}
