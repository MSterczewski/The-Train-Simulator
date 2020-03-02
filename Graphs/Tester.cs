using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class Tester
    {
        public static void Main(string[] args)
        {
            bool create = false;
            Graph g = null;

            if (create)
            {
                g = new Graph();

                for (int i = 0; i <= 20; ++i)
                    g.AddNode(new Node(i));


                int[] line0 = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
                for (int i = 0; i < line0.Length - 1; ++i)
                    g.AddEdge(new Edge(line0[i], line0[i + 1], Color: 0));

                int[] line1 = { 0, 1, 2, 3, 4, 9, 11, 12, 13 };
                for (int i = 0; i < line1.Length - 1; ++i)
                    g.AddEdge(new Edge(line1[i], line1[i + 1], Color: 1));

                int[] line2 = { 14, 3, 15, 16, 17, 18 };
                for (int i = 0; i < line2.Length - 1; ++i)
                    g.AddEdge(new Edge(line2[i], line2[i + 1], Color: 2));

                int[] line3 = { 0, 3, 5, 8 };
                for (int i = 0; i < line3.Length - 1; ++i)
                    g.AddEdge(new Edge(line3[i], line3[i + 1], Color: 3));

                int[] line4 = { 1, 2, 3, 4, 9, 10, 20, 13, 19, 18 };
                for (int i = 0; i < line4.Length - 1; ++i)
                    g.AddEdge(new Edge(line4[i], line4[i + 1], Color: 4));
            }

            else
                g = Graph.FromFile("graph.txt");

            var v = Algo.DijkstraAlgorithm(8, 19, g);
            foreach(var p in v)
            {
                Console.WriteLine(p.Id);
            }

            //Graph.PrintGraph(g);
            //Graph.PrintGraph( Algo.PrepareGraphForDijkstra(g));
            //Console.WriteLine("Edges: {0}", g.Edges);

            //var p = Algo.DepthSearch(g, 8, 19);
            //foreach (int n in p)
            //    Console.WriteLine(n.ToString());

            //Console.WriteLine("End");

            //if (create)
            //    g.SaveToFile("graph.txt");
        }
    }
}
