using UnityEngine;
using EnumTypes;

namespace yjlee.Player
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D playerRigidbody;
        private Animator playerAnimator;
        private PlayerState playerState;

        [SerializeField]
        private float moveSpeed = 0.5f;
        [SerializeField]
        private float maxSpeed = 1.5f;
        [SerializeField]
        private float climbSpeed = 0.5f;
        [SerializeField]
        private float jumpSpeed = 0.4f;

        private void Start()
        {
            playerRigidbody = GetComponent<Rigidbody2D>();
            playerState = PlayerState.Idle;
        }

        private void Update()
        {
            Move();
            Climb();
            Jump();
            Action();
        }

        // 이동 동작 실행
        public void Move()
        {
            if (playerState == PlayerState.Climb)
            {
                return;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                playerState = PlayerState.Move;
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                playerRigidbody.AddForce(Vector2.right * moveSpeed, ForceMode2D.Impulse);
                playerRigidbody.linearVelocity = new Vector2(Mathf.Max(-playerRigidbody.linearVelocityX, maxSpeed), playerRigidbody.linearVelocityY);
            }
            else if(Input.GetKey(KeyCode.LeftArrow))
            {
                playerState = PlayerState.Move;
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

                playerRigidbody.AddForce(Vector2.left * moveSpeed, ForceMode2D.Impulse);
                playerRigidbody.linearVelocity = new Vector2(Mathf.Max(playerRigidbody.linearVelocityX, -maxSpeed), playerRigidbody.linearVelocityY);
            }
            else if(Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                playerRigidbody.linearVelocity = new Vector3(playerRigidbody.linearVelocity.normalized.x, playerRigidbody.linearVelocity.y);
            } 
        }

        // 사다리에서 이동 동작 실행
        public void Climb()
        {
            if (IsLadder())
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    playerState = PlayerState.Climb;

                    playerRigidbody.gravityScale = 0;
                    playerRigidbody.linearVelocity = Vector3.zero;
                    transform.Translate(0.0f, climbSpeed * Time.deltaTime, 0.0f);
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    playerState = PlayerState.Climb;

                    playerRigidbody.gravityScale = 0;
                    playerRigidbody.linearVelocity = Vector3.zero;
                    transform.Translate(0.0f, -climbSpeed * Time.deltaTime, 0.0f);
                }
            }
        }

        // 점프 동작 실행
        public void Jump()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (IsGrounded() || IsLadder())
                {
                    playerState = PlayerState.Jump;

                    playerRigidbody.gravityScale = 1.0f;
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

        // 플레이어가 땅을 밟고 있는지 확인
        private bool IsGrounded()
        {
            var _ray = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, 1 << LayerMask.NameToLayer("Plane"));

            if(_ray.collider != null)
            {
                playerState = PlayerState.Idle;
                playerRigidbody.gravityScale = 1.0f;

                return true;
            }
            else
            {
                return false;
            }
        }

        // 플레이어가 사다리를 올라갈 수 있는지 확인
        private bool IsLadder()
        {
            var _ray = Physics2D.Raycast(transform.position, Vector2.down, 0.25f, 1 << LayerMask.NameToLayer("Ladder"));
            return _ray.collider != null;
        }

        private void OnTriggerEnter2D(Collider2D coll)
        {
            //if(coll.CompareTag("AI") || coll.CompareTag("Player"))
            //{
            //    // 충돌 애니메이션 실행
            //    playerState = PLAYER_STATE.Crash;
            //}
        }
    }
}
