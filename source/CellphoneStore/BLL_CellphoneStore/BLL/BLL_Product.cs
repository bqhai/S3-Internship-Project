using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_CellPhoneStore.DAL;
using DAL_CellPhoneStore.Model;
namespace BLL_CellPhoneStore.BLL
{
    public class BLL_Product
    {
        DAL_Product dalProduct = new DAL_Product();
        public BLL_Product()
        {

        }
        public IEnumerable<Product> GetAllProduct()
        {
            return dalProduct.GetAllProduct();
        }
    }
}
