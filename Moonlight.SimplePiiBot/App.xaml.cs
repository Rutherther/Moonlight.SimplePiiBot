using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Moonlight.Core;
using Moonlight.Local.Extensions;
using Moonlight.SimplePiiBot.ViewModels;
using Moonlight.Translation;

namespace Moonlight.SimplePiiBot
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : IServiceConfiguration
    {
        public void ConfigureServices(MoonlightAPI api, IServiceCollection services)
        {
            services.AddSingleton(api);
            services.AddTransient<BotWindowViewModel>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                AppConfig config = new AppConfig
                {
                    Configuration = (App)App.Current
                };

                MoonlightAPI api = new MoonlightAPI(config)
                {
                    Language = Language.EN
                };


                api.AllocConsole();


                api.DeferPackets();

                BotWindow bot = new BotWindow()
                {
                    DataContext = api.Services.GetService<BotWindowViewModel>()
                };

                bot.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}