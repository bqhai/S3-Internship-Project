using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CellphoneStore.Repository
{
    public static class Common
    {
        public static string AutoGenVerifyCode()
        {
            int min = 100000;
            int max = 999999;
            Random rdm = new Random();
            return rdm.Next(min, max).ToString();
        }
    }
}