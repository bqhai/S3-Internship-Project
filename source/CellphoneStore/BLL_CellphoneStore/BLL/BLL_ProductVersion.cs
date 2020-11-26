using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_CellPhoneStore.DAL;
using DAL_CellPhoneStore.Model;
using DAL_CellPhoneStore.MappingClass;
using Model_CellphoneStore;
using Model_CellphoneStore.Repository;
using System.Collections;

namespace BLL_CellPhoneStore.BLL
{
    public class BLL_ProductVersion
    {
        DAL_ProductVesion dalProductVersion = new DAL_ProductVesion();
        EntityMapper<ProductVersion, ProductVersionMapped> convertToPrdvMapped = new EntityMapper<ProductVersion, ProductVersionMapped>();
        EntityMapper<ProductVersionMapped, ProductVersion> convertToPrdv = new EntityMapper<ProductVersionMapped, ProductVersion>();
        EntityMapper<ProductVersionInfo, ProductVersionInfoMapped> convertToPrdvMappedInfo = new EntityMapper<ProductVersionInfo, ProductVersionInfoMapped>();
        public BLL_ProductVersion()
        {

        }
        public List<ProductVersionMapped> GetAllProductVersion()
        {
            
            IEnumerable<ProductVersion> productVersions = dalProductVersion.GetAllProductVersion();
            List<ProductVersionMapped> productVersionMappeds = new List<ProductVersionMapped>();
            foreach (var item in productVersions)
            {
                productVersionMappeds.Add(convertToPrdvMapped.Translate(item));
            }
            return productVersionMappeds;
        }
        public ProductVersionInfoMapped GetInfoProductVersionByID(string productVersionID)
        {          
            ProductVersionInfo productVersionInfo = dalProductVersion.GetInfoProductVersionByID(productVersionID);
            ProductVersionInfoMapped productVersionInfoMapped = convertToPrdvMappedInfo.Translate(productVersionInfo);
            return productVersionInfoMapped;
        }
        public ProductVersionMapped GetProductVersionByID(string productVersionID)
        {
            ProductVersion productVersion = dalProductVersion.GetProductVersionByID(productVersionID);
            ProductVersionMapped productVersionMapped = convertToPrdvMapped.Translate(productVersion);
            return productVersionMapped;
        }
        public List<ProductVersionMapped> GetListProductVersionByProductID(string productID)
        {         
            IEnumerable<ProductVersion> productVersions = dalProductVersion.GetListProductVersionByProductID(productID);
            List<ProductVersionMapped> productVersionMappeds = new List<ProductVersionMapped>();
            foreach (var item in productVersions)
            {
                productVersionMappeds.Add(convertToPrdvMapped.Translate(item));
            }
            return productVersionMappeds;
        }
        public List<ProductVersionMapped> GetListHotSale()
        {     
            IEnumerable<ProductVersion> productVersions = dalProductVersion.GetListHotSale();
            List<ProductVersionMapped> productVersionMappeds = new List<ProductVersionMapped>();
            foreach (var item in productVersions)
            {
                productVersionMappeds.Add(convertToPrdvMapped.Translate(item));
            }
            return productVersionMappeds;
        }
        public List<ProductVersionMapped> FilterProductVersionByRAM(string ram)
        {           
            IEnumerable<ProductVersion> productVersions = dalProductVersion.FilterProductVersionByRAM(ram);
            List<ProductVersionMapped> productVersionMappeds = new List<ProductVersionMapped>();
            foreach (var item in productVersions)
            {
                productVersionMappeds.Add(convertToPrdvMapped.Translate(item));
            }
            return productVersionMappeds;
        }
        public List<ProductVersionMapped> FilterProductVersionByROM(string rom)
        {    
            IEnumerable<ProductVersion> productVersions = dalProductVersion.FilterProductVersionByROM(rom);
            List<ProductVersionMapped> productVersionMappeds = new List<ProductVersionMapped>();
            foreach (var item in productVersions)
            {
                productVersionMappeds.Add(convertToPrdvMapped.Translate(item));
            }
            return productVersionMappeds;
        }
        public List<ProductVersionMapped> FilterProductVersionByOS(string os)
        {         
            IEnumerable<ProductVersion> productVersions = dalProductVersion.FilterProductVersionByOS(os);
            List<ProductVersionMapped> productVersionMappeds = new List<ProductVersionMapped>();
            foreach (var item in productVersions)
            {
                productVersionMappeds.Add(convertToPrdvMapped.Translate(item));
            }
            return productVersionMappeds;
        }
        public List<ProductVersionMapped> FilterProductVersionByScreenSize(double minScreenSize, double maxScreenSize)
        {        
            IEnumerable<ProductVersion> productVersions = dalProductVersion.FilterProductVersionByScreenSize(minScreenSize, maxScreenSize);
            List<ProductVersionMapped> productVersionMappeds = new List<ProductVersionMapped>();
            foreach (var item in productVersions)
            {
                productVersionMappeds.Add(convertToPrdvMapped.Translate(item));
            }
            return productVersionMappeds;
        }
        public List<ProductVersionMapped> FilterProductVersionByBrand(string brandID)
        {
            IEnumerable<ProductVersion> productVersions = dalProductVersion.FilterProductVersionByBrand(brandID);
            List<ProductVersionMapped> productVersionMappeds = new List<ProductVersionMapped>();
            foreach (var item in productVersions)
            {
                productVersionMappeds.Add(convertToPrdvMapped.Translate(item));
            }
            return productVersionMappeds;
        }
        public List<ProductVersionMapped> FilterProductVersionByPrice(int minPrice, int maxPrice)
        {
            IEnumerable<ProductVersion> productVersions = dalProductVersion.FilterProductVersionByPrice(minPrice, maxPrice);
            List<ProductVersionMapped> productVersionMappeds = new List<ProductVersionMapped>();
            foreach (var item in productVersions)
            {
                productVersionMappeds.Add(convertToPrdvMapped.Translate(item));
            }
            return productVersionMappeds;
        }
        public List<ProductVersionMapped> SearchProductVersion(string keyWord)
        {
            IEnumerable<ProductVersion> productVersions = dalProductVersion.SearchProductVersion(keyWord);
            List<ProductVersionMapped> productVersionMappeds = new List<ProductVersionMapped>();
            foreach (var item in productVersions)
            {
                productVersionMappeds.Add(convertToPrdvMapped.Translate(item));
            }
            return productVersionMappeds;
        }
        public bool UpdateProductVersion(ProductVersionMapped productVersionMapped)
        {
            try
            {
                ProductVersion prdv = convertToPrdv.Translate(productVersionMapped);
                dalProductVersion.UpdateProductVersion(prdv);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
