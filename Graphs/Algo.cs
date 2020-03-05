using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public static class Algo
    {
        private static Graph PrepareGraphForDijkstra(Graph g)
        {
            Graph g2 = new Graph();
            foreach(var n in g)
            {
                g2.AddNode(new DijkstraNode(n.Value, float.MaxValue));
            }
            return g2;
        }
        /// <summary>
        /// Function which realises Dijkstra algorith.
        /// </summary>
        /// <param name="sID">source ID</param>
        /// <param name="dID">destination ID</param>
        /// <param name="g">Graph</param>
        /// <returns>the path from source to destination</returns>
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
