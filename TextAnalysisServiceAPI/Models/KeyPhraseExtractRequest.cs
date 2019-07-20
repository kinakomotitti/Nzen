namespace TextAnalysisServiceAPI.Models
{
    using CommonWebAPI.Models;
    #region using

    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Text;

    #endregion


    /// <summary>
    /// 
    /// </summary>
    public class KeyPhraseExtractRequest : BaseRequestModel
    {
        [JsonProperty("documents")]
        public List<Document> Documents { get; set; } = new List<Document>();


        /// <summary>
        /// 
        /// </summary>
        public class Document
        {
            [JsonProperty("language")]
            public string Language { get; set; }

            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("text")]
            public string Text { get; set; }
        }
    }
}
