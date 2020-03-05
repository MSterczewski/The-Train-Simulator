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
        public int Id { get; protected set; };
        public List<Edge> OutgoingEdges;
        public int Degree
           => this.OutgoingEdges.Count;
        public Node(int Id)
        {
            this.Id = Id;
            this.OutgoingEdges = new List<Edge>();
        }
        public bool IsNeighbour(int NodeId)
            => this.OutgoingEdges.Any(edge => (edge.Destination == NodeId));
        //Single direction only
        public void AddNeighbour(int NeighbourId, int Color = 0, float Weight = 1.0f)
        {
            var e = new Edge(this.Id, NeighbourId);
            e.Color = Color;
            e.Weight = Weight;

            this.OutgoingEdges.Add(e);
        }
    }

    public class DijkstraNode : Node
        //Subclass just for Dijkstra Algorithm
    {
        public float value;
        public DijkstraNode(Node n, float val):base(n.Id)
        {
            OutgoingEdges = n.OutgoingEdges;
            //Id = n.Id;
            value = val;
        }
    }
}
