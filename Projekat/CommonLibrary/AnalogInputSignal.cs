using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    [DataContract]
    public class AnalogInputSignal : InputSignal
    {

        [DataMember]
        private double lowLimit;
        public double LowLimit
        {
            get
            {
                return lowLimit;
            }

            set
            {
                lowLimit = value;
            }
        }

        [DataMember]
        private double highLimit;
        public double HighLimit
        {
            get
            {
                return highLimit;
            }

            set
            {
                highLimit = value;
            }
        }

        [DataMember]
        private string units;
        public string Units
        {
            get
            {
                return units;
            }

            set
            {
                units = value;
            }
        }

        [DataMember]
        private AnalogAlarm alarm;
        public AnalogAlarm Alarm
        {
            get
            {
                return alarm;
            }

            set
            {
                alarm = value;
            }
        }

        public AnalogInputSignal() { }

        public AnalogInputSignal(string tagName, string description, string ioAddress, SimulationDriver driver, int scanTime, double lowLimit, double highLimit, string units, AnalogAlarm alarm, bool onOffScan, bool autoManual)
            : base(tagName, description, ioAddress, driver, scanTime, onOffScan, autoManual)
        {
            this.LowLimit = lowLimit;
            this.HighLimit = highLimit;
            this.Units = units;
            this.Alarm = alarm;
        }

    }
}
