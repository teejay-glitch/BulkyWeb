using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderHeader orderHeader)
        {
            _db.Update(orderHeader);
        }

        public void UpdateStatus(int id, string OrderStatus, string? PaymentStatus = null)
        {
            var order = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);
            if(order != null)
            {
                order.OrderStatus = OrderStatus;
                if (!string.IsNullOrEmpty(PaymentStatus)) order.PaymentStatus = PaymentStatus;
            }
        }

        public void UpdateStripePaymentId(int id, string SessionId, string PaymentIntentId)
        {
            var order = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);
            if (!string.IsNullOrEmpty(SessionId))
            {
                order.SessionId = SessionId;
            }
            if (!string.IsNullOrEmpty(PaymentIntentId))
            {
                order.PaymentIntentId = PaymentIntentId;
                order.PaymentDate = DateTime.Now;
            }
        }
    }
}
