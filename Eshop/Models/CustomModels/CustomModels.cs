using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eshop.Models;

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
        public int? Camera { get; set; }
    }

    public class ProductImages
    {
        public string SmallImage { get; set; }
        public string LargeImage { get; set; }
    }

    public class ProductType
    {
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public bool IsSelected { get; set; }
    }

    public class Filters
    {
        public int FilterTypeId { get; set; }
        public string FilterType { get; set; }
        public IEnumerable<FilterValue> FilterValues { get; set; }
    }

    public class FilterValue
    {
        public int FilterValueId { get; set; }
        public string FilterValueName { get; set; }
        public bool IsSelected { get; set; }
    }

    public class ProductTypesFilters
    {
        public IEnumerable<ProductType> ProductTypes { get; set; }
        public IEnumerable<Filters> Filters { get; set; }
        public decimal Price { get; set; }
    }
}