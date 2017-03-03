using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Restoran;
using RestoranApi.ViewModel.LocationViewModel;
using RestoranApi.ViewModel.ProductStorageViewModel;

namespace RestoranApi.Controllers
{
    [RoutePrefix("location")]
    public class LocationController : ApiController
    {
        private RestoranContext context;
        public LocationController()
        {
            context = new RestoranContext();
        }
        [Route("locations")]
        public IEnumerable<LocationViewModel> GetAllLocation()
        {
            var locations = context.Location.ToList();
            var model = locations.Select(x => new LocationViewModel
            {
                Id = x.ID,
                Name = x.Name,
                MarketId = x.MarketId,
                Market = x.Market.Name,
            });
            return model;
        }
        [Route("{id:int}/products")]
        public HttpResponseMessage GetProductsLocation(int id)
        {
            var entity = context.Location.Find(id);
            if (entity == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, string.Format($"Локация {id} не найдена."));
            }
            var model = entity.Products.Select(x => new ProductStorageViewModel
            {
                Id = x.ProductStorageId,
                Name = x.Product.Name,
                Value = x.Value,
                Price = x.Price
            });
            return Request.CreateResponse(HttpStatusCode.OK, model);
        }
    }
}
