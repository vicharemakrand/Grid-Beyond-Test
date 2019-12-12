using AutoMapper;
using Graph.IDomainServices.AutoMapper;
 
namespace Graph.Common.Tests
{
    public class AutoMapperInit
    {
        //public static void BuildMap()
        //{
        //    Mapper.(o => o.AddProfile(new ModelAutoMapperProfiler()));
        //}

        public static MapperConfiguration InitializeAutoMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ModelAutoMapperProfiler());
            });

            return config;
        }
    }
}
