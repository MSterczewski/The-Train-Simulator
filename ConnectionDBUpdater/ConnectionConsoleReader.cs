using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionDBUpdater
{
    static class ConnectionConsoleReader
    {
        private static int nextID = 1;
        private static List<int> usedIDs = new List<int>();
        public static Connection ReadConnectionFromConsole()
        {

            return Connection.ReadConnectionFromConsole(ref nextID,ref usedIDs);
        }
    }
}
