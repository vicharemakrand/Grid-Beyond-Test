using Graph.EntityModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Graph.Repositories.Tests
{
    [TestClass]
    public class UserRepositoryTest : BaseRepositoryTest<GraphChart>
    {

        [TestInitialize]
        public void Initialize()
        {
            base.BaseInitialize();
        }

        [TestCleanup]
        public void Cleanup()
        {
            base.BaseCleanup();
        }

        [TestMethod]
        public void Add_AddNewUser_returnsSaveUser()
        {
            unitOfWork.GraphRepository.Add(new GraphChart() { Id = 11, ChartDate = DateTime.Now.AddDays(1), MarketPrice = 99 });
            unitOfWork.Commit();
            var model = unitOfWork.GraphRepository.GetAll().FirstOrDefault();
            Assert.AreEqual(11, model.Id);
        }

        [TestMethod]
        public void DeleteAll_User_returnsnull()
        {
            unitOfWork.GraphRepository.DeleteAll();
            unitOfWork.Commit();
            var model = unitOfWork.GraphRepository.GetAll();
            Assert.IsNull(model);
        }
    }
}
