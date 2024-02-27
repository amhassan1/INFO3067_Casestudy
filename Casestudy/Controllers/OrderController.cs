using Casestudy.DAL;
using Casestudy.DAL.DAO;
using Casestudy.DAL.DomainClasses;
using Casestudy.Helpers;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Casestudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {

        readonly AppDbContext? _ctx;
        public OrderController(AppDbContext context) // injected here
        {
            _ctx = context;
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<string>> Index(OrderHelper helper)
        {
            string retVal;
            try
            {
                CustomerDAO customerDao = new(_ctx!);
                Customer? customer = await customerDao.GetByEmail(helper.Email);
                OrderDAO orderDao = new(_ctx!);
                int[] result = await orderDao.AddOrder(customer!.Id, helper.Selections!);

                int orderId = result[0];
                int productsOnBackOrder = result[1];

                retVal = orderId > 0
                ? "Order " + orderId + " Placed!"
               : "Order was not placed";

                retVal = productsOnBackOrder > 0 ? retVal + " " + productsOnBackOrder + " Product(s) Backorderd!" : retVal;
            }
            catch (Exception ex)
            {
                retVal = "Order was not placed " + ex.Message;
            }
            return retVal;
        }

        [Route("{email}")]
        [HttpGet]
        public async Task<ActionResult<List<Order>>> List(string email)
        {
            List<Order> orders;
            CustomerDAO customerDao = new(_ctx!);
            Customer? customer = await customerDao.GetByEmail(email);
            OrderDAO OrderDao = new(_ctx!);
            orders = await OrderDao.GetAll(customer!.Id);
            return orders;
        }

        [Route("{orderid}/{email}")]
        [HttpGet]
        public async Task<ActionResult<List<OrderDetailsHelper>>> GetOrderDetails(int orderid, string email)
        {
            OrderDAO dao = new(_ctx!);
            return await dao.GetOrderDetails(orderid, email);
        }

    }
}
