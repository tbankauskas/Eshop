using Eshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Eshop.Controllers
{
    public class FiltersController : ApiController
    {
        private IEnumerable<Filters> GetFilters(IEnumerable<ProductType> productTypes)
        {
            try
            {
                using (DBEntities context = new DBEntities())
                {

                    IEnumerable<int> ptypes = productTypes.Where(a => a.IsSelected).Select(a => a.ProductTypeId).ToList();

                    var filters = (from f in context.FilterTypes
                                   join fa in context.ProductTypeFilterAssigns on f.FilterTypeId equals fa.FilterTypeId
                                   where ptypes.Contains(fa.ProductTypeId)
                                   select new Filters()
                                     {
                                         FilterTypeId = f.FilterTypeId,
                                         FilterType = f.FilterType
                                     }).Distinct().ToList();

                    foreach (var filter in filters)
                    {
                        switch (filter.FilterType)
                        {
                            case "Manufacturer":
                                filter.FilterValues = (from m in context.Manufacturers
                                                       join ma in context.ProductTypeManufacturerAssigns on m.ManufacturerId equals ma.ManufacturerId
                                                       where ptypes.Contains(ma.ProductTypeId)
                                                       select new FilterValue()
                                                       {
                                                           FilterValueId = m.ManufacturerId,
                                                           FilterValueName = m.Manufacturer
                                                       }).Distinct().ToList();
                                break;
                            case "OSSystem":
                                filter.FilterValues = (from m in context.OSSystems
                                                       join ma in context.ProductTypeOSSystemAssigns on m.OSSystemId equals ma.OSSystemId
                                                       where ptypes.Contains(ma.ProductTypeId)
                                                       select new FilterValue()
                                                       {
                                                           FilterValueId = m.OSSystemId,
                                                           FilterValueName = m.OS
                                                       }).Distinct().ToList();
                                break;
                            case "Storage":
                                filter.FilterValues = (from m in context.Storages
                                                       join ma in context.ProductTypeStorageAssigns on m.StorageId equals ma.StorageId
                                                       where ptypes.Contains(ma.ProductTypeId)
                                                       select new FilterValue()
                                                       {
                                                           FilterValueId = m.StorageId,
                                                           FilterValueName = m.Storage.ToString()
                                                       }).Distinct().ToList();
                                break;
                            case "Screen":
                                filter.FilterValues = (from m in context.ScreenTypes
                                                       select new FilterValue()
                                                       {
                                                           FilterValueId = m.ScreenTypeId,
                                                           FilterValueName = m.ScreenType
                                                       }).ToList();
                                break;
                            case "HDD":
                                filter.FilterValues = (from m in context.HDDTypes
                                                       select new FilterValue()
                                                       {
                                                           FilterValueId = m.HDDTypeId,
                                                           FilterValueName = m.HDDType
                                                       }).ToList();
                                break;
                            case "RAM":
                                filter.FilterValues = (from m in context.RAM
                                                       select new FilterValue()
                                                       {
                                                           FilterValueId = m.RAMId,
                                                           FilterValueName = m.RAM1
                                                       }).ToList();
                                break;
                            case "Color":
                                filter.FilterValues = (from m in context.Colors
                                                       select new FilterValue()
                                                       {
                                                           FilterValueId = m.ColorId,
                                                           FilterValueName = m.Color
                                                       }).ToList();
                                break;
                            default:
                                break;
                        }
                    }

                    return filters;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpPost]
        public IEnumerable<Filters> GetFilteredFiltersByProductType(List<ProductType> types)
        {
            return GetFilters(types);
        }

    }
}
