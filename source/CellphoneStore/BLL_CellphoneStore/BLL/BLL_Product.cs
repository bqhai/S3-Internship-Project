using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_CellPhoneStore.DAL;
using DAL_CellPhoneStore.MappingClass;
using Model_CellphoneStore;
using Model_CellphoneStore.Repository;

namespace BLL_CellPhoneStore.BLL
{
    public class BLL_Product
    {
        DAL_Product dalProduct = new DAL_Product();
        EntityMapper<ProductInfo, ProductInfoMapped> convertToPrdInfo = new EntityMapper<ProductInfo, ProductInfoMapped>();
        public BLL_Product()
        {

        }
        public List<ProductInfoMapped> GetAllProduct()
        {
            IEnumerable<ProductInfo> productInfos = dalProduct.GetAllProduct();
            List<ProductInfoMapped> productInfoMappeds = new List<ProductInfoMapped>();
            foreach (var item in productInfos)
            {
                productInfoMappeds.Add(convertToPrdInfo.Translate(item));
            }
            return productInfoMappeds;
        }
    }
}
