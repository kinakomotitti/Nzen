﻿namespace NzenUnitTest.Manager.DatabaseManagerTest
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
    public class InsertEventContentsTest
    {
        [TestInitialize]
        public void TestInitFunction()
        {
            ApplicationEnv.Env.ConnectionString = "Host=localhost;Port=5432;Username=postgres;Password=postgres; Database=nzen_db";
        }

        [TestMethod]
        public void TestMethod1()
        {
            DatabaseManager.Executor.InsertEventContents("test1", "test1", "test1");
        }
    }
}
