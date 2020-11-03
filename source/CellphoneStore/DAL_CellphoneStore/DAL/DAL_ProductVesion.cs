using DAL_CellPhoneStore.MappingClass;
using DAL_CellPhoneStore.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public ProductVersion GetProductVersionByID(string productVersionID)
        {
            return db.ProductVersions.Where(prdv => prdv.ProductVersionID == productVersionID).FirstOrDefault();
        }
        public IEnumerable<ProductVersion> GetListProductVersionByProductID(string productID)
        {
            return db.ProductVersions.Where(prdv => prdv.ProductID == productID);
        }
        public IEnumerable<Product_ProductVersion_Brand> GetTechnicalInfo(string productVersionID)
        {
            var query = from prd in db.Products
                        join prdv in db.ProductVersions on prd.ProductID equals prdv.ProductID
                        join br in db.Brands on prd.BrandID equals br.BrandID
                        where prdv.ProductVersionID == productVersionID
                        select new Product_ProductVersion_Brand
                        {
                            BrandName = br.BrandName,
                            Size = prd.Size,
                            Weight = prd.Weight,
                            RAM = prdv.RAM,
                            ROM = prdv.ROM,
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
                            Battery = prd.Battery
                        };
            return query;
        }
    }
}
