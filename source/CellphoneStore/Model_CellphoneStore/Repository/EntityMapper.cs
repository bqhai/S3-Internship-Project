using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_CellPhoneStore.Model;
using AutoMapper;
namespace Model_CellphoneStore.Repository
{
    public class EntityMapper<TSource, TDestination> where TSource:class where TDestination:class
    {
        public EntityMapper()
        {
            Mapper.CreateMap<ProductMapped, Product>();
            Mapper.CreateMap<Product, ProductMapped>();

            Mapper.CreateMap<BrandMapped, Brand>();
            Mapper.CreateMap<Brand, BrandMapped>();
        }
        public TDestination Translate(TSource obj)
        {
            return Mapper.Map<TDestination>(obj);
        }
    }
}
