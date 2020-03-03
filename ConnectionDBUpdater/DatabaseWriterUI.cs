using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionDBUpdater
{
    class DatabaseWriterUI
    {
        public static int Main()
        {
            string path = "../../../connectionDB.txt";
            Graphs.Graph g = Graphs.Graph.FromFile(path);
            Console.WriteLine(":-)");
            Console.WriteLine("Welcome to our train connections simulator - Database input program");
            Console.WriteLine("Press enter to continue...");//HOW TO MAKE IT GO WITH A SINGLE KEY
            Console.ReadLine();
            ContinueWorking(path, g);
            //ContinueWorking(path);
            return 0;
        }
        private static int PrintMenu(bool clearBefore=true,bool clearAfter=true)
        //Function that prints a menu on the console
        //Prompts the user to select an option
        {
            if(clearBefore)Console.Clear();
            Console.WriteLine("Options: ");
            Console.WriteLine("\t1.Print the graph");
            Console.WriteLine("\t2.Add a connection to the Database");
            Console.WriteLine("\t3.Clear the Database");
            Console.WriteLine("\t4.Quit");
            Console.WriteLine("Please select your option:");
            string s = Console.ReadLine();
            if (clearAfter) Console.Clear();
            return int.Parse(s);//THROW AN EXCEPTION IF BAD INPUT
        }

        private static void AnalyseTheSelectedOption(int option, string path, Graphs.Graph g)
        //Function that analyzes the options presented in teh menu
        //FIX
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
                        ContinueWorking(path,g);
                        return;//Add to DB
                    }
                case 3:
                    {
                        //This can be changed to remove a SPECIFIC line
                        //or to MODIFY a line
                        Console.WriteLine("Are you sure? Write \"y\" to confirm");
                        string s = Console.ReadLine();
                        if (s == "y")
                        {
                            Console.WriteLine("Deleting current graph");
                            g = new Graphs.Graph();
                        }
                        else
                            Console.WriteLine("Aborting operation");
                        Console.WriteLine("Press enter to continue");
                        Console.ReadLine();
                        ContinueWorking(path, g);
                        return;
                    }
                case 4:
                    {
                        Graphs.Graph.SaveToFile(path, g);
                        return;
                    }
            }

        }
        private static void ContinueWorking(string path, Graphs.Graph g)
        //Function that is responsible for a single menu display and analyze of it
        {
            int option = PrintMenu(true, true);
            AnalyseTheSelectedOption(option, path,g);
        }
        private static void AddConnectionToGraphFromConsole(string path,Graphs.Graph g)
        {
            //To do
        }
    }
}
