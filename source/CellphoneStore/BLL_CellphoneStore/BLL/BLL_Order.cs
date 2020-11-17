using DAL_CellPhoneStore.DAL;
using BLL_CellPhoneStore.Lib;
using Model_CellphoneStore;
using Model_CellphoneStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_CellPhoneStore.Model;

namespace BLL_CellPhoneStore.BLL
{
    public class BLL_Order
    {
        DAL_Order dalOrder = new DAL_Order();
        EntityMapper<Order, OrderMapped> convertToOrdMapped = new EntityMapper<Order, OrderMapped>();
        EntityMapper<OrderMapped, Order> convverToOrd = new EntityMapper<OrderMapped, Order>();

        EntityMapper<OrderDetail, OrderDetailMapped> convertToOrdDetailMapped = new EntityMapper<OrderDetail, OrderDetailMapped>();
        EntityMapper<OrderDetailMapped, OrderDetail> convertToOrdDetail = new EntityMapper<OrderDetailMapped, OrderDetail>();
        public BLL_Order()
        {

        }
        public bool AddNewOrder(OrderMapped orderMapped)
        {
            string lastOrderID = dalOrder.GetLastOrderID();
            if (lastOrderID != null)
            {
                try
                {
                    string orderID = AutoGen.CreateID("ORD", lastOrderID);
                    orderMapped.OrderID = orderID;                    
                    Order order = convverToOrd.Translate(orderMapped);
                    dalOrder.AddNewOrder(order);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }
        public bool AddNewOrderDetail(OrderDetailMapped orderDetailMapped)
        {
            string lastOrderID = dalOrder.GetLastOrderID();
            try
            {
                orderDetailMapped.OrderID = lastOrderID;
                OrderDetail orderDetail = convertToOrdDetail.Translate(orderDetailMapped);
                dalOrder.AddNewOrderDetail(orderDetail);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
