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
using System.Collections;

namespace API_CellphoneStore.Controllers
{
    [RoutePrefix("api/API_Product")]
    public class API_ProductController : ApiController
    {
        BLL_ProductVersion bllProductVersion = new BLL_ProductVersion();
        BLL_Brand bllBrand = new BLL_Brand();
        BLL_ProductIntroduce bllProductIntroduce = new BLL_ProductIntroduce();
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Route("GetAllBrand")]
        public List<BrandMapped> GetAllBrand()
        {
            return bllBrand.GetAllBrand();
        }

        [Route("GetAllProductVersion")]
        public List<ProductVersionMapped> GetAllProductVersion()
        {
            return bllProductVersion.GetAllProductVersion();
        }

        [Route("GetListProductVersionByProductID/{productID}")]
        public List<ProductVersionMapped> GetListProductVersionByProductID(string productID)
        {
            return bllProductVersion.GetListProductVersionByProductID(productID);
        }

        [Route("GetProductVersionByID/{productVersionID}")]
        public ProductVersionInfoMapped GetProductVersionByID(string productVersionID)
        {
            return bllProductVersion.GetProductVersionByID(productVersionID);
        }

        [Route("GetProductIntroduceByID/{productID}")]
        public ProductIntroduceMapped GetProductIntroduceByID(string productID)
        {
            return bllProductIntroduce.GetProductIntroduceByID(productID);
        }
        [Route("GetListHotSale")]
        public List<ProductVersionMapped> GetListHotSale()
        {
            return bllProductVersion.GetListHotSale();
        }
    }
}
