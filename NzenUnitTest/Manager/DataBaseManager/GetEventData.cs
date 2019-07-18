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
    public class GetEventData
    {
        [TestInitialize]
        public void TestInitFunction()
        {
            ApplicationEnv.Env.ConnectionString = "Host=localhost;Port=5432;Username=postgres;Password=postgres; Database=nzen_db";
        }

        [TestMethod]
        public void TestMethod2()
        {
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(ApplicationEnv.Env.ConnectionString))
                {
                    var command = con.CreateCommand();
                    command.CommandText = "INSERT INTO tran_event_info_detail  ( group_id, group_entry_date,data) VALUES ('test1','2019-01-01','test data sample')";
                    con.Open();
                    command.ExecuteNonQuery();
                    con.Close();
                }
                var actual = DatabaseManager.Executor.GetEventData("test1", new DateTime(2019, 1, 1));
                Assert.IsTrue(actual.Count==1);

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
                    command.CommandText = "DELETE FROM tran_event_info_detail WHERE group_entry_date='2019-01-01'";
                    con.Open();
                    command.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}
