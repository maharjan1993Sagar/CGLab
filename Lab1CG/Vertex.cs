using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1CG
{
    //https://www.c-sharpcorner.com/article/linked-list-implementation-in-c-sharp/
    public class Vertices
    {
        static int noOfDLinkedList = 0;
        public Point Vertex;
        public Vertices nextVertices;
        public Vertices previousVertices;

        public Vertices(Point Vertex)
        {
            this.Vertex = Vertex;
            noOfDLinkedList++;
        }
    }
}
