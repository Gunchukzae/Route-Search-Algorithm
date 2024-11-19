using System;
using System.Collections.Generic;

class PRM
{
    public class Node
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Node(double x, double y)
        {
            X = x;
            Y = y;
        }

        // 0,0에서 해당 노드까지의 거리 계산 (Euclidean distance)
        public double DistanceTo(Node other)
        {
            return Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
        }
    }

    public class Edge
    {
        public Node Start { get; set; }
        public Node End { get; set; }

        public Edge(Node start, Node end)
        {
            Start = start;
            End = end;
        }
    }

    private Random _random;
    private int _numSamples;
    private double _connectionRadius;
    private List<Node> _nodes;
    private List<Edge> _edges;

    public PRM(int numSamples, double connectionRadius)
    {
        _numSamples = numSamples;
        _connectionRadius = connectionRadius;
        _nodes = new List<Node>();
        _edges = new List<Edge>();
        _random = new Random();
    }

    public Random GetRandom() => _random;
    public int GetNumSamples() => _numSamples;
    public double GetConnectionRadius() => _connectionRadius;
    public List<Node> GetNodes() => new List<Node>(_nodes);
    public List<Edge> GetEdges() => new List<Edge>(_edges);

    // Randomly sample nodes within the space
    public void SampleNodes(int width, int height)
    {
        for (int i = 0; i < _numSamples; i++)
        {
            // 랜덤하게 노드 생성
            double x = _random.NextDouble() * width;
            double y = _random.NextDouble() * height;
            Node newNode = new Node(x, y);

            _nodes.Add(newNode);
        }

        _nodes = _nodes.OrderBy(node => node.DistanceTo(new Node(0, 0))).ToList();
    }

    // Connect nodes that are within the connection radius
    public void ConnectNodes()
    {
        for (int i = 0; i < _nodes.Count; i++)
        {
            for (int j = i + 1; j < _nodes.Count; j++)
            {
                Node node1 = _nodes[i];
                Node node2 = _nodes[j];

                double distance = Math.Sqrt(Math.Pow(node1.X - node2.X, 2) + Math.Pow(node1.Y - node2.Y, 2));

                // If the nodes are within the connection radius, create an edge
                if (distance <= _connectionRadius)
                {
                    _edges.Add(new Edge(node1, node2));
                }
            }
        }
    }

    // Print nodes and edges
    public void PrintGraph()
    {
        Console.WriteLine("Nodes:");
        foreach (var node in _nodes)
        {
            Console.WriteLine($"({node.X}, {node.Y})");
        }

        Console.WriteLine("\nEdges:");
        foreach (var edge in _edges)
        {
            Console.WriteLine($"({edge.Start.X}, {edge.Start.Y}) -> ({edge.End.X}, {edge.End.Y})");
        }
    }

    // A simple pathfinding method (not optimal) to find a path from the first to the last node
    public List<Node> FindPath(Node start, Node goal)
    {
        List<Node> path = new List<Node>();
        path.Add(start);

        // Simple greedy approach: add nodes directly connected to the current node
        Node current = start;

        while (current != goal)
        {
            Node next = null;
            double minDistance = double.MaxValue;

            foreach (var edge in _edges)
            {
                if (edge.Start == current && !path.Contains(edge.End))
                {
                    double distance = Math.Sqrt(Math.Pow(edge.End.X - goal.X, 2) + Math.Pow(edge.End.Y - goal.Y, 2));
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        next = edge.End;
                    }
                }
            }

            if (next == null)
            {
                Console.WriteLine("No path found.");
                return null;
            }

            path.Add(next);
            current = next;
        }

        return path;
    }
}