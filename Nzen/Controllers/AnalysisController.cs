using System;
namespace Nzen.Controllers
{
    #region using
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using CotohaAPI;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Nzen.Manager;
    using Nzen.Models;
    #endregion


    public class AnalysisController : Controller
    {

        [HttpGet]
        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        public async Task<ActionResult> ResultData(TargetEventInfo info)
        {
            AnalysisModel model = new AnalysisModel();

            var tokenResult = HttpClientManager.GetAccessToken().Result;

            if (tokenResult.StatusCode != HttpStatusCode.Created) return View("Index");

            model.KeywordResult= await HttpClientManager.ExtractionKeywords(new CotohaAPI.Models.KeyWordRequest()
            {

            });

            model.SentimentResult= await HttpClientManager.SentimentAnalysis(new CotohaAPI.Models.SentimentRequest()
            {

            });

            return View("Result");
        }
    }
}