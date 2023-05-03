using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    internal class AStar
    {
        /******************************************************
        * A* 알고리즘
        * 
        * 다익스트라 알고리즘을 확장하여 만든 최단경로 탐색알고리즘
        * 경로 탐색의 우선순위를 두고 유망한 해부터 우선적으로 탐색
        ******************************************************/

        private const int CostStraight = 10;
        private const int CostDiagonal = 14;

        public struct Pos
        {
            public int x;
            public int y;

            public Pos(int _x, int _y)
            {
                this.x = _x;
                this.y = _y;
            }
        }

        public Pos[] Direction =
        {
            new Pos(  0, +1 ),			// 상
			new Pos(  0, -1 ),			// 하
			new Pos( -1,  0 ),			// 좌
			new Pos( +1,  0 ),			// 우
			// new Point( -1, +1 ),		    // 좌상
			// new Point( -1, -1 ),		    // 좌하
			// new Point( +1, +1 ),		    // 우상
			// new Point( +1, -1 )		    // 우하
		};

        private class Vertex
        {
            public Pos currPos;        // 현재 정점 위치
            public Pos? parentPos;     // 이 정점을 탐색한 정점

            public int f;           // f(x) = g(x) + h(x) : 총 거리
            public int g;           // 현재까지의 거리, 즉 지금까지 경로 가중치
            public int h;           // 휴리스틱 : 앞으로 예상되는 거리, 목표까지 추정 경로 가중치

            public Vertex(Pos curr, Pos? parent, int g, int h)
            {
                this.currPos = curr;
                this.parentPos = parent;
                this.g = g;
                this.h = h;
                this.f = g + h;
            }
        }

        public bool PathFinding(bool[,] tileMap, Pos start, Pos end, out List<Pos> path)
        {
            int ySize = tileMap.GetLength(0);
            int xSize = tileMap.GetLength(1);

            bool[,] visited = new bool[ySize, xSize];
            Vertex[,] nodes = new Vertex[ySize, xSize];
            PriorityQueue<Vertex, int> nextPointPQ = new PriorityQueue<Vertex, int>();

            // 0. 시작 정점을 생성하여 추가
            Vertex startNode = new Vertex(start, null, 0, Heuristic(start, end));
            nodes[startNode.currPos.y, startNode.currPos.x] = startNode;
            nextPointPQ.Enqueue(startNode, startNode.f);

            while (nextPointPQ.Count > 0)
            {
                // 1. 다음으로 탐색할 정점 꺼내기
                Vertex nextNode = nextPointPQ.Dequeue();

                // 2. 방문한 정점은 방문표시
                visited[nextNode.currPos.y, nextNode.currPos.x] = true;

                // 3. 다음으로 탐색할 정점이 도착지인 경우
                // 도착했다고 판단해서 경로 반환
                if (nextNode.currPos.x == end.x && nextNode.currPos.y == end.y)
                {
                    Pos? pathPos = end;
                    path = new List<Pos>();

                    while (pathPos != null)
                    {
                        Pos pos = pathPos.GetValueOrDefault();
                        path.Add(pos);
                        pathPos = nodes[pos.y, pos.x].parentPos;
                    }

                    path.Reverse();
                    return true;
                }


                // 4. AStar 탐색을 진행
                // 방향 탐색
                for (int i = 0; i < Direction.Length; i++)
                {
                    int x = nextNode.currPos.x + Direction[i].x;
                    int y = nextNode.currPos.y + Direction[i].y;

                    // 4-1. 탐색하면 안되는 경우
                    // 맵을 벗어났을 경우
                    if (x < 0 || x >= xSize || y < 0 || y >= ySize)
                        continue;
                    // 탐색할 수 없는 정점일 경우
                    else if (tileMap[y, x] == false)
                        continue;
                    // 이미 방문한 정점일 경우
                    else if (visited[y, x])
                        continue;

                    // 4-2. 탐색한 정점 만들기
                    int g = nextNode.g + ((nextNode.currPos.x == x || nextNode.currPos.y == y) ? CostStraight : CostDiagonal);
                    int h = Heuristic(new Pos(x, y), end);
                    Vertex newNode = new Vertex(new Pos(x, y), nextNode.currPos, g, h);

                    // 4-3. 정점의 갱신이 필요한 경우 새로운 정점으로 할당
                    if (nodes[y, x] == null ||      // 탐색하지 않은 정점이거나
                        nodes[y, x].f > newNode.f)  // 가중치가 높은 정점인 경우
                    {
                        nodes[y, x] = newNode;
                        nextPointPQ.Enqueue(newNode, newNode.f);
                    }
                }
            }

            path = null;
            return false;
        }

        // 휴리스틱 (Heuristic) : 최상의 경로를 추정하는 순위값
        // 휴리스틱에 의해 경로탐색 효율이 결정됨
        private int Heuristic(Pos start, Pos end)
        {

            int xSize = Math.Abs(start.x - end.x);  // 가로로 가야하는 횟수
            int ySize = Math.Abs(start.y - end.y);  // 세로로 가야하는 횟수

            // 맨해튼 거리 : 가로 세로를 통해 이동하는 거리
             return CostStraight * (xSize + ySize);

            // 유클리드 거리 : 대각선을 통해 이동하는 거리
            //return CostStraight * (int)Math.Sqrt(xSize * xSize + ySize * ySize);
        }
    }
}
