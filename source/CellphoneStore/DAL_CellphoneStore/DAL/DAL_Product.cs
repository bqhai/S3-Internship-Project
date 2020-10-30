using DAL_CellPhoneStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_CellPhoneStore.DAL
{
    public class DAL_Product
    {
        DbCellPhoneStoreEntities db = new DbCellPhoneStoreEntities();
        public DAL_Product()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        public IEnumerable<Product> GetAllProduct()
        {
            return db.Products.Select(prd => prd);
        }
    }
}
