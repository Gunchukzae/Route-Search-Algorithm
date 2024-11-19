using System;
using System.Collections.Generic;
using System.Linq;

public class Node
{
    public int X { get; }
    public int Y { get; }
    public bool IsWalkable { get; set; }
    public Node Parent { get; set; }
    public int GCost { get; set; } // Cost from start node
    public int HCost { get; set; } // Heuristic cost to end node

    public int FCost => GCost + HCost; // Total cost

    public Node(int x, int y, bool isWalkable = true)
    {
        X = x;
        Y = y;
        IsWalkable = isWalkable;
        GCost = int.MaxValue;
        HCost = 0;
        Parent = null;
    }
}

public class AStarPathfinder
{
    private readonly int[,] _grid;
    private readonly List<Node> _nodes;

    public AStarPathfinder(int[,] grid)
    {
        _grid = grid;
        _nodes = new List<Node>();

        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                _nodes.Add(new Node(x, y, grid[y, x] == 0)); // 0 means walkable
            }
        }
    }

    public List<Node> FindPath(int startX, int startY, int endX, int endY)
    {
        Node startNode = _nodes.First(n => n.X == startX && n.Y == startY);
        Node endNode = _nodes.First(n => n.X == endX && n.Y == endY);

        startNode.GCost = 0;
        startNode.HCost = GetHeuristic(startNode, endNode);

        var openList = new List<Node> { startNode };
        var closedList = new HashSet<Node>();

        while (openList.Count > 0)
        {
            // Sort openList by FCost
            var currentNode = openList.OrderBy(n => n.FCost).ThenBy(n => n.HCost).First();

            if (currentNode == endNode)
                return RetracePath(startNode, endNode);

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (var neighbor in GetNeighbors(currentNode))
            {
                if (!neighbor.IsWalkable || closedList.Contains(neighbor))
                    continue;

                int tentativeGCost = currentNode.GCost + GetDistance(currentNode, neighbor);
                if (tentativeGCost < neighbor.GCost)
                {
                    neighbor.GCost = tentativeGCost;
                    neighbor.HCost = GetHeuristic(neighbor, endNode);
                    neighbor.Parent = currentNode;

                    if (!openList.Contains(neighbor))
                        openList.Add(neighbor);
                }
            }
        }

        return null; // No path found
    }

    private List<Node> GetNeighbors(Node node)
    {
        var neighbors = new List<Node>();

        // First check for horizontal and vertical neighbors (no diagonals)
        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                if (x == 0 && y == 0) continue;

                int checkX = node.X + x;
                int checkY = node.Y + y;

                // Horizontal/Vertical directions first
                if ((x == 0 || y == 0) && checkX >= 0 && checkX < _grid.GetLength(1) && checkY >= 0 && checkY < _grid.GetLength(0))
                {
                    neighbors.Add(_nodes.First(n => n.X == checkX && n.Y == checkY));
                }
            }
        }

        return neighbors;
    }

    private int GetHeuristic(Node a, Node b)
    {
        // Manhattan distance for a grid
        return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
    }

    private int GetDistance(Node a, Node b)
    {
        // Manhattan distance (8 directions)
        if (a.X != b.X && a.Y != b.Y)
            return 14; // Diagonal move
        return 10; // Horizontal/Vertical move
    }

    private List<Node> RetracePath(Node startNode, Node endNode)
    {
        var path = new List<Node>();
        var currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }

        path.Reverse();
        return path;
    }
}
