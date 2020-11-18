using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_CellphoneStore
{
    public class OrderDetailInfoMapped
    {
        public string OrderID { get; set; }
        public string ProductVersionID { get; set; }
        public string ProductVersionName { get; set; }
        public int Amount { get; set; }
    }
}
