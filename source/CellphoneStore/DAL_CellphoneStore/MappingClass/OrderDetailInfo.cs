using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_CellPhoneStore.MappingClass
{
    public class OrderDetailInfo
    {
        public string OrderID { get; set; }
        public string ProductVersionID { get; set; }
        public string ProductVersionName { get; set; }
        public int Amount { get; set; }
    }
}
