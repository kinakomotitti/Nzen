using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;
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
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(ApplicationEnv.Env.ConnectionString))
                {
                    var command = con.CreateCommand();
                    command.CommandText = "INSERT INTO tran_event_info_detail  (group_id, group_entry_date,data,info_type) VALUES ('54OJ','2019-06-17','test data sample','presentor')";
                    con.Open();
                    command.ExecuteNonQuery();
                    con.Close();
                }
                var actual = DatabaseManager.Executor.GetPresentationText("54OJ", DateTime.Parse("2019-06-17"));
                Assert.AreEqual("test data sample", actual);
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
                    command.CommandText = "DELETE FROM tran_event_info_detail WHERE group_entry_date='2019-06-17'";
                    con.Open();
                    command.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}
