using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Eshop.Models;

namespace Eshop.Controllers
{
    public class PhonesController : ApiController
    {
        private IEnumerable<Product> GetProducts()
        {
            return GetProducts(null);
        }

        private IEnumerable<Product> GetProducts(ProductFilter filters)
        {
            using (DBEntities context = new DBEntities())
            {
                IQueryable<Phones> products = context.Phones.AsQueryable();

                if (filters != null)
                {
                    if (filters.Manufacturer != null)
                        products = from p in products
                                   where filters.Manufacturer.Contains(p.PhoneSpecifications.Manufacturers.Manufacturer)
                                   select p;
                    if (filters.Os != null)
                        products = from p in products
                                   where filters.Os.Contains(p.PhoneSpecifications.OSSystems.OS)
                                   select p;
                    if (filters.Storage != null)
                        products = from p in products
                                   where filters.Storage.Contains(p.PhoneSpecifications.Storage.ToString())
                                   select p;
                }

                var result = from p in products
                             select new Product()
                                 {
                                     Id = p.PhoneId,
                                     Name = p.Name,
                                     Price = p.Price,
                                     Specs = new ProductSpecs()
                                     {
                                         Manufacturer = p.PhoneSpecifications.Manufacturers.Manufacturer,
                                         Storage = p.PhoneSpecifications.Storage,
                                         Os = p.PhoneSpecifications.OSSystems.OS,
                                         Camera = p.PhoneSpecifications.Camera
                                     },
                                     Image = new ProductImages()
                                     {
                                         SmallImage = p.SmallImage,
                                         LargeImage = p.LargeImage
                                     }
                                 };
                return result.ToList();
            }
        }

        [HttpGet]
        public IEnumerable<Product> GetAllProducts()
        {
            return GetProducts();
        }

        [HttpPost]
        public IEnumerable<Product> GetFilteredProducts([FromBody]ProductFilter filters)
        {
            return GetProducts(filters);
        }
    }

    public class ProductFilter
    {
        public List<string> Manufacturer { get; set; }
        public List<string> Storage { get; set; }
        public List<string> Os { get; set; }
    }
}
