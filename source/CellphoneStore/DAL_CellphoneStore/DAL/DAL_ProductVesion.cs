using DAL_CellPhoneStore.MappingClass;
using DAL_CellPhoneStore.Model;
using System;
using System.Collections;
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
        public IEnumerable<ProductVersion> GetListProductVersionByProductID(string productID)
        {
            return db.ProductVersions.Where(prdv => prdv.ProductID == productID);
        }
        public ProductVersionInfo GetProductVersionByID(string productVersionID)
        {
            var query = (from prd in db.Products
                         join prdv in db.ProductVersions on prd.ProductID equals prdv.ProductID
                         join br in db.Brands on prd.BrandID equals br.BrandID
                         where prdv.ProductVersionID == productVersionID
                         select new ProductVersionInfo
                         {
                             ProductID = prd.ProductID,
                             ProductVersionName = prdv.ProductVersionName,
                             Image = prdv.Image,
                             Price = prdv.Price,
                             ListPrice = prdv.ListPrice,
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
                         }).FirstOrDefault();
            return query;
        }
        public IEnumerable<ProductVersion> GetListHotSale()
        {
            return db.ProductVersions.Where(prdv => prdv.HotSale == true);
        }
        public IEnumerable<ProductVersion> FilterProductVersionByRAM(string ram)
        {
            return db.ProductVersions.Where(prdv => prdv.RAM == ram);
        }
        public IEnumerable<ProductVersion> FilterProductVersionByROM(string rom)
        {
            return db.ProductVersions.Where(prdv => prdv.ROM == rom);
        }
        public IEnumerable<ProductVersion> FilterProductVersionByOS(string os)
        {
            var query = from prd in db.Products
                        join prdv in db.ProductVersions on prd.ProductID equals prdv.ProductID
                        where prd.OperatingSystem == os
                        select prdv;
            return query;
        }
        public IEnumerable<ProductVersion> FilterProductVersionByScreenSize(double min, double max)
        {
            //if(screenSizeOption == 1)
            //{
            //    var query = from prd in db.Products
            //                join prdv in db.ProductVersions on prd.ProductID equals prdv.ProductID
            //                where prd.ScreenSize < 5
            //                select prdv;
            //    return query;
            //}
            //else if(screenSizeOption == 2)
            //{
            //    var query = from prd in db.Products
            //                join prdv in db.ProductVersions on prd.ProductID equals prdv.ProductID
            //                where prd.ScreenSize > 5 && prd.ScreenSize <= 5.5
            //                select prdv;
            //    return query;
            //}

            var query = from prd in db.Products
                        join prdv in db.ProductVersions on prd.ProductID equals prdv.ProductID
                        where prd.ScreenSize < max && prd.ScreenSize > min
                        select prdv;
            return null;
        }
    }
}
