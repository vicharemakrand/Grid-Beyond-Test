using Lamar;
 
namespace Net.Core.Tests.Common
{
    public static class TestBootstrapper
    {
        public static IContainer MockRepositoryContainer()
        {
 
            var container = new Container(x =>
            {
                x.IncludeRegistry<MockRepositoryRegistry>();
            });
             container.AssertConfigurationIsValid();
            return container;
        }

        public static IContainer MockServiceContainer()
        {
            var container = new Container(x =>
            {
                x.IncludeRegistry<MockServiceRegistry>();
            });
             container.AssertConfigurationIsValid();
            return container;
        }
    }
}
