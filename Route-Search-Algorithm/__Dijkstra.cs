using System;
using System.Collections.Generic;

class Dijkstra
{
    public class Edge
    {
        public int Target { get; }
        public int Weight { get; }

        public Edge(int target, int weight)
        {
            Target = target;
            Weight = weight;
        }
    }

    public class Graph
    {
        public List<Edge>[] AdjacencyList;

        public Graph(int nodes)
        {
            AdjacencyList = new List<Edge>[nodes];
            for (int i = 0; i < nodes; i++)
            {
                AdjacencyList[i] = new List<Edge>();
            }
        }

        // ���� �߰� �޼���
        public void AddEdge(int source, int target, int weight)
        {
            AdjacencyList[source].Add(new Edge(target, weight));
            AdjacencyList[target].Add(new Edge(source, weight)); // ������ �׷����� ���
        }
    }

    public static int[] DijkstraAlgorithm(Graph graph, int start)
    {
        int n = graph.AdjacencyList.Length;
        int[] distances = new int[n];
        bool[] visited = new bool[n];

        // ��� ����� �Ÿ� �ʱ�ȭ
        for (int i = 0; i < n; i++)
        {
            distances[i] = int.MaxValue;  // ���Ѵ� �ʱ�ȭ
            visited[i] = false;
        }

        // ���� ����� �Ÿ��� 0
        distances[start] = 0;

        // �켱���� ť (distance, node)
        var priorityQueue = new SortedSet<(int distance, int node)>(
            Comparer<(int, int)>.Create((x, y) =>
            {
                // ���� distance�� ���ϰ�, ���� ������ node�� ��
                if (x.Item1 == y.Item1) return x.Item2.CompareTo(y.Item2); // x.Item1 = distance, x.Item2 = node
                return x.Item1.CompareTo(y.Item1); // x.Item1 = distance
            })
        );

        priorityQueue.Add((0, start));

        while (priorityQueue.Count > 0)
        {
            var current = priorityQueue.Min;
            priorityQueue.Remove(current);

            int currentNode = current.Item2; // x.Item2 = node
            int currentDistance = current.Item1; // x.Item1 = distance

            // �̹� �湮�� ���� ����
            if (visited[currentNode]) continue;
            visited[currentNode] = true;

            // ���� ��忡�� ������ ��� ��忡 ���� �ִ� �Ÿ� ���
            foreach (var edge in graph.AdjacencyList[currentNode])
            {
                if (visited[edge.Target]) continue;

                int newDist = currentDistance + edge.Weight;
                if (newDist < distances[edge.Target])
                {
                    distances[edge.Target] = newDist;
                    priorityQueue.Add((newDist, edge.Target));
                }
            }
        }

        return distances;
    }
}