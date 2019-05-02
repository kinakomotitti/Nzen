namespace Nzen.Manager
{
    #region using
    using System;
    using Npgsql;
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
            paramList.Add(new NpgsqlParameter("group_id", group));
            paramList.Add(new NpgsqlParameter("group_entry_date", DateTime.Now));
            paramList.Add(new NpgsqlParameter("data", "test"));
            paramList.Add(new NpgsqlParameter("info_type", contents));
            paramList.Add(new NpgsqlParameter("insert_date", DateTime.Now));
            paramList.Add(new NpgsqlParameter("insert_user", userName));
            paramList.Add(new NpgsqlParameter("update_date", DateTime.Now));
            paramList.Add(new NpgsqlParameter("update_user", userName));
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO public.tran_event_info_detail ");
            sb.Append("(group_id, group_entry_date, info_type, data, insert_date, insert_user, update_date, update_user)");
            sb.Append("VALUES (");
            sb.Append("@group_id,");
            sb.Append("@group_entry_date,");
            sb.Append("@info_type,");
            sb.Append("@data,");
            sb.Append("@insert_date, ");
            sb.Append("@insert_user, ");
            sb.Append("@update_date, ");
            sb.Append("@update_user");
            sb.Append(");");
            this.ExecuteNonQuery(sb.ToString(), paramList);
        }

        public void InsertEventOverview(string groupId,string userName)
        {
            var paramList = new List<NpgsqlParameter>();
            paramList.Add(new NpgsqlParameter("event_id", groupId));
            paramList.Add(new NpgsqlParameter("event_entry_date", DateTime.Now));
            paramList.Add(new NpgsqlParameter("event_host_user_name", userName));
            paramList.Add(new NpgsqlParameter("insert_date", DateTime.Now));
            paramList.Add(new NpgsqlParameter("insert_user", userName));
            paramList.Add(new NpgsqlParameter("update_date", DateTime.Now));
            paramList.Add(new NpgsqlParameter("update_user", userName));

            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO public.tran_event_overview");
            sb.Append(" VALUES (");
            sb.Append("@event_id, ");
            sb.Append("@event_entry_date, ");
            sb.Append("@event_host_user_name, ");
            sb.Append("@insert_date, ");
            sb.Append("@insert_user, ");
            sb.Append("@update_date, ");
            sb.Append("@update_user");
            sb.Append(");");

            this.ExecuteNonQuery(sb.ToString(), paramList);
        }


        #region private

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

        #endregion

    }
}
