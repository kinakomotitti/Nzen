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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetText"></param>
        /// <returns></returns>
        public static async Task<KeyPhraseExtractResponce> ExecuteJapaneseKeyPhraseExtractAsync(string targetText)
        {
            KeyPhraseExtractRequest request = new KeyPhraseExtractRequest();
            request.Documents.Add(new KeyPhraseExtractRequest.Document()
            {
                Id = "1",
                Text = targetText,
                Language = "ja"
            });

            return await ExecuteKeyPhraseExtractAsync(request);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetText"></param>
        /// <returns></returns>
        public static async Task<KeyPhraseExtractResponce> ExecuteKeyPhraseExtractAsync(List<string> targetTextList,string lang)
        {
            KeyPhraseExtractRequest request = new KeyPhraseExtractRequest();
            var counter = 1;
            foreach (var item in targetTextList)
            {
                request.Documents.Add(new KeyPhraseExtractRequest.Document()
                {
                    Id = counter.ToString(),
                    Text = item,
                    Language = lang
                });
                counter++;
            }

            return await ExecuteKeyPhraseExtractAsync(request);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetList">Key:Text,Value:language</param>
        /// <returns></returns>
        public static async Task<KeyPhraseExtractResponce> ExecuteKeyPhraseExtractAsync(Dictionary<string,string> targetList)
        {
            KeyPhraseExtractRequest request = new KeyPhraseExtractRequest();
            var counter = 1;
            foreach (var item in targetList)
            {
                request.Documents.Add(new KeyPhraseExtractRequest.Document()
                {
                    Id = counter.ToString(),
                    Text = item.Key,
                    Language = item.Value
                });
                counter++;
            }

            return await ExecuteKeyPhraseExtractAsync(request);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static async Task<KeyPhraseExtractResponce> ExecuteKeyPhraseExtractAsync(KeyPhraseExtractRequest request)
        {
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