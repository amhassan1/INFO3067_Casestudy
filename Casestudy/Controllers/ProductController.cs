using Casestudy.DAL;
using Casestudy.DAL.DAO;
using Casestudy.DAL.DomainClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Casestudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {

        readonly AppDbContext? _ctx;
        public ProductController(AppDbContext context) // injected here
        {
            _ctx = context;
        }


        [HttpGet]
        [Route("{brandId}")]
        public async Task<ActionResult<List<Product>>> Index(int brandId)
        {
            ProductDAO dao = new(_ctx!);
            List<Product> productsPerBrand = await dao.GetAllByBrand(brandId);
            return productsPerBrand;
        }
    }
        
}
