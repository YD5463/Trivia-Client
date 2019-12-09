using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    class GetGameResultsResponse
    {
        public uint status;
        public Dictionary<string, List<uint>> results;
    }
}
