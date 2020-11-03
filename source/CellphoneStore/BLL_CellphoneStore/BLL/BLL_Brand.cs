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
    public class BLL_Brand
    {
        DAL_Brand dalBrand = new DAL_Brand();
        public BLL_Brand()
        {

        }
        public List<BrandMapped> GetAllBrand()
        {
            EntityMapper<Brand, BrandMapped> mapObj = new EntityMapper<Brand, BrandMapped>();
            IEnumerable<Brand> brands = dalBrand.GetAllBrand();
            List<BrandMapped> brandMappeds = new List<BrandMapped>();
            foreach (var item in brands)
            {
                brandMappeds.Add(mapObj.Translate(item));
            }
            return brandMappeds;
        }
    }
}
