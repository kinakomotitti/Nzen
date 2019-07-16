namespace CotohaAPI
{
    #region using
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System;
    using System.Globalization;
    #endregion


    internal class Converter<T> 
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };

        public T FromJson(string json) => JsonConvert.DeserializeObject<T>(json, Converter<T>.Settings);

        public string ToJson(T self) => JsonConvert.SerializeObject(self, Converter<T>.Settings);
    }
}
