// See https://aka.ms/new-console-template for more information

/*--------------------- A Algorithm ---------------------*/

//int[,] grid = {
//        { 0, 0, 0, 0, 1 },
//        { 1, 1, 0, 1, 0 },
//        { 0, 0, 0, 0, 0 },
//        { 0, 1, 0, 1, 0 },
//        { 0, 0, 0, 0, 0 }
//    };

//var pathfinder = new AStarPathfinder(grid);
//var path = pathfinder.FindPath(0, 0, 4, 4);

//if (path != null)
//{
//    Console.WriteLine("Path found:");
//    foreach (var node in path)
//    {
//        Console.WriteLine($"({node.X}, {node.Y})");
//    }
//}
//else
//{
//    Console.WriteLine("No path found.");
//}

/*--------------------- GraphbasedSearch Algorithm ---------------------*/

//int[,,] grid = new int[,,] //3D 그리드 (1은 비어있는 공간, 0은 장애물)
//{
//    {
//        {1, 1, 1},
//        {1, 0, 1},
//        {1, 1, 1}
//    },
//    {
//        {1, 1, 1},
//        {1, 0, 1},
//        {1, 1, 1}
//    },
//    {
//        {1, 1, 1},
//        {1, 1, 1},
//        {1, 1, 1}
//    }
//};

//// 시작점과 목표점 설정
//Cell start = new Cell(0, 0, 0);
//Cell goal = new Cell(2, 2, 2);

//// 경로 탐색
//var path = PathFinding.Dijkstra(grid, start, goal);

//if (path != null)
//{
//    Console.WriteLine("경로 찾음:");
//    foreach (var cell in path)
//    {
//        Console.WriteLine($"({cell.X}, {cell.Y}, {cell.Z})");
//    }
//}
//else
//{
//    Console.WriteLine("경로를 찾을 수 없습니다.");
//}

/*--------------------- Dijkstra Algorithm ---------------------*/
//// O((V + E) log V)
//// 그래프 초기화
//using static Dijkstra;

//Graph graph = new Graph(6); // 6개의 노드를 가진 그래프
//graph.AddEdge(0, 1, 7);
//graph.AddEdge(0, 2, 9);
//graph.AddEdge(0, 5, 14);
//graph.AddEdge(1, 2, 10);
//graph.AddEdge(1, 3, 15);
//graph.AddEdge(2, 3, 11);
//graph.AddEdge(2, 5, 2);
//graph.AddEdge(3, 4, 6);
//graph.AddEdge(4, 5, 9);

//// 다익스트라 알고리즘 실행
//int startNode = 0;
//int[] shortestPaths = DijkstraAlgorithm(graph, startNode);

//// 결과 출력
//Console.WriteLine("최단 거리 (출발점: " + startNode + ")");
//for (int i = 0; i < shortestPaths.Length; i++)
//{
//    if (shortestPaths[i] == int.MaxValue)
//        Console.WriteLine($"노드 {i}: 도달 불가");
//    else
//        Console.WriteLine($"노드 {i}: {shortestPaths[i]}");
//}

/*--------------------- Probabilistic Road Map Algorithm ---------------------*/

int width = 100;        // Width of the area
int height = 100;       // Height of the area
int numSamples = 50;    // Number of nodes to sample
double connectionRadius = 20;  // Maximum distance to connect nodes

PRM prm = new PRM(numSamples, connectionRadius);

// Sample nodes within the defined area
prm.SampleNodes(width, height);

// Connect the nodes
prm.ConnectNodes();

// Print the graph
prm.PrintGraph();

// Find a path from the first node to the last node (simple greedy approach)
PRM.Node start = prm.GetNodes()[0];
PRM.Node goal = prm.GetNodes()[prm.GetNodes().Count-1];

var path = prm.FindPath(start, goal);

// Print the found path
if (path != null)
{
    Console.WriteLine("\nPath found:");
    foreach (var node in path)
    {
        Console.WriteLine($"({node.X}, {node.Y})");
    }
}

// test