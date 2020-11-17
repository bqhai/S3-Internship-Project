using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CellphoneStore.Models
{
    public class CartInfo
    {
        public int CartTotalPrice { get; set; }
        public int Discount { get; set; }
        public int IntoMoney { get; set; }
        public string PromotionCode { get; set; }
        public int Amount { get; set; }
    }
}