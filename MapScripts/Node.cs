using System.Collections;
using System.Collections.Generic;

public class Node 
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public ICell Cell { get; set; }

    public Node(int x, int y)
    {
        X = x;
        Y = y;
    }    

   
}
