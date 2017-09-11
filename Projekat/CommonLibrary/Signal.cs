using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    [DataContract]
    [KnownType(typeof(InputSignal))]
    [KnownType(typeof(OutputSignal))]
    public abstract class Signal
    {

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

        [DataMember]
        private string description;
        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }

        [DataMember]
        private SimulationDriver driver;
        public SimulationDriver Driver
        {
            get
            {
                return driver;
            }

            set
            {
                driver = value;
            }
        }

        [DataMember]
        private string ioAddress;
        public string IoAddress
        {
            get
            {
                return ioAddress;
            }

            set
            {
                ioAddress = value;
            }
        }

        public Signal() { }

        public Signal(string tagName, string description, string ioAddress, SimulationDriver driver)
        {
            this.tagName = tagName;
            this.description = description;
            this.ioAddress = ioAddress;
            this.driver = new SimulationDriver();
        }

    }
}
