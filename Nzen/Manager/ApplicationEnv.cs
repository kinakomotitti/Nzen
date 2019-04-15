﻿namespace Nzen.Manager
{
    #region using
    using Microsoft.Extensions.Configuration;
    using System;
    #endregion

    public class ApplicationEnv
    {
        public static ApplicationEnv Env = new ApplicationEnv();

        private ApplicationEnv()
        {
            var config = new ConfigurationBuilder().AddEnvironmentVariables().Build();
            var propList = typeof(Environment).GetProperties();
            foreach (var item in propList)
            {
                var temp = string.Empty;
                try
                {
                    temp = config.GetValue<string>(item.Name.ToUpper().ToString());
                }
                catch (Exception) { }
                if (temp != null) item.SetValue(this, temp);
            }
        }

        #region プロパティ

        /// <summary>
        /// (いずれ実装する)DBへの接続文字列
        /// </summary>
        public string ConnectionString { get; set; } = string.Empty;

        /// <summary>
        /// Cognitive ServiceのAPI設定
        /// </summary>
        public string SpeechAPI_Key { get; set; } = string.Empty;

        /// <summary>
        /// Cognitive Serviceのリージョン設定
        /// </summary>
        public string SpeechAPI_Region { get; set; } = string.Empty;

        #endregion
    }
}