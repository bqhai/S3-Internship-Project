using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_CellPhoneStore.DAL;
using DAL_CellPhoneStore.Model;
using Model_CellphoneStore;
using Model_CellphoneStore.Repository;

namespace BLL_CellPhoneStore.BLL
{
    public class BLL_Product
    {
        DAL_Product dalProduct = new DAL_Product();
        public BLL_Product()
        {

        }
        public List<ProductMapped> GetAllProduct()
        {
            EntityMapper<Product, ProductMapped> mapObj = new EntityMapper<Product, ProductMapped>();
            IEnumerable<Product> products = dalProduct.GetAllProduct();
            List<ProductMapped> productMappeds = new List<ProductMapped>();
            foreach (var item in products)
            {
                productMappeds.Add(mapObj.Translate(item));
            }
            return productMappeds;
        }
    }
}
