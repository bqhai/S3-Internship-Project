using DAL_CellPhoneStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_CellPhoneStore.DAL
{
    public class DAL_Brand
    {
        DbCellPhoneStoreEntities db = new DbCellPhoneStoreEntities();
        public DAL_Brand()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        public IEnumerable<Brand> GetAllBrand()
        {
            return db.Brands.Select(br => br);
        }
    }
}
