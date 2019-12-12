using Graph.EntityModels.Core;
using System;

namespace Graph.EntityModels
{
    public class GraphChart : BaseEntity
    {

         public DateTime ChartDate { get; set; }
        public double MarketPrice { get; set; }
 
     }
}
