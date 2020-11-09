﻿using DAL_CellPhoneStore.MappingClass;
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
        public ProductVersionInfo GetInfoProductVersionByID(string productVersionID)
        {
            var query = (from prd in db.Products
                         join prdv in db.ProductVersions on prd.ProductID equals prdv.ProductID
                         join br in db.Brands on prd.BrandID equals br.BrandID
                         where prdv.ProductVersionID == productVersionID
                         select new ProductVersionInfo
                         {
                             ProductID = prd.ProductID,
                             ProductVersionID = prdv.ProductVersionID,
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
        public ProductVersion GetProductVersionByID(string productVersionID)
        {
            return db.ProductVersions.Where(prdv => prdv.ProductVersionID == productVersionID).FirstOrDefault();
        }
        public IEnumerable<ProductVersion> GetListHotSale()
        {
            List<HotSale> hotSales = db.HotSales.Select(hs => hs).ToList();
            List<ProductVersion> productVersions = new List<ProductVersion>();
            foreach (var item in hotSales)
            {
                ProductVersion productVersion = db.ProductVersions.Where(prdv => prdv.ProductVersionID == item.ProductVersionID).FirstOrDefault();
                productVersions.Add(productVersion);
            }
            return productVersions;
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
        public IEnumerable<ProductVersion> FilterProductVersionByScreenSize(double minScreenSize, double maxScreenSize)
        {
            var query = from prd in db.Products
                        join prdv in db.ProductVersions on prd.ProductID equals prdv.ProductID
                        where prd.ScreenSize > minScreenSize && prd.ScreenSize <= maxScreenSize  
                        select prdv;
            return query;
        }
        public IEnumerable<ProductVersion> FilterProductVersionByBrand(string brandID)
        {
            var query = from prd in db.Products
                        join prdv in db.ProductVersions on prd.ProductID equals prdv.ProductID
                        where prd.BrandID == brandID
                        select prdv;
            return query;
        }
        public IEnumerable<ProductVersion> FilterProductVersionByPrice(int minPrice, int maxPrice)
        {
            var query = from prd in db.Products
                        join prdv in db.ProductVersions on prd.ProductID equals prdv.ProductID
                        where prdv.Price > minPrice && prdv.Price <= maxPrice
                        select prdv;
            return query;
        }
        public IEnumerable<ProductVersion> SearchProductVersion(string keyWord)
        {
            return db.ProductVersions.Where(prdv => prdv.ProductVersionName.Contains(keyWord));
        }
    }
}
