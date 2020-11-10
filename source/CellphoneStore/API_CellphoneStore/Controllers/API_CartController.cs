using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BLL_CellPhoneStore.BLL;

namespace API_CellphoneStore.Controllers
{
    [RoutePrefix("api/API_Cart")]
    public class API_CartController : ApiController
    {
        BLL_PromotionCode bllPromotionCode = new BLL_PromotionCode();        
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        [Route("GetResultUsingPromotionCode/{code}/{username}/{totalPrice}")]
        public dynamic GetResultUsingPromotionCode(string code, string username, int totalPrice)
        {
            return bllPromotionCode.UsingPromotionCode(code, username, totalPrice);
        }

    }
}
