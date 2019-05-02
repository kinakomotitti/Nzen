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
    public class InsertEventOverviewTest
    {
        [TestInitialize]
        public void TestInitFunction()
        {
            ApplicationEnv.Env.ConnectionString = "Host=localhost;Port=5432;Username=postgres;Password=postgres; Database=nzen_db";
        }


        [TestMethod]
        public void TestMethod1()
        {
            using (Npgsql.NpgsqlConnection con = new Npgsql.NpgsqlConnection(ApplicationEnv.Env.ConnectionString))
            {
                con.Open();
                using (Npgsql.NpgsqlCommand command = con.CreateCommand())
                {
                    command.CommandText = "DELETE FROM tran_event_overview WHERE event_id='test1'";
                    command.ExecuteNonQuery();
                }
                con.Close();
            }

            DatabaseManager.Executor.InsertEventOverview("test1", "test1");
        }
    }
}
