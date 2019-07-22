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

    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using Nzen.Manager;
    using Nzen.Models;
    using CotohaAPI;
    using TextAnalysisServiceAPI;

    #endregion


    /// <summary>
    /// 
    /// </summary>
    public class AnalysisController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View("Index");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> ResultData(TargetEventInfo info)
        {
            var rawDataList = DatabaseManager.Executor.GetEventData(info.GroupId, info.EventDate);

            if (rawDataList.Count() == 0) return View("Index");


            AnalysisModel model = new AnalysisModel();
            model.Presenter.SpeachText = rawDataList.Where(data => data.info_type == "presentor")
                                                    .OrderBy(data => data.serial_no)
                                                    .Select((data) => data.data)
                                                    .ToList();


            await ExecuteAnalysis(model);

            return View("Result", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task ExecuteAnalysis(AnalysisModel model)
        {
            var tokenResult = await CotohaApiManager.GetAccessTokenAsync();

            if (tokenResult.StatusCode == HttpStatusCode.Created)
            {
                model.KeywordResult = await CotohaApiManager.ExtractionKeywordsAsync(new CotohaAPI.Models.KeyWordRequest()
                {
                    Document = model.Presenter.SpeachText,
                    Type = "kuzure",
                    DoSegment = true,
                    MaxKeywordNum = 10
                });

                model.KeyPhraseExtractResult = 
                    await TextAnalyticsManager.ExecuteJapaneseKeyPhraseExtractAsync(string.Join("。", model.Presenter.SpeachText));
            }
        }
    }
}