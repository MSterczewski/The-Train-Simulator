using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public static class Algo
    {
        /*
         * 
         * Experimental class of experimental functions.
         *           Do not expect quality.
         * 

         */
        private static bool DepthSearchR(Graph G, int Start, int End, List<int> Path, List<int> Visited)
        {
            if (Start == End)
            {
                Path.Add(Start);
                return true;
            }

            Visited.Add(Start);

            foreach (Edge e in G.GetNode(Start).OutgoingEdges)
            {
                int neigId = e.Destination;
                if (Visited.Contains(neigId))
                    continue;

                bool found = DepthSearchR(G, neigId, End, Path, Visited);

                if (found)
                {
                    Path.Add(Start);
                    return true;
                }
            }

            return false;
        }

        public static List<int> DepthSearch(Graph G, int Start, int End)
        {
            var Path = new List<int>();
            var Visited = new List<int>();

            DepthSearchR(G, Start, End, Path, Visited);

            Path.Reverse();
            return Path;
        }
    }
}
