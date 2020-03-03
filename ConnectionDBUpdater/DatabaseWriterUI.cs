using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionDBUpdater
{
    static class DatabaseWriterUI
    {
        //To do:    option to delete a line
        //          option to modify a line
        public static int Main()
        {
            string path = "../../../connectionDB.txt";
            Graphs.Graph g = Graphs.Graph.FromFile(path);
            BeginMenu();
            ContinueWorking(path, g);
            return 0;
        }
        private static void BeginMenu()
        {
            //Beginning Menu
            Console.WriteLine(":-)");
            Console.WriteLine("Welcome to our train connections simulator - Database input program");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        private static int PrintMenu(bool clearBefore = true, bool clearAfter = true)
        {
            if (clearBefore) Console.Clear();
            Console.WriteLine("Options: ");
            Console.WriteLine("\t1.Print the graph");
            Console.WriteLine("\t2.Add a connection to the Database");
            Console.WriteLine("\t3.Clear the Database");
            Console.WriteLine("\t4.Quit");
            Console.WriteLine("Please select your option:");
            string s = Console.ReadLine();
            if (clearAfter) Console.Clear();
            try
            {
                int p = int.Parse(s);
                if (p <= 0 || p > 4) throw new InvalidOptionException(p);
            }
            catch(InvalidOptionException)
            {
                Console.WriteLine("Your option was invalid");
                Console.WriteLine("Please select an option from 1 to 4");
                return PrintMenu(false, true);
            }
            catch(FormatException)
            {
                Console.WriteLine("Your option was invalid");
                Console.WriteLine("Please select an option from 1 to 4");
                return PrintMenu(false, true);
            }
            return int.Parse(s);//THROW AN EXCEPTION IF BAD INPUT
        }
        private static void AnalyseTheSelectedOption(int option, string path, Graphs.Graph g)
        {
            switch (option)
            {
                case 1:
                    {
                        Graphs.Graph.PrintGraph(g);
                        Console.WriteLine("Press enter to continue");
                        Console.ReadLine();
                        ContinueWorking(path, g);
                        return;//ClearTheDB
                    }
                case 2:
                    {
                        AddConnectionToGraphFromConsole(path, g);
                        ContinueWorking(path, g);
                        return;
                    }
                case 3:
                    {
                        Console.WriteLine("Are you sure? Write \"y\" to confirm");
                        string s = Console.ReadLine();
                        if (s == "y")
                        {
                            Console.WriteLine("Deleting current graph");
                            g = new Graphs.Graph();
                        }
                        else
                            Console.WriteLine("Aborting operation");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        ContinueWorking(path, g);
                        return;
                    }
                case 4:
                    {
                        g.SaveToFile(path);
                        return;
                    }
            }

        }
        private static void ContinueWorking(string path, Graphs.Graph g)
        {
            int option = PrintMenu(true, true);
            AnalyseTheSelectedOption(option, path, g);
        }
        private static int TellNextLineID(Graphs.Graph g)
        {
            int id = 0;
            while (g.colors.Contains(id) == true) id++;
            return id;
        }
        private static void AddConnectionToGraphID(string path,Graphs.Graph g,out int ID)
        {
            int nextLineID = TellNextLineID(g);
            Console.WriteLine("Suggested connection ID: " + nextLineID);
            Console.WriteLine("Please write connection ID");
            string line = Console.ReadLine();
            try
            {
                ID = int.Parse(line);
            }
            catch(FormatException)
            {
                Console.WriteLine("Input was not an int!");
                Console.WriteLine("Please try again");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                AddConnectionToGraphID(path, g, out ID);
                Console.Clear();
                return;
            }
            if (g.colors.Contains(ID))
            {
                Console.WriteLine("The graph already contains the line with this ID");
                Console.WriteLine("Please try again");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                AddConnectionToGraphID(path, g,out ID);
                return;
            }
        }
        private static void AddConnectionToGraphStations(string path,Graphs.Graph g,out List<int> stations)
        {
            Console.WriteLine("Please write the connection in the format of:");
            Console.WriteLine("A B C D   (which means the connection A->B->C->D");
            string s = Console.ReadLine();
            stations = new List<int>();
            try
            {
                foreach (string subs in s.Split(' '))
                {
                    int i = int.Parse(subs);
                    stations.Add(i);
                }
            }
            catch(FormatException)
            {
                Console.WriteLine("Desired input is \"A B C D\" where A, B, C, D are integers");
                Console.WriteLine("Please try again");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                AddConnectionToGraphStations(path, g, out stations);
                return;
            }
        }
        private static void AddConnectionToGraphCosts(string path, Graphs.Graph g, out List<int> connectionCosts)
        {
            //TO DO: HERE MUST BE A DISPLAY OF SELECTED OPTIONS!!!
            Console.WriteLine("Please write the connection costs in the format of:");
            Console.WriteLine("1 3 2   (which means the connection costs A->B = 1, B->C = 3, C->D = 2");
            string s = Console.ReadLine();
            connectionCosts = new List<int>();
            try
            {
                foreach (string subs in s.Split(' '))
                {
                    int i = int.Parse(subs);
                    connectionCosts.Add(i);
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Desired input is \"A B C \" where A, B, C, D are integers");
                Console.WriteLine("Please try again");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                AddConnectionToGraphStations(path, g, out connectionCosts);
                return;
            }
        }
        private static void AddConnecitonToGraphBad(string path,Graphs.Graph g,int sc,int ec)
        {
            Console.WriteLine("The number of stations and connection values was incorrect.");
            Console.WriteLine("\tstation count: " + sc);
            Console.WriteLine("\tconnection values count: " + ec);
            Console.WriteLine("Desired format: station count - 1 = connection values count");
            Console.WriteLine("Please try again");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            AddConnectionToGraphFromConsole(path, g);
        }
        private static void AddConnectionToGraphFromConsole(string path, Graphs.Graph g)
        {
            //TO DO: can also have some kind of confirmation...
            int ID;
            List<int> stations;
            List<int> edgeValues;
            AddConnectionToGraphID(path, g, out ID);
            AddConnectionToGraphStations(path, g, out stations);
            AddConnectionToGraphCosts(path, g, out edgeValues);
            if(stations.Count-1!=edgeValues.Count)
            {
                AddConnecitonToGraphBad(path, g, stations.Count, edgeValues.Count);
                return;
            }
            AddConnectionToGraph(g, stations, edgeValues, ID);
        }
        private static void AddConnectionToGraph(Graphs.Graph g, List<int> stations, List<int> edgeValues, int connectionID)
        {
            try
            {
                Graphs.Node n = new Graphs.Node(stations[0]);
                n.AddNeighbour(stations[1], connectionID, edgeValues[0]);
                g.AddNode(n);
            }
            catch (Graphs.ExistingStationException)
            {
                g.GetNode(stations[0]).AddNeighbour(stations[1], connectionID, edgeValues[0]);
            }
            for (int i = 1; i < stations.Count - 1; i++)
            {
                try
                {
                    Graphs.Node n = new Graphs.Node(stations[i]);
                    n.AddNeighbour(stations[i + 1], connectionID, edgeValues[i]);
                    n.AddNeighbour(stations[i - 1], connectionID, edgeValues[i - 1]);
                    g.AddNode(n);
                }
                catch (Graphs.ExistingStationException)
                {
                    Graphs.Node n = g.GetNode(stations[i]);
                    n.AddNeighbour(stations[i + 1], connectionID, edgeValues[i]);
                    n.AddNeighbour(stations[i - 1], connectionID, edgeValues[i - 1]);
                }
            }
            int lastIndex = stations.Count - 1;
            try
            {
                Graphs.Node n = new Graphs.Node(stations[lastIndex]);
                n.AddNeighbour(stations[lastIndex - 1], connectionID, edgeValues[lastIndex - 1]);
                g.AddNode(n);
            }
            catch (Graphs.ExistingStationException)
            {
                g.GetNode(stations[lastIndex]).AddNeighbour(stations[lastIndex - 1], connectionID, edgeValues[lastIndex - 1]);
            }
        }
    }
    internal class InvalidOptionException:Exception
    {
        int op;
        internal InvalidOptionException(int o)
        {
            op = o;
        }
    }
}
