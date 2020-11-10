using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_CellphoneStore
{
    public class PromotionCodeMapped
    {
        public PromotionCodeMapped()
        {

        }
        public string Code { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public int Maximum { get; set; }
        public int Require { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime ExpiryDate { get; set; }
    }
}
