using CotohaAPI;
using CotohaAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace CotohaAPIUnitTest
{
    [TestClass]
    public class KeywordAPI
    {
        [TestMethod]
        public void Success_Cace()
        {
            if (string.IsNullOrEmpty(CotohaApiManager.BearerValue))
                CotohaApiManager.GetAccessTokenAsync().Wait();


            var result = CotohaApiManager.ExtractionKeywordsAsync(new KeyWordRequest()
            {
                Document = new List<string>() { "レストランで昼食を食べた。"},
                Type = "kuzure",
                DoSegment = true,
                MaxKeywordNum = 2
            }).Result;

            Assert.AreEqual(2, result.Result.Count);

            var resultList = result.Result.ToList();

            Assert.IsTrue(resultList.Where(i => i.Form == "レストラン").Count() > 0);
            Assert.IsTrue(resultList.Where(i => i.Form == "昼食").Count() > 0);
        }

        [TestMethod]
        public void Failure_LongTextCace()
        {
            if (string.IsNullOrEmpty(CotohaApiManager.BearerValue))
                CotohaApiManager.GetAccessTokenAsync().Wait();


            var result = CotohaApiManager.ExtractionKeywordsAsync(new KeyWordRequest()
            {
                //282文字
                Document = new List<string>()
                {
                    "階層的な行セット、つまりチャプター (OLE DB 型DBTYPE_HCHAPTER、ADO 型adChapter) を使用して取得できます、OleDbDataReaderします。 チャプターを含むクエリとして返される場合、 DataReader、章を内の列として返されますDataReaderとして公開されると、 DataReaderオブジェクト。",
                    "ADO.NETデータセットテーブル間の親子リレーションシップを使用して階層的な行セットを表すためも使用できます。詳細については、次を参照してください。",
                    "Dataset、Datatable、および Dataviewします。",
                    "MSDataShape プロバイダーを使用して、顧客リストの顧客別オーダーのチャプター列を生成するコード サンプルを次に示します。",
                    "NET Framework Data Provider for Oracle は、クエリ結果を返すために、Oracle REF CURSOR の使用をサポートしています。 Oracle REF CURSOR は OracleDataReader として返されます。"
                },
                Type = "kuzure",
                DoSegment = true,
                MaxKeywordNum = 10
            }).Result;

            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [TestMethod]
        public void Failure_BearerEmptyCase()
        {
            CotohaApiManager.BearerValue = string.Empty;
            var result = CotohaApiManager.ExtractionKeywordsAsync(new KeyWordRequest()
            {
                Document = new List<string>() { "レストランで昼食を食べた。" },
                Type = "kuzure",
                DoSegment = true,
                MaxKeywordNum = 2
            }).Result;

            Assert.AreEqual(HttpStatusCode.NotAcceptable, result.StatusCode );
        }



    }
}