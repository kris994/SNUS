using CommonLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SCADACore
{

    public delegate void AlarmDetected(string alaram);

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class WCFServer : IAlarmDisplay, IDatabaseManager, ITrending
    {

        public static Dictionary<String, Thread> threads = new Dictionary<string, Thread>();
        public static Dictionary<String, Signal> signals = new Dictionary<string, Signal>();

        private ReaderWriterLockSlim lo = new ReaderWriterLockSlim();
        public static event AlarmDetected alarmDetected;

        public WCFServer(){

            Thread t = new Thread(printSignals);
            t.Start();

            alarmDetected += new AlarmDetected(Log);

        }

        public static void OnAlarmStatusChanged(string message)
        {
            if (alarmDetected != null)
                alarmDetected(message);
        }

        private static void printSignals()
        {
            while (true)
            {
                Console.WriteLine("=============");
                foreach (Signal signal in signals.Values)
                {
                    if (signal is InputSignal)
                    {
                        if (((InputSignal)signal).OnOffScan)
                            Console.WriteLine(signal.TagName + " \t " + signal.Description);
                    }
                    else
                    {
                        Console.WriteLine(signal.TagName + " \t " + signal.Description);
                    }
                }
                Console.WriteLine("=============");
                Thread.Sleep(3000);
            }
        }

        private static void Log(string message)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("Alarms.txt", true))
            {
                file.WriteLine(message);
            }
        }

        public void writeXML()
        {
            FileStream writter = new FileStream("data.xml", FileMode.Create);
            DataContractSerializer serializer = new DataContractSerializer(typeof(Dictionary<string, Signal>));
            serializer.WriteObject(writter, signals);
            writter.Close();
        }

        public void readXML()
        {
            if ((File.Exists("data.xml")) && (new FileInfo("data.xml").Length != 0))
            {
                FileStream fs = new FileStream("data.xml", FileMode.Open);
                XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
                DataContractSerializer deserializer = new DataContractSerializer(typeof(Dictionary<string, Signal>));

                signals = (Dictionary<string, Signal>)deserializer.ReadObject(reader, true);

                reader.Close();
                fs.Close();

                foreach (Signal signal in signals.Values)
                {
                    Thread t = new Thread(new ParameterizedThreadStart(Scan));
                    lock (threads)
                    {
                        threads.Add(signal.TagName, t);
                    }
                    t.Start(signal);
                }
            }

        }

        public static void Scan(object tag)
        {

            if (tag is DigitalInputSignal)
            {
                DigitalInputSignal x;
                x = (DigitalInputSignal)tag;
                while (true)
                {
                    if (x.AutoManual == true)
                    {
                        
                       x.Driver.Digital();
                    }

                    OnAlarmStatusChanged("The state of the digital signal has been changed");
                    Thread.Sleep(x.ScanTime);
                }
            }

            if (tag is AnalogInputSignal)
            {
                AnalogInputSignal x;
                x = (AnalogInputSignal)tag;
                while (true)
                {
                    if (x.AutoManual == true)
                    {
                        switch (x.IoAddress)
                        {
                            case "sine":
                                x.Driver.Sine();
                                break;
                            case "cosine":
                                x.Driver.Cosine();
                                break;
                            case "ramp":
                                x.Driver.Ramp();
                                break;
                            case "triangle":
                                x.Driver.Triangle();
                                break;
                            case "rectangle":
                                x.Driver.Rectangle();
                                break;
                        }
                    }

                    if (x.Alarm != null) {
                        string s = x.Alarm.Check(x.Driver.Value);
                        if (s != null && x.OnOffScan == true)
                        {
                            OnAlarmStatusChanged(s);
                        }
                    }

                    Thread.Sleep(x.ScanTime);
                }
            }

        }

        public void addSignal(Signal signal)
        {
            lock (signals)
                {
                    if (signals.ContainsKey(signal.TagName))
                    {
                        Console.WriteLine("The signal with ID: " + signal.TagName + " already exists.");
                    }
                    else
                    {
                        if (signal is InputSignal)
                        {

                        }

                    }
                    Console.WriteLine("Signal with ID: " + signal.TagName + " has been added.");
                    signals.Add(signal.TagName, signal);

                }

            Thread t = new Thread(new ParameterizedThreadStart(Scan));
            lock (threads)
            {
                threads.Add(signal.TagName, t);
            }
            t.Start(signal);
        }

        public bool removeSignal(string signalName)
        {
            if (signals.ContainsKey(signalName))
            {
                threads[signalName].Abort();
                lock (threads)
                {
                    threads.Remove(signalName);
                }
                lock (signals)
                {
                    signals.Remove(signalName);
                }
                return true;
            }
            return false;
        }

        public Dictionary<String, Signal> getSignals()
        {
            return signals;
        }

        public void setInitialValue(string signalName, double value)
        {
            lock (signals)
            {
                if (signals.ContainsKey(signalName))
                {
                    ((OutputSignal)signals[signalName]).InitialValue = value;
                }
            }
        }

        public void setManualValue(string signalName, double value)
        {
            lock (signals)
            {

                foreach (Signal signal in signals.Values)
                {
                    if (signal.TagName == signalName)
                    {
                        ((Signal)signals[signalName]).Driver.Value = value;
                    }
                }
            }
        }

        public void toggleOnOffScan(string signalName)
        {
            lock (signals)
            {

                if (signals.ContainsKey(signalName))
                {

                    if (((InputSignal)signals[signalName]).OnOffScan == false)
                    {
                        ((InputSignal)signals[signalName]).OnOffScan = true;
                    }
                    else { 
                        ((InputSignal)signals[signalName]).OnOffScan = false;
                    }
                }
            }
        }

        public void toggleTagAutoManual(string signalName)
        {
            lock (signals)
            {

                if (signals.ContainsKey(signalName))
                {

                    if (((InputSignal)signals[signalName]).AutoManual == false)
                    {
                        ((InputSignal)signals[signalName]).AutoManual = true;
                    }
                    else { 
                        ((InputSignal)signals[signalName]).AutoManual = false;
                    }
                }
            }
        }

        public Dictionary<string, Signal> showTrending()
        {
            return signals;
        }

        public string printAlarms()
        {
            string x = "";
            foreach (Signal signal in signals.Values)
            {
                if (signal is AnalogInputSignal && threads.ContainsKey(signal.TagName))
                {
                    AnalogAlarm v = ((AnalogInputSignal)signal).Alarm;
                    if (v != null)
                    {
                        x += DateTime.Now + "\t Tag Signal ID: " + v.TagName + "\t Priority: " + v.Priority + "\t Status: " + v.AlarmTypeAnalog + "\t Value: " + signal.Driver.Value + "\n";
                    }
                }
                else if (signal is DigitalInputSignal && threads.ContainsKey(signal.TagName))
                {
                    DigitalAlarm v = ((DigitalInputSignal)signal).Alarm;
                    if (v != null)
                    {
                        x += DateTime.Now + "\t Tag Signal ID: " + v.TagName + "\t Priority: " + v.Priority + "\t Type: " + v.Type + "\n";
                    }
                }
            }
            return x;
        }

        public void removeAlarm(string signalName)
        {
            lock (signals)
            {

                foreach (Signal signal in signals.Values)
                {
                    if (signal is InputSignal)
                    {
                        if (signal.TagName == signalName)
                        {
                            if (signal is DigitalInputSignal)
                            {
                                ((DigitalInputSignal)signals[signalName]).Alarm = null;
                            }
                            else
                            {
                                ((AnalogInputSignal)signals[signalName]).Alarm = null;
                            }
                        }
                    }
                }
            }
        }
    }
}
