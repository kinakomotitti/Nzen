namespace CommonWebAPI
{
    #region using

    using CommonWebAPI.Models;
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

        #region Method


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="url"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<S> ExecutePostAsyncWithBearer<T, S>(string url, T request) where T : BaseRequestModel
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


        public static async Task<S> ExecutePostAsyncWithHeaderValue<T, S>(string url,
                                                                          T request,
                                                                          Dictionary<string, string> headers,
                                                                          bool needBearer = false)
            where T : BaseRequestModel
            where S : BaseResponceModel, new()
        {
            if (needBearer == true &&
                string.IsNullOrWhiteSpace(HttpClientManager.BearerValue) == true)

                return new S() { StatusCode = HttpStatusCode.NotAcceptable };


            HttpClientManager.Client.DefaultRequestHeaders.Accept.Clear();
            foreach (var header in headers)
            {
                HttpClientManager.Client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }

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
            S result = new S();

            if ((response.StatusCode == HttpStatusCode.OK) ||
                (response.StatusCode == HttpStatusCode.Accepted) ||
                (response.StatusCode == HttpStatusCode.Created) ||
                (response.StatusCode == HttpStatusCode.NonAuthoritativeInformation) ||
                (response.StatusCode == HttpStatusCode.NoContent) ||
                (response.StatusCode == HttpStatusCode.ResetContent) ||
                (response.StatusCode == HttpStatusCode.PartialContent))
                result = JsonConvert.DeserializeObject<S>(await response.Content.ReadAsStringAsync());
            result.StatusCode = response.StatusCode;

            return result;
        }

        #endregion

    }
}