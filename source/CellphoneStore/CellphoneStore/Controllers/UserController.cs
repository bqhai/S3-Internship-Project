using CellphoneStore.Repository;
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
        ServiceRepository serviceObj = new ServiceRepository();
        HttpResponseMessage response;
        #region Login/Logout/Register
        public ActionResult Login()
        {
            return View();
        }
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
                    return RedirectToAction("Index", "Admin");
                }
                else if (accountType == 2)
                {
                    return RedirectToAction("Index", "Admin");
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
        public ActionResult ProcessLogout()
        {
            Session.Remove("Account");
            Session.Remove("Cart");
            Session.Remove("Amount");
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Register()
        {
            return View();
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
            return PartialView();
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
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public int GetAccountType(string username)
        {
            var url = "api/API_User/GetAccountType/" + username;
            response = serviceObj.GetResponse(url);
            response.EnsureSuccessStatusCode();
            var accountType = response.Content.ReadAsAsync<int>().Result;
            return accountType;
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        public ActionResult ProcessForgotPassword(string email)
        {
            string resetCode = Guid.NewGuid().ToString();
            var verifyUrl = "/User/ResetPassword/";
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            var url = "api/API_User/GetCustomerByEmail/" + email + "/";
            response = serviceObj.GetResponse(url);
            CustomerMapped customerMapped = response.Content.ReadAsAsync<CustomerMapped>().Result;
            if (customerMapped != null)
            {
                customerMapped.ResetPasswordCode = resetCode;
                response = serviceObj.PutResponse("api/API_User/AddResetPasswordCode/", customerMapped);
                var resultAddCode = response.Content.ReadAsAsync<bool>().Result;
                if (resultAddCode)
                {
                    var subject = "Yêu cầu cấp lại mật khẩu";
                    var body = "Xin chào " + customerMapped.Name + ", "
                        + "<br/> Bạn vừa gửi yêu cầu cấp lại mật khẩu mới.! "
                        + "<br/> Mã xác thực của bạn là: " + resetCode + "<br/>"
                        + "Nếu bạn không gửi yêu cầu này, vui lòng bỏ qua email này hoặc thông báo lại với chúng tôi!<br/><br/> Thank you";
                    SendEmail(customerMapped.Email, body, subject);
                    TempData["SuccessMessage"] = "Yêu cầu cấp lại mật khẩu đã được gửi tới email của bạn.";
                    Session["EmailForResetPass"] = email;
                    return RedirectToAction("ResetPassword", "User");
                }
                else
                {
                    TempData["DangerMessage"] = "Lỗi gửi mã xác thực.";
                }               
            }
            else
            {
                TempData["DangerMessage"] = "Email không tồn tại";
            }
            return RedirectToAction("ForgotPassword", "User");
        }
        public ActionResult ResetPassword()
        {
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
            CustomerMapped customerMapped = response.Content.ReadAsAsync<CustomerMapped>().Result;
            if(customerMapped != null)
            {
                if(customerMapped.ResetPasswordCode == resetCode)
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
                    }
                }
                else
                {
                    TempData["DangerMessage"] = "Mã xác thực không đúng";                   
                }

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
                    TempData["DangerMessage"] = "Cập nhật thông tin thất bại";
                }
                return Redirect(this.Request.UrlReferrer.ToString());
            }
            TempData["DangerMessage"] = "Cập nhật thông tin thất bại";
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
            return PartialView();
        }

    }
}