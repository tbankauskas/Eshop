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
    
    public partial class Storages
    {
        public Storages()
        {
            this.ProductTypeStorageAssigns = new HashSet<ProductTypeStorageAssigns>();
        }
    
        public int StorageId { get; set; }
        public int Storage { get; set; }
    
        public virtual ICollection<ProductTypeStorageAssigns> ProductTypeStorageAssigns { get; set; }
    }
}