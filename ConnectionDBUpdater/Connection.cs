using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

namespace ConnectionDBUpdater
{
    [Serializable]
    public class Connection
    {
        public int connectionID;//stores the ID of connection
        public int[] stations;//stores stationsIDs
        public Connection(int _connectionID, int[] _stations)
        {
            connectionID = _connectionID;
            stations = new int[_stations.Length];
            int it = 0;
            foreach (int i in _stations)
            {
                stations[it] = _stations[it];
                it++;
            }
        }
            //this as well
        public static void ReadConnectionsFromFile(string destPath, out List<Connection> connectionList)
            //Function which will read all the connections from the destPath and put it into the conneciton
            //Function uses serialization by binary formatter
            //So far, it requires for the destPath to be a viable one
        {
            IFormatter formatter = new BinaryFormatter();
            Connection c;
            connectionList = new List<Connection>();
            Stream stream = new FileStream(destPath, FileMode.Open, FileAccess.Read);//Update to catch exceptions
            while (stream.Position < stream.Length)
            {
                c = (Connection)formatter.Deserialize(stream);
                connectionList.Add(c);
            }
            stream.Close();
        }
        
        
        //Change it from out parameter into return
        public static void WriteConnectionToFile(string destPath, Connection connection)
            //Function that writes a Conneciton into the file represented by destPath
            //Function uses serialization by binary formatter
            //Check if there are no errors!!
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(destPath, FileMode.Append, FileAccess.Write);
            formatter.Serialize(stream, connection);
            stream.Close();

        }
        public static Connection  ReadConnectionFromConsole()
            //Function which reads the connection from the console and puts it in the out parameter
        {
            Console.WriteLine("Please write connection ID");
            string line = Console.ReadLine();
            int ID = int.Parse(line);
            Console.WriteLine("Please write the connection in the format of:");
            Console.WriteLine("A B C D   (which means the connection A->B->C->D");
            string s = Console.ReadLine();
            List<int> stationsInList = new List<int>();
            foreach (string subs in s.Split(' '))
            {
                int i = int.Parse(subs);
                stationsInList.Add(i);
            }
            return new Connection(ID, stationsInList.ToArray());
        }
        public static void PrintTheConnectionToConsole(Connection connection)
            //Funtion that prints a connection on the console
        {
            StringBuilder s = new StringBuilder();
            s.Append("Connection nr. " + connection.connectionID + ": ");
            for (int i = 0; i < connection.stations.Length - 1; i++)
            {
                s.Append(connection.stations[i]);
                s.Append("->");
            }
            s.Append(connection.stations[connection.stations.Length - 1]);
            Console.WriteLine(s);
        }
    }
}
