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
            //Graph init
            string path = "../../../connectionDB.txt";
            Graphs.Graph g = ConnectionSearcherDatabaseReader.ReadGraphFromDB(path);

            //Input and finding the path
            int from, to;
            ConnectionPrinter.ReadFromTo(out from, out to);
            List<Graphs.Node> stations = Graphs.Algo.DijkstraAlgorithm(from, to, g);

            //Printing
            var p = ConnectionPrinter.AnalyseTheResult(stations);
            ConnectionPrinter.PrintTheConnection(p, from, to);
            return 0;
        }
    }

}
