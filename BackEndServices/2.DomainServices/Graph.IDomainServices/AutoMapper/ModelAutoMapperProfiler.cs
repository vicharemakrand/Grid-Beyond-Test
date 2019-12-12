using AutoMapper;
using Graph.EntityModels;
using Graph.EntityModels.Core;
using Graph.ViewModels;
using Graph.ViewModels.Core;
using System.Data;

namespace Graph.IDomainServices.AutoMapper
{
    public partial class ModelAutoMapperProfiler : Profile
    {
        public ModelAutoMapperProfiler()
        {
            CreateMap<BaseEntity, BaseViewModel>().ReverseMap();
            CreateMap<GraphChart, GraphChartViewModel>().ReverseMap();
 
             CreateMap<DataRow, GraphChartViewModel>()
            .ForMember(d => d.ChartDate, o => o.ConvertUsing(new DateConvertor(),s => s.Field<string>("Date")))
            .ForMember(d => d.MarketPrice, o => o.ConvertUsing(new MarketPriceConvertor(), s => s.Field<string>("Market Price EX1")));
        }
    }
}
