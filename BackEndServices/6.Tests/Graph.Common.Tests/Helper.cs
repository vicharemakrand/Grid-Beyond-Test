using Graph.EntityModels;
using Graph.EntityModels.Core;
using Graph.Repositories.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Net.Core.Tests.Common
{
    public static class Helper
    {
        public static List<T> LoadInMemoryData<T>() where T: class
        {
            var jsonData = GetJsonData(typeof(T).Name + ".js");
            var mockDataList = new List<T>();
            if(!String.IsNullOrEmpty(jsonData))
            {
                 mockDataList = JsonConvert.DeserializeObject<List<T>>(jsonData);
            }

            return mockDataList;
        }

        public static void LoadMockData<T>() where T : class
        {
            var jsonData = GetJsonData(typeof(T).Name + ".js");
            var mockDataList = new List<T>();
            if (!String.IsNullOrEmpty(jsonData))
            {
                 mockDataList = JsonConvert.DeserializeObject<List<T>>(jsonData);
            }

            if (typeof(T).Name == typeof(GraphChart).Name)
            {
                MockDB.Collections.GraphCharts = mockDataList.Cast<GraphChart>().ToList();
            }
        }

        private static string GetJsonData(string dataFileName)
        {
            var defaultDataFolderPath = Path.Combine(@"D:\codepractice-prj\Ardanis.Makarand.Vichare.CodeChallange\BackEndServices\6.Tests\Graph.Common.Tests\", "Data");

            var dataFilePath = Path.Combine(defaultDataFolderPath, dataFileName);

            return File.ReadAllText(dataFilePath);
        }

        public static T DeepClone<T>(this T source)
        {
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }
            // return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source));
            return source;
        }

        public static void AddInMemoryData<T>(DataContext dataContext) where T: BaseEntity
        {
            var ts = dataContext.DbSet<T>().ToList();
            var models = Helper.LoadInMemoryData<T>();
            foreach(var model in models)
            {
                dataContext.DbSet<T>().Add(model);
            }
            dataContext.Commit();
        }

        public static void RemoveInMemoryData<T>(DataContext dataContext) where T : BaseEntity
        {
            var models = dataContext.DbSet<T>().ToList();
            foreach (var model in models)
            {
                dataContext.DbSet<T>().Remove(model);
            }
            dataContext.Commit();
        }

        public static void ResetInMemoryData<T>(DataContext dataContext) where T : BaseEntity
        {
            RemoveInMemoryData<T>(dataContext);
            AddInMemoryData<T>(dataContext);
        }

    }
}
