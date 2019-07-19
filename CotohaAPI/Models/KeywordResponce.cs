using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CotohaAPI.Models
{
    public  class KeyWordResponce:BaseResponceModel
    {
        [JsonProperty("result")]
        public List<KeywordResult> Result { get; set; } = new List<KeywordResult>();

        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public class KeywordResult
    {
        [JsonProperty("form")]
        public string Form { get; set; }

        [JsonProperty("score")]
        public double Score { get; set; }
    }
}
