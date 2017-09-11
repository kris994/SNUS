using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    [DataContract]
    [KnownType(typeof(DigitalOutputSignal))]
    [KnownType(typeof(AnalogOutputSignal))]
    public abstract class OutputSignal : Signal
    {

        [DataMember]
        private double initialValue;
        public double InitialValue
        {
            get
            {
                return initialValue;
            }

            set
            {
                initialValue = value;
            }
        }

        public OutputSignal() { }

        public OutputSignal(string tagName, string description, string ioAddress, SimulationDriver driver,double initialValue)
            : base(tagName, description, ioAddress, driver)
        {
            this.InitialValue = initialValue;
        }

    }
}
