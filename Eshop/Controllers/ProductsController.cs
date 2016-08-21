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
using System.Web.Services.Protocols;

namespace Eshop.Controllers
{
    public class ProductsController : ApiController
    {
        private IEnumerable<Product> GetProducts()
        {
            return GetProducts(null);
        }

        private IEnumerable<Product> GetProducts(ProductTypesFilters filters)
        {
            using (DBEntities context = new DBEntities())
            {
                IQueryable<Products> products = context.Products.AsQueryable();

                if (filters != null)
                {
                    List<int> ptypes = filters.ProductTypes.Where(a => a.IsSelected).Select(a => a.ProductTypeId).ToList();

                    if (ptypes.Count != 0)
                    {
                        products = from p in products
                                   where ptypes.Contains(p.ProductSpecifications.ProductTypeId)
                                   select p;
                    }

                    if (filters.Price != 0)
                    {
                        products = from p in products
                                   where p.Price <= filters.Price
                                   select p;
                    }
                    if (filters.Filters != null)
                    {
                        foreach (var filter in filters.Filters)
                        {
                            var li = filter.FilterValues.Where(a => a.IsSelected).Select(a => a.FilterValueId).ToList();
                            switch (filter.FilterType)
                            {
                                case "Manufacturer":
                                    if (li.Count != 0)
                                    {
                                        products = from p in products
                                                   where li.Contains(p.ProductSpecifications.ProductTypeManufacturerAssigns.ManufacturerId)
                                                   select p;
                                    }
                                    break;
                                case "OSSystem":
                                    if (li.Count != 0)
                                    {
                                        products = from p in products
                                                   where li.Contains(p.ProductSpecifications.ProductTypeOSSystemAssigns.OSSystemId)
                                                   select p;
                                    }
                                    break;
                                case "Storage":
                                    if (li.Count != 0)
                                    {
                                        products = from p in products
                                                   where li.Contains(p.ProductSpecifications.ProductTypeStorageAssigns.StorageId)
                                                   select p;
                                    }
                                    break;
                                case "Screen":
                                    if (li.Count != 0)
                                    {
                                        products = from p in products
                                                   where li.Contains((int)p.ProductSpecifications.ScreenTypeId)
                                                   select p;
                                    }
                                    break;
                                case "HDD":
                                    if (li.Count != 0)
                                    {
                                        products = from p in products
                                                   where li.Contains((int)p.ProductSpecifications.HDDTypeId)
                                                   select p;
                                    }
                                    break;
                                case "RAM":
                                    if (li.Count != 0)
                                    {
                                        products = from p in products
                                                   where li.Contains((int)p.ProductSpecifications.RAMId)
                                                   select p;
                                    }
                                    break;
                                case "Color":
                                    if (li.Count != 0)
                                    {
                                        products = from p in products
                                                   where li.Contains((int)p.ProductSpecifications.ColorId)
                                                   select p;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }

                var result = from p in products
                             select new Product()
                                 {
                                     Id = p.ProductId,
                                     Name = p.Name,
                                     Price = p.Price,
                                     Specs = new ProductSpecs()
                                     {
                                         Manufacturer = p.ProductSpecifications.ProductTypeManufacturerAssigns.Manufacturers.Manufacturer,
                                         Storage = p.ProductSpecifications.ProductTypeStorageAssigns.Storages.Storage,
                                         Os = p.ProductSpecifications.ProductTypeOSSystemAssigns.OSSystems.OS,
                                         Camera = p.ProductSpecifications.Camera
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
        public IEnumerable<Product> GetFilteredProducts([FromBody]ProductTypesFilters filters)
        {
            return GetProducts(filters);
        }
    }

    public class ProductTypesController : ApiController
    {
        public IEnumerable<ProductType> GetFilterTypes()
        {
            using (DBEntities context = new DBEntities())
            {
                return (from pt in context.ProductTypes
                        select new ProductType()
                        {
                            ProductTypeId = pt.ProductTypeId,
                            ProductTypeName = pt.ProductTypeName,
                            IsSelected = false
                        }).ToList();
            }
        }
    }

    public class ProductFilter
    {
        public List<string> Manufacturer { get; set; }
        public List<string> Storage { get; set; }
        public List<string> Os { get; set; }
    }
}
