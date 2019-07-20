using System;
using System.Collections.Generic;
using System.Text;

namespace TextAnalysisServiceAPI.Settings
{

    public class URL
    {

        public static string KeyPhraseUrl { get; set; } = $"{AccountInfo.TextAnalyticsEndpointUrl}/keyPhrases";

    }
}
