using ECommerce.Api.Search.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService ordersService;
        private readonly IProductsService productsService;
        private readonly ICustomerService customerService;
        public SearchService(IOrdersService ordersService, IProductsService productsService, ICustomerService customerService)
        {
          this.ordersService = ordersService;
          this.productsService = productsService;
          this.customerService = customerService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var ordersResult = await ordersService.GetOrdersAsync(customerId);
            var productsResult = await productsService.GetProductsAsync();
            var customersResult = await customerService.GetCustomersAsync(customerId);

            if(ordersResult.IsSuccess)
            {
                foreach(var order in ordersResult.Orders)
                {
                    foreach(var item in order.Items)
                    {
                        item.ProductName = productsResult.isSuccess ?
                            productsResult.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name :
                            "Product inforamtion is not available";
                    }
                }

                var result = new
                {
                    Customer = customersResult.IsSuccess ?
                    customersResult.Customer :
                    new { Name = "Customer information is not available" },

                    Orders = ordersResult.Orders,
                };
                return (true, result);
            }
            return (false, null);
        }
    }
}
