using DAL_CellPhoneStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_CellPhoneStore.DAL
{
    public class DAL_Address
    {
        DbCellPhoneStoreEntities db = new DbCellPhoneStoreEntities();
        public DAL_Address()
        {

        }
        public IEnumerable<Province> GetProvinces()
        {
            //var query = from prov in db.Provinces
            //            select new Province
            //            { 
            //                Id = prov.Id,
            //                Name = prov.Name,
            //                Type = prov.Type
            //            };
            return db.Provinces.Select(prov => prov);
        }
        public IEnumerable<District> GetDistrictsByProvinceID(int provinceID)
        {
            return db.Districts.Where(dis => dis.ProvinceId == provinceID);
        }
        public IEnumerable<Ward> GetWardsByDistrictID(int districtID)
        {
            return db.Wards.Where(ward => ward.DistrictID == districtID);
        }
    }
}
