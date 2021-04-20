using GuzelSozlerim.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuzelSozlerim
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // https://github.com/dotnet-architecture/eShopOnWeb/blob/master/src/Web/Program.cs
            var host = CreateHostBuilder(args).Build();

            //uygulamay� �al��t�rmadan uygulaman�n hizmetlerini kullanmay� sa�l�yor. O hizmetlerde startup.cs deki servicesler
            using (var scope = host.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Kullanici>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                await DataSeed.SeedRollerVeKullanicilarAsync(roleManager, userManager);
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
