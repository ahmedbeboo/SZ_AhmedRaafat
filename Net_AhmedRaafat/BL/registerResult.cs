using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net_AhmedRaafat.BL
{
    public class registerResult
    {
        public Guid id { get; set; }
        public bool success { get; set; }
        public string token { get; set; }
    }
}
