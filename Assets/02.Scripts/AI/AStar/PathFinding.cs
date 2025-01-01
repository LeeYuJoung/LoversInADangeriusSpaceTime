using UnityEngine;

namespace yjlee.AI
{
    /* A* Algorithm 개요
     * OPEN SET : 평가되어야 할 노드 집합
     * CLOSED SET : 이미 평가된 노드 집합
     * 
     * 1. OPEN SET에서 가장 낮은 fCost를 가진 노드 획득 후 CLOSED SET 삽입
     * 2. 이 노드가 목적지라면 반복문 탈출
     * 3. 이 노드의 주변 노드들을 CLOSED SET에 넣고, 주변 노드의 f값 계산 (주변 노드의 g값 보다 작다면 f값으로 g값 최신화)
     * 4. 1번 반복
     */
    public class PathFinding : MonoBehaviour
    {

    }
}