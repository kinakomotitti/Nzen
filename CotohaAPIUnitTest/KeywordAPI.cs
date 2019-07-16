using CotohaAPI;
using CotohaAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace CotohaAPIUnitTest
{
    [TestClass]
    public class KeywordAPI
    {
        [TestMethod]
        public void Success_Cace()
        {
            HttpClientManager.GetAccessToken().Wait();


            var result = HttpClientManager.ExtractionKeywords(new KeyWordRequest()
            {
                Document = "レストランで昼食を食べた。",
                Type = "default",
                DoSegment = true,
                MaxKeywordNum = 2
            }).Result;


            Assert.AreEqual(2, result.Result.Length);

            var resultList = result.Result.ToList();

            Assert.IsTrue(resultList.Where(i => i.Form == "レストラン").Count() > 0);
            Assert.IsTrue(resultList.Where(i => i.Form == "昼食").Count() > 0);
        }

        [TestMethod]
        public void Failure_BearerEmptyCase()
        {
            var result = HttpClientManager.ExtractionKeywords(new KeyWordRequest()
            {
                Document = "レストランで昼食を食べた。",
                Type = "default",
                DoSegment = true,
                MaxKeywordNum = 2
            }).Result;

            Assert.AreEqual(result.StatusCode, HttpStatusCode.NotAcceptable);
        }



    }
}