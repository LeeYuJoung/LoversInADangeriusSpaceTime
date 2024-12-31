using NUnit.Framework;
using UnityEngine;

namespace yjlee.AI
{
    // 설정한 x, y 기준으로 범위를 설정 후, 설정된 지름만큼 범위안에 노드를을 생성
    // 스크린을 Grid - Node로 분할
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
        public Node[] path;   // A*에서 사용할 Path  

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

        }

        private void OnDrawGizmos()
        {
            
        }
    }
}