namespace Nzen
{
    #region using
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using System.Net;
    #endregion

    public class Program
    {
        public static void Main(string[] args) =>
            CreateWebHostBuilder(args)
            .CaptureStartupErrors(true)
            .UseSetting(WebHostDefaults.DetailedErrorsKey, "true")
            .UseKestrel(options=>
            {
                options.Listen(IPAddress.Loopback, 5000, listenOptions =>
                  {
                      listenOptions.UseHttps("Nzen.pfx", "839da23a-df3b-4a61-a280-6118f7c52d7b");
                  });
            })
            .Build()
            .Run();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .UseStartup<Startup>();
    }
}
