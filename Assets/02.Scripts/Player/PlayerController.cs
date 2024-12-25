using UnityEngine;

namespace yjlee
{
    public class PlayerController : MonoBehaviour
    {
        // 플레이어 상태
        public enum STATE
        {
            Idle,      // 기본
            Move,      // 이동
            Jump,      // 점프
            Crash,     // 충돌
            Climb,     // 사다리
            Operation  // 기계 조작
        }

        private Rigidbody2D playerRigidbody;

        [SerializeField]
        private float moveSpeed = 2.0f;
        [SerializeField]
        private float climbSpeed = 1.5f;

        void Start()
        {
            playerRigidbody = GetComponent<Rigidbody2D>();
        }

        void Update()
        {

        }
    }
}
