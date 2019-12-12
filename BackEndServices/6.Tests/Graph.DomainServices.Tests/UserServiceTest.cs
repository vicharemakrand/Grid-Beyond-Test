using Graph.DomainService.Tests;
using Graph.EntityModels;
using Graph.IDomainServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;

namespace SampleCode.DomainService.Test
{
    [TestClass]
    public class UserServiceTest : BaseServiceTest<GraphChart>
    {
        private IGraphService domainService;


        /// <summary>
        ///Initialize() is called once during test execution before
        ///test methods in this test class are executed.
        ///</summary>
        [TestInitialize]
        public void Initialize()
        {
            base.BaseInitialize();
            domainService = container.GetInstance<IGraphService>();
        }

        /// <summary>
        ///Cleanup() is called once during test execution after
        ///test methods in this class have executed unless
        ///this test class' Initialize() method throws an exception.
        ///</summary>
        [TestCleanup()]
        public void Cleanup()
        {

            //  TODO: Add test cleanup code
        }

        [TestMethod]
        public void Save_AddNewUser_returnsSaveUser()
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("Date");
            dataTable.Columns.Add("MarketPrice XT");

            dataTable.Rows.Add(DateTime.Now.AddDays(1), 99);
            var response = domainService.SaveAll(dataTable, true);
            Assert.AreEqual(1, response.ViewModels.Count);
        }

    }
}
