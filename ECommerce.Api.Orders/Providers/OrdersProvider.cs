using AutoMapper;
using ECommerce.Api.Orders.Db;
using ECommerce.Api.Orders.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrdersDbContext dbContext;
        private readonly ILogger<OrdersProvider> logger;
        private readonly IMapper mapper;

        public OrdersProvider(OrdersDbContext dbContext, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            List<OrderItem> orderItems = new List<OrderItem>
            {
               new OrderItem { Id = 1, ProductId = 101, Quantity = 2, UnitPrice = 19.99m },
               new OrderItem { Id = 2, ProductId = 102, Quantity = 1, UnitPrice = 29.99m },
               new OrderItem { Id = 3, ProductId = 103, Quantity = 3, UnitPrice = 9.99m }
            };

            if (!dbContext.Orders.Any())
            {
                dbContext.Orders.Add(new Order { Id = 1, CustomerId = 1, OrderDate = DateTime.Now, Total = 0, Items = orderItems });
                dbContext.Orders.Add(new Order { Id = 2, CustomerId = 2, OrderDate = DateTime.Now, Total = 0, Items = orderItems });
                dbContext.Orders.Add(new Order { Id = 3, CustomerId = 3, OrderDate = DateTime.Now, Total = 0, Items = orderItems });
                dbContext.Orders.Add(new Order { Id = 4, CustomerId = 4, OrderDate = DateTime.Now, Total = 0, Items = orderItems });
                dbContext.Orders.Add(new Order { Id = 5, CustomerId = 4, OrderDate = DateTime.Now, Total = 0, Items = orderItems });

                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                var orders = await dbContext.Orders.Where(o => o.CustomerId == customerId).ToListAsync();
                if (orders != null && orders.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {

                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync()
        {
            try
            {
                var orders = await dbContext.Orders.ToListAsync();
                if (orders != null && orders.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {

                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
