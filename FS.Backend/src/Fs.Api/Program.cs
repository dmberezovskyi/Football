using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Fs.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<StartupSetup>();
            });
        }
    }
}