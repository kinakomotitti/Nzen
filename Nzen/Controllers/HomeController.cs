namespace Nzen.Controllers
{
    #region using
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Nzen.Manager;
    using Nzen.Models;
    #endregion

    public class HomeController : Controller
    {
        #region Common


        [HttpGet]
        public IActionResult Index()
        {
            return View(new HomeModel());
        }

        #endregion

        #region user

        [HttpPost]
        public IActionResult ChatRoom(HomeModel model)
        {
            ViewData["Message"] = "ChatRoom.";
            return View(model);
        }
        #endregion

        #region presenter

        [HttpPost]
        public IActionResult PresenterPrepare([Bind("Content")]HomeModel model)
        {
            if (string.IsNullOrEmpty(model.GroupId))
            {
                model.GroupId = GroupIdManager.Instance.GetGroupId(model.UserName);
            }
            else
            {
                if (GroupIdManager.Instance.IsExistGroupId(model.GroupId)==false)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("指定されたグループIDは存在しません。\r\n");
                    sb.AppendLine("新しいグループIDを作成する場合は、\r\n");
                    sb.AppendLine("グループIDを空白にしたままJoinしてください。");
                    this.ViewData["ErrorMessage"] = sb.ToString();
                    return View("Index", new HomeModel());
                }
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult PresentationRoom(HomeModel model)
        {
            model.Env = ApplicationEnv.Env;
            return View(model);
        }

        #endregion

        #region system error

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion

    }
}
