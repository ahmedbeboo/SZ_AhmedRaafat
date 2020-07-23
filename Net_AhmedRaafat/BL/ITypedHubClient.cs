using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net_AhmedRaafat.BL
{
    public interface ITypedHubClient
    {
        Task BroadcastMessage(string type, string payload);
    }

}
