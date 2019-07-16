using CotohaAPI;
using CotohaAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Web;

namespace CotohaAPIUnitTest
{

    [TestClass]
    public class SentimentAPI
    {
        [TestMethod]
        public void Success_Cace()
        {
            HttpClientManager.GetAccessToken(new AccessTokenRequest()
            {
                GrantType = "client_credentials",
                ClientId = CotohaAPI.Settings.AccountInfo.DeveloperClientId,
                ClientSecret = CotohaAPI.Settings.AccountInfo.DeveloperClientSecret
            }).Wait();


            var result = HttpClientManager.SentimentAnalysis(new SentimentRequest()
            {
                Sentence= "人生の春を謳歌しています"
            }).Result;


            Assert.AreEqual("Positive", result.Result.Sentiment);
        }

        [TestMethod]
        public void Failure_BearerEmptyCase()
        {
            var result = HttpClientManager.SentimentAnalysis(new SentimentRequest()
            {
                Sentence = "人生の春を謳歌しています"
            }).Result;

            Assert.AreEqual(result.StatusCode, HttpStatusCode.NotAcceptable);
        }



    }
}
