using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model_CellphoneStore;
using BLL_CellPhoneStore.BLL;
using System.Web.Http.Cors;

namespace API_CellphoneStore.Controllers
{
    public class API_BrandController : ApiController
    {
        BLL_Brand bllBrand = new BLL_Brand();
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public List<BrandMapped> GetAllBrand()
        {
            return bllBrand.GetAllBrand();
        }
    }
}
