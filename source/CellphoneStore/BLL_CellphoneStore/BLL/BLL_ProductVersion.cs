using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_CellPhoneStore.DAL;
using DAL_CellPhoneStore.Model;
using Model_CellphoneStore;
using Model_CellphoneStore.Repository;

namespace BLL_CellPhoneStore.BLL
{
    public class BLL_ProductVersion
    {
        DAL_ProductVesion dalProduct = new DAL_ProductVesion();
        EntityMapper<ProductVersion, ProductVersionMapped> mapObj = new EntityMapper<ProductVersion, ProductVersionMapped>();
        public BLL_ProductVersion()
        {

        }
        public List<ProductVersionMapped> GetAllProductVersion()
        {         
            IEnumerable<ProductVersion> productVersions = dalProduct.GetAllProductVersion();
            List<ProductVersionMapped> productVersionMappeds = new List<ProductVersionMapped>();
            foreach (var item in productVersions)
            {
                productVersionMappeds.Add(mapObj.Translate(item));
            }
            return productVersionMappeds;
        }
        public ProductVersionMapped GetProductVersionByID(string productVersionID)
        {
            ProductVersion productVersion = dalProduct.GetProductVersionByID(productVersionID);
                ProductVersionMapped productVersionMapped = mapObj.Translate(productVersion);
                return productVersionMapped;
        }
        public List<ProductVersionMapped> GetListProductVersionByProductID(string productID)
        {
            IEnumerable<ProductVersion> productVersions = dalProduct.GetListProductVersionByProductID(productID);
            List<ProductVersionMapped> productVersionMappeds = new List<ProductVersionMapped>();
            foreach (var item in productVersions)
            {
                productVersionMappeds.Add(mapObj.Translate(item));
            }
            return productVersionMappeds;
        }
    }
}
