using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model_CellphoneStore;
using BLL_CellPhoneStore.BLL;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using Model_CellphoneStore.Repository;

namespace API_CellphoneStore.Controllers
{
    public class API_ProductController : ApiController
    {
        BLL_Product bll = new BLL_Product();
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public List<ProductMapped> GetAllProduct()
        {
            return bll.GetAllProduct();
        }
        
    }
}
