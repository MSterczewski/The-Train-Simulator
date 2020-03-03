using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class ExistingStationException:Exception
    {
        int stationID;
        public ExistingStationException(int ID)
        {
            stationID = ID;
        }
    }
    public class NodeNotInGraphException:Exception
    {
        int stationID;
        public NodeNotInGraphException(int ID)
        {
            stationID = ID;
        }
    }
    public class EdgeNotViableException:Exception
    {
        Edge edge;
        public EdgeNotViableException(Edge e)
        {
            edge = e;
        }
    }
}
