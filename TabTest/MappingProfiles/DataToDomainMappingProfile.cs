using AutoMapper;
using Mapping;
using NorthwindData;

namespace TabTest.MappingProfiles
{
    [MapperProfile]
    public class DataToDomainMappingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Product, TabTest.Models.ProductModel>()
                .IgnoreAllNonExisting();
        }
    }
}
