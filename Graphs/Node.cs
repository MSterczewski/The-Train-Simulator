using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{

    [Serializable]
    public class Node
    {
        public int Id { get; private set; }

        // Ce n'est pas un dictionnaire ~ Rene Magritte, 1929
        private Dictionary<int, Edge> Neighbours;//NOTE: change this to Dict<Node,Edge>
                                                 //NOTE: does this work for multigraphs?


        // Almost a method
        public int Degree
           => this.Neighbours.Count;


        public Node(int Id)
        {
            this.Id = Id;
            this.Neighbours = new Dictionary<int, Edge>();
        }


        public bool IsNeighbour(int NodeId)//NOTE: if change into Dict<Node> this neads to be changed
            => this.Neighbours.ContainsKey(NodeId);


        // Single-directional connection only
        // Asks to just accept an Edge as a single parameter
        // TODO: ^^
        public void AddNeighbour(int NeighbourId, int Color = 0, float Weight = 1.0f)
        {
            var e = new Edge(this.Id, NeighbourId);
            e.Color = Color;
            e.Weight = Weight;

            this.Neighbours[NeighbourId] = e;//NO
            //this.Neighbours.Add()
        }


        // Workaround allowing iteration over neighbours, 
        // whereas GetEnumerator would create an illusion
        // of the edged being somehow a part of a vertice
        public Dictionary<int, Edge> GetNeighbours()//NOTE: Why this when there is a variable?
            => this.Neighbours;

    }
}
}
