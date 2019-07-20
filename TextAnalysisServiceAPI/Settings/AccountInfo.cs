namespace TextAnalysisServiceAPI.Settings
{
    #region using

    using System;
    using System.Collections.Generic;
    using System.Text;

    #endregion


    /// <summary>
    /// 
    /// </summary>
    public static class AccountInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public static string KeyPhraseExtractionKey { get; set; } = Environment.GetEnvironmentVariable(nameof(AccountInfo.KeyPhraseExtractionKey));


        /// <summary>
        /// 
        /// </summary>
        public static string TextAnalyticsEndpointUrl { get; set; }= Environment.GetEnvironmentVariable(nameof(AccountInfo.TextAnalyticsEndpointUrl));
    }
}