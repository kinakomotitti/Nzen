namespace NzenUnitTest.Manager.DatabaseManagerTest
{
    #region using
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Extensions.Configuration;
    using Nzen.Manager;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Npgsql;
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
            var actual = DatabaseManager.Executor.GetGroupId("aaaaa", DateTime.Now);
            Assert.IsTrue(string.IsNullOrEmpty(actual));
        }
        [TestMethod]
        public void TestMethod2()
        {
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(ApplicationEnv.Env.ConnectionString))
                {
                    var command = con.CreateCommand();
                    command.CommandText = "INSERT INTO tran_event_overview  (event_id,event_entry_date,event_host_user_name) VALUES ('test1','2019-01-01','test99')";
                    con.Open();
                    command.ExecuteNonQuery();
                    con.Close();
                }
                var actual = DatabaseManager.Executor.GetGroupId("test1", new DateTime(2019, 1, 1));
                Assert.IsFalse(string.IsNullOrEmpty(actual));

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                using (NpgsqlConnection con = new NpgsqlConnection(ApplicationEnv.Env.ConnectionString))
                {
                    var command = con.CreateCommand();
                    command.CommandText = "DELETE FROM tran_event_overview WHERE event_entry_date='2019-01-01'";
                    con.Open();
                    command.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}
