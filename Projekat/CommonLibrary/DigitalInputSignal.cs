using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    [DataContract]
    public class DigitalInputSignal : InputSignal
    {

        [DataMember]
        private DigitalAlarm alarm;
        public DigitalAlarm Alarm
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

        public DigitalInputSignal() { }

        public DigitalInputSignal(string tagName, string description, string ioAddress, SimulationDriver driver ,int scanTime, DigitalAlarm alarm, bool onOffScan, bool autoManual)
            : base(tagName, description, ioAddress, driver, scanTime, onOffScan, autoManual)
        {
            this.Alarm = alarm;
        }

    }
}
