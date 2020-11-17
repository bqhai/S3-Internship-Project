using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_CellPhoneStore.Lib
{
    public static class AutoGen
    {
        public static string CreateID(string headID, string lastID)
        {
            string ID = headID;
            if (lastID == null)
            {
                ID += "10000";
            }
            else
            {
                int k = Convert.ToInt32(lastID.Substring(3, 5));
                k++;
                ID += k.ToString();
            }
            return ID;
        }
    }
}
