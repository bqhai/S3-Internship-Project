using CellphoneStore.Repository;
using log4net;
using Microsoft.Ajax.Utilities;
using Model_CellphoneStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace CellphoneStore.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ServiceRepository serviceObj = new ServiceRepository();
        HttpResponseMessage response;
        #region Login/Logout/Register
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult ProcessLogin(AccountMapped accountMapped)
        {
            Log.Info("Start process login");
            var url = "api/API_User/ProcessLogin/" + accountMapped.Username + "/" + accountMapped.Password + "/";
            Log.Info("Call api login");
            response = serviceObj.GetResponse(url);
            Log.Info("Status code: " + response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                var resultLogin = response.Content.ReadAsAsync<bool>().Result;
                int accountType = GetAccountType(accountMapped.Username);             

                if (resultLogin && accountType != -1)
                {
                    Log.Info("Get api token for " + accountMapped.Username);
                    string token = GetAPIToken(accountMapped.Username);
                    HttpCookie cookie = new HttpCookie("Token", token);
                    Response.Cookies.Add(cookie);
                    Session["Account"] = accountMapped.Username;
                    if (accountType == 1)
                    {
                        Log.Info("Login successfully with role admin");
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (accountType == 2)
                    {
                        Log.Info("Login successfully with role employee");
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        Log.Info("Login successfully with role user");                      
                        TempData["SuccessMessage"] = "Xin chào" + "  " + accountMapped.Username;
                        return Redirect(this.Request.UrlReferrer.ToString());
                    }
                }
                else
                {
                    Log.Info("Login failed, wrong username or password");
                    TempData["DangerMessage"] = Message.AuthenticateFail();
                    return Redirect(this.Request.UrlReferrer.ToString());
                }
            }
            Log.Error("Connect failed");
            TempData["DangerMessage"] = Message.ConnectFail();
            return Redirect(this.Request.UrlReferrer.ToString());
        }
        public string GetAPIToken(string username)
        {
            var url = "api/API_User/GenToken/" + username;
            response = serviceObj.GetResponse(url);
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<string>().Result;               
            }
            return null;
        }
        public ActionResult ProcessLogout()
        {
            Log.Info("Start process logout");
            Log.Info("Remove Session Account/Cart/Amount");
            Session.Remove("Account");
            Session.Remove("Cart");
            Session.Remove("Amount");
            Log.Info("Logout successfully, redirect to Index-Home");
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult ProcessRegister(AccountMapped accountMapped, CustomerMapped customerMapped)
        {
            response = serviceObj.GetResponse("api/API_User/AccountAlreadyExists/" + accountMapped.Username);
            if (response.IsSuccessStatusCode)
            {
                var resultCheck = response.Content.ReadAsAsync<bool>().Result;
                if (!resultCheck)
                {
                    response = serviceObj.PostResponse("api/API_User/AddNewAccount/", accountMapped);
                    var resultAddAcc = response.Content.ReadAsAsync<bool>().Result;
                   
                    if (resultAddAcc)
                    {
                        TempData["SuccessMessage"] = Message.RegisterSuccess();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["DangerMessage"] = Message.RegisterFail() + "Vui lòng kiểm tra lại email và số điện thoại";
                        return RedirectToAction("Register", "User");
                    }
                }
                else
                {
                    TempData["DangerMessage"] = Message.UserAlreadyExists();
                    return RedirectToAction("Register", "User");
                }
            }           
            return RedirectToAction("Register", "User");
        }
        #endregion

        public ActionResult AccountInfo(int chooseOption)
        {
            TempData["ChooseOption"] = chooseOption;
            return View();
        }
        public ActionResult CustomerInfo()
        {
            if (Session["Account"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var url = "api/API_User/GetCustomerByUsername/" + Session["Account"].ToString();
            response = serviceObj.GetResponse(url);
            if (response.IsSuccessStatusCode)
            {
                CustomerMapped customerMapped = response.Content.ReadAsAsync<CustomerMapped>().Result;
                return PartialView(customerMapped);
            }
            TempData["DangerMessage"] = Message.ConnectFail();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Notification()
        {
            return PartialView();
        }
        public ActionResult OrderInfo()
        {
            if (Session["Account"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var url = "api/API_User/GetListOrderByUserName/" + Session["Account"].ToString();
            response = serviceObj.GetResponse(url);
            if (response.IsSuccessStatusCode)
            {
                List<OrderInfoMapped> orderInfoMappeds = response.Content.ReadAsAsync<List<OrderInfoMapped>>().Result;
                return PartialView(orderInfoMappeds);
            }
            TempData["DangerMessage"] = Message.ConnectFail();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult OrderDetailInfo(string orderID)
        {
            if (Session["Account"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var url = "api/API_User/GetListOrderDetailByOrderID/" + orderID;
            response = serviceObj.GetResponse(url);
            if (response.IsSuccessStatusCode)
            {
                List<OrderDetailInfoMapped> orderDetailInfoMappeds = response.Content.ReadAsAsync<List<OrderDetailInfoMapped>>().Result;
                return PartialView(orderDetailInfoMappeds);
            }
            TempData["DangerMessage"] = Message.ConnectFail();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public int GetAccountType(string username)
        {
            var url = "api/API_User/GetAccountType/" + username;
            response = serviceObj.GetResponse(url);
            if (response.IsSuccessStatusCode)
            {
                var accountType = response.Content.ReadAsAsync<int>().Result;
                return accountType;
            }
            return -1;
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        
        public ActionResult ProcessForgotPassword(string email)
        {
            string resetCode = Common.AutoGenVerifyCode();
            //var verifyUrl = "/User/ResetPassword/";
            //var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            var url = "api/API_User/GetCustomerByEmail/" + email + "/";
            response = serviceObj.GetResponse(url);
            if (response.IsSuccessStatusCode)
            {
                CustomerMapped customerMapped = response.Content.ReadAsAsync<CustomerMapped>().Result;
                if (customerMapped != null)
                {
                    customerMapped.ResetPasswordCode = resetCode;
                    response = serviceObj.PutResponse("api/API_User/AddResetPasswordCode/", customerMapped);
                    if (response.IsSuccessStatusCode)
                    {
                        var resultAddVerifyCode = response.Content.ReadAsAsync<bool>().Result;
                        if (resultAddVerifyCode)
                        {
                            var subject = "Yêu cầu cấp lại mật khẩu";
                            var body = "Xin chào " + customerMapped.Name + ", "
                                + "<br/> Bạn vừa gửi yêu cầu cấp lại mật khẩu mới.! "
                                + "<br/> Mã xác thực của bạn là: " + "<strong>" + resetCode + "</strong>" + "<br/>"
                                + "Nếu bạn không gửi yêu cầu này, vui lòng bỏ qua email này hoặc thông báo lại với chúng tôi!<br/><br/> Thank you";
                            SendEmail(customerMapped.Email, body, subject);
                            TempData["SuccessMessage"] = "Yêu cầu cấp lại mật khẩu đã được gửi tới email của bạn.";
                            Session["EmailForResetPass"] = email;
                            return RedirectToAction("ResetPassword", "User");
                        }
                        else
                        {
                            TempData["DangerMessage"] = Message.SendVerifyCodeFail();
                            return RedirectToAction("ForgotPassword", "User");
                        }
                    }
                    else
                    {
                        TempData["DangerMessage"] = Message.ConnectFail();
                        return RedirectToAction("ForgotPassword", "User");
                    }
                }
                else
                {
                    TempData["DangerMessage"] = Message.EmailNotExists();
                    return RedirectToAction("ForgotPassword", "User");
                }
            }
            TempData["DangerMessage"] = Message.ConnectFail();
            return RedirectToAction("ForgotPassword", "User");
        }
        public ActionResult ResetPassword()
        {
            if (Session["EmailForResetPass"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult ProcessResetPassword(AccountMapped accountMapped)
        {
            if(Session["EmailForResetPass"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var resetCode = Request.Form["ResetCode"];
            var url = "api/API_User/GetCustomerByEmail/" + Session["EmailForResetPass"].ToString() + "/";
            response = serviceObj.GetResponse(url);
            if (response.IsSuccessStatusCode)
            {
                CustomerMapped customerMapped = response.Content.ReadAsAsync<CustomerMapped>().Result;
                if (customerMapped != null)
                {
                    if (customerMapped.ResetPasswordCode == resetCode)
                    {
                        accountMapped.Username = customerMapped.Username;
                        response = serviceObj.PutResponse("api/API_User/ResetPassword/", accountMapped);
                        var resultResetPass = response.Content.ReadAsAsync<bool>().Result;
                        if (resultResetPass)
                        {
                            TempData["SuccessMessage"] = "Đổi mật khẩu thành công";
                            Session.Remove("EmailForResetPass");
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            TempData["DangerMessage"] = "Thất bại";
                            return Redirect(this.Request.UrlReferrer.ToString());
                        }
                    }
                    else
                    {
                        TempData["DangerMessage"] = "Mã xác thực không đúng";
                        return Redirect(this.Request.UrlReferrer.ToString());
                    }

                }
            }
            else
            {
                TempData["DangerMessage"] = Message.ConnectFail();               
            }
            return Redirect(this.Request.UrlReferrer.ToString());
        }
        private void SendEmail(string receiveEmail, string body, string subject)
        {
            using (MailMessage mailMessage = new MailMessage("nguyenvannammmrv2@gmail.com", receiveEmail))
            {
                mailMessage.Subject = subject;
                mailMessage.Body = body;

                mailMessage.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("nguyenvannammmrv2@gmail.com", "7412325789");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mailMessage);
            }
        }
        [HttpPost]
        public ActionResult UpdateCustomerInfo(CustomerMapped customerMapped)
        {
            if (Session["Account"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            customerMapped.Username = Session["Account"].ToString();
            customerMapped.Gender = Request.Form["Gender"].ToString();
            response = serviceObj.PutResponse("api/API_User/UpdateCustomerInfo/", customerMapped);
            if (response.IsSuccessStatusCode)
            {
                var resultUpdateCus = response.Content.ReadAsAsync<bool>().Result;
                if (resultUpdateCus)
                {
                    TempData["SuccessMessage"] = "Cập nhật thông tin thành công";
                }
                else
                {
                    TempData["DangerMessage"] = "Số điện thoại hoặc email đã tồn tại";
                }
                return Redirect(this.Request.UrlReferrer.ToString());
            }
            TempData["DangerMessage"] = Message.ConnectFail();
            return Redirect(this.Request.UrlReferrer.ToString());
        }
        [HttpPost]
        public ActionResult UpdateCustomerAddress(CustomerMapped customerMapped)
        {
            if (Session["Account"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            customerMapped.Username = Session["Account"].ToString();
            var houseNumber = Request.Form["HouseNumber"];
            var ward = Request.Form["Ward"];
            var district = Request.Form["District"];
            var province = Request.Form["Province"];

            customerMapped.Address = houseNumber + ", " + ward + ", " + district + ", " + province;
            response = serviceObj.PutResponse("api/API_User/UpdateCustomerAddress/", customerMapped);
            if (response.IsSuccessStatusCode)
            {
                var resultUpdateAddress = response.Content.ReadAsAsync<bool>().Result;
                if (resultUpdateAddress)
                {
                    TempData["SuccessMessage"] = "Cập nhật địa chỉ thành công";
                }
                else
                {
                    TempData["DangerMessage"] = "Cập nhật địa chỉ thất bại";
                }
                return Redirect(this.Request.UrlReferrer.ToString());
            }
            TempData["DangerMessage"] = Message.ConnectFail();
            return Redirect(this.Request.UrlReferrer.ToString());
        }
        [HttpPost]
        public ActionResult ChangePassword(AccountMapped accountMapped, FormCollection c)
        {
            if (Session["Account"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            accountMapped.Username = Session["Account"].ToString();
            accountMapped.Password = c["OldPassword"];
            accountMapped.NewPassword = c["NewPassword"];
            response = serviceObj.PutResponse("api/API_User/ChangePassword/", accountMapped);
            if (response.IsSuccessStatusCode)
            {
                var resultChangePass = response.Content.ReadAsAsync<int>().Result;
                if (resultChangePass == 1)
                {
                    TempData["SuccessMessage"] = "Đổi mật khẩu thành công";
                }
                else if (resultChangePass == -1)
                {
                    TempData["DangerMessage"] = "Mật khẩu cũ không đúng";
                }
                else
                {
                    TempData["DangerMessage"] = "Thất bại";
                }
                return Redirect(this.Request.UrlReferrer.ToString());
            }
            TempData["DangerMessage"] = Message.ConnectFail();
            return Redirect(this.Request.UrlReferrer.ToString());
        }
        public ActionResult CustomerAddress()
        {
            if (Session["Account"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var url = "api/API_User/GetCustomerByUsername/" + Session["Account"].ToString();
            response = serviceObj.GetResponse(url);
            if (response.IsSuccessStatusCode)
            {
                CustomerMapped customerMapped = response.Content.ReadAsAsync<CustomerMapped>().Result;
                return PartialView(customerMapped);
            }
            TempData["DangerMessage"] = Message.ConnectFail();
            return RedirectToAction("Index", "Home");
        }
    }
}