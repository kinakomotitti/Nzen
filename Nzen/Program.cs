namespace Nzen
{
    #region using
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    #endregion

    public class Program
    {
        public static void Main(string[] args) =>
            CreateWebHostBuilder(args)
            .CaptureStartupErrors(true)
            .UseSetting(WebHostDefaults.DetailedErrorsKey, "true")
            .Build()
            .Run();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .UseStartup<Startup>();
    }
}
