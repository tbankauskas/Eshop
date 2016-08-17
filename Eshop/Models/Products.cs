using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eshop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ProductSpecs Specs { get; set; }
        public ProductImages Image { get; set; }
    }

    public class ProductSpecs
    {
        public string Manufacturer { get; set; }
        public int Storage { get; set; }
        public string Os { get; set; }
        public int Camera { get; set; }
    }

    public class ProductImages
    {
        public string SmallImage { get; set; }
        public string LargeImage { get; set; }
    }
}