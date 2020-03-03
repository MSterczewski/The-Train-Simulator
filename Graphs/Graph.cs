using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Graphs
{
    /*
     * Represents a directed graph
     * 
     * Contains:
     *  - a list of Nodes (see: Node.cs) implemented as a dictionary, 
     *    to allow comfortable operation with Node ID's
     *  
     * Implements:
     *  - adding unconnected or already connected nodes to itself 
     *  - adding edges between existing nodes
     *  - getting a node by it's ID
     *  - serializing itself into a string/file
     *  - creation of Graph objects from string/file
     *  
     */

    [Serializable]
    public class Graph
    {
        Dictionary<int, Node> Nodes;

        public int Vertices { get => Nodes.Count; }

        // Directional edges, when working with effectively
        // non-directional graphs - divide by two
        public int Edges { get; private set; } = 0;


        public Graph()
        {
            this.Nodes = new Dictionary<int, Node>();
        }


        public Graph(IEnumerable<Node> InitialNodes)
        {
            this.Nodes = new Dictionary<int, Node>();

            foreach (Node N in InitialNodes)
            {
                this.Nodes[N.Id] = N;
                this.Edges += N.Degree;
            }
        }

        /*
         * Serialization procedure:
         *  - BinarySerialize itself
         *  - compress the binary as good as possible
         *  - encode the compressed data as a Base64 string
         *  
         * Deserialization: properly reverse the above
         * 
         * Implemented with streams on top of streams.
         * 
         */


            /*
             * So boss, this part idk what it's for
             * It hasn't been working :/
             * And idk if we need a graph represented in strings
             * */
        public static Graph FromString(string Representation)
        {
            byte[] raw = Convert.FromBase64String(Representation);

            var mems = new MemoryStream(raw);
            var defs = new DeflateStream(mems, CompressionMode.Decompress);

            BinaryFormatter bf = new BinaryFormatter();
            Graph g = (Graph)bf.Deserialize(defs);

            defs.Close();
            mems.Close();
            return g;
        }


        public static Graph FromFileOld(string Path)
        {
            var file = new StreamReader(Path);
            string s64 = file.ReadToEnd();

            file.Close();
            return Graph.FromString(s64);
        }


        public override string ToString()
        {
            var mems = new MemoryStream();
            var defs = new DeflateStream(mems, CompressionLevel.Optimal);

            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(defs, this);

            byte[] bytes = mems.ToArray();
            return Convert.ToBase64String(bytes);
        }


        public void SaveToFileOld(string Path)
        {
            var file = File.Open(Path, FileMode.Create);

            byte[] data = System.Text.Encoding.ASCII.GetBytes(this.ToString());

            file.Write(data, 0, data.Length);
            file.Close();
        }

        /*
         * My suggestion for fix here
         */
        public static void SaveToFile(string path,Graph g)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, g);
            stream.Close();
        }

        public static Graph FromFile(string path)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            Graph g = (Graph)formatter.Deserialize(stream);
            stream.Close();
            return g;
        }

        /*
         * Mindset: Don't check for possible exceptions only to throw them manually
         *          when an invalid operation will throw the exception for you.
         *          
         */
        public bool ContainsNode(Node N)
        {
            return Nodes.ContainsKey(N.Id);
        }
        public bool ContainsNode(int n)
        {
            return Nodes.ContainsKey(n);
        }
        private bool isEdgeViable(Edge e)
        {
            int found = 0;
            foreach(var n in Nodes)
            {
                if (n.Value.Id == e.Source) found++;
                if (n.Value.Id == e.Destination) found++;
                if (found == 2) return true;
            }
            return false;
        }


        public void AddNode(Node N)
        {
            if (ContainsNode(N))
                throw new ExistingStationException(N.Id);

            this.Nodes[N.Id] = N;
        }


        public Node GetNode(int NodeId)
        {
            if (ContainsNode(NodeId) == true)
            {
                return this.Nodes[NodeId];
            }
            else throw new NodeNotInGraphException(NodeId);
        }


        public void AddEdge(Edge E, bool Bidirectional = true)
        {
            if (isEdgeViable(E) == false) throw new EdgeNotViableException(E);
            this.Nodes[E.Source].AddNeighbour(E.Destination, E.Color, E.Weight);
            this.Edges++;

            if (Bidirectional)
            {
                this.Nodes[E.Destination].AddNeighbour(E.Source, E.Color, E.Weight);
                this.Edges++;
            }
        }


        public IEnumerator<KeyValuePair<int, Node>> GetEnumerator()
            => this.Nodes.GetEnumerator();

        public static void PrintGraph(Graph g)
        {
            StringBuilder s = new StringBuilder();
            foreach(var v in g.Nodes)
            {
                s.Clear();
                s.Append("Node ");
                s.Append(v.Key);
                s.Append(" with neighbours: ");
                foreach(Edge e in v.Value.OutgoingEdges)
                {
                    s.Append(e.Destination);
                    s.Append("(");
                    s.Append(e.Color);
                    s.Append(") ");
                }
                Console.WriteLine(s);
            }
        }
    }
}
