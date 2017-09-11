using CommonLibrary;
using SCADACore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ScadaCore
{
    class StartMain
    {
        private static ServiceHost svc;
        private static WCFServer scada;



        public static void Start()
        {
            //scada.readXML();

            svc = new ServiceHost(scada);
            NetTcpBinding bind1 = new NetTcpBinding();
            bind1.Security.Mode = SecurityMode.None;

            NetTcpBinding bind2 = new NetTcpBinding();
            bind2.Security.Mode = SecurityMode.None;

            svc.AddServiceEndpoint(typeof(IAlarmDisplay), new NetTcpBinding(), new Uri("net.tcp://localhost:4001/IAlarmDisplay"));
            svc.AddServiceEndpoint(typeof(IDatabaseManager), new NetTcpBinding(), new Uri("net.tcp://localhost:4000/IDatabaseManager"));
            svc.AddServiceEndpoint(typeof(ITrending), bind2, new Uri("net.tcp://localhost:4002/ITrending"));
            svc.Open();
            Console.WriteLine("Server is ready and waiting for requests.");

        }
        public static void Stop()
        {

            svc.Close();
        }

        public static void Main(string[] args)
        {

            scada = new WCFServer();
            Start();
            Console.ReadKey();
            Console.WriteLine("The data has been saved");
            //scada.writeXML();
            Stop();
        }
    }
}