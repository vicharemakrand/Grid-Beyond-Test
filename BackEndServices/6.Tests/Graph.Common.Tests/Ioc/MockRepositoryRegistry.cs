using Graph.Common.Tests;
using Graph.IRepositories.Core;
using Lamar;

namespace Net.Core.Tests.Common
{
    public class MockRepositoryRegistry : ServiceRegistry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyConfigurationRegistry"/> class.
        /// </summary>
        public MockRepositoryRegistry()
        {
            For<IUnitOfWork>().Use(UnitOfWorkGenerator.MockUnitOfWork());
        }
    }
}
