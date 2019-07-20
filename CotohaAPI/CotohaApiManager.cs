namespace CotohaAPI
{
    #region using
    using CommonWebAPI;
    using CotohaAPI.Models;

    using Newtonsoft.Json;

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    #endregion


    public static class CotohaApiManager
    {

        private static HttpClient Client = new HttpClient();


        /// <summary>
        /// 認証ありのWeb APIを利用する時に利用するパラメータです。
        /// </summary>
        public static string BearerValue { get; set; } = string.Empty;

        #region CotohaAPI

        /// <summary>
        /// 環境変数から値を設定して、アクセストークンを取得する。
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<AccessTokenResponce> GetAccessTokenAsync()
        {
            var url = Settings.URL.AccessTokenUrl;
            var request = new AccessTokenRequest()
            {
                GrantType = "client_credentials",
                ClientId = CotohaAPI.Settings.AccountInfo.DeveloperClientId,
                ClientSecret = CotohaAPI.Settings.AccountInfo.DeveloperClientSecret
            };


            AccessTokenResponce responce =
                await HttpClientManager.ExecutePostAsync<AccessTokenRequest, AccessTokenResponce>(url, request);

            //set the bearer value for the next API call.
            CotohaApiManager.BearerValue =
                (responce.StatusCode == HttpStatusCode.Created) ? responce.AccessToken : string.Empty;
            return responce;
        }


        /// <summary>
        /// 環境変数以外から取得した値を利用して、アクセストークンを取得する。
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<AccessTokenResponce> GetAccessTokenAsync(AccessTokenRequest request)
        {
            var url = Settings.URL.AccessTokenUrl;
            AccessTokenResponce responce =
                await HttpClientManager.ExecutePostAsync<AccessTokenRequest, AccessTokenResponce>(url, request);

            //set the bearer value for the next API call.
            CotohaApiManager.BearerValue =
                responce.StatusCode == HttpStatusCode.Created ? responce.AccessToken : string.Empty;

            return responce;
        }


        /// <summary>
        /// キーワード抽出API。
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        /// キーワード抽出APIの結果。
        /// 事前にBearerを発行していない場合は、HttpStatusCode.NotAcceptableがResponceオブジェクトに格納されて返却される。
        /// </returns>
        public static async Task<KeyWordResponce> ExtractionKeywordsAsync(KeyWordRequest request)
        {
            var url = Settings.URL.KeywordUrl;
            return await HttpClientManager.ExecutePostAsyncWithBearer<KeyWordRequest, KeyWordResponce>(url, request);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<SentimentResponce> SentimentAnalysisAsync(SentimentRequest request)
        {
            var url = Settings.URL.SentimentUrl;
            return await HttpClientManager.ExecutePostAsyncWithBearer<SentimentRequest, SentimentResponce>(url, request);
        }

        #endregion

    }
}