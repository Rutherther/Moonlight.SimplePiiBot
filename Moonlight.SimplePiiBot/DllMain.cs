using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Moonlight.SimplePiiBot
{
    public class DllMain
    {
        [DllExport]
        public static void Main()
        {
            Thread thread = new Thread(() =>
            {
                App app = new App();

                app.InitializeComponent();
                app.Run();
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
    }
}
