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
        public int Id { get; protected set; }

        
        public List<Edge> OutgoingEdges;


        // Almost a method
        public int Degree
           => this.OutgoingEdges.Count;


        public Node(int Id)
        {
            this.Id = Id;
            this.OutgoingEdges = new List<Edge>();
        }


        public bool IsNeighbour(int NodeId) //NOTE: if change into Dict<Node> this neads to be changed
            => this.OutgoingEdges.Any(edge => (edge.Destination == NodeId));


        // Single-directional connection only
        // Asks to just accept an Edge as a single parameter
        // TODO: ^^
        public void AddNeighbour(int NeighbourId, int Color = 0, float Weight = 1.0f)
        {
            var e = new Edge(this.Id, NeighbourId);
            e.Color = Color;
            e.Weight = Weight;

            this.OutgoingEdges.Add(e);
        }
    }

    public class DijkstraNode : Node
        //Idk if its good idea
    {
        public float value;
        public DijkstraNode(int Id) : base(Id)
        { 
            value = 0;
        }
        public DijkstraNode(int Id, float val):base(Id)
        {
            value = val;
        }
        public DijkstraNode(Node n, float val):base(n.Id)
        {
            OutgoingEdges = n.OutgoingEdges;
            //Id = n.Id;
            value = val;
        }
    }
}
