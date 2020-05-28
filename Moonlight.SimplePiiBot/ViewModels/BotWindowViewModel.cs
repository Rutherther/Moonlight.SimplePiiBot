using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Moonlight.Clients;
using Moonlight.Local.Extensions;
using Moonlight.SimplePiiBot.Models;

namespace Moonlight.SimplePiiBot.ViewModels
{
    public class BotWindowViewModel
    {
        public BotWindowViewModel(MoonlightAPI api)
        {
            Client = api.CreateLocalClient();

            Bot = new Bot(Client);

            StartCommand = new RelayCommand(Bot.Start);
            StopCommand = new RelayCommand(Bot.Stop);
        }

        public Bot Bot { get; }

        public Client Client { get; }

        public RelayCommand StartCommand { get; }
        public RelayCommand StopCommand { get; }
    }
}
