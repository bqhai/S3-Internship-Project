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
    [RoutePrefix("api/API_ProductVersion")]
    public class API_ProductVersionController : ApiController
    {
        BLL_ProductVersion bllProductVersion = new BLL_ProductVersion();
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Route("GetAllProductVersion")]
        public List<ProductVersionMapped> GetAllProductVersion()
        {
            return bllProductVersion.GetAllProductVersion();
        }
        [Route("GetProductVersionByID/{productVersionID}")]
        public ProductVersionMapped GetProductVersionByID(string productVersionID)
        {
            return bllProductVersion.GetProductVersionByID(productVersionID);
        }
        [Route("GetListProductVersionByProductID/{productID}")]
        public List<ProductVersionMapped> GetListProductVersionByProductID(string productID)
        {
            return bllProductVersion.GetListProductVersionByProductID(productID);
        }
    }
}
