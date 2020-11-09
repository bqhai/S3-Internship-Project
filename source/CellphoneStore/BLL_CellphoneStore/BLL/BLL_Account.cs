using BLL_CellPhoneStore.Lib;
using DAL_CellPhoneStore.DAL;
using DAL_CellPhoneStore.Model;
using Model_CellphoneStore;
using Model_CellphoneStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_CellPhoneStore.BLL
{
    public class BLL_Account
    {
        DAL_Account dalAccount = new DAL_Account();
        EntityMapper<AccountMapped, Account> accMappedToAcc = new EntityMapper<AccountMapped, Account>();
        public BLL_Account()
        {

        }
        public bool ProcessLogin(string username, string password)
        {
            string passwordMD5 = MD5_Encryptor.HashMD5(password);
            Account account = dalAccount.GetAccount(username);
            if (account != null)
            {
                if(account.Password == passwordMD5)
                {
                    return true;
                }
            }
            return false;
        }
        public int GetAccountType(string username)
        {
            Account account = dalAccount.GetAccount(username);
            if(account != null)
            {
                if (account.AccountTypeID == 1)
                {
                    return 1;
                }
                else if (account.AccountTypeID == 2)
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            }
            return -1;          
        }
        public bool AddNewAccount(AccountMapped accountMapped)
        {
            try
            {
                accountMapped.Password = MD5_Encryptor.HashMD5(accountMapped.Password);
                Account acc = accMappedToAcc.Translate(accountMapped);
                dalAccount.AddNewUserAccount(acc);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
