using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPharmacy.Data;

namespace WebPharmacy.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        
        public OrderRepository(ApplicationDbContext appDbContext)
        {
            context = appDbContext;
        }
        public IEnumerable<Order> Orders => context.Orders.Include(o => o.Lines).ThenInclude(l => l.Medicament);
        public void SaveOrder(Order order)
        {
            context.AttachRange(order.Lines.Select(l => l.Medicament));
            if (order.OrderId == 0)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
    }
}
