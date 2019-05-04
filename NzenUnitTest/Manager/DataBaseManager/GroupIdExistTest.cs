namespace NzenUnitTest.Manager.DatabaseManagerTest
{
    #region using
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Extensions.Configuration;
    using Nzen.Manager;
    using System;
    using System.Collections.Generic;
    using System.Text;
    #endregion

    [TestClass]
    public class GroupIdExistTest
    {
        [TestInitialize]
        public void TestInitFunction()
        {
            ApplicationEnv.Env.ConnectionString = "Host=localhost;Port=5432;Username=postgres;Password=postgres; Database=nzen_db";
        }

        [TestMethod]
        public void TestMethod1()
        {
            var actual = DatabaseManager.Executor.GroupIdExist("aaaaa");
            Assert.AreEqual(false, actual);
        }
        [TestMethod]
        public void TestMethod2()
        {
            var actual = DatabaseManager.Executor.GroupIdExist("test1");
            Assert.AreEqual(true, actual);
        }
    }
}
