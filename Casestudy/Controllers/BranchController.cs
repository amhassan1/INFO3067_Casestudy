using Casestudy.DAL;
using Casestudy.DAL.DAO;
using Casestudy.DAL.DomainClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Casestudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        readonly AppDbContext? _ctx;
        public BranchController(AppDbContext context) // injected here
        {
            _ctx = context;
        }

        [HttpGet("{lat}/{lon}")]
        public async Task<ActionResult<List<Branch>?>> Index(float lat, float lon)
        {
            BranchDAO dao = new(_ctx!);
            return await dao.GetThreeClosestBranches(lat, lon);
        }
    }
}
