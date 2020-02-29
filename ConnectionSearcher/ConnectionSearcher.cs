using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionSearcher
{
    public class ConnectionSearcherPrinter
    //A class for printing the result of Dijkstra Algorithm
    //Prints the shortest path from source to destination
    {
        public static void PrintTheConnection(List<(List<Station>, int)> lists, int sID, int dID)
        {
            Console.WriteLine("Connection from: " + sID + " to " + dID);
            foreach (var v in lists)
            {
                StringBuilder sb = new StringBuilder();
                Console.WriteLine("\tline nr " + v.Item2 + ", you need to travel " + v.Item1.Count + " stops");
                Console.WriteLine("\twhich is from: " + v.Item1.First().stationID + " to: " + v.Item1.Last().stationID);
                Console.WriteLine("\tdetails of the connection: ");
                sb.Append("\t\t");
                for (int i = 0; i < v.Item1.Count - 1; i++)
                {
                    sb.Append(v.Item1[i].stationID);
                    sb.Append("->");
                }
                sb.Append(v.Item1[v.Item1.Count - 1].stationID);
                Console.WriteLine(sb);
            }
        }
    }
}
