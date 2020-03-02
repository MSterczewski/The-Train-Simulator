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

        //I will complete this in the evening

        //public static List<Node> DijkstraAlgorithm(int sID, int dID, Graph g)
        //{

        //}
        //public static List<Node> DijkstraReccursive(Node source,Node dest, List<Node> currentPath,List<Node> finalPath)
        //{
        //    if(source==dest)
        //    {
        //        //ending point
        //        finalPath.Clear();
        //        foreach (var r in currentPath)
        //            finalPath.Add(r);
        //        return finalPath;
        //    }
        //    foreach(var r in source.OutgoingEdges)
        //    {
        //        int alt = source.
        //    }
        //}

        //    private static List<Station> DijkstraAlgorithmReccursive(Station source, Station dest, List<Station> currentPath, List<Station> finalPath)
        //{
        //    if (source == dest)
        //    {
        //        //ending point
        //        finalPath.Clear();
        //        foreach (var r in currentPath)
        //            finalPath.Add(r);

        //        return finalPath;
        //    }
        //    foreach (var v in source.neighbours)
        //    {
        //        int alt = source.value + v.Item2;
        //        if (alt < v.Item1.value)
        //        {
        //            v.Item1.value = alt;

        //            currentPath.Add(v.Item1);
        //            finalPath = DijkstraAlgorithmReccursive(v.Item1, dest, currentPath, finalPath);
        //            currentPath.RemoveAt(currentPath.Count - 1);
        //        }
        //    }
        //    return finalPath;
        //}

    }
}
