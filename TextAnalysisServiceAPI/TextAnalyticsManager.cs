namespace TextAnalysisServiceAPI
{
    #region using

    using CommonWebAPI;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TextAnalysisServiceAPI.Models;
    using TextAnalysisServiceAPI.Settings;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    public class TextAnalyticsManager
    {
        public static async Task<KeyPhraseExtractResponce> ExecuteKeyPhraseExtractAsync(string targetText)
        {
            KeyPhraseExtractRequest request = new KeyPhraseExtractRequest();
            request.Documents.Add(new KeyPhraseExtractRequest.Document()
            {
                Id = "1",
                Text = targetText,
                Language = "ja"
            });

            return
                await HttpClientManager.ExecutePostAsyncWithHeaderValue<KeyPhraseExtractRequest, KeyPhraseExtractResponce>
                    (
                        URL.KeyPhraseUrl,
                        request,
                        new Dictionary<string, string>()
                        {
                            {  "Ocp-Apim-Subscription-Key",AccountInfo.KeyPhraseExtractionKey }
                        }
                     );
        }
    }
}
