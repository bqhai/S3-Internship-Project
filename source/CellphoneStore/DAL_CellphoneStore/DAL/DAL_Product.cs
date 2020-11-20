using DAL_CellPhoneStore.MappingClass;
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
        public IEnumerable<ProductInfo> GetAllProduct()
        {
            var query = from prd in db.Products
                        join brd in db.Brands on prd.BrandID equals brd.BrandID
                        select new ProductInfo
                        {
                            ProductID = prd.ProductID,
                            ProductName = prd.ProductName,
                            BrandID = prd.BrandID,
                            BrandName = brd.BrandName,
                            Country = brd.Country,
                            Size = prd.Size,
                            Weight = prd.Weight,
                            SIM = prd.SIM,
                            ScreenType = prd.ScreenType,
                            ScreenSize = prd.ScreenSize,
                            ScreenResolution = prd.ScreenResolution,
                            OperatingSystem = prd.OperatingSystem,
                            Chipset = prd.Chipset,
                            CPU = prd.CPU,
                            GPU = prd.GPU,
                            SDCard = prd.SDCard,
                            BackCamera = prd.BackCamera,
                            FrontCamera = prd.FrontCamera,
                            WLAN = prd.WLAN,
                            Bluetooth = prd.Bluetooth,
                            GPS = prd.GPS,
                            NFC = prd.NFC,
                            USB = prd.USB,
                            Sensor = prd.Sensor,
                            Battery = prd.Battery,
                            Image = prd.Image,
                        };
            return query;
        }
    }
}
