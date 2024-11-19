using System;
using System.Collections.Generic;

// 3D 공간을 나타내는 Cell 클래스
public class Cell
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }
    public int Cost { get; set; }
    public Cell Parent { get; set; }

    public Cell(int x, int y, int z, int cost = int.MaxValue)
    {
        X = x;
        Y = y;
        Z = z;
        Cost = cost;
        Parent = null;
    }
}

public static class PathFinding
{
    // 방향 벡터: 6방향 (상, 하, 좌, 우, 전, 후)
    private static readonly int[][] Directions = new int[][]
    {
        new int[] { 1, 0, 0 },
        new int[] { -1, 0, 0 },
        new int[] { 0, 1, 0 },
        new int[] { 0, -1, 0 },
        new int[] { 0, 0, 1 },
        new int[] { 0, 0, -1 }
    };

    public static List<Cell> Dijkstra(int[,,] grid, Cell start, Cell goal)
    {
        int depth = grid.GetLength(0);
        int rows = grid.GetLength(1);
        int cols = grid.GetLength(2);

        // PriorityQueue를 사용하여 최소 비용 우선 처리
        var priorityQueue = new PriorityQueue<Cell, int>();
        start.Cost = 0;
        priorityQueue.Enqueue(start, start.Cost);

        // 방문 여부 체크
        var visited = new bool[depth, rows, cols];

        while (priorityQueue.Count > 0)
        {
            var current = priorityQueue.Dequeue();

            // 목표에 도달한 경우 경로 리턴
            if (current.X == goal.X && current.Y == goal.Y && current.Z == goal.Z)
            {
                var path = new List<Cell>();
                while (current != null)
                {
                    path.Add(current);
                    current = current.Parent;
                }
                path.Reverse();
                return path;
            }

            // 인접한 노드들 탐색
            foreach (var direction in Directions)
            {
                int newX = current.X + direction[0];
                int newY = current.Y + direction[1];
                int newZ = current.Z + direction[2];

                if (newX >= 0 && newX < depth && newY >= 0 && newY < rows && newZ >= 0 && newZ < cols)
                {
                    if (grid[newX, newY, newZ] == 1 && !visited[newX, newY, newZ]) // 장애물이 없고, 아직 방문하지 않은 셀
                    {
                        visited[newX, newY, newZ] = true;
                        var neighbor = new Cell(newX, newY, newZ, current.Cost + 1) { Parent = current };
                        priorityQueue.Enqueue(neighbor, neighbor.Cost); // 큐에 셀을 추가
                    }
                }
            }
        }

        // 경로를 찾지 못한 경우
        return null;
    }
}