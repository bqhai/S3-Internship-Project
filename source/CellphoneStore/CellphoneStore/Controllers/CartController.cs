using CellphoneStore.Models;
using Model_CellphoneStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CellphoneStore.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Cart()
        {
            List<CartItem> cartItems = GetCart();
            ViewBag.TotalAmount = GetTotalAmount();
            ViewBag.TotalPrice = GetTotalPrice();
            return View(cartItems);
        }
        public List<CartItem> GetCart()
        {
            List<CartItem> cartItems = Session["Cart"] as List<CartItem>;
            if(cartItems == null)
            {
                cartItems = new List<CartItem>();
                Session["Cart"] = cartItems;
            }
            return cartItems;
        }
        public ActionResult AddProductToCart(string productVersionID)
        {
            List<CartItem> cartItems = GetCart();
            CartItem cartItem = cartItems.FirstOrDefault(c => c.ProductVersionID == productVersionID);
            if(cartItem == null)
            {
                CartItem newItem = new CartItem(productVersionID);
                cartItems.Add(newItem);
                TempData["InfoMessage"] = "Đã thêm vào giỏ hàng";
            }
            else if(cartItem.Amount > 2)
            {          
                TempData["WarningMessage"] = "Số lượng được mua tối đa là 3";                           
            }
            else
            {
                cartItem.Amount++;
            }
            return Redirect(this.Request.UrlReferrer.ToString());
        }
        public ActionResult ReduceProductFromCart(string productVersionID)
        {
            List<CartItem> cartItems = GetCart();
            CartItem cartItem = cartItems.FirstOrDefault(c => c.ProductVersionID == productVersionID);
            if(cartItem != null && cartItem.Amount > 1)
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
        public int GetTotalAmount()
        {
            int totalAmount = 0;
            List<CartItem> cartItems = Session["Cart"] as List<CartItem>;
            if(cartItems != null)
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
    }
}