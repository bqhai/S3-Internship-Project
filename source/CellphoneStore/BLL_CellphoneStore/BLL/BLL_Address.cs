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
    public class BLL_Address
    {
        DAL_Address dalAddress = new DAL_Address();
        EntityMapper<Province, ProvinceMapped> provToProvMapped = new EntityMapper<Province, ProvinceMapped>();
        EntityMapper<District, DistrictMapped> distToDistMapped = new EntityMapper<District, DistrictMapped>();
        EntityMapper<Ward, WardMapped> wardToWardMapped = new EntityMapper<Ward, WardMapped>();
        public BLL_Address()
        {

        }
        public List<ProvinceMapped> GetProvinces()
        {
            List<Province> provinces = dalAddress.GetProvinces().ToList();
            if(provinces != null)
            {
                List<ProvinceMapped> provinceMappeds = new List<ProvinceMapped>();
                foreach (var item in provinces)
                {
                    provinceMappeds.Add(provToProvMapped.Translate(item));
                }
                return provinceMappeds;
            }
            return null;
        }
        public List<DistrictMapped> GetDistrictsByProvinceID(int provinceID)
        {
            List<District> districts = dalAddress.GetDistrictsByProvinceID(provinceID).ToList();
            if(districts != null)
            {
                List<DistrictMapped> districtMappeds = new List<DistrictMapped>();
                foreach (var item in districts)
                {
                    districtMappeds.Add(distToDistMapped.Translate(item));
                }
                return districtMappeds;
            }
            return null;
        }
        public List<WardMapped> GetWardsByDistrictID(int districtID)
        {
            List<Ward> wards = dalAddress.GetWardsByDistrictID(districtID).ToList();
            if(wards != null)
            {
                List<WardMapped> wardMappeds = new List<WardMapped>();
                foreach (var item in wards)
                {
                    wardMappeds.Add(wardToWardMapped.Translate(item));
                }
                return wardMappeds;
            }
            return null;
        }
    }
}
