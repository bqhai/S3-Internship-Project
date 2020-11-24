using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_CellphoneStore
{
    public class AccountMapped
    {
        public AccountMapped()
        {

        }
        public string Username { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public int AccountTypeID { get; set; }
        public Nullable<int> Coin { get; set; }
        public bool Status { get; set; }
        public string CustomerID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
    }
}
