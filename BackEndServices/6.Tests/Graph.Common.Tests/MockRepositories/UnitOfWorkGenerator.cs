using Graph.IRepositories.Core;
using Moq;
using Net.Core.Tests.Common.MockRepositories;

namespace Graph.Common.Tests
{
    public class UnitOfWorkGenerator
    {
        //public static DataContext InMemoryDb { get; set; }

        public static IUnitOfWork MockUnitOfWork()
        {
            var dataContext = GetDataContext();
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
            mockUnitOfWork.SetupProperty(a => a.DataContext, dataContext);
            mockUnitOfWork.SetupProperty(a => a.GraphRepository, GraphRepositoryGenerator.GetMockRepository(dataContext).Object);
            mockUnitOfWork.Setup(a=>a.Commit()).Returns(() =>
            {
               return dataContext.Commit();
            });
            return mockUnitOfWork.Object;
        }

        private static IDataContext GetDataContext()
        {
            //var options = new DbContextOptionsBuilder<DataContext>()
            //    .UseInMemoryDatabase(databaseName: "TestDb")
            //    .EnableSensitiveDataLogging(true)
            //    .EnableDetailedErrors()
            //    .Options;
            //var dataContext = new DataContext(options);
            //InMemoryDb = dataContext;
            var dataContext = new Mock<IDataContext>();
            return dataContext.Object;
        }
    }
}
