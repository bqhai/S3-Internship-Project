using CellphoneStore.Repository;
using Model_CellphoneStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace CellphoneStore.Controllers
{
    public class AdminController : Controller
    {
        ServiceRepository serviceObj = new ServiceRepository();
        HttpResponseMessage response;
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        #region Employee Management
        public ActionResult Employee()
        {
            var url = "api/API_Admin/GetAllEmployee";
            response = serviceObj.GetResponse(url);
            if (response.IsSuccessStatusCode)
            {
                List<EmployeeMapped> employeeMappeds = response.Content.ReadAsAsync<List<EmployeeMapped>>().Result;
                return View(employeeMappeds);
            }
            TempData["DangerMessage"] = "Kết nối server thất bại";
            return RedirectToAction("Index", "Admin");
        }
        [HttpPost]
        public ActionResult AddNewEmployee(EmployeeMapped employeeMapped)
        {
            response = serviceObj.PostResponse("api/API_Admin/AddNewEmployee/", employeeMapped);
            if (response.IsSuccessStatusCode)
            {
                var resultAddEmp = response.Content.ReadAsAsync<bool>().Result;
                if (resultAddEmp)
                {
                    return RedirectToAction("Employee", "Admin");
                }
                else
                {
                    TempData["DangerMessage"] = "Số điện thoại hoặc email đã tồn tại";
                    return Redirect(this.Request.UrlReferrer.ToString());
                }
            }
            TempData["DangerMessage"] = "Kết nối server thất bại";
            return Redirect(this.Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult UpdateEmployee(EmployeeMapped employeeMapped, string employeeID)
        {
            employeeMapped.EmployeeID = employeeID;
            response = serviceObj.PutResponse("api/API_Admin/UpdateEmployee/", employeeMapped);
            if (response.IsSuccessStatusCode)
            {
                var resultUpdateEmp = response.Content.ReadAsAsync<bool>().Result;
                if (resultUpdateEmp)
                {
                    TempData["SuccessMessage"] = "Cập nhật thông tin thành công";
                }
                else
                {
                    TempData["DangerMessage"] = "Số điện thoại hoặc email đã tồn tại";
                }
                return Redirect(this.Request.UrlReferrer.ToString());
            }
            TempData["DangerMessage"] = "Kết nối server thất bại";
            return Redirect(this.Request.UrlReferrer.ToString());
        }
        #endregion
        #region Customer Management
        public ActionResult Customer()
        {
            var url = "api/API_Admin/GetAllCustomer";
            response = serviceObj.GetResponse(url);
            if (response.IsSuccessStatusCode)
            {
                List<CustomerMapped> customerMappeds = response.Content.ReadAsAsync<List<CustomerMapped>>().Result;
                return View(customerMappeds);
            }
            TempData["DangerMessage"] = Message.ConnectFail();
            return RedirectToAction("Index", "Admin");
        }
        #endregion
        #region Product Management
        public ActionResult Product()
        {
            var url = "api/API_Admin/GetAllProduct";
            response = serviceObj.GetResponse(url);
            if (response.IsSuccessStatusCode)
            {
                List<ProductInfoMapped> productInfoMappeds = response.Content.ReadAsAsync<List<ProductInfoMapped>>().Result;
                return View(productInfoMappeds);
            }
            TempData["DangerMessage"] = Message.ConnectFail();
            return RedirectToAction("Index", "Admin");
        }
        public ActionResult ProductVersion(string productID)
        {
            var url = "api/API_Admin/GetListProductVersionByProductID/" + productID;
            response = serviceObj.GetResponse(url);
            if (response.IsSuccessStatusCode)
            {
                List<ProductVersionMapped> productVersionMappeds = response.Content.ReadAsAsync<List<ProductVersionMapped>>().Result;
                return PartialView(productVersionMappeds);
            }
            TempData["DangerMessage"] = Message.ConnectFail();
            return RedirectToAction("Index", "Admin");
        }
        #endregion
    }
}