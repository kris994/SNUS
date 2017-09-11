using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    [ServiceContract]
    public interface IDatabaseManager
    {
        [OperationContract]
        void addSignal(Signal signal);

        [OperationContract]
        bool removeSignal(string signalName);

        [OperationContract]
        void removeAlarm(string signalName);

        [OperationContract]
        void toggleOnOffScan(string signalName);

        [OperationContract]
        void toggleTagAutoManual(string signalName);

        [OperationContract]
        void setInitialValue(string signalName, double value);

        [OperationContract]
        void setManualValue(string signalName, double value);

        [OperationContract]
        Dictionary<String, Signal> getSignals();

    }
}
