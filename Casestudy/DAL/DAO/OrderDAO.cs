using Casestudy.DAL.DomainClasses;
using Casestudy.Helpers;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.EntityFrameworkCore;

namespace Casestudy.DAL.DAO
{
    public class OrderDAO
    {
        private readonly AppDbContext _db;
        public OrderDAO(AppDbContext ctx)
        {
            _db = ctx;
        }

        public async Task<int[]> AddOrder(int customerId, OrderSelectionHelper[] selections)
        {
            int orderId = -1;
            int productsOnBackOrder = 0;
            // we need a transaction as multiple entities involved
            using (var _trans = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    Order order = new();
                    order.OrderDate = System.DateTime.Now;
                    order.OrderAmount = 0;
                    order.CustomerId = customerId;
                    
                    
                    // calculate the order amount and then add the order row to the table
                    foreach (OrderSelectionHelper selection in selections)
                    {
                        order.OrderAmount += selection.Product!.MSRP * selection.Qty;
                    }
                    await _db.Orders!.AddAsync(order);
                    await _db.SaveChangesAsync();

                    // then add each product to the OrderLines table
                    // and update the qtys for products
                    foreach (OrderSelectionHelper selection in selections)
                    {
                        
                        Product? product = await _db.Products!.FindAsync(selection.Product!.Id);
                        OrderLineItem orderLineItem = new();

                        _db.Products!.Update(product!);

                        orderLineItem.OrderId = order.Id;
                        orderLineItem.ProductId = selection.Product.Id;
                        orderLineItem.SellingPrice = product!.MSRP;

                        if (selection.Qty <= product!.QtyOnHand)
                        {
                            product!.QtyOnHand -= selection.Qty;

                            orderLineItem.QtyOrdered = selection.Qty;
                            orderLineItem.QtySold = selection.Qty;
                            orderLineItem.QtyBackOrdered = 0;
                        }
                        else
                        {
                            orderLineItem.QtyOrdered = selection.Qty;
                            orderLineItem.QtySold = product.QtyOnHand;
                            orderLineItem.QtyBackOrdered = selection.Qty - product.QtyOnHand;

                            product!.QtyOnBackOrder += selection.Qty - product!.QtyOnHand;
                            product!.QtyOnHand = 0;

                            productsOnBackOrder += 1;
                        }

                        await _db.OrderLines!.AddAsync(orderLineItem);
                        await _db.SaveChangesAsync();
                    }
                    // test trans by uncommenting out these 3 lines
                    //int x = 1;
                    //int y = 0;
                    //x = x / y;
                    await _trans.CommitAsync();
                    orderId = order.Id;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await _trans.RollbackAsync();
                }
            }
            return new int[] { orderId, productsOnBackOrder };
        }

        public async Task<List<Order>> GetAll(int id)
        {
            return await _db.Orders!.Where(tray => tray.CustomerId == id).ToListAsync<Order>();
        }

        public async Task<List<OrderDetailsHelper>> GetOrderDetails(int orderId, string email)
        {
            Customer? customer = _db.Customers!.FirstOrDefault(customer => customer.Email == email);
            List<OrderDetailsHelper> allDetails = new();
            // LINQ way of doing INNER JOINS
            var results = from o in _db.Orders
                          join ol in _db.OrderLines! on o.Id equals ol.OrderId
                          join p in _db.Products! on ol.ProductId equals p.Id
                          where (o.CustomerId == customer!.Id && o.Id == orderId)
                          select new OrderDetailsHelper
                          {
                              OrderId = o.Id,
                              CustomerId = customer!.Id,
                              SellingPrice = ol.SellingPrice,
                              ProductName = p.ProductName,
                              QtyOrdered = ol.QtyOrdered,
                              QtyBackOrdered = ol.QtyBackOrdered,
                              QtySold = ol.QtySold,
                              ProductId = ol.ProductId,
                              OrderDate = o.OrderDate.ToString("yyyy/MM/dd - hh:mm tt")
                          };
            allDetails = await results.ToListAsync();
            return allDetails;
        }

    }
}
