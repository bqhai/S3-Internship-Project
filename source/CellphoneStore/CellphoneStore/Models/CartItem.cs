using CellphoneStore.Repository;
using Model_CellphoneStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace CellphoneStore.Models
{
    public class CartItem
    {
        ServiceRepository serviceObj = new ServiceRepository();
        HttpResponseMessage response;
        public string ProductVersionID { get; set; }
        public string ProductVersionName { get; set; }
        public int ListPrice { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public int Amount { get; set; }
        public int TotalPrice
        {
            get { return Amount * Price; }
        }
        public ProductVersionMapped GetProductVersionByID(string productVersionID)
        {
            var url = "api/API_Product/GetProductVersionByID/" + productVersionID;
            response = serviceObj.GetResponse(url);
            response.EnsureSuccessStatusCode();
            ProductVersionMapped productVersionMapped = response.Content.ReadAsAsync<ProductVersionMapped>().Result;
            return productVersionMapped;
        }
        public CartItem(string productVersionID)
        {
            ProductVersionMapped prdv = GetProductVersionByID(productVersionID);
            ProductVersionID = productVersionID;
            ProductVersionName = prdv.ProductVersionName;
            ListPrice = prdv.ListPrice;
            Price = prdv.Price;
            Image = prdv.Image;
            Amount = 1;
        }
    }
}