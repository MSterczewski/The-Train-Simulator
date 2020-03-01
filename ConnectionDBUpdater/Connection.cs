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
        public int[] connectionValues;
        public Connection(int _connectionID, int[] _stations, int[] _connectionValues)
        {
            connectionID = _connectionID;
            stations = new int[_stations.Length];
            connectionValues = new int[_connectionValues.Length];
            for (int i = 0; i < _connectionValues.Length; i++)
            {
                stations[i] = _stations[i];
                connectionValues[i] = _connectionValues[i];
            }
            stations[_stations.Length - 1] = _stations[_stations.Length - 1];
        }
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
        public static Connection  ReadConnectionFromConsole(ref int connectionID,ref List<int> listOfConnectionIDs)
            //Function which reads the connection from the console and puts it in the return parameter
            //To do: Error control and suggested ID
        {
            Console.Clear();
            Console.WriteLine("Suggested connection ID: "+connectionID);//to do!!!
            Console.WriteLine("Please write connection ID");
            string line = Console.ReadLine();
            int ID = int.Parse(line);
            if(ID==connectionID)
            {
                //not working!!!!
                connectionID++;
                
            }
            else
            {
                if(listOfConnectionIDs.Contains(ID))
                {
                    //INPUT ERROR
                    return ReadConnectionFromConsole(ref connectionID, ref listOfConnectionIDs);
                }
                else
                {
                    listOfConnectionIDs.Add(ID);
                }
            }
            Console.WriteLine("Please write the connection in the format of:");
            Console.WriteLine("A B C D   (which means the connection A->B->C->D");
            string s = Console.ReadLine();
            List<int> stationsInList = new List<int>();
            List<int> connectionValuesInList = new List<int>();
            foreach (string subs in s.Split(' '))
            {
                int i = int.Parse(subs);
                stationsInList.Add(i);
            }
            Console.WriteLine("Please write the connection costs in the format of:");
            Console.WriteLine("1 3 2   (which means the connection costs A->B = 1, B->C = 3, C->D = 2");
            s = Console.ReadLine();
            foreach (string subs in s.Split(' '))
            {
                int i = int.Parse(subs);
                connectionValuesInList.Add(i);
            }
            return new Connection(ID, stationsInList.ToArray(),connectionValuesInList.ToArray());
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
