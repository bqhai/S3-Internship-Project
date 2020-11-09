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
    public class BLL_ProductIntroduce
    {
        DAL_ProductIntroduce dalProductIntroduce = new DAL_ProductIntroduce();
        public BLL_ProductIntroduce()
        {

        }
        public ProductIntroduceMapped GetProductIntroduceByID(string productID)
        {
            EntityMapper<ProductIntroduce, ProductIntroduceMapped> mapObj = new EntityMapper<ProductIntroduce, ProductIntroduceMapped>();
            ProductIntroduce productIntroduce = dalProductIntroduce.GetProductIntroduceByID(productID);
            ProductIntroduceMapped productIntroduceMapped = mapObj.Translate(productIntroduce);
            return productIntroduceMapped;
        }
    }
}
