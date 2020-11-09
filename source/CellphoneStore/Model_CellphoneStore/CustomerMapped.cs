using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_CellphoneStore
{
    public class CustomerMapped
    {
        public CustomerMapped()
        {

        }
        public string CustomerID { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
    }
}
