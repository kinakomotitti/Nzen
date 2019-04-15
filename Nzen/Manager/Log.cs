namespace Nzen.Manager
{
    #region using
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Xml;
    #endregion

    public class Log
    {
        public static Log Logger { get; set; } = new Log();
        private readonly ILog log = LogManager.GetLogger(typeof(Hubs.ChatHub));

        private Log()
        {
            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(System.IO.File.OpenRead("log4net.config"));
            var repo = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
            log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
        }


        #region 公開メソッド

        public void Debug(string message)
        {
            this.log.Debug(message);
        }


        public void Info(string message)
        {
            this.log.Info(message);
        }

        public void Error(string message)
        {
            this.log.Error(message);
        }

        #endregion

    }
}
