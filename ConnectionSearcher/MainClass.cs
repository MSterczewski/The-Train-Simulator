using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionSearcher
{
    class MainClass
    {
        public static int Main()
        {
            List<Station> stations = ConnectionSearcherDatabaseReader.ReadConnections("../../../connectionDB.txt");
            //ConnectionSearcherDatabaseReader.ReadConnections("../../../connectionDB.txt", out stations);

            List<Station> final;
            final = Dijkstra.DijkstraAlgorithm(1, 6, stations);

            var p = Dijkstra.AnalyseTheResult(final);
            ConnectionSearcherPrinter.PrintTheConnection(p, 1, 6);
            return 0;
        }


    }

}
