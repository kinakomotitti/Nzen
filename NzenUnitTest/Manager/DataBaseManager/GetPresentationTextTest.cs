using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nzen.Manager;
using System;

namespace NzenUnitTest
{
    [TestClass]
    public class GetPresentationTextTest
    {
        [TestInitialize]
        public void TestInitFunction()
        {
            ApplicationEnv.Env.ConnectionString = "Host=localhost;Port=5432;Username=postgres;Password=postgres; Database=nzen_db";
        }

        [TestMethod]
        public void TestMethod1()
        {
            var actual = DatabaseManager.Executor.GetPresentationText("54OJ", DateTime.Parse("2019-06-17"));

            Assert.AreEqual("さーて今週のサザエさんはと言ってたさーて今週のサザエさんはじゃんけんぽんふふふ。そんなことを言いつつ。テストとしてテスト。", actual);
        }
    }
}
