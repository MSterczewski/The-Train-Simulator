using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionSearcher
{
    public class Station
    {
        public int stationID; //the unique ID of the station
        public List<ValueTuple<Station, int, int>> neighbours;   //stores list of neighbours: 
                                                                 //Station - the neighbour station
                                                                 //int - the value of the connection
                                                                 //int - the connection (line) number
        public int value; //for Dijkstra algorithm
        public Station(int sID, ValueTuple<Station, int, int> station)
        {
            stationID = sID;
            neighbours = new List<(Station, int, int)>();
            neighbours.Add(station);
        }
        public Station(int sID)
        {
            stationID = sID;
            neighbours = new List<(Station, int, int)>();
        }
        public void PrintStation()
        {
            StringBuilder s = new StringBuilder();
            Console.WriteLine("Station nr." + stationID + ": ");
            foreach (var n in neighbours)
            {
                s.Append("\t" + "neighbour: ");
                s.Append(n.Item1.stationID);
                s.Append(" with connection: ");
                s.Append(n.Item3);
                s.Append(" cost of connection: ");
                s.Append(n.Item2);
                Console.WriteLine(s);
                s.Clear();
            }
        }
    }
}
