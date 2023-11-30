using ECommerce.Api.Orders.Interfaces;
using ECommerce.Api.Orders.Providers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Controllers
{
    /*
* Course: 		Web Programming 3
* Assessment: 	Milestone 3
* Created by: 	Philip Sidhom - 1968084
* Date: 		13-11-2023
* Class Name: 	OrdersController.cs
* Description: 	This controller manages order-related HTTP requests. 
*               It uses an IOrdersProvider for data access and includes methods to retrieve all orders and orders for a specific customer.
*               The class is also used for routing.
   */


    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider ordersProvider;

        public OrdersController(IOrdersProvider ordersProvider)
        {
            this.ordersProvider = ordersProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersAsync()
        {
            var result = await ordersProvider.GetOrdersAsync();

            if (result.IsSuccess)
            {
                return Ok(result.Orders);
            }
            return NotFound();
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrdersAsync(int customerId)
        {
            var result = await ordersProvider.GetOrdersAsync(customerId);
            if(result.IsSuccess)
            {
                return Ok(result.Orders);
            }

            return NotFound();
        }
    }
}
