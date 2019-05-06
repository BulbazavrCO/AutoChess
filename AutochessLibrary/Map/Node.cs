using System.Collections;
using System.Collections.Generic;

namespace AutoChess
{
    public class Node
    {
        public int X { get; private set; }

        public int Y { get; private set; }

        public int gCost;
        public int hCost;

        public ICell Cell { get; set; }        

        public int fCost
        {
            get
            {
                return gCost + hCost;
            }
        }

        public Node(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Node parrent;

        public bool OnMove()
        {
            return Cell == null;
        }
    }
}
