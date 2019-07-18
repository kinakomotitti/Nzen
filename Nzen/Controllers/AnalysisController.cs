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

            await ExecuteAnalysis(model);

            return View("Result", model);
        }


        private async Task ExecuteAnalysis(AnalysisModel model)
        {
            var tokenResult = HttpClientManager.GetAccessTokenAsync().Result;

            if (tokenResult.StatusCode == HttpStatusCode.Created)
            {
                model.KeywordResult = await HttpClientManager.ExtractionKeywordsAsync(new CotohaAPI.Models.KeyWordRequest()
                {

                });

                model.SentimentResult = await HttpClientManager.SentimentAnalysisAsync(new CotohaAPI.Models.SentimentRequest()
                {

                });
            }
        }
    }
}