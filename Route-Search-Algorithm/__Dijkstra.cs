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

        // 간선 추가 메서드
        public void AddEdge(int source, int target, int weight)
        {
            AdjacencyList[source].Add(new Edge(target, weight));
            AdjacencyList[target].Add(new Edge(source, weight)); // 무방향 그래프인 경우
        }
    }

    public static int[] DijkstraAlgorithm(Graph graph, int start)
    {
        int n = graph.AdjacencyList.Length;
        int[] distances = new int[n];
        bool[] visited = new bool[n];

        // 모든 노드의 거리 초기화
        for (int i = 0; i < n; i++)
        {
            distances[i] = int.MaxValue;  // 무한대 초기화
            visited[i] = false;
        }

        // 시작 노드의 거리는 0
        distances[start] = 0;

        // 우선순위 큐 (distance, node)
        var priorityQueue = new SortedSet<(int distance, int node)>(
            Comparer<(int, int)>.Create((x, y) =>
            {
                // 먼저 distance를 비교하고, 만약 같으면 node로 비교
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

            // 이미 방문한 노드는 무시
            if (visited[currentNode]) continue;
            visited[currentNode] = true;

            // 현재 노드에서 인접한 모든 노드에 대해 최단 거리 계산
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