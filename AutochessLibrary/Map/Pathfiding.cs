using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoChess
{
    public class Pathfiding
    {
        Map map;

        public Pathfiding (Map map)
        {
            this.map = map;
        }

        public List<Node> FindPath(Node startPos, Node endPos)
        {
            Node startNode = startPos;
            Node targetNode = endPos;           

            List<Node> openSet = new List<Node>();
            HashSet<Node> closeSet = new HashSet<Node>();

            List<Node> path = new List<Node>();

            openSet.Add(startPos);

            while (openSet.Count > 0)
            {                
                Node currentNode = openSet[0];
                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                    {
                        currentNode = openSet[i];
                    }
                }

                openSet.Remove(currentNode);
                closeSet.Add(currentNode);
                if (currentNode == targetNode)
                {
                    path = RetracePath(startNode, targetNode);
                    return path;
                }

                foreach (Node n in map.GetNeighbours(currentNode))
                {
                    if (!n.OnMove() || closeSet.Contains(n))
                        continue;

                    int newMovement = currentNode.gCost + GetDistanceCell(currentNode, n);
                    if (newMovement < n.gCost || !openSet.Contains(n))
                    {
                        n.gCost = newMovement;
                        n.hCost = GetDistanceCell(n, targetNode);
                        n.parrent = currentNode;

                        if (!openSet.Contains(n))
                            openSet.Add(n);
                    }
                }

            }
            return path;

        }

        private List<Node> RetracePath(Node startNode, Node endCell)
        {
            List<Node> path = new List<Node>();
            Node currentNode = endCell;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.parrent;
            }

            path.Reverse();

            return path;
        }

        private int GetDistanceCell(Node nodeA, Node nodeB)
        {
            int distX = Abs(nodeA.X - nodeB.X);
            int distY = Abs(nodeA.Y - nodeB.Y);

            if (distX > distY)
                return 14 * distY + 10 * (distX - distY);
            return 14 * distX + 10 * (distY - distX);
        }

        private int Abs(int x)
        {
            return x < 0 ? x * -1 : x;
        }
    }

}
