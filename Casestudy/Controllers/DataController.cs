using Casestudy.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Casestudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class DataController : ControllerBase
    {
        readonly AppDbContext? _ctx;
        public DataController(AppDbContext context) // injected here
        {
            _ctx = context;
        }

        [HttpGet]
        public async Task<ActionResult<String>> Index()
        {
            DataUtility util = new(_ctx!);
            string payload = "";
            var json = await GetProductsFromSourceAsync();
            try
            {
                payload = (await util.LoadProductInfoFromWebToDb(json)) ? "tables loaded" : "problem loading tables";
            }
            catch (Exception ex)
            {
                payload = ex.Message;
            }
            return JsonSerializer.Serialize(payload);
        }

        private static Task<String> GetProductsFromSourceAsync()
        {
            string file = "products-data.json";
            string path = Path.GetFullPath(file);
            string result = System.IO.File.ReadAllText(path);
            return Task.FromResult(result);
        }

    }
}
