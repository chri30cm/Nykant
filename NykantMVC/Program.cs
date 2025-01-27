using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace NykantMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //webBuilder.UseSetting("https_port", "443");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
