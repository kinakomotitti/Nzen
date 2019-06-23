namespace Nzen.Controllers
{
    #region using
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Nzen.Manager;
    #endregion


    [Route("api/[controller]")]
    [ApiController]
    public class PresentationDataController : ControllerBase
    {

        //https://localhost:44362/api/PresentationData?groupId=54OJ&date=2019-06-17
        [HttpGet]
        public JsonResult GetPresentationText(string date, string groupId)
        {
            System.Text.RegularExpressions.Regex.IsMatch(date, "^[0-9]{4}-[0-9]{1,2}-[0-9]{1,2}$");


            return new JsonResult(DatabaseManager.Executor.GetPresentationText(groupId, DateTime.Parse(date)));
        }
    }
}