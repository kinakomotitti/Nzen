namespace Nzen.Models
{
    #region using
    using CotohaAPI.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    #endregion

    /// <summary>
    /// 
    /// </summary>
    public class AnalysisModel
    {
        /// <summary>
        /// 
        /// </summary>
        public KeyWordResponce KeywordResult { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SentimentResponce SentimentResult { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TargetEventInfo info { get; set; }
    }

    public class TargetEventInfo
    {
        public string GroupId { get; set; }

        public DateTime EventDate { get; set; }
    }
}