using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    [DataContract]
    [KnownType(typeof(DigitalInputSignal))]
    [KnownType(typeof(AnalogInputSignal))]
    public abstract class InputSignal : Signal
    {
        
        [DataMember]
        private int scanTime;
        public int ScanTime
        {
            get
            {
                return scanTime;
            }

            set
            {
                scanTime = value;
            }
        }

        [DataMember]
        private bool onOffScan;
        public bool OnOffScan
        {
            get
            {
                return onOffScan;
            }

            set
            {
                onOffScan = value;
            }
        }

        [DataMember]
        private bool autoManual;
        public bool AutoManual
        {
            get
            {
                return autoManual;
            }

            set
            {
                autoManual = value;
            }
        }

        public InputSignal() { }

        public InputSignal(string tagName, string description, string IOaddress, SimulationDriver driver ,int scanTime, bool onOffScan = true, bool autoManual = true)
            : base(tagName, description, IOaddress, driver)
        {
            this.ScanTime = scanTime;
            this.OnOffScan = onOffScan;
            this.AutoManual = autoManual;
        }
    }
}
