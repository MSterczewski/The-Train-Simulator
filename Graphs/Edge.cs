using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    [Serializable]
    public class Edge
    {
        public int Source;
        public int Destination;

        public int Color;
        public float Weight;

        public Edge(int Source, int Destination, int Color = 0, float Weight = 1.0f)
        {
            this.Source = Source;
            this.Destination = Destination;
            this.Color = Color;
            this.Weight = Weight;
        }
    }
}
