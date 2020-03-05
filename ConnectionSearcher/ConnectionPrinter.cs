using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionSearcher
{
    public static class ConnectionPrinter
    {
        /// <summary>
        /// This function outputs for the console found path from sID to dID
        /// It also differentiate edge colors
        /// </summary>
        /// <param name="lists"> list of list of stations and the color of which they are connected</param>
        /// <param name="sID">source ID</param>
        /// <param name="dID">destination ID</param>
        public static void PrintTheConnection(List<(List<Graphs.Node>, int)> lists, int sID, int dID)
        {
            Console.WriteLine("Connection from: " + sID + " to " + dID);
            foreach (var v in lists)
            {
                StringBuilder sb = new StringBuilder();
                Console.WriteLine("\tline nr " + v.Item2 + ", you need to travel " + v.Item1.Count + " stops");
                Console.WriteLine("\twhich is from: " + v.Item1.First().Id + " to: " + v.Item1.Last().Id);
                Console.WriteLine("\tdetails of the connection: ");
                sb.Append("\t\t");
                for (int i = 0; i < v.Item1.Count - 1; i++)
                {
                    sb.Append(v.Item1[i].Id);
                    sb.Append("->");
                }
                sb.Append(v.Item1[v.Item1.Count - 1].Id);
                Console.WriteLine(sb);
            }
        }
        public static void ReadFromTo(out int from, out int to)
        {
            Console.WriteLine("Please write from which station do you want to travel:");
            from = int.Parse(Console.ReadLine());
            Console.WriteLine("Please write to which station do you want to travel:");
            to = int.Parse(Console.ReadLine());
        }
        /// <summary>
        /// Splits the stations by the available colors between them
        /// </summary>
        /// <param name="stations"></param>
        /// <returns>read description of PrinTheConnection parameters</returns>
        public static List<(List<Graphs.Node>, int)> AnalyseTheResult(List<Graphs.Node> stations)
        {
            List<(List<Graphs.Node>, int)> lists = new List<(List<Graphs.Node>, int)>();
            int lastConnection = -1;
            Graphs.Edge toSave = new Graphs.Edge(0, 0);
            bool toSaveFlag;
            bool toSaveFlag2;
            for (int i = 0; i < stations.Count - 1; i++)
            {
                toSaveFlag = true;
                toSaveFlag2 = false;
                foreach (var edge in stations[i + 1].OutgoingEdges)
                {
                    if (edge.Destination==stations[i].Id)
                    //found the neighbour
                    {
                        if (edge.Color == lastConnection)
                        //on the same line as the last station
                        {
                            lists.Find(x=>x.Item2 == lastConnection).Item1.Add(stations[i]);
                            toSaveFlag = false;
                            break;
                        }
                        else
                        {
                            toSaveFlag2 = true;
                            toSave = edge;
                        }
                    }
                }
                if (toSaveFlag == true && toSaveFlag2 == true)
                //starting a new line
                {
                    if (lists.Exists(x => x.Item2 == lastConnection)) lists.Find(x => x.Item2 == lastConnection).Item1.Add(stations[i]);//not optimal!!!
                    var item = new ValueTuple<List<Graphs.Node>, int>();
                    item.Item1 = new List<Graphs.Node>();
                    item.Item1.Add(stations[i]);
                    item.Item2 = toSave.Color; 
                    lists.Add(item);
                    lastConnection = toSave.Color;
                }

            }
            lists.Find(x => x.Item2 == lastConnection).Item1.Add(stations.Last());//adding last station
            return lists;
        }
    }
}
