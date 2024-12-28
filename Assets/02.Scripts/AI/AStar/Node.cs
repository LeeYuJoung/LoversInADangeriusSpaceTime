using UnityEngine;

namespace yjlee.AI
{
    // AI 봇의 이동 가능 여부 및 노드의 위치를 변수로 할당
    public class Node
    {
        public Vector3 currentPos;
        public int currentX;
        public int currentY;

        public int gCost;  // 출발지로부터 현재 노드까지의 최단거리
        public int hCost;  // 목적지까지의 예상거리

        public bool isWalkable;

        public Node(bool _walkable, Vector3 _pos, int _x, int _y)
        {
            isWalkable = _walkable;  // 지나갈 수 있는 노드인지
            currentPos = _pos;       // 노드의 게임 내 위치값
            currentX = _x;           // 노드의 x좌표 값
            currentY = _y;           // 노드의 y좌표 값
        }

        // gCost와 hCost를 합친 최종점수 반환
        public int fCost
        {
            get{ return gCost + hCost; }
        }
    }
}
