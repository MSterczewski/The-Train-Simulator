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
    }
}
