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
                if(accountType == 1)
                {
                    Session["Account"] = accountMapped.Username;
                    return Redirect(this.Request.UrlReferrer.ToString());
                }
                else if(accountType == 2)
                {
                    Session["Account"] = accountMapped.Username;
                    return Redirect(this.Request.UrlReferrer.ToString());
                }
                else 
                {
                    Session["Account"] = accountMapped.Username;
                    return Redirect(this.Request.UrlReferrer.ToString());
                }
            }
            else
            {
                TempData["Message"] = "Username or password is incorrect";
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
            return Redirect(this.Request.UrlReferrer.ToString());
        }
        public ActionResult ProcessRegister(AccountMapped accountMapped, CustomerMapped customerMapped)
        {
            HttpResponseMessage resAddAcc = serviceObj.PostResponse("api/API_User/AddNewAccount/", accountMapped);
            HttpResponseMessage resAddCus = serviceObj.PostResponse("api/API_User/AddNewCustomer/", customerMapped);
            var resultAddAcc = resAddAcc.Content.ReadAsAsync<bool>().Result;
            var resultAddCus = resAddCus.Content.ReadAsAsync<bool>().Result;
            if(resultAddAcc  && resultAddCus)
            {
                TempData["Message"] = "Register successfully";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Message"] = "Register failed";
                return RedirectToAction("Register", "User");
            }
            
        }
    }
}