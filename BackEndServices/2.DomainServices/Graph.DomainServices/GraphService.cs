using Graph.DomainServices.Core;
using Graph.EntityModels;
using Graph.IDomainServices;
using Graph.IDomainServices.AutoMapper;
using Graph.ServiceResponse;
using Graph.Utility;
using Graph.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Graph.DomainServices
{
    public partial class GraphService : BaseService<GraphChart, GraphChartViewModel>, IGraphService
    {

        public virtual ResponseResults<GraphChartViewModel> SaveAll(DataTable dataTable,bool overWrite)
        {
            var response = new ResponseResults<GraphChartViewModel>() { IsSucceed = true, Message = AppMessages.ACTION_SAVE_SUCCEEDED };
            var viewModels = Mapper.ToViewModel<GraphChartViewModel>(dataTable.Rows.Cast<DataRow>().ToList());

            if (overWrite)
            {
                UnitOfWork.SetDbContext(BaseRepository).DeleteAll();
            }
            foreach (var viewModel in viewModels)
            {
                var model = Mapper.ToEntityModel<GraphChart, GraphChartViewModel>(viewModel);
                UnitOfWork.SetDbContext(BaseRepository).Add(model);
            }

            UnitOfWork.Commit();
            response.ViewModels = viewModels;
            return response;
        }


        public ResponseResult<BarChartViewModel> GetGraphData()
        {
            var result = new ResponseResult<BarChartViewModel> { IsSucceed = true };
            var completeList = base.GetAll().ViewModels.OrderBy(o => o.ChartDate).ToList() ;

            var averagePrice = completeList.Sum(o=>o.MarketPrice)/completeList.Count;

            var prevItem = completeList.FirstOrDefault();
            var minItem = prevItem;
            var maxItem = prevItem;
            var expensiveYearItemlow = prevItem;
            var expensiveYearItemhigh = prevItem;
            var maxDiff = 0d;
            foreach (var graphItem in completeList.Skip(1))
            {
                if(minItem.MarketPrice > graphItem.MarketPrice)
                {
                    minItem = graphItem;
                }

                if (maxItem.MarketPrice < graphItem.MarketPrice)
                {
                    maxItem = graphItem;
                }

                if (maxDiff > (graphItem.MarketPrice - prevItem.MarketPrice))
                {
                    maxDiff = graphItem.MarketPrice - prevItem.MarketPrice;
                    expensiveYearItemlow = prevItem;
                    expensiveYearItemhigh = graphItem;
                }

                prevItem = graphItem;
            }

            result.ViewModel = new BarChartViewModel { 
                                                    GraphPoints = completeList.Select(o=> new ChartPointViewModel { Name= o.ChartDate,Value=o.MarketPrice }).ToList(),
                                                    AveragePrice = averagePrice, 
                                                    MaxPricePoint = maxItem,
                                                    MinPricePoint = minItem,
                                                    ExpensiveHour = new List<GraphChartViewModel> { expensiveYearItemlow, expensiveYearItemhigh } };

            return result;
        }
    }
}
