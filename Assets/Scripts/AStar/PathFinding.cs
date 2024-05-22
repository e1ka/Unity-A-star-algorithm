using System.Collections.Generic;
public static class PathFinding 
{
    private static Node[,] Grid;
    private static int GridX;
    private static int GridY;
    private static Node startNode;
    private static Node finishNode;
    public static void CreateGrid(int X, int Y, List<(int x, int y)> obstacles, (int x, int y)startPos, (int x, int y)finishPos)
    {
        Grid = new Node[X, Y];
        GridX = X;
        GridY = Y;
        SetStart(startPos.x, startPos.y);
        SetFinish(finishPos.x, finishPos.y);
        foreach(var o in obstacles)
        {
            SetObstacle(o.x, o.y);
        }

    }
    private static void SetObstacle(int X, int Y)
    {
        if(Grid[X,Y] == null)
        {
            Grid[X, Y] = new Node
            {
                Pos = (X, Y),
                Walkable = false
            };
        }
        else
        {
            Grid[X, Y].Walkable = false;
        }
    }
    private static void SetStart(int X, int Y)
    {
        if(Grid[X,Y] == null)
        {
            Grid[X, Y] = new Node
            {
                Pos = (X, Y),
                Walkable = true
            };
            startNode = Grid[X, Y];
        }
        else
        {
            startNode = Grid[X, Y];
        }
    }
    private static void SetFinish(int X, int Y)
    {
        if(Grid[X,Y] == null)
        {
            Grid[X, Y] = new Node
            {
                Pos = (X, Y),
                Walkable = true
            };
            finishNode = Grid[X, Y];
        }
        else
        {
            finishNode = Grid[X, Y];
        }
    }

    private static Node StoreNode(int X, int Y)
    {
        if(Grid[X,Y] == null)
        {
            Grid[X, Y] = new Node
            {
                Pos = (X, Y)
            };
        }

        return Grid[X,Y];
    }
    public static Stack<(int x, int y)> ASearch()
    {
        PriorityQueue<Node> open = new PriorityQueue<Node>();
        List<Node> closed = new List<Node>();
        Stack<(int x, int y)> path = new Stack<(int x, int y)>();

        Node currentNode = startNode;
        int currentG = 0;
        open.Enqueue(currentNode, 0);

        while (open.Count != 0)
        {   
            currentNode = open.Dequeue();
            closed.Add(currentNode);
            currentNode.Neighbours = GetNodeNeighbours(currentNode);
            foreach(Node n in currentNode.Neighbours)
            {
                if(n == finishNode)
                {
                    n.Parent = currentNode;
                    closed.Add(n);
                    open.Clear();
                    break;
                }
                if(!closed.Contains(n) && n.Walkable)
                {
                    if(!open.Contains(n))
                    {
                        n.Parent = currentNode;
                        n.H = Heuristic.Manhattan(n.Pos, finishNode.Pos);
                        n.G = currentG + 1;
                        open.Enqueue(n, n.F);
                    }
                    else
                    {
                        if(currentG + 1 < n.G)
                        {
                            n.Parent = currentNode;
                            n.G = currentG;
                        }
                    }
                }
            }
            currentG++;
        }
        
            if(!closed.Contains(finishNode))
            {
                return null;
            }
            else
            {
                Node temp = finishNode;
                while(temp != null)
                {
                    path.Push(temp.Pos);
                    temp = temp.Parent;
                }
            }
        return path;
    }

    public static List<Node> GetNodeNeighbours(Node n)
    {
        List<Node> temp = new List<Node>();

        int row = (int)n.Pos.y;
        int col = (int)n.Pos.x;

        if(row + 1 < GridY)
        {
            temp.Add(StoreNode(col,row + 1));
        }
        if(row - 1 >= 0)
        {
            temp.Add(StoreNode(col,row - 1));
        }
        if(col - 1 >= 0)
        {
            temp.Add(StoreNode(col -1,row));
        }
        if(col + 1 < GridX)
        {
            temp.Add(StoreNode(col + 1,row));
        }

        return temp;
    }
}
