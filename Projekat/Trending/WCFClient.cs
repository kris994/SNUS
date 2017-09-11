using CommonLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trending
{
   static class WCFClient
    {
       [STAThread]
        static void Main() {

            Thread.Sleep(1000);

            Uri address = new Uri("net.tcp://localhost:4002/ITrending");
            NetTcpBinding b = new NetTcpBinding();

            b.Security.Mode = SecurityMode.None;

            ChannelFactory<ITrending> factory = new ChannelFactory<ITrending>(b, new EndpointAddress(address));
            ITrending proxy = factory.CreateChannel();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(proxy));

        }          
    }
}


