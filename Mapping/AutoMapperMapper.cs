using System;
using System.Linq;
using AutoMapper;

namespace Mapping
{
    public class AutoMapperMapper : IMapper
    {
        static AutoMapperMapper()
        {
            Mapper.Initialize(
                x =>
                    {
                        var profileTypes = from a in AppDomain.CurrentDomain.GetAssemblies()
                                           from t in a.GetTypes()
                                           where
                                               t.GetCustomAttributes(true).FirstOrDefault(
                                                   ca => ca is MapperProfileAttribute) != null
                                               && !t.IsAbstract && typeof (Profile).IsAssignableFrom(t)
                                           select t;

                        foreach (var profileType in profileTypes)
                        {
                            var profile = (Profile) Activator.CreateInstance(profileType);
                            x.AddProfile(profile);
                        }
                    });
        }

        public TDestination Map<TDestination>(object source)
        {
            return Mapper.Map<TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return Mapper.Map(source, destination);
        }
    }
}
