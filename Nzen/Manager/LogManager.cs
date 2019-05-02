using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nzen.Manager
{
    public class LogManager
    {
        public static LogManager Writer = new LogManager();

        
        public void Debug(string message)
        {
            this.WriteCore("Debug",message);
        }


        private void WriteCore(string logLevel,string message)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.ffffff")},");
            sb.Append($"{logLevel},");
            sb.Append($"{message},");

            Console.WriteLine(sb.ToString());
        }
    }
}
