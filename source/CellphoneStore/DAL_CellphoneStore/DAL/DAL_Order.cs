using DAL_CellPhoneStore.MappingClass;
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
        public IEnumerable<OrderInfo> GetListOrderByUserName(string username)
        {
            var query = from ord in db.Orders
                        join cus in db.Customers on ord.CustomerID equals cus.CustomerID
                        join acc in db.Accounts on cus.Username equals acc.Username
                        join ordstate in db.OrderStates on ord.OrderStateID equals ordstate.OrderStateID
                        join delstate in db.DeliveryStates on ord.DeliveryStateID equals delstate.DeliveryStateID
                        where acc.Username == username
                        select new OrderInfo
                        {
                            OrderID = ord.OrderID,
                            Payment = ord.Payment,
                            Delivery = ord.Delivery,
                            Notes = ord.Notes,
                            OrderDate = ord.OrderDate,
                            IntoMoney = ord.IntoMoney,
                            CustomerID = ord.CustomerID,
                            OrderDescription = ordstate.OrderDescription,
                            DeliveryDescription = delstate.DeliveryDescription

                        };
            return query;                      
        }
        public IEnumerable<OrderDetailInfo> GetListOrderDetailByOrderID(string orderID)
        {
            var query = from ordd in db.OrderDetails
                        join prdv in db.ProductVersions on ordd.ProductVersionID equals prdv.ProductVersionID
                        where ordd.OrderID == orderID
                        select new OrderDetailInfo
                        {
                            ProductVersionID = prdv.ProductVersionID,
                            ProductVersionName = prdv.ProductVersionName,
                            Amount = ordd.Amount
                        };
            return query;
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
            }
            else
            {
                ordDetail.Amount++;
            }
            db.SaveChanges();
        }

    }
}
