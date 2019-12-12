using Graph.EntityModels.Core;
using Graph.IRepositories.Core;
using Lamar;
using Net.Core.Tests.Common;

namespace Graph.DomainService.Tests
{
    public class BaseServiceTest<T> where T: BaseEntity
    {
        protected IUnitOfWork unitOfWork;
        protected static IContainer container;

        //[AssemblyInitialize]
        //public static void AssemblyInit(TestContext context)
        //{
        //    BaseAssemblyInit();
        //}

        public static void BaseAssemblyInit()
        {
            if (container == null)
            {
                container = TestBootstrapper.MockServiceContainer();
            }
        }

        public void BaseInitialize()
        {
            BaseAssemblyInit();
            Helper.LoadMockData<T>();
        }

        public void BaseCleanup()
        {
           // Helper.RemoveMockData<T>();
        }
    }
}
