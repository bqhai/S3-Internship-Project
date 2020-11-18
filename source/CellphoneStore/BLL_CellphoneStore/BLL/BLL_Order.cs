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
using DAL_CellPhoneStore.MappingClass;

namespace BLL_CellPhoneStore.BLL
{
    public class BLL_Order
    {
        DAL_Order dalOrder = new DAL_Order();
        EntityMapper<OrderMapped, Order> convverToOrd = new EntityMapper<OrderMapped, Order>();
        EntityMapper<OrderDetailMapped, OrderDetail> convertToOrdDetail = new EntityMapper<OrderDetailMapped, OrderDetail>();
        EntityMapper<OrderInfo, OrderInfoMapped> convertToOrdInfoMapped = new EntityMapper<OrderInfo, OrderInfoMapped>();
        EntityMapper<OrderDetailInfo, OrderDetailInfoMapped> convertToOrdDeInfoMapped = new EntityMapper<OrderDetailInfo, OrderDetailInfoMapped>();       
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
        public List<OrderInfoMapped> GetListOrderByUserName(string username)
        {
            IEnumerable<OrderInfo> orderInfos = dalOrder.GetListOrderByUserName(username);
            List<OrderInfoMapped> orderInfoMappeds = new List<OrderInfoMapped>();
            foreach (var item in orderInfos)
            {
                orderInfoMappeds.Add(convertToOrdInfoMapped.Translate(item));
            }
            return orderInfoMappeds.OrderByDescending(ordi => ordi.OrderDate).ToList();
        }
        public List<OrderDetailInfoMapped> GetListOrderDetailByOrderID(string orderID)
        {
            IEnumerable<OrderDetailInfo> orderDetailInfos = dalOrder.GetListOrderDetailByOrderID(orderID);
            List<OrderDetailInfoMapped> orderDetailInfoMappeds = new List<OrderDetailInfoMapped>();
            foreach (var item in orderDetailInfos)
            {
                orderDetailInfoMappeds.Add(convertToOrdDeInfoMapped.Translate(item));
            }
            return orderDetailInfoMappeds;
        }
    }
}
