using CotohaAPI;
using CotohaAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace CotohaAPIUnitTest
{
    [TestClass]
    public class AccessTokenAPI
    {
        public HttpStatusCode HttpSTatusCode { get; private set; }

        [TestMethod]
        public void Success_Pattern01Cace()
        {
            var task = HttpClientManager.GetAccessToken(new AccessTokenRequest()
            {
                GrantType = "client_credentials",
                ClientId = CotohaAPI.Settings.AccountInfo.DeveloperClientId,
                ClientSecret = CotohaAPI.Settings.AccountInfo.DeveloperClientSecret
            });

            var result = task.Result;
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
            Assert.IsFalse(string.IsNullOrWhiteSpace(HttpClientManager.BearerValue));
        }

        [TestMethod]
        public void Success_Pattern02Cace()
        {
            var task = HttpClientManager.GetAccessToken();
            var result = task.Result;

            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
            Assert.IsFalse(string.IsNullOrWhiteSpace(HttpClientManager.BearerValue));
        }
    }
}