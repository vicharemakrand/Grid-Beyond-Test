using Moq;
using Graph.Repositories.Core;
using System.Linq;
using Graph.IRepositories.Identity;
using Graph.EntityModels;
using Graph.IRepositories.Core;

namespace Net.Core.Tests.Common.MockRepositories
{
    public class GraphRepositoryGenerator
    {
        public static Mock<IGraphRepository> GetMockRepository(IDataContext dataContext)
        {
            var mockRepository = new Mock<IGraphRepository>();

            var DataContext = ((DataContext)dataContext);
            mockRepository.SetupProperty(a => a.DbContext, DataContext);

            mockRepository.Setup(a => a.Add(It.IsAny<GraphChart>())).Callback<GraphChart>(userEntity =>
            {
                DataContext.GraphCharts.Add(userEntity);
            });
      
            mockRepository.Setup(a => a.DeleteAll()).Callback(() =>
            {
                var models = DataContext.GraphCharts.ToList();
                DataContext.GraphCharts.RemoveRange(models);
            });

            mockRepository.Setup(a => a.GetAll()).Returns(()=>
            {
                return DataContext.GraphCharts.ToList();
            });


            return mockRepository;
        }
    }
}
