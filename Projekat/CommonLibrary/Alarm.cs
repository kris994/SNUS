using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    public enum Priority
    {
        Low,
        Medium,
        High
    }

    [DataContract]
    [KnownType(typeof(DigitalAlarm))]
    [KnownType(typeof(AnalogAlarm))]
    public abstract class Alarm
    {

        public delegate void AlarmHandler(string message);
        public event AlarmHandler AlarmTriggered;

        [DataMember]
        private Priority priority;
        public Priority Priority
        {
            get
            {
                return priority;
            }

            set
            {
                priority = value;
            }
        }

        [DataMember]
        private DateTime alarmTime;
        public DateTime AlarmTime
        {
            get
            {
                return alarmTime;
            }

            set
            {
                alarmTime = value;
            }
        }

        [DataMember]
        private string tagName;
        public string TagName
        {
            get
            {
                return tagName;
            }

            set
            {
                tagName = value;
            }
        }

        public Alarm()
        {
        }

        public Alarm(string tagName, Priority priority)
        {
            this.TagName = tagName;
            this.Priority = priority;
        }

    }
}