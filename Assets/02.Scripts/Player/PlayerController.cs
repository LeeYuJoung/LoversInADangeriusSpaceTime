using UnityEngine;
using EnumTypes;
using UnityEngine.Playables;

namespace yjlee
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D playerRigidbody;
        private PLAYER_STATE playerState;

        [SerializeField]
        private float moveSpeed = 0.5f;
        [SerializeField]
        private float maxSpeed = 2.0f;
        [SerializeField]
        private float climbSpeed = 0.25f;
        [SerializeField]
        private float jumpSpeed = 0.65f;

        private void Start()
        {
            playerRigidbody = GetComponent<Rigidbody2D>();
            playerState = PLAYER_STATE.Idle;
        }

        private void Update()
        {
            Move();
            Climb();
            Jump();
            Action();
        }

        public void Move()
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                playerState = PLAYER_STATE.Move;
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                playerRigidbody.AddForce(Vector2.right * moveSpeed, ForceMode2D.Impulse);
                playerRigidbody.linearVelocity = new Vector2(Mathf.Max(-playerRigidbody.linearVelocityX, maxSpeed), playerRigidbody.linearVelocityY);
            }
            else if(Input.GetKey(KeyCode.LeftArrow))
            {
                playerState = PLAYER_STATE.Move;
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

                playerRigidbody.AddForce(Vector2.left * moveSpeed, ForceMode2D.Impulse);
                playerRigidbody.linearVelocity = new Vector2(Mathf.Max(playerRigidbody.linearVelocityX, -maxSpeed), playerRigidbody.linearVelocityY);
            }
            else if(Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                playerState = PLAYER_STATE.Idle;
                playerRigidbody.linearVelocity = new Vector3(playerRigidbody.linearVelocity.normalized.x, playerRigidbody.linearVelocity.y);
            } 
        }

        // 사다리에서 이동 동작 실행
        public void Climb()
        {
            if(Input.GetKey(KeyCode.UpArrow))
            {
                playerState = PLAYER_STATE.Climb;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                playerState = PLAYER_STATE.Climb;
            }
        }

        // 점프 동작 실행
        public void Jump()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (IsGrounded())
                {
                    playerState = PLAYER_STATE.Jump;
                    playerRigidbody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                }
            }
        }

        public void Action()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                // 우주선의 기기와 상호작용 버튼

            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                // AI 봇에 명령 버튼

            }
        }

        // 플레이어가 땅을 밟고 있는지 체크
        private bool IsGrounded()
        {
            var _ray = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, 1 << LayerMask.NameToLayer("Ground"));
            return _ray.collider != null;
        }

        private void OnTriggerEnter2D(Collider2D coll)
        {
            if(coll.CompareTag("AI") || coll.CompareTag("Player"))
            {
                // 충돌 애니메이션 실행
                playerState = PLAYER_STATE.Crash;
            }
        }
    }
}
