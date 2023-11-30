﻿using ECommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Controllers
{
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
