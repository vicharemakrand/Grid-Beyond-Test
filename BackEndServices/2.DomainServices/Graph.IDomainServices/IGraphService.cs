using Graph.EntityModels;
using Graph.IDomainServices.Core;
using Graph.ServiceResponse;
using Graph.ViewModels;
using System.Data;

namespace Graph.IDomainServices
{
    public interface IGraphService : IBaseService<GraphChart, GraphChartViewModel>
    {
        ResponseResults<GraphChartViewModel> SaveAll(DataTable dataTable, bool overWrite);
        ResponseResult<BarChartViewModel> GetGraphData();
    }

}
