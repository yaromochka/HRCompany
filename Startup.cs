using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SoftwareCompanyApp.Data.SoftwareCompanyApp.Data;
using SoftwareCompanyApp.ViewModels;
using System;

namespace SoftwareCompanyApp
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        // Конструктор для получения IConfiguration
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Регистрируем IConfiguration для доступа к appsettings.json
            services.AddSingleton<IConfiguration>(_configuration);

            // Регистрация DbContext с использованием строки подключения из appsettings.json
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));

            // Регистрация сервисов

            // Регистрация ViewModels
            services.AddTransient<MainViewModel>();
            services.AddTransient<StatisticsViewModel>();

            return services.BuildServiceProvider();
        }
    }
}