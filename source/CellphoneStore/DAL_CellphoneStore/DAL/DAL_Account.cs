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
            return db.Accounts.SingleOrDefault(acc => acc.Username == username);
        }
        public void AddNewUserAccount(Account account)
        {
            account.AccountTypeID = 3;
            account.Status = true;
            db.Accounts.Add(account);
            db.SaveChanges();
        }
        public void UpdateAccount(string username, string oldPassword, string newPassword)
        {
            Account acc = db.Accounts.SingleOrDefault(a => a.Username == username && a.Password == oldPassword);
            if(acc != null)
            {
                acc.Password = newPassword;
                db.SaveChanges();
            }
        }
    }
}
