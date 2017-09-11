using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    [ServiceContract]
    public interface ITrending
    {
        [OperationContract]
        Dictionary<string, Signal> showTrending();
    }
}
