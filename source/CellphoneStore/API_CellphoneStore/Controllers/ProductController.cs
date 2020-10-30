using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DAL_CellPhoneStore.Model;
using BLL_CellPhoneStore.BLL;
using System.Web.Http.Cors;

namespace API_CellphoneStore.Controllers
{
    public class ProductController : ApiController
    {
        BLL_Product bll = new BLL_Product();
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<Product> GetAllProduct()
        {
            return bll.GetAllProduct();
        }
    }
}
