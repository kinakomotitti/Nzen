namespace CotohaAPI
{
    #region using
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


    public static class HttpClientManager
    {

        private static HttpClient Client = new HttpClient();


        /// <summary>
        /// 認証ありのWeb APIを利用する時に利用するパラメータです。
        /// </summary>
        public static string BearerValue { get; set; } = string.Empty;

        #region CommonMethod


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="url"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<S> ExecutePostAsyncWithBearer<T,S>(string url, T request) where T : BaseRequestModel
                                                                                            where S : BaseResponceModel, new()
        {
            //when this method is called without bearer value, return null.
            if (string.IsNullOrWhiteSpace(HttpClientManager.BearerValue))
            {
                return new S()
                {
                    StatusCode = HttpStatusCode.NotAcceptable
                };
            }

            HttpClientManager.Client.DefaultRequestHeaders.Accept.Clear();
            HttpClientManager.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpClientManager.BearerValue);

            return await ExecutePostAsync<T, S>(url, request);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="url"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<S> ExecutePostAsync<T, S>(string url, T request) where T : BaseRequestModel
                                                                                       where S : BaseResponceModel, new()
        {
            var content = new StringContent(new Converter<T>().ToJson(request),
                                            Encoding.UTF8,
                                            "application/json");

            var response = await HttpClientManager.Client.PostAsync(url, content);

            S result = JsonConvert.DeserializeObject<S>(await response.Content.ReadAsStringAsync());
            result.StatusCode = response.StatusCode;
            return result;
        }

        #endregion

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
                await ExecutePostAsync<AccessTokenRequest, AccessTokenResponce>(url,request);

            //set the bearer value for the next API call.
            HttpClientManager.BearerValue = 
                (responce.StatusCode == HttpStatusCode.Created )? responce.AccessToken : string.Empty;
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
                await ExecutePostAsync<AccessTokenRequest, AccessTokenResponce>(url,request);

            //set the bearer value for the next API call.
            HttpClientManager.BearerValue = 
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
            return await ExecutePostAsyncWithBearer<KeyWordRequest, KeyWordResponce>(url, request);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<SentimentResponce> SentimentAnalysisAsync(SentimentRequest request)
        {
            var url = Settings.URL.SentimentUrl;
            return await ExecutePostAsyncWithBearer<SentimentRequest, SentimentResponce>(url, request);
        }

        #endregion

    }
}