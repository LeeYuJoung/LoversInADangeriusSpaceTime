using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yjlee.AI
{    
    // 스크린을 Grid - Node로 분할
    // 설정한 x, y 기준으로 범위를 설정 후, 설정된 지름만큼 범위안에 노드를을 생성
    public class Grid : MonoBehaviour
    {
        public Transform pet;
        public LayerMask unwalkableMask;  // 걸을 수 없는 레이어 
        public Vector2 gridWorldSize;     // 화면의 크기

        private Node[,] grid;
        public float nodeRadius;     // 반지름
        private float nodeDiameter;  // 격자의 지름 
        private int gridSizeX;
        private int gridSizeY;

        public bool displayGridGizmos;

        [SerializeField]
        public List<Node> path;   // A*에서 사용할 Path  

        private void Awake()
        {
            nodeDiameter = nodeRadius * 2;  // 설정한 반지름으로 지름 구함
            gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);   // 그리드의 가로 크기
            gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);   // 그리드의 세로 크기

            CreateGrid();
        }

        // 격자 생성 함수
        private void CreateGrid()
        {
            grid = new Node[gridSizeX, gridSizeY];

            // 격자 생성은 좌측 최하단부터 시작 (transform은 월드 중앙에 위치)
            // 이에 x와 y좌표를 반반 씩 왼쪽, 아래쪽으로 옮김
            Vector2 worldBottomLeft = (Vector2)transform.position - Vector2.right * gridWorldSize.x / 2 - Vector2.up * gridWorldSize.y / 2;
            
            for(int x = 0; x < gridSizeX; x++)
            {
                for(int y = 0; y < gridSizeY; y++)
                {
                    Vector2 worldPoint = worldBottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.up * (y * nodeDiameter + nodeRadius);

                    // 해당 격자가 walkable한지 아닌지 판단
                    bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius, unwalkableMask));
                    // 노드 할당
                    grid[x, y] = new Node(walkable, worldPoint, x, y);
                }
            }
        }

        // node 상하 좌우 대각 노드를 반환하는 함수
        public List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new List<Node>();

            for(int x = -1; x <= 1; x++)
            {
                for(int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;

                    int checkX = node.gridX + x;
                    int checkY = node.gridY + y;

                    if(checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                    {
                        if (!grid[node.gridX, checkY].isWalkable && !grid[checkX, node.gridY].isWalkable)
                            continue;
                        if (!grid[node.gridX, checkY].isWalkable || !grid[checkX, node.gridY].isWalkable)
                            continue;

                        neighbours.Add(grid[checkX, checkY]);
                    }
                }
            }

            return neighbours;
        }

        // 입력으로 들어온 월드좌표를 node 좌표계로 변환하는 함수
        public Node NodeFromWorldPoint(Vector2 worldPosition)
        {
            float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
            float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;

            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);

            int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
            int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

            return grid[x, y];
        }

        // Scene View 출력용 Gizmos
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, new Vector2(gridWorldSize.x, gridWorldSize.y));

            if(grid != null)
            {
                Node playerNode = NodeFromWorldPoint(pet.position);

                foreach(Node n in grid)
                {
                    Gizmos.color = (n.isWalkable) ? new Color(1, 1, 1, 0.3f) : new Color(1, 0, 0, 0.3f);

                    if (!n.isWalkable)
                    {
                        if(path != null)
                        {
                            if (path.Contains(n))
                            {
                                Gizmos.color = new Color(0, 0, 0, 0.3f);
                                Debug.Log("?");
                            }
                        }
                    }

                    if(playerNode == n)
                    {
                        Gizmos.color = new Color(0, 1, 1, 0.3f);
                    }
                    Gizmos.DrawCube(n.worldPosition, Vector2.one * (nodeDiameter - 0.1f));
                }
            }
        }
    }
}