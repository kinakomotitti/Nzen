namespace NzenUnitTest.Manager
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
    public class DatabaseManagerTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            ApplicationEnv.Env.ConnectionString = "Host=localhost;Port=5432;Username=postgres;Password=postgres; Database=nzen_db";
            DatabaseManager.Executor.InsertEventContents("test1", "test1", "test1");
        }
    }
}
