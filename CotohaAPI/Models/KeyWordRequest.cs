namespace CotohaAPI.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public  class KeyWordRequest:BaseRequestModel
    {
        [JsonProperty("document")]
        public string Document { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("do_segment")]
        public bool DoSegment { get; set; }

        [JsonProperty("max_keyword_num")]
        public long MaxKeywordNum { get; set; }
    }
}