using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mapping;
using NorthwindData;
using TabTest.Helpers;

namespace TabTest.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            AutoMapper.Mapper.CreateMap<Product, TabTest.Models.ProductModel>()
                .IgnoreAllNonExisting();
        }
    }
}