using ECommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Controllers
{
    /*
* Course: 		Web Programming 3
* Assessment: 	Milestone 3
* Created by: 	Philip Sidhom - 1968084
* Date: 		13-11-2023
* Class Name: 	CustomersController.cs
* Description:  This controller processes customer-related HTTP requests, employing an ICustomersProvider for data access.
*               It encompasses methods for retrieving all customers and customer-specific data, 
*               and it is configured for routing functionalities.
*/

    [ApiController]
    [Route("api/customers")]

    public class CustomersController : ControllerBase
    {
      
      
            private readonly ICustomersProvider customersProvider;

            public CustomersController(ICustomersProvider productsProvider)
            {
                this.customersProvider = productsProvider;
            }

            [HttpGet]
            public async Task<IActionResult> GetProductsAsync()
            {
                var result = await customersProvider.GetCustomersAsync();

                if (result.IsSuccess)
                {
                    return Ok(result.Customers);
                }
                return NotFound();
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetProductAsync(int id)
            {
                var result = await customersProvider.GetCustomerAsync(id);

                if (result.IsSuccess)
                {
                    return Ok(result.Customer);
                }
                return NotFound();
            }
    }
}
