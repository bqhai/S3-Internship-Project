using DAL_CellPhoneStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_CellPhoneStore.DAL
{
    public class DAL_Order
    {
        DbCellPhoneStoreEntities db = new DbCellPhoneStoreEntities();
        public DAL_Order()
        {
        }
        public string GetLastOrderID()
        {
            List<Order> orders = db.Orders.Select(ord => ord).ToList();
            Order order = orders.LastOrDefault();
            if(order == null)
            {
                return "ORD10000";
            }
            return order.OrderID;
        }
        public void AddNewOrder(Order order)
        {
            order.OrderDate = DateTime.Now;
            order.OrderStateID = 0;
            order.DeliveryStateID = 0;
            db.Orders.Add(order);
            db.SaveChanges();
        }
        public void AddNewOrderDetail(OrderDetail orderDetail)
        {
            OrderDetail ordDetail = db.OrderDetails.Where(o => o.OrderID == orderDetail.OrderID && o.ProductVersionID == orderDetail.ProductVersionID).SingleOrDefault();
            if(ordDetail == null)
            {
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
            }
            else
            {
                ordDetail.Amount++;
                db.SaveChanges();
            }
        }

    }
}
