using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{

    public enum AlarmType
    {
        None,
        Close,
        Open,
        ChangeOfState
    }

    [DataContract]
    public class DigitalAlarm : Alarm
    {

        public DigitalAlarm()
        {
        }

        public DigitalAlarm(string tagName, Priority priority, AlarmType type) : base(tagName, priority)
        {
            this.Type = type;
        }


        [DataMember]
        private AlarmType type;
        public AlarmType Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }
    }
}