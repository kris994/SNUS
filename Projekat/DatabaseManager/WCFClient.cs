using CommonLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DatabaseManager
{
    class WCFClient
    {

        static void Main(string[] args)
        {
            Thread.Sleep(1000);

            Uri address2 = new Uri("net.tcp://localhost:4000/IDatabaseManager");
            NetTcpBinding binding2 = new NetTcpBinding();

            ChannelFactory<IDatabaseManager> factory = new ChannelFactory<IDatabaseManager>(binding2, new EndpointAddress(address2));

            IDatabaseManager proxy = factory.CreateChannel();

            //First Signal DIS

            /*Console.WriteLine("First Signal");
            DigitalInputSignal dis = new DigitalInputSignal("Pumpa1","Bazen1","digital", new SimulationDriver(), 500, new DigitalAlarm("Pumpa1", Priority.Medium, AlarmType.ChangeOfState), true, true);
            proxy.addSignal(dis);

            Thread.Sleep(5000);
            Console.WriteLine("On/Off Scan");
            proxy.toggleOnOffScan("Pumpa1");

            Thread.Sleep(5000);
            Console.WriteLine("On/Off Scan");
            proxy.toggleOnOffScan("Pumpa1");

            Thread.Sleep(5000);
            Console.WriteLine("Auto/Manual");
            proxy.toggleTagAutoManual("Pumpa1");

            Thread.Sleep(1000);
            Console.WriteLine("Manual value");
            proxy.setManualValue("Pumpa1",20);

            Thread.Sleep(5000);
            Console.WriteLine("Deleting Signal");
            proxy.removeSignal("Pumpa1");

            Thread.Sleep(5000);
            Console.WriteLine("Deleting Alarm");
            proxy.removeAlarm("Pumpa1");*/



            // 2nd Signal DOS

            /*Console.WriteLine("Secound Signal");
            DigitalOutputSignal dos = new DigitalOutputSignal("Ventil1", "Bazen1", "sine", new SimulationDriver(), 0);
            proxy.addSignal(dos);*/


            // 3rd Signal AIS

            Console.WriteLine("Third Signal");
            AnalogInputSignal ais = new AnalogInputSignal("Analog1", "Opis1", "sine", new SimulationDriver(), 500, 0, 100, "volts", new AnalogAlarm("Test", Priority.Medium, 10, 20, 80, 90), true, true);
            //AnalogInputSignal ais = new AnalogInputSignal("Analog", "Opis1", "cosine", new SimulationDriver(), 500, 0, 100, "volts", new AnalogAlarm("Test", Priority.Medium, 10, 20, 80, 90), true, true);
            //AnalogInputSignal ais = new AnalogInputSignal("Analog", "Opis1", "ramp", new SimulationDriver(), 500, 0, 100, "volts", new AnalogAlarm("Test", Priority.Medium, 10, 20, 80, 90), true, true);
            //AnalogInputSignal ais = new AnalogInputSignal("Analog", "Opis1", "triangle", new SimulationDriver(), 500, 10, 80, "volts", new AnalogAlarm("Test", Priority.Medium, 10, 20, 80, 90), true, true);
            //AnalogInputSignal ais2 = new AnalogInputSignal("Analog2", "Opis1", "rectangle", new SimulationDriver(), 500, 20, 90, "volts", new AnalogAlarm("Test", Priority.Medium, 10, 20, 80, 90), true, true);
            proxy.addSignal(ais);
            //Thread.Sleep(5000);
            //proxy.addSignal(ais1);
            //Thread.Sleep(5000);
            //proxy.addSignal(ais2);

            /*Thread.Sleep(5000);
            Console.WriteLine("Auto/Manual");
            proxy.toggleTagAutoManual("Analog1");

            Thread.Sleep(5000);
            Console.WriteLine("Auto/Manual");
            proxy.toggleTagAutoManual("Analog2");

            Thread.Sleep(5000);
            Console.WriteLine("Auto/Manual");
            proxy.toggleTagAutoManual("Analog");*/

            /*Thread.Sleep(5000);
            Console.WriteLine("Deleting Signal");
            proxy.removeAlarm("Analog2");

            Thread.Sleep(5000);
            Console.WriteLine("Deleting Alarm");
            proxy.removeSignal("Analog2");

            Thread.Sleep(5000);
            Console.WriteLine("Deleting Signal");
            proxy.removeAlarm("Analog1");

            Thread.Sleep(5000);
            Console.WriteLine("Deleting Alarm");
            proxy.removeSignal("Analog1");

            Thread.Sleep(5000);
            Console.WriteLine("Deleting Signal");
            proxy.removeAlarm("Analog");

            Thread.Sleep(5000);
            Console.WriteLine("Deleting Alarm");
            proxy.removeSignal("Analog");*/


            // 4th Signal AOS

            /*Console.WriteLine("Fourth Signal");
            AnalogOutputSignal aos = new AnalogOutputSignal("Frekvencija", "Frekvenicja pormene sinusa", "ramp", new SimulationDriver(), 5, 0, 100, "Mhz");
            proxy.addSignal(aos);*/

            Console.Read();
        }

    }
}
