using Graph.EntityModels;
 using System.Collections.Generic;

namespace Net.Core.Tests.Common
{
    public static class MockDB
    {
        public static MockDataCollection Collections = new MockDataCollection();

        public static void LoadAllDataFiles()
        {
            Helper.LoadMockData<GraphChart>();
        }
    }

    public class MockDataCollection
    {
        public List<GraphChart> GraphCharts = new List<GraphChart>();
    }
}
