namespace Nzen.Manager
{
    #region using
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    #endregion

    /// <summary>
    /// 当日のみ有効のグループIDの生成を行うクラス
    /// </summary>
    public class GroupIdManager
    {
        public static GroupIdManager Instance = new GroupIdManager();
        private static string groupChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ!#@?";
        private static int groupIdLength = 4;

        #region コンストラクタ

        private GroupIdManager()
        {

        }

        #endregion

        #region public

        public string GetGroupId(string userName)
        {

            //重複した場合、２回リトライする。
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    string groupId = this.GenerateGroupId();
                    this.InsertGroupInformation(groupId, userName);
                    return groupId;
                }
                catch (Npgsql.PostgresException ex)
                {
                    if (i < 3) continue;
                    else throw ex;
                }
            }

            //コンパイルエラー対策
            return string.Empty;
        }

        #endregion

        #region private

        private string GenerateGroupId()
        {
            StringBuilder sb = new StringBuilder(GroupIdManager.groupIdLength);
            Random r = new Random();

            for (int i = 0; i < GroupIdManager.groupIdLength; i++)
            {
                int pos = r.Next(GroupIdManager.groupChars.Length);
                char c = GroupIdManager.groupChars[pos];
                sb.Append(c);
            }

            return sb.ToString();
        }

        private void InsertGroupInformation(string groupId, string userName)
        {
            DatabaseManager.Executor.InsertEventOverview(groupId, userName);
        }

        #endregion

    }
}
