using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_CellphoneStore
{
    public class ProductVersionMapped
    {
        public ProductVersionMapped()
        {

        }
        public string ProductVersionID { get; set; }
        public string ProductID { get; set; }
        public string ProductVersionName { get; set; }
        public string RAM { get; set; }
        public string ROM { get; set; }
        public string Color { get; set; }
        public int ListPrice { get; set; }
        public int Price { get; set; }
        public int QuantityInStock { get; set; }
        public bool Status { get; set; }
        public string Image { get; set; }
    }
}
