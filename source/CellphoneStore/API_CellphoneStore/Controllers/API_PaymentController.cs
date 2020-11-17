using Model_CellphoneStore;
using BLL_CellPhoneStore.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API_CellphoneStore.Controllers
{
    [RoutePrefix("api/API_Payment")]
    public class API_PaymentController : ApiController
    {
        BLL_Order bllOrder = new BLL_Order();
        BLL_PromotionCode bllPromotionCode = new BLL_PromotionCode();
        [HttpPost]
        [Route("AddNewOrder")]
        public bool AddNewOrder(OrderMapped orderMapped)
        {
            return bllOrder.AddNewOrder(orderMapped);
        }

        [HttpPost]
        [Route("AddNewOrderDetail")]
        public bool AddNewOrderDetail(OrderDetailMapped orderDetailMapped)
        {
            return bllOrder.AddNewOrderDetail(orderDetailMapped);
        }

        [HttpPost]
        [Route("AddPromotionCodeUsed/")]
        public bool AddPromotionCodeUsed(PromotionCodeUsedMapped promotionCodeUsedMapped)
        {
            return bllPromotionCode.AddPromotionCodeUsed(promotionCodeUsedMapped);
        }
    }
}
