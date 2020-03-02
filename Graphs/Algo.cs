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

        public static Graph PrepareGraphForDijkstra(Graph g)
        {
            //FIX maybe
            Graph g2 = new Graph();
            foreach(var n in g)
            {
                g2.AddNode(new DijkstraNode(n.Value, float.MaxValue));
            }
            return g2;
        }

        public static List<Node> DijkstraAlgorithm(int sID, int dID, Graph g)
        {
            Graph g2 = PrepareGraphForDijkstra(g);
            DijkstraNode source = g2.GetNode(sID) as DijkstraNode;
            DijkstraNode destination = g2.GetNode(dID) as DijkstraNode;
            source.value = 0;
            List<DijkstraNode> finalPath = new List<DijkstraNode>();
            List<DijkstraNode> currentPath = new List<DijkstraNode>();
            currentPath.Add(source);
            finalPath = DijkstraReccursive(source, destination, currentPath, finalPath, g2);
            return DijkstraConvertingLists(finalPath);
        }

        private static List<Node> DijkstraConvertingLists(List<DijkstraNode> list)
        {
            List<Node> newList = new List<Node>();
            foreach(DijkstraNode d in list)
            {
                newList.Add(d as Node);
            }
            return newList;
        }

        //public static List<Station> DijkstraAlgorithm(int sID, int dID, List<Station> stations)
        ////Non-recursive part of the Dijkstra Algorithm.
        ////sID and dID means source/destination station's ID
        ////returns the shortest path between sID and dID
        //{
        //    Dijkstra d = new Dijkstra(stations, sID, dID);
        //    d.begStation.value = 0;
        //    List<Station> finalPath = new List<Station>();
        //    List<Station> currentPath = new List<Station>();
        //    currentPath.Add(d.begStation);
        //    finalPath = DijkstraAlgorithmReccursive(d.begStation, d.destStation, currentPath, finalPath);
        //    return finalPath;
        //}
        private static List<DijkstraNode> DijkstraReccursive(DijkstraNode source, DijkstraNode dest, List<DijkstraNode> currentPath, List<DijkstraNode> finalPath,Graph g)
        {
            if (source == dest)
            {
                //ending point
                finalPath.Clear();
                foreach (var r in currentPath)
                    finalPath.Add(r);
                return finalPath;
            }
            foreach (var r in source.OutgoingEdges)
            {
                float alt = source.value + r.Weight;
                DijkstraNode edgeDest = g.GetNode(r.Destination) as DijkstraNode;
                if(alt<edgeDest.value)
                {
                    edgeDest.value = alt;
                    currentPath.Add(edgeDest);
                    finalPath = DijkstraReccursive(edgeDest, dest, currentPath, finalPath, g);
                    currentPath.RemoveAt(currentPath.Count - 1);
                }
            }
            return finalPath;//useless but whatever
                            //maybe could throw exception when not found a path
        }

    }
}
