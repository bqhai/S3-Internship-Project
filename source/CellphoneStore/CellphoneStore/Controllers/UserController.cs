using CellphoneStore.Repository;
using Microsoft.Ajax.Utilities;
using Model_CellphoneStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace CellphoneStore.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        ServiceRepository serviceObj = new ServiceRepository();
        HttpResponseMessage response;
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult AccountInfo(int chooseOption)
        {
            TempData["ChooseOption"] = chooseOption;           
            return View();
        }
        public ActionResult CustomerInfo()
        {
            var url = "api/API_User/GetCustomerByUsername/" + Session["Account"].ToString();
            response = serviceObj.GetResponse(url);          
            if (response.IsSuccessStatusCode)
            {
                CustomerMapped customerMapped = response.Content.ReadAsAsync<CustomerMapped>().Result;
                return PartialView(customerMapped);
            }
            return PartialView();
        }
        public ActionResult Notification()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult ProcessLogin(AccountMapped accountMapped)
        {
            var url = "api/API_User/ProcessLogin/" + accountMapped.Username + "/" + accountMapped.Password;
            response = serviceObj.GetResponse(url);
            response.EnsureSuccessStatusCode();
            var resultLogin = response.Content.ReadAsAsync<bool>().Result;
            if (resultLogin)
            {
                int accountType = GetAccountType(accountMapped.Username);
                Session["Account"] = accountMapped.Username;
                TempData["SuccessMessage"] = "Xin chào" + "  " + accountMapped.Username;
                if (accountType == 1)
                {                  
                    return Redirect(this.Request.UrlReferrer.ToString());
                }
                else if(accountType == 2)
                {
                    return Redirect(this.Request.UrlReferrer.ToString());
                }
                else 
                {
                    return Redirect(this.Request.UrlReferrer.ToString());
                }               
            }
            else
            {
                TempData["DangerMessage"] = "Tài khoản hoặc mật khẩu không chính xác";
                return Redirect(this.Request.UrlReferrer.ToString());
            }
        }
        public int GetAccountType(string username)
        {
            var url = "api/API_User/GetAccountType/" + username;
            response = serviceObj.GetResponse(url);
            response.EnsureSuccessStatusCode();
            var accountType = response.Content.ReadAsAsync<int>().Result;
            return accountType;
        }
        public ActionResult ProcessLogout()
        {
            Session.Remove("Account");
            Session.Remove("Cart");
            Session.Remove("Amount");
            return RedirectToAction("Index", "Home");
        }
        public ActionResult ProcessRegister(AccountMapped accountMapped, CustomerMapped customerMapped)
        {
            response = serviceObj.GetResponse("api/API_User/AccountAlreadyExists/" + accountMapped.Username);
            var resultCheck = response.Content.ReadAsAsync<bool>().Result;
            if (!resultCheck)
            {
                HttpResponseMessage resAddAcc = serviceObj.PostResponse("api/API_User/AddNewAccount/", accountMapped);              
                var resultAddAcc = resAddAcc.Content.ReadAsAsync<bool>().Result;
                
                if (resultAddAcc)
                {                   
                    HttpResponseMessage resAddCus = serviceObj.PostResponse("api/API_User/AddNewCustomer/", customerMapped);
                    var resultAddCus = resAddCus.Content.ReadAsAsync<bool>().Result;
                    if (resultAddCus)
                    {
                        TempData["SuccessMessage"] = "Đăng ký thành công!";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["DangerMessage"] = "Đăng ký thất bại";
                        return RedirectToAction("Register", "User");
                    }
                }
                else
                {
                    TempData["DangerMessage"] = "Đăng ký thất bại";
                    return RedirectToAction("Register", "User");
                }
            }
            else
            {
                TempData["DangerMessage"] = "Tài khoản này đã tồn tại";
                return RedirectToAction("Register", "User");
            }
            
        }
        [HttpPost]
        public ActionResult UpdateCustomer(CustomerMapped customerMapped)
        {
            customerMapped.Username = Session["Account"].ToString();
            customerMapped.Gender = Request.Form["Gender"].ToString();
            response = serviceObj.PostResponse("api/API_User/UpdateCustomer/", customerMapped);
            if (response.IsSuccessStatusCode)
            {
                var resultUpdateCus = response.Content.ReadAsAsync<bool>().Result;
                if (resultUpdateCus)
                {
                    TempData["SuccessMessage"] = "Cập nhật thông tin thành công";                  
                }
                else
                {
                    TempData["DangerMessage"] = "Cập nhật thông tin thất bại";
                }
                return Redirect(this.Request.UrlReferrer.ToString());
            }
            return Redirect(this.Request.UrlReferrer.ToString());
        }
        [HttpPost]
        public ActionResult ChangePassword(AccountMapped accountMapped, FormCollection c)
        {
            accountMapped.Username = Session["Account"].ToString();
            accountMapped.Password = c["OldPassword"];
            accountMapped.NewPassword = c["NewPassword"];
            response = serviceObj.PostResponse("api/API_User/ChangePassword/", accountMapped);
            if (response.IsSuccessStatusCode)
            {
                var resultChangePass = response.Content.ReadAsAsync<int>().Result;
                if(resultChangePass == 1)
                {
                    TempData["SuccessMessage"] = "Đổi mật khẩu thành công";
                }
                else if(resultChangePass == -1)
                {
                    TempData["DangerMessage"] = "Mật khẩu cũ không đúng";
                }
                else
                {
                    TempData["DangerMessage"] = "Thất bại";
                }
                return Redirect(this.Request.UrlReferrer.ToString());
            }
            return Redirect(this.Request.UrlReferrer.ToString());
        }
    }
}