namespace Nzen.Manager
{
    #region using
    using System;
    using Npgsql;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Nzen.Models;
    #endregion

    public class DatabaseManager
    {
        public static DatabaseManager Executor { get; set; } = new DatabaseManager();

        #region Insert

        public void InsertEventContents(string userName, string group, string message, string type = "audience")
        {
            var paramList = new List<NpgsqlParameter>();
            paramList.Add(new NpgsqlParameter("group_id", group));
            paramList.Add(new NpgsqlParameter("group_entry_date", DateTime.Now));
            paramList.Add(new NpgsqlParameter("data", message));
            paramList.Add(new NpgsqlParameter("info_type", type));
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

        public void InsertEventOverview(string groupId, string userName)
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

        #endregion

        #region Select

        public string GetGroupId(string groupId, DateTime eventTime)
        {
            List<NpgsqlParameter> paramList = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter()
                {
                    ParameterName = "event_id",
                    Value = groupId
                },
                 new NpgsqlParameter()
                 {
                    ParameterName = "event_entry_date",
                    Value = eventTime.Date
                }
            };
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT event_id,event_host_user_name ");
            sb.Append("FROM tran_event_overview ");
            sb.Append("WHERE event_id=@event_id AND");
            sb.Append("      event_entry_date=@event_entry_date;");

            string sql = sb.ToString();
            return this.ExecuteReader(sql, paramList);
        }

        public string GetPresentationText(string groupId, DateTime eventTime)
        {
            List<NpgsqlParameter> paramList = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter()
                {
                    ParameterName = "group_id",
                    Value = groupId
                },
                 new NpgsqlParameter()
                 {
                    ParameterName = "group_entry_date",
                    Value = eventTime.Date
                }
            };

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT data ");
            sb.Append("FROM tran_event_info_detail ");
            sb.Append("WHERE info_type='presentor' AND ");
            sb.Append("      group_id=@group_id AND ");
            sb.Append("      group_entry_date=@group_entry_date;");

            string sql = sb.ToString();
            return this.ExecuteReader(sql, paramList);
        }

        public List<EventDataModel> GetEventData(string groupId, DateTime eventTime)
        {
            List<NpgsqlParameter> paramList = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter()
                {
                    ParameterName = "group_id",
                    Value = groupId
                },
                 new NpgsqlParameter()
                 {
                    ParameterName = "group_entry_date",
                    Value = eventTime.Date
                }
            };

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT serial_no,data ");
            sb.Append("FROM tran_event_info_detail ");
            sb.Append("WHERE group_id=@group_id AND ");
            sb.Append("      group_entry_date=@group_entry_date;");

            string sql = sb.ToString();
            return this.ExecuteReaderWithPoco<EventDataModel>(sql, paramList);
        }

        #endregion


        #region private

        private void ExecuteNonQuery(string sql, List<NpgsqlParameter> parameters = null)
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
                                command.Parameters.AddWithValue(item.ParameterName, item.Value);
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

        private string ExecuteReader(string sql, List<NpgsqlParameter> parameters = null)
        {
            string result = string.Empty;

            this.ExecuteReaderCore((readerResult) => {
                while (readerResult.Read())
                {
                    result += readerResult.GetString(0);
                }
            }, sql, parameters);

            return result;
        }

        private List<T> ExecuteReaderWithPoco<T>(string sql, List<NpgsqlParameter> parameters = null) where T : new()
        {
            List<T> resultList = new List<T>();
            this.ExecuteReaderCore((readerResult) =>
            {
                if (readerResult.HasRows)
                {
                    while (readerResult.Read())
                    {
                        var result = new T();
                        for (int counter = 0; counter < readerResult.VisibleFieldCount - 1; counter++)
                        {
                            var resultProperty = typeof(T).GetProperty(readerResult.GetName(counter));

                            if (resultProperty.PropertyType == typeof(int))
                            {
                                resultProperty.SetValue(result, readerResult.GetInt32(counter));

                            }
                            else if (resultProperty.PropertyType == typeof(string))
                            {
                                resultProperty.SetValue(result, readerResult.GetString(counter));

                            }
                            else
                            {
                                //date
                                resultProperty.SetValue(result, readerResult.GetDateTime(counter));

                            }
                        }
                        resultList.Add(result);
                    }
                }
            }, sql, parameters);
                
            return resultList;
        }



        private void ExecuteReaderCore(Action<NpgsqlDataReader> action, string sql, List<NpgsqlParameter> parameters = null)
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
                    if (parameters != null)
                        parameters.ForEach(item =>
                        {
                            command.Parameters.AddWithValue(item.ParameterName, item.Value);
                        });
                    var readerResult = command.ExecuteReader();
                    action(readerResult);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                con.Close();
            }
        }
        #endregion

    }
}
