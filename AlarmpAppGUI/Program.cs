using AlarmApp.Components;
using Microsoft.Extensions.Hosting;
using AlarmApp.Components;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;
using Quartz.Impl;
using Quartz;

namespace AlarmpAppGUI
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            //ApplicationConfiguration.Initialize();
            var host = CreateHostBuilder().Build();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var alarmManager = host.Services.GetRequiredService<IAlarmManager>();
            Application.Run(new Form1(alarmManager));
        }

        static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {

                    services.AddSingleton<IAlarmManager, AlarmManager>();
                    services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
                    services.AddSingleton(provider => provider.GetRequiredService<ISchedulerFactory>().GetScheduler().Result);
                    //services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
                });
    
    }
}