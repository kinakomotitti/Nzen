namespace Nzen.Models
{
    #region using
    using CotohaAPI.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TextAnalysisServiceAPI.Models;
    #endregion

    /// <summary>
    /// 
    /// </summary>
    public class AnalysisModel
    {

        public KeyPhraseExtractResponce KeyPhraseExtractResult { get; set; } = new KeyPhraseExtractResponce();

        /// <summary>
        /// 
        /// </summary>
        public KeyWordResponce KeywordResult { get; set; } = new KeyWordResponce();

        /// <summary>
        /// 
        /// </summary>
        public SentimentResponce SentimentResult { get; set; } = new SentimentResponce();

        /// <summary>
        /// 
        /// </summary>
        public TargetEventInfo Info { get; set; } = new TargetEventInfo();

        public PresenterInfo Presenter { get; set; } = new PresenterInfo();
    }

    /// <summary>
    /// 
    /// </summary>
    public class TargetEventInfo
    {
        public string GroupId { get; set; }

        public DateTime EventDate { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public class PresenterInfo
    {
        public List<string> SpeachText { get; set; } = new List<string>();

    }

    /// <summary>
    /// 
    /// </summary>
    public class AudienceInfo
    {
        public List<string> Comments { get; set; } = new List<string>();

    }
}