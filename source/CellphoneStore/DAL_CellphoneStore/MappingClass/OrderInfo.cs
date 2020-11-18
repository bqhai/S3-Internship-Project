﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_CellPhoneStore.MappingClass
{
    public class OrderInfo
    {
        public string OrderID { get; set; }
        public string Payment { get; set; }
        public string Delivery { get; set; }
        public string Notes { get; set; }
        public System.DateTime OrderDate { get; set; }
        public int IntoMoney { get; set; }
        public string CustomerID { get; set; }
        public int OrderStateID { get; set; }
        public string OrderDescription { get; set; }
        public int DeliveryStateID { get; set; }
        public string DeliveryDescription { get; set; }
    }
}
