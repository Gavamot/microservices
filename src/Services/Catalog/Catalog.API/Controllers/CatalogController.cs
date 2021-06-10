using Catalog.API.Entities;
using Catalog.API.Rep;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        readonly IProductRep rep;
        readonly ILogger<CatalogController> log;
        public CatalogController(IProductRep rep, ILogger<CatalogController> log)
        {
            this.rep = rep;
            this.log = log;
        }

        [HttpGet]
        public Task<IEnumerable<Product>> GetProducts()
        {
            var products = rep.GetProducts();
            return products;
        }
    }
}
