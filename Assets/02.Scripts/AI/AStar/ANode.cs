using UnityEngine;

namespace yjlee.AI
{
    // AI 봇의 이동 가능 여부 및 노드의 유니티 상 위치를 변수로 할당
    public class ANode
    {
        public bool isWalkable;
        public Vector3 worldPos;

        public ANode(bool nWalkable, Vector3 nWorldPos)
        {
            isWalkable = nWalkable;
            worldPos = nWorldPos;
        }
    }
}
