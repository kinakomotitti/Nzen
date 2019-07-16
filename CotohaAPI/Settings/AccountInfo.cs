namespace CotohaAPI.Settings
{
    #region using
    using System;
    using System.Collections.Generic;
    using System.Text;
    #endregion

    public static class AccountInfo
    {
        //利用する場合は、環境変数に以下の定義をする。

        public static string DeveloperClientId { get; set; } = Environment.GetEnvironmentVariable(nameof(AccountInfo.DeveloperClientId));

        public static string DeveloperClientSecret { get; set; } = Environment.GetEnvironmentVariable(nameof(AccountInfo.DeveloperClientSecret));

    }
}
