using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Moonlight.Clients;
using Moonlight.Game.Battle;
using Moonlight.Game.Entities;

namespace Moonlight.SimplePiiBot.Models
{
    public class Bot
    {
        private Task _runningTask;

        public Bot(Client client)
        {
            Client = client;
        }

        public bool IsRunning { get; private set; }

        public Client Client { get; }

        public void Start()
        {
            if (IsRunning)
            {
                return;
            }

            IsRunning = true;

            _runningTask = Task.Run(async () =>
            {
                Character character = Client.Character;

                while (IsRunning)
                {
                    IEnumerable<Monster> allPii;

                    do
                    {
                        Monster pod;
                        do
                        {
                            pod = await GetClosest(MonsterConstants.SoftPiiPodVnum);
                            if (pod == null)
                            {
                                await Task.Delay(100);
                            }
                        }
                        while (pod == null);

                        await character.Attack(pod);

                        allPii = await GetClose(MonsterConstants.SoftPiiVnum);

                        await Task.Delay(100);
                    }
                    while (allPii.Count() < 10);

                    await character.Attack(allPii.First());
                }
            });
        }

        public void Stop()
        {
            IsRunning = false;
        }

        private async Task<Monster> GetClosest(int vnum)
        {
            return (await GetClose(vnum)).FirstOrDefault();
        }

        private async Task<IEnumerable<Monster>> GetClose(int vnum)
        {
            return Client.Character.Map.Monsters
                    .Where(x => x.Vnum == vnum)
                    .Where(x => x.Position.IsInRange(Client.Character.Position, 10))
                    .OrderBy(x => x.Position.GetDistance(Client.Character.Position));
        }
    }
}
