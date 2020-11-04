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
        public BLL_ProductVersion()
        {

        }
        public List<ProductVersionMapped> GetAllProductVersion()
        {
            EntityMapper<ProductVersion, ProductVersionMapped> mapObj = new EntityMapper<ProductVersion, ProductVersionMapped>();
            IEnumerable<ProductVersion> productVersions = dalProductVersion.GetAllProductVersion();
            List<ProductVersionMapped> productVersionMappeds = new List<ProductVersionMapped>();
            foreach (var item in productVersions)
            {
                productVersionMappeds.Add(mapObj.Translate(item));
            }
            return productVersionMappeds;
        }
        public ProductVersionInfoMapped GetProductVersionByID(string productVersionID)
        {
            EntityMapper<ProductVersionInfo, ProductVersionInfoMapped> mapObj = new EntityMapper<ProductVersionInfo, ProductVersionInfoMapped>();
            ProductVersionInfo productVersionInfo = dalProductVersion.GetProductVersionByID(productVersionID);
            ProductVersionInfoMapped productVersionInfoMapped = mapObj.Translate(productVersionInfo);
            return productVersionInfoMapped;
        }
        public List<ProductVersionMapped> GetListProductVersionByProductID(string productID)
        {
            EntityMapper<ProductVersion, ProductVersionMapped> mapObj = new EntityMapper<ProductVersion, ProductVersionMapped>();
            IEnumerable<ProductVersion> productVersions = dalProductVersion.GetListProductVersionByProductID(productID);
            List<ProductVersionMapped> productVersionMappeds = new List<ProductVersionMapped>();
            foreach (var item in productVersions)
            {
                productVersionMappeds.Add(mapObj.Translate(item));
            }
            return productVersionMappeds;
        }
        public List<ProductVersionMapped> GetListHotSale()
        {
            EntityMapper<ProductVersion, ProductVersionMapped> mapObj = new EntityMapper<ProductVersion, ProductVersionMapped>();
            IEnumerable<ProductVersion> productVersions = dalProductVersion.GetListHotSale();
            List<ProductVersionMapped> productVersionMappeds = new List<ProductVersionMapped>();
            foreach (var item in productVersions)
            {
                productVersionMappeds.Add(mapObj.Translate(item));
            }
            return productVersionMappeds;
        }

    }
}
