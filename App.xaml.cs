using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SoftwareCompanyApp.Data.SoftwareCompanyApp.Data;
using System.Windows;
using System;
using Microsoft.EntityFrameworkCore;
using SoftwareCompanyApp.Services;
using SoftwareCompanyApp.ViewModels;

namespace SoftwareCompanyApp
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        // Публичное свойство для доступа к контейнеру DI
        public static IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
            ServiceProvider = _serviceProvider; // Сохраняем ссылку на сервис провайдер
        }

        private void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<VacancyService>();
            services.AddSingleton<JobSeekerService>();


            services.AddTransient<VacancyViewModel>();
            services.AddTransient<JobSeekerViewModel>();
            services.AddTransient<OneJobSeekerViewModel>();
            services.AddTransient<OneVacancyViewModel>();
            services.AddTransient<StatisticsViewModel>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString, npgsqlOptions =>
                    npgsqlOptions.EnableRetryOnFailure())
                       .LogTo(Console.WriteLine, LogLevel.Information)
                       .EnableSensitiveDataLogging());

            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            });

            services.AddSingleton<MainApp>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var logger = _serviceProvider.GetRequiredService<ILogger<App>>();

            EnsureDatabaseCreated(logger);

            var mainWindow = _serviceProvider.GetRequiredService<MainApp>();
            mainWindow.Show();
        }

        private void EnsureDatabaseCreated(ILogger<App> logger)
        {
            try
            {
                var dbContext = _serviceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate(); // Применяем миграции
                logger.LogInformation("Database migrations applied successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to apply migrations.");
                MessageBox.Show($"Ошибка подключения или миграции: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            if (_serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
