using AutoMapper;
using Mapping;
using NorthwindData;
using TabTest.Models;
using ProductModel = TabTest.Models.ProductModel;

namespace TabTest.MappingProfiles
{
    [MapperProfile]
    public class DataToDomainMappingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Product, ProductModel>()
                .ForMember( d => d.ProductCategoryName, opt => opt.MapFrom( s => s.ProductCategory.Name));

            Mapper.CreateMap<ProductCategory, ProductCategoryModel>()
                .IgnoreAllNonExisting();
        }
    }
}
