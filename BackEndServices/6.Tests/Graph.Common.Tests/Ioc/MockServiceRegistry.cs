using AutoMapper;
using Graph.Common.Tests;
using Lamar;

namespace Net.Core.Tests.Common
{
    public class MockServiceRegistry : ServiceRegistry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyConfigurationRegistry"/> class.
        /// </summary>
        public MockServiceRegistry()
        {
            var mapperConfig = AutoMapperInit.InitializeAutoMapper();
            var mapper = mapperConfig.CreateMapper();

            For<MapperConfiguration>().Use(mapperConfig);
            For<IMapper>().Use(mapper);

        }
    }
}
