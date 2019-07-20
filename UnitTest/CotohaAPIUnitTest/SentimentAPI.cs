using CotohaAPI;
using CotohaAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Threading;
using System.Web;

namespace CotohaAPIUnitTest
{

    [TestClass]
    public class SentimentAPI
    {
        [TestMethod]
        public void Success_Cace()
        {
                CotohaApiManager.GetAccessTokenAsync().Wait();
                System.Diagnostics.Debugger.Break();

            var result = CotohaApiManager.SentimentAnalysisAsync(new SentimentRequest()
            {
                Sentence = "人生の春を謳歌しています"
            }).Result;

            //TODO TEST 単体実行では成功するが、VSから一括実行すると、成功しない（NullReferrenceの例外）
            Assert.AreEqual("Positive", result.Result.Sentiment);
        }


        [TestMethod]
        public void Failure_BearerEmptyCase()
        {
            var result = CotohaApiManager.SentimentAnalysisAsync(new SentimentRequest()
            {
                Sentence = "人生の春を謳歌しています"
            }).Result;

            Assert.AreEqual(result.StatusCode, HttpStatusCode.NotAcceptable);
        }



    }
}
