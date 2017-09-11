using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    [DataContract]
    public class DigitalOutputSignal : OutputSignal
    {

        public DigitalOutputSignal() { }

        public DigitalOutputSignal(string tagName, string description, string ioAddress, SimulationDriver driver, double initialValue)
            : base(tagName, description, ioAddress, driver, initialValue) { }


    }
}
