using CellphoneStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Mvc;
using Model_CellphoneStore;
using Newtonsoft.Json;
using PagedList;
using PagedList.Mvc;
using System.Collections;
using System.Threading.Tasks;

namespace CellphoneStore.Controllers
{
    public class ProductController : Controller
    {
        ServiceRepository serviceObj = new ServiceRepository();
        HttpResponseMessage response;
        public ActionResult AllProduct(int pageIndex = 1, int pageSize = 15)
        {
            var url = "api/API_Product/GetAllProductVersion/";
            response = serviceObj.GetResponse(url);
            if (response.IsSuccessStatusCode)
            {
                List<ProductVersionMapped> productVersionMappeds = response.Content.ReadAsAsync<List<ProductVersionMapped>>().Result;
                ViewBag.Title = "All Products";
                ViewData["State"] = "AllProduct";
                return View(productVersionMappeds.ToPagedList(pageIndex, pageSize));
            }
            TempData["DangerMessage"] = Message.ConnectFail();
            return RedirectToAction("Index", "Home");

        }
        public ActionResult ProductDetails(string productVersionID)
        {
            var url = "api/API_Product/GetInfoProductVersionByID/" + productVersionID;
            response = serviceObj.GetResponse(url);
            if (response.IsSuccessStatusCode)
            {
                ViewBag.Title = "Product details";
                var productVersionInfo = response.Content.ReadAsAsync<ProductVersionInfoMapped>().Result;
                if(productVersionInfo != null)
                {
                    return View(productVersionInfo);
                }
                else
                {
                    TempData["DangerMessage"] = "Mã sản phẩm không tồn tại";
                    return RedirectToAction("Index", "Home");
                }
            }
            TempData["DangerMessage"] = Message.ConnectFail();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult GetAllBrand()
        {
            var url = "api/API_Product/GetAllBrand";
            response = serviceObj.GetResponse(url);
            if (response.IsSuccessStatusCode)
            {
                List<BrandMapped> brandMappeds = response.Content.ReadAsAsync<List<BrandMapped>>().Result;
                return PartialView(brandMappeds);
            }
            TempData["DangerMessage"] = Message.ConnectFail();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult ListProductVersion(string productID)
        {
            var url = "api/API_Product/GetListProductVersionByProductID/" + productID;
            response = serviceObj.GetResponse(url);
            if (response.IsSuccessStatusCode)
            {
                List<ProductVersionMapped> productVersionMappeds = response.Content.ReadAsAsync<List<ProductVersionMapped>>().Result;
                return PartialView(productVersionMappeds);
            }
            TempData["DangerMessage"] = Message.ConnectFail();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult ProductIntroduce(string productID)
        {
            var url = "api/API_Product/GetProductIntroduceByID/" + productID;
            response = serviceObj.GetResponse(url);
            if (response.IsSuccessStatusCode)
            {
                ProductIntroduceMapped productIntroduceMapped = response.Content.ReadAsAsync<ProductIntroduceMapped>().Result;
                return PartialView(productIntroduceMapped);
            }
            TempData["DangerMessage"] = Message.ConnectFail();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult SortProductVersionByPrice(string sortOption, int pageIndex = 1, int pageSize = 15)
        {
            var url = "api/API_Product/GetAllProductVersion";
            response = serviceObj.GetResponse(url);
            if (response.IsSuccessStatusCode)
            {
                List<ProductVersionMapped> productVersionMappeds = response.Content.ReadAsAsync<List<ProductVersionMapped>>().Result;
                if (sortOption == "desPrice")
                {
                    productVersionMappeds = productVersionMappeds.OrderByDescending(prdv => prdv.Price).ToList();
                    ViewData["Option"] = "desPrice";
                }
                else
                {
                    productVersionMappeds = productVersionMappeds.OrderBy(prdv => prdv.Price).ToList();
                    ViewData["Option"] = "ascPrice";
                }
                ViewData["State"] = "SortByPrice";
                return View("~/Views/Product/AllProduct.cshtml", productVersionMappeds.ToPagedList(pageIndex, pageSize));
            }
            TempData["DangerMessage"] = Message.ConnectFail();
            return RedirectToAction("AllProduct", "Product");
        }
        public ActionResult FilterProductVersionByRAM(string ram, int pageIndex = 1, int pageSize = 15)
        {
            var url = "api/API_Product/FilterProductVersionByRAM/" + ram;
            response = serviceObj.GetResponse(url);
            if (response.IsSuccessStatusCode)
            {
                List<ProductVersionMapped> productVersionMappeds = response.Content.ReadAsAsync<List<ProductVersionMapped>>().Result;
                ViewData["State"] = "FilterByRam";
                ViewData["Option"] = ram;
                return View("~/Views/Product/AllProduct.cshtml", productVersionMappeds.ToPagedList(pageIndex, pageSize));
            }
            TempData["DangerMessage"] = Message.ConnectFail();
            return RedirectToAction("AllProduct", "Product");
        }
        public ActionResult FilterProductVersionByROM(string rom, int pageIndex = 1, int pageSize = 15)
        {
            var url = "api/API_Product/FilterProductVersionByROM/" + rom;
            response = serviceObj.GetResponse(url);
            if (response.IsSuccessStatusCode)
            {
                List<ProductVersionMapped> productVersionMappeds = response.Content.ReadAsAsync<List<ProductVersionMapped>>().Result;
                ViewData["State"] = "FilterByRom";
                ViewData["Option"] = rom;
                return View("~/Views/Product/AllProduct.cshtml", productVersionMappeds.ToPagedList(pageIndex, pageSize));
            }
            TempData["DangerMessage"] = Message.ConnectFail();
            return RedirectToAction("AllProduct", "Product");
        }
        public ActionResult FilterProductVersionByOS(string os, int pageIndex = 1, int pageSize = 15)
        {
            var url = "api/API_Product/FilterProductVersionByOS/" + os;
            response = serviceObj.GetResponse(url);
            if (response.IsSuccessStatusCode)
            {
                List<ProductVersionMapped> productVersionMappeds = response.Content.ReadAsAsync<List<ProductVersionMapped>>().Result;
                ViewData["State"] = "FilterByOs";
                ViewData["Option"] = os;
                return View("~/Views/Product/AllProduct.cshtml", productVersionMappeds.ToPagedList(pageIndex, pageSize));
            }
            TempData["DangerMessage"] = Message.ConnectFail();
            return RedirectToAction("AllProduct", "Product");
        }
        public ActionResult FilterProductVersionByScreenSize(double minScreenSize, double maxScreenSize, int pageIndex = 1, int pageSize = 15)
        {
            var url = "api/API_Product/FilterProductVersionByScreenSize/" + minScreenSize + "/" + maxScreenSize + "/";
            response = serviceObj.GetResponse(url);
            if (response.IsSuccessStatusCode)
            {
                List<ProductVersionMapped> productVersionMappeds = response.Content.ReadAsAsync<List<ProductVersionMapped>>().Result;
                ViewData["State"] = "FilterByScreenSize";
                ViewData["Option"] = minScreenSize;
                ViewData["Option2"] = maxScreenSize;
                return View("~/Views/Product/AllProduct.cshtml", productVersionMappeds.ToPagedList(pageIndex, pageSize));
            }
            TempData["DangerMessage"] = Message.ConnectFail();
            return RedirectToAction("AllProduct", "Product");
        }
        public ActionResult FilterProductVersionByBrand(string brandID, int pageIndex = 1, int pageSize = 15)
        {
            var url = "api/API_Product/FilterProductVersionByBrand/" + brandID;
            response = serviceObj.GetResponse(url);
            if (response.IsSuccessStatusCode)
            {
                List<ProductVersionMapped> productVersionMappeds = response.Content.ReadAsAsync<List<ProductVersionMapped>>().Result;
                ViewData["State"] = "FilterByBrand";
                ViewData["Option"] = brandID;
                return View("~/Views/Product/AllProduct.cshtml", productVersionMappeds.ToPagedList(pageIndex, pageSize));
            }
            TempData["DangerMessage"] = Message.ConnectFail();
            return RedirectToAction("AllProduct", "Product");
        }
        public ActionResult FilterProductVersionByPrice(int minPrice, int maxPrice, int pageIndex = 1, int pageSize = 15)
        {
            var url = "api/API_Product/FilterProductVersionByPrice/" + minPrice + "/" + maxPrice + "/";
            response = serviceObj.GetResponse(url);
            if (response.IsSuccessStatusCode)
            {
                List<ProductVersionMapped> productVersionMappeds = response.Content.ReadAsAsync<List<ProductVersionMapped>>().Result;
                ViewData["State"] = "FilterByPrice";
                ViewData["Option"] = minPrice;
                ViewData["Option2"] = maxPrice;
                return View("~/Views/Product/AllProduct.cshtml", productVersionMappeds.ToPagedList(pageIndex, pageSize));
            }
            TempData["DangerMessage"] = Message.ConnectFail();
            return RedirectToAction("AllProduct", "Product");
        }
        public ActionResult SearchProductVersion(string keyWord, int pageIndex = 1, int pageSize = 15)
        {
            var url = "api/API_Product/SearchProductVersion/" + keyWord;
            response = serviceObj.GetResponse(url);
            if (response.IsSuccessStatusCode)
            {
                List<ProductVersionMapped> productVersionMappeds = response.Content.ReadAsAsync<List<ProductVersionMapped>>().Result;
                if (productVersionMappeds.Count > 0)
                {
                    ViewData["State"] = "Search";
                    ViewData["Option"] = keyWord;
                    return View("~/Views/Product/AllProduct.cshtml", productVersionMappeds.ToPagedList(pageIndex, pageSize));
                }
                TempData["WarningMessage"] = "Không tìm thấy sản phẩm";
                return Redirect(this.Request.UrlReferrer.ToString());
            }
            else
            {
                TempData["WarningMessage"] = "Không tìm thấy sản phẩm";
                return Redirect(this.Request.UrlReferrer.ToString());
            }
        }

    }
}