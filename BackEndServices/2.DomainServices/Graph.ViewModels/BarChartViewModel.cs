using Graph.ViewModels.Core;
using System;
using System.Collections.Generic;

namespace Graph.ViewModels
{

    public  class BarChartViewModel : BaseViewModel
    {
        public BarChartViewModel()
        {
            ExpensiveHour = new List<GraphChartViewModel>();
            GraphPoints = new List<ChartPointViewModel>();
        }
        public GraphChartViewModel MinPricePoint { get; set; }
        public GraphChartViewModel MaxPricePoint { get; set; }
        public double AveragePrice { get; set; }

        public List<GraphChartViewModel> ExpensiveHour { get; set; }

        public List<ChartPointViewModel> GraphPoints { get; set; }

    }
}
