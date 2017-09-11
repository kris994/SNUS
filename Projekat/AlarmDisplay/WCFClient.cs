using CommonLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AlarmDisplay
{
    class WCFClient
    {
        static void Main(string[] args)
        {
            Thread.Sleep(1000);

            Uri address2 = new Uri("net.tcp://localhost:4001/IAlarmDisplay");
            NetTcpBinding binding2 = new NetTcpBinding();

            ChannelFactory<IAlarmDisplay> factory = new ChannelFactory<IAlarmDisplay>(binding2, new EndpointAddress(address2));

            IAlarmDisplay proxy = factory.CreateChannel();

            Thread thread = new Thread(new ParameterizedThreadStart(print));
            thread.Start(proxy);

            Console.ReadKey();

        }

        public static void print(Object o)
        {
            IAlarmDisplay proxy = (IAlarmDisplay)o;
            while (true)
            {
                Console.WriteLine("=============");
                string l = proxy.printAlarms();
                Console.WriteLine(l);
                Thread.Sleep(2000);
            }
        }
    }
}
