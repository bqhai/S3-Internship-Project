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
        
        [HttpGet]
        [Route("FilterProductVersionByRAM/{ram}")]
        public List<ProductVersionMapped> FilterProductVersionByRAM(string ram)
        {
            return bllProductVersion.FilterProductVersionByRAM(ram);
        }
        
        [HttpGet]
        [Route("FilterProductVersionByROM/{rom}")]
        public List<ProductVersionMapped> FilterProductVersionByROM(string rom)
        {
            return bllProductVersion.FilterProductVersionByROM(rom);
        }
        
        [HttpGet]
        [Route("FilterProductVersionByOS/{os}")]
        public List<ProductVersionMapped> FilterProductVersionByOS(string os)
        {
            return bllProductVersion.FilterProductVersionByOS(os);
        }
        
        [HttpGet]
        [Route("FilterProductVersionByScreenSize/{minScreenSize}/{maxScreenSize}")]
        public List<ProductVersionMapped> FilterProductVersionByScreenSize(double minScreenSize, double maxScreenSize)
        {
            return bllProductVersion.FilterProductVersionByScreenSize(minScreenSize, maxScreenSize);
        }

        [HttpGet]
        [Route("FilterProductVersionByBrand/{brandID}")]
        public List<ProductVersionMapped> FilterProductVersionByBrand(string brandID)
        {
            return bllProductVersion.FilterProductVersionByBrand(brandID);
        }
        
        [HttpGet]
        [Route("FilterProductVersionByPrice/{minPrice}/{maxPrice}")]
        public List<ProductVersionMapped> FilterProductVersionByPrice(int minPrice, int maxPrice)
        {
            return bllProductVersion.FilterProductVersionByPrice(minPrice, maxPrice);
        }

    }
}
