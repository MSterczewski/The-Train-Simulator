using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    class ExistingStationException:Exception
    {
        int stationID;
        public ExistingStationException(int ID)
        {
            stationID = ID;
        }
    }
    class NodeNotInGraphException:Exception
    {
        int stationID;
        public NodeNotInGraphException(int ID)
        {
            stationID = ID;
        }
    }
    class EdgeNotViableException:Exception
    {
        Edge edge;
        public EdgeNotViableException(Edge e)
        {
            edge = e;
        }
    }
}
