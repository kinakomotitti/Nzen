using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CotohaAPI.Models
{
    public  class SentimentResponce: BaseResponceModel
    {
        [JsonProperty("result")]
        public SentimentResult Result { get; set; }

        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public  class SentimentResult
    {
        [JsonProperty("sentiment")]
        public string Sentiment { get; set; }

        [JsonProperty("score")]
        public double Score { get; set; }

        [JsonProperty("emotional_phrase")]
        public EmotionalPhrase[] EmotionalPhrase { get; set; }
    }

    public  class EmotionalPhrase
    {
        [JsonProperty("form")]
        public string Form { get; set; }

        [JsonProperty("emotion")]
        public string Emotion { get; set; }
    }
}
