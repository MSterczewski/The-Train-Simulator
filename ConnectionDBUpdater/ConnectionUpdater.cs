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
    class ConnectionUpdater
    {
        public static int Main()
        {
            string path = "../../../connectionDB.txt";
            ContinueWorking(path);
            return 0;
        }
        private static int PrintMenu()
        //Function that prints a menu on the console
        //Prompts the user to select an option
        {
            Console.WriteLine("Options: ");
            Console.WriteLine("\t1.Clear the Database");
            Console.WriteLine("\t2.Add a connection to the Database");
            Console.WriteLine("\t3.Quit");
            Console.WriteLine("Please select your option:");
            string s = Console.ReadLine();
            return int.Parse(s);
        }
        private static void AnalyseTheSelectedOption(int option, string path)
        //Function that analyzes the options presented in teh menu
        //Boss: does this maybe overflow the stack??? This reccursive shit
        //Maybe is better to have an infinite loop?
        {
            switch (option)
            {
                case 1:
                    {
                        ClearTheDatabase(path);
                        ContinueWorking(path);

                        return;//ClearTheDB
                    }
                case 2:
                    {
                        WriteFromConsoleToDatabase(path);
                        ContinueWorking(path);
                        //PrintMenu();
                        return;//Add to DB
                    }
                case 3:
                    {
                        return;
                    }
            }
        }
        private static void ClearTheDatabase(string path)
            //Function that clears the DB file presented by path
            //Boss: maybe there is a more optiomal option?
        {
            FileStream stream = File.Open(path, FileMode.Create);
            stream.Close();
        }
        private static void WriteFromConsoleToDatabase(string path)
            //Function that writes a connection from console to the file presented by path
            //Note for later: check errors
        {
            Connection connection = Connection.ReadConnectionFromConsole();
            Connection.WriteConnectionToFile(path, connection);
        }
        private static void ContinueWorking(string path)
            //Function that is responsible for a single menu display and analyze of it
        {
            Console.Clear();
            int option = PrintMenu();
            AnalyseTheSelectedOption(option, path);
        }
    }
}
