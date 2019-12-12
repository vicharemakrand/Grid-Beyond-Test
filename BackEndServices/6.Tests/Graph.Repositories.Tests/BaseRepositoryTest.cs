using Graph.Common.Tests;
using Graph.EntityModels.Core;
using Graph.IRepositories.Core;
using Lamar;
using Net.Core.Tests.Common;

namespace Graph.Repositories.Tests
{
    public class BaseRepositoryTest<T> where T:BaseEntity
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
                container = TestBootstrapper.MockRepositoryContainer();
            }
        }

        public void BaseInitialize()
        {
            BaseAssemblyInit();
            unitOfWork = container.GetInstance<IUnitOfWork>();
            //Helper.AddInMemoryData<T>(UnitOfWorkGenerator.InMemoryDb);
        }

        public void BaseCleanup()
        {
            //Helper.RemoveInMemoryData<T>(UnitOfWorkGenerator.InMemoryDb);
        }
    }
}
