using CellphoneStore.Models;
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
    public class PaymentController : Controller
    {
        // GET: Payment
        ServiceRepository serviceObj = new ServiceRepository();
        HttpResponseMessage response;
        public ActionResult Payment()
        {
            if (Session["Account"] == null)
            {
                TempData["WarningMessage"] = "Bạn chưa đăng nhập";
                return RedirectToAction("Cart", "Cart");
            }
            List<CartItem> cartItems = Session["Cart"] as List<CartItem>;
            //ViewBag.TotalPrice = totalPrice;
            //ViewBag.Discount = discount;
            //ViewBag.IntoMoney = intoMoney;
            ViewBag.PaymentInfo = TempData["CartInfo"];
            ViewBag.Coin = GetCoin();
            return View(cartItems);
        }
        public int GetCoin()
        {
            response = serviceObj.GetResponse("api/API_User/GetCoin/" + Session["Account"].ToString());
            var resultGetCoin = response.Content.ReadAsAsync<int>().Result;
            return resultGetCoin;
        }
        public string GetCustomerID()
        {
            var url = "api/API_User/GetCustomerByUsername/" + Session["Account"].ToString();
            response = serviceObj.GetResponse(url);
            CustomerMapped customerMapped = response.Content.ReadAsAsync<CustomerMapped>().Result;
            return customerMapped.CustomerID;
        }
        [HttpPost]
        public ActionResult ProcessOrder()
        {
            OrderMapped orderMapped = new OrderMapped()
            {
                Delivery = Request.Form["Delivery"],
                Payment = Request.Form["Payment"],
                IntoMoney = Convert.ToInt32(TempData["IntoMoney"]),
                CustomerID = GetCustomerID()

            };
                      
            response = serviceObj.PostResponse("api/API_Payment/AddNewOrder/", orderMapped);
            var resultAddOrder = response.Content.ReadAsAsync<bool>().Result;

            if (resultAddOrder)
            {
                List<CartItem> cartItems = Session["Cart"] as List<CartItem>;
                foreach (var item in cartItems)
                {
                    OrderDetailMapped orderDetailMapped = new OrderDetailMapped()
                    {
                        ProductVersionID = item.ProductVersionID,
                        Amount = item.Amount
                    };
                    response = serviceObj.PostResponse("api/API_Payment/AddNewOrderDetail/", orderDetailMapped);
                    var resultAddOrderDetail = response.Content.ReadAsAsync<bool>().Result;
                    if (resultAddOrderDetail == false)
                    {
                        TempData["DangerMessage"] = "Đặt mua thất bại";
                        return RedirectToAction("Index", "Home");
                    }
                }
                TempData["SuccessMessage"] = "Đặt mua thành công";
                Session.Remove("Cart");
            }
            else
            {
                TempData["DangerMessage"] = "Đặt mua thất bại";
            }
            return RedirectToAction("Index", "Home");
        }
    }
}