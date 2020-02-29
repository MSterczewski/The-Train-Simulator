using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;


namespace ConnectionSearcher
{


    public static class ConnectionSearcherDatabaseReader
    {
        //Path = "C:\\Users\\Mateus\\Desktop\\Programowanie\\PROJEKTKOLEJE\\connectionDB.txt"
        public static void ReadConnections(string destPath, out List<Station> stations)
        //The function which reads from destPath into stations.
        //The data presented in the file represented in destPath is serialized with binary formatter.
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(destPath, FileMode.Open, FileAccess.Read);
            ConnectionDBUpdater.Connection c;
            stations = new List<Station>();
            while (stream.Position < stream.Length)
            {
                c = (ConnectionDBUpdater.Connection)formatter.Deserialize(stream);//Read a connection
                for (int i = 0; i < c.stations.Length - 1; i++)
                {
                    Station s, d;
                    FindTheStations(out s, out d, c.stations[i], c.stations[i + 1], stations);//transform ID's into Station objects if they are on the list
                    AddTheStationsToList(s, d, stations, c.connectionID, c.stations[i], c.stations[i + 1]);//add objects into list

                }
            }
            stream.Close();
        }

        private static void FindTheStations(out Station s, out Station d, int sID, int dID, List<Station> currentStations)
        {
            int found = 0;
            s = null;
            d = null;
            foreach (Station station in currentStations)
            {
                if (sID == station.stationID)
                {
                    found++;
                    s = station;
                }
                if (dID == station.stationID)
                {
                    d = station;
                    found++;
                }
                if (found == 2) return;
            }
        }
        private static void AddTheStationsToList(Station s, Station d, List<Station> currentStations, int connectionID, int sID, int dID)//int connectionValue
        {
            if (s == null)
            {
                s = new Station(sID);
                currentStations.Add(s);


            }
            if (d == null)
            {

                d = new Station(dID);
                currentStations.Add(d);
                // currentStations.Add(new Station(dID));
                //currentStations.Add(new Station(sID));

            }
            s.neighbours.Add((d, 1, connectionID));
            d.neighbours.Add((s, 1, connectionID));
        }
    }
}
