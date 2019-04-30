namespace Nzen.Manager
{
    using Npgsql;
    #region using
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    #endregion

    public class DatabaseManager
    {
        public static DatabaseManager Executor { get; set; } = new DatabaseManager();


        public void InsertEventContents(string userName, string group, string contents)
        {
            var paramList = new List<NpgsqlParameter>();
            paramList.Add(new NpgsqlParameter("event_code", group));
            paramList.Add(new NpgsqlParameter("info_type", "test"));
            paramList.Add(new NpgsqlParameter("data", contents));
            paramList.Add(new NpgsqlParameter("insert_date", DateTime.Now));
            paramList.Add(new NpgsqlParameter("insert_user", userName));
            paramList.Add(new NpgsqlParameter("update_date", DateTime.Now));
            paramList.Add(new NpgsqlParameter("update_user", userName));
            this.ExecuteNonQuery("INSERT INTO public.tran_event_info_detail VALUES (@event_code, @info_type, @data, @insert_date, @insert_user, @update_date, @update_user)", paramList);
        }


        private void ExecuteNonQuery(string sql, List<NpgsqlParameter> parameters)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(ApplicationEnv.Env.ConnectionString))
            {
                con.Open();
                using (NpgsqlTransaction tran = con.BeginTransaction())
                {
                    try
                    {
                        NpgsqlCommand command = new NpgsqlCommand()
                        {
                            Connection = con,
                            Transaction = tran,
                            CommandText = sql
                        };

                        if (parameters != null)
                            parameters.ForEach(item =>
                            {
                                command.Parameters.AddWithValue(item.ParameterName,item.Value);
                            });

                        command.ExecuteNonQuery();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }
                    con.Close();
                }
            }
        }

        private void ExecuteReader(string sql)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(ApplicationEnv.Env.ConnectionString))
            {
                con.Open();
                try
                {
                    NpgsqlCommand command = new NpgsqlCommand()
                    {
                        Connection = con,
                        CommandText = sql
                    };
                    var result = command.ExecuteReader();
                }
                catch (Exception)
                {
                }
                con.Close();
            }
        }
    }
}
