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
        public static Graphs.Graph ReadGraphFromDB(string path)
        {
            return Graphs.Graph.FromFile(path);
        }



        //OLD


        //public static List<Station> ReadConnections(string destPath)
        ////The function which reads from destPath into stations.
        ////The data presented in the file represented in destPath is serialized with binary formatter.
        //{
        //    IFormatter formatter = new BinaryFormatter();
        //    Stream stream = new FileStream(destPath, FileMode.Open, FileAccess.Read);
        //    ConnectionDBUpdater.Connection c;
        //    List<Station> stations = new List<Station>();
        //    while (stream.Position < stream.Length)
        //    {
        //        c = (ConnectionDBUpdater.Connection)formatter.Deserialize(stream);//Read a connection
        //        for (int i = 0; i < c.stations.Length - 1; i++)
        //        {
        //            Station s, d;
        //            FindTheStations(out s, out d, c.stations[i], c.stations[i + 1], stations);//transform ID's into Station objects if they are on the list
        //            AddTheStationsToList(s, d, stations, c.connectionID, c.stations[i], c.stations[i + 1]);//add objects into list

        //        }
        //    }
        //    stream.Close();
        //    return stations;
        //}

    
    }
}
