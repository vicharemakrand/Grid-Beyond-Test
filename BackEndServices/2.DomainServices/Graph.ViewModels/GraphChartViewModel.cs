using Graph.ViewModels.Core;
using System;

namespace Graph.ViewModels
{

    public  class GraphChartViewModel : BaseViewModel
    {
        public DateTime ChartDate { get; set; }
        public double MarketPrice { get; set; }

    }
}
