using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CotohaAPI.Models
{
    public class AccessTokenRequest : BaseRequestModel
    {
        [JsonProperty("grantType")]
        public string GrantType { get; set; }

        [JsonProperty("clientId")]
        public string ClientId { get; set; }

        [JsonProperty("clientSecret")]
        public string ClientSecret { get; set; }
    }
}
