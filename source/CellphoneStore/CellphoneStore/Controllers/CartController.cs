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
    public class CartController : Controller
    {
        // GET: Cart
        ServiceRepository serviceObj = new ServiceRepository();
        HttpResponseMessage response;
        public const int maxOrderValue = 100000000;
        public ActionResult Cart()
        {
            List<CartItem> cartItems = GetCart();
            Session["Amount"] = GetItemAmount();
            CartInfo cartInfo = new CartInfo();
            cartInfo.CartTotalPrice = GetTotalPrice();        
            if (TempData["Discount"] != null && TempData["Discount"].GetType() != typeof(string) && TempData["PromotionCode"] != null)
            {
                cartInfo.PromotionCode = TempData["PromotionCode"].ToString();
                cartInfo.Discount = Convert.ToInt32(TempData["Discount"]);
                cartInfo.IntoMoney = cartInfo.CartTotalPrice - cartInfo.Discount;
            }
            else
            {
                cartInfo.Discount = 0;
                cartInfo.IntoMoney = cartInfo.CartTotalPrice;
            }
            Session["CartInfo"] = cartInfo;
            return View(cartItems);
        }
        public List<CartItem> GetCart()
        {
            List<CartItem> cartItems = Session["Cart"] as List<CartItem>;
            if (cartItems == null)
            {
                cartItems = new List<CartItem>();
                Session["Cart"] = cartItems;
            }
            return cartItems;
        }
        public ActionResult AddProductToCart(string productVersionID, bool buynow)
        {
            if (GetTotalPrice() > maxOrderValue)
            {
                TempData["WarningMessage"] = "Đơn hàng vượt quá giá trị tối đa";
                return Redirect(this.Request.UrlReferrer.ToString());
            }
            List<CartItem> cartItems = GetCart();
            CartItem cartItem = cartItems.FirstOrDefault(c => c.ProductVersionID == productVersionID);
            if (cartItem == null)
            {
                CartItem newItem = new CartItem(productVersionID);
                cartItems.Add(newItem);
                TempData["InfoMessage"] = "Đã thêm vào giỏ hàng";
            }
            else if (cartItem.Amount > 2)
            {
                TempData["WarningMessage"] = "Số lượng được mua tối đa là 3";
            }
            else
            {
                cartItem.Amount++;
            }
            if (buynow)
            {
                TempData["TotalPrice"] = GetTotalPrice();
                return RedirectToAction("Cart", "Cart");
            }
            else
            {
                Session["Amount"] = GetItemAmount();
                return Redirect(this.Request.UrlReferrer.ToString());
            }
        }
        public ActionResult ReduceProductFromCart(string productVersionID)
        {
            List<CartItem> cartItems = GetCart();
            CartItem cartItem = cartItems.FirstOrDefault(c => c.ProductVersionID == productVersionID);
            if (cartItem != null && cartItem.Amount > 1)
            {
                cartItem.Amount--;
            }
            return RedirectToAction("Cart", "Cart");
        }
        public ActionResult RemoveProductFromCart(string productVersionID)
        {
            List<CartItem> cartItems = GetCart();
            CartItem cartItem = cartItems.FirstOrDefault(c => c.ProductVersionID == productVersionID);
            if (cartItem != null)
            {
                cartItems.Remove(cartItem);
            }
            return RedirectToAction("Cart", "Cart");
        }
        public int GetItemAmount()
        {
            int totalAmount = 0;
            List<CartItem> cartItems = Session["Cart"] as List<CartItem>;
            if (cartItems != null)
            {
                totalAmount = cartItems.Sum(c => c.Amount);
            }
            return totalAmount;
        }
        public int GetTotalPrice()
        {
            int totalPrice = 0;
            List<CartItem> cartItems = Session["Cart"] as List<CartItem>;
            if (cartItems != null)
            {
                totalPrice = cartItems.Sum(c => c.TotalPrice);
            }
            return totalPrice;
        }
        public dynamic GetPromotionCodeValue(string prmCode)
        {
            if (Session["Account"] == null)
            {
                return -1;
            }
            else
            {
                var url = "api/API_Cart/GetResultUsingPromotionCode/" + prmCode + "/" + Session["Account"].ToString() + "/" + GetTotalPrice();
                response = serviceObj.GetResponse(url);
                if (response.IsSuccessStatusCode)
                {
                    dynamic result = response.Content.ReadAsAsync<dynamic>().Result;
                    return result;
                }
            }
            return -2;
        }
        public ActionResult GetResultUsingPromotionCode(PromotionCodeMapped prmCode)
        {
            dynamic promotionCodeValue = GetPromotionCodeValue(prmCode.Code);
            if (promotionCodeValue.GetType() == typeof(string))
            {
                TempData["WarningMessage"] = promotionCodeValue;
            }
            else if (promotionCodeValue == -1)
            {
                TempData["WarningMessage"] = "Bạn chưa đăng nhập";
            }
            else
            {
                TempData["SuccessMessage"] = "Đã áp dụng mã giảm giá";
                TempData["PromotionCode"] = prmCode.Code;
                TempData["Discount"] = promotionCodeValue;
            }
            return RedirectToAction("Cart", "Cart");
        }
        public ActionResult GetShipmentDetails(string username)
        {
            var url = "api/API_User/GetCustomerByUsername/" + username;
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