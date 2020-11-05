using DAL_CellPhoneStore.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_CellPhoneStore.DAL
{
    public class DAL_Account
    {
        DbCellPhoneStoreEntities db = new DbCellPhoneStoreEntities();
        public DAL_Account()
        {

        }
        public Account GetAccount(string username)
        {
            return db.Accounts.Where(acc => acc.Username == username).FirstOrDefault();
        }
    }
}
