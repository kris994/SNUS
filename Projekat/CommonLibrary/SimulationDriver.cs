using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    [DataContract]
    public class SimulationDriver
    {
        [DataMember]
        private double value;

        static double amplitude = 100;

        public double Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public SimulationDriver()
        {
        }

        public double Sine()
        {
            return value = amplitude * Math.Sin((double)DateTime.Now.Second / 60 * Math.PI);
        }

        public double Cosine()
        {
            return value = amplitude * Math.Cos((double)DateTime.Now.Second / 60 * Math.PI);
            
        }

        public double Ramp()
        {
            return value = amplitude * DateTime.Now.Second / 60;
        }

        public double Triangle()
        {
            if (DateTime.Now.Second % 2 == 0)
                return value = amplitude / 2;
            else
                return value = -amplitude / 2;
        }

        public double Rectangle()
        {
            if (DateTime.Now.Minute % 2 == 0)
                return value = (amplitude / Math.PI) * Math.Asin(Math.Sin(Math.PI * ((double)DateTime.Now.Second) / 60));
            else
                return value = (amplitude / Math.PI) * Math.Asin(Math.Sin(Math.PI * ((double)DateTime.Now.Second / 60 + 1)));
        }

        public double Digital()
        {
            return value =  DateTime.Now.Second % 2;
        }

    }
}
