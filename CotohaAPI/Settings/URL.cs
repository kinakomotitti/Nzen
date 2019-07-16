using System;
using System.Collections.Generic;
using System.Text;

namespace CotohaAPI.Settings
{
    public static class URL
    {

        private static string BaseUrl { get; set; } = "https://api.ce-cotoha.com/api/dev";

        public static string AccessTokenUrl { get; set; } = "https://api.ce-cotoha.com/v1/oauth/accesstokens";

        public static string ParseUrl { get; set; } = $"{BaseUrl}/nlp/v1/parse";

        public static string KeywordUrl { get; set; } = $"{BaseUrl}/nlp/v1/keyword";

        public static string UserAttributeUrl { get; set; } = $"{BaseUrl}/nlp/beta/user_attribute";

        public static string SentimentUrl { get; set; } = $"{BaseUrl}/nlp/v1/sentiment";

    }
}
