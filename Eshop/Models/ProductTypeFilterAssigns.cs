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
    
    public partial class ProductTypeFilterAssigns
    {
        public int ProductTypeFilterAssignId { get; set; }
        public int ProductTypeId { get; set; }
        public int FilterTypeId { get; set; }
    
        public virtual FilterTypes FilterTypes { get; set; }
        public virtual ProductTypes ProductTypes { get; set; }
    }
}