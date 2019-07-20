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
    public class KeyPhraseExtractResponce:BaseResponceModel
    {

        [JsonProperty("documents")]
        public List<Document> Documents { get; set; } = new List<Document>();

        [JsonProperty("errors")]
        public object[] Errors { get; set; }



        /// <summary>
        /// 
        /// </summary>
        public class Document
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("keyPhrases")]
            public List<string> KeyPhrases { get; set; }
        }
    }


}