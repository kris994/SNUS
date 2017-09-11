using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    public enum AlarmTypeAnalog
    {
        Ok,
        Low,
        High,
        Low_Low,
        High_High
    }


    [DataContract]
    public class AnalogAlarm : Alarm
    {
                       
        [DataMember]
        private double low;
        public double Low
        {
            get
            {
                return low;
            }

            set
            {
                low = value;
            }
        }

        [DataMember]
        private double high;
        public double High
        {
            get
            {
                return high;
            }

            set
            {
                high = value;
            }
        }

        [DataMember]
        private double low_low;
        public double Low_Low
        {
            get
            {
                return low_low;
            }

            set
            {
                low_low = value;
            }
        }

        [DataMember]
        private double high_high;
        public double High_High
        {
            get
            {
                return high_high;
            }

            set
            {
                high_high = value;
            }
        }

        [DataMember]
        private AlarmTypeAnalog alarmTypeAnalog;
        public AlarmTypeAnalog AlarmTypeAnalog
        {
            get
            {
                return alarmTypeAnalog;
            }

            set
            {
                alarmTypeAnalog = value;
            }
        }

        public AnalogAlarm()
        {
        }

        public AnalogAlarm(string tagName, Priority priority, double low, double high, double lowLow, double highHigh) : base(tagName, priority)
        {
            this.Low = low;
            this.High = high;
            this.Low_Low = lowLow;
            this.High_High = highHigh;
            this.AlarmTypeAnalog = AlarmTypeAnalog.Ok;
        }

        public string Check(double x)
        {

            DateTime vreme = DateTime.Now;
            if (x < Low_Low)
            {
                this.AlarmTypeAnalog = AlarmTypeAnalog.Low_Low;
                return vreme + "\t Tag: " + TagName + "\t Status: Low_Low" + "\tValue " + x;
            }
            else if (x < Low)
            {
                this.AlarmTypeAnalog = AlarmTypeAnalog.Low;
                return vreme + "\t Tag: " + TagName + "\t Status: Low" + "\tValue " + x;
            }
            else if (x > High_High)
            {
                this.AlarmTypeAnalog = AlarmTypeAnalog.High_High;
                return vreme + "\t Tag: " + TagName + "\t Status: High_High" + "\tValue " + x;
            }
            else if (x > High)
            {
                this.AlarmTypeAnalog = AlarmTypeAnalog.High;
                return vreme + "\t Tag: " + TagName + "\t Status: High" + "\tValue " + x;
            }

            return null;

        }

    }

}
