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
    
    public partial class Phones
    {
        public int PhoneId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int PhoneSpecificationId { get; set; }
        public string SmallImage { get; set; }
        public string LargeImage { get; set; }
    
        public virtual PhoneSpecifications PhoneSpecifications { get; set; }
    }
}