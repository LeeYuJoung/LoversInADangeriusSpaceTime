using UnityEngine;

namespace yjlee
{
    public class PlayerController : MonoBehaviour
    {
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
            Move();
            Jump();
            Action();
        }

        public void Move()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {

            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {

            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {

            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {

            }
        }

        public void Jump()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
            }
        }

        public void Action()
        {
            if(Input.GetKeyDown(KeyCode.M))
            {

            }
        }
    }
}
