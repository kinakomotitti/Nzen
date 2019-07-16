using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CotohaAPI.Models
{
    public class SentimentRequest:BaseRequestModel
    {
        [JsonProperty("sentence")]
        public string Sentence { get; set; }
    }
}
