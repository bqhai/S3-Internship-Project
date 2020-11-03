using DAL_CellPhoneStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_CellPhoneStore.DAL
{
    public class DAL_ProductVesion
    {
        DbCellPhoneStoreEntities db = new DbCellPhoneStoreEntities();
        public DAL_ProductVesion()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        public IEnumerable<ProductVersion> GetAllProductVersion()
        {
            return db.ProductVersions.Select(prdv => prdv);
        }

    }
}
