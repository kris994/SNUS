using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    [DataContract]
    public class AnalogOutputSignal : OutputSignal
    {

        [DataMember]
        public double lowLimit { get; set; }

        [DataMember]
        public double highLimit { get; set; }

        [DataMember]
        public string units { get; set; }

        public AnalogOutputSignal() { }

        public AnalogOutputSignal(string tagName, string description, string ioAdress, SimulationDriver driver, double initialValue, double lowLimit, double highLimit, string units)
            : base(tagName, description, ioAdress, driver, initialValue)
        {
            this.lowLimit = lowLimit;
            this.highLimit = highLimit;
            this.units = units;
        }


    }
}
