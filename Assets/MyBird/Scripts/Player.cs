using UnityEngine;
using UnityEngine.UIElements;

namespace MyBird
{
    public class Player : MonoBehaviour
    {
        #region Variables
        private Rigidbody2D rb2D;

        //점프
        private bool keyJump = false;       //점프 인풋 체크
        [SerializeField]private float jumpForce = 5f;       //위쪽 방향으로 주는 힘

        //회전
        private Vector3 birdRotation;
        //위로 올라갈때 회전 속도
        [SerializeField] private float upRotate = 5f;
        //아래로 내려갈때 회전 속도
        [SerializeField] private float downRotate = -7f;
        //이동속도 - translate
        [SerializeField] private float moveSpeed = 1f;
        #endregion

        #region Unity Event Method

        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            rb2D = this.GetComponent<Rigidbody2D>();

            
        }

        // Update is called once per frame
        void Update()
        {
            //인풋 처리
            InputBird();
            MoveBird();
            RotateBird();
        }

        private void FixedUpdate()
        {
            if (keyJump)
            {
                // Jump
                Debug.Log("Jump");
                Jump();
                keyJump = false;
            }
        }
        #endregion

        #region Custon Method
        //인풋처리
        void InputBird()
        {
            keyJump |= Input.GetKeyDown(KeyCode.Space);
            keyJump |= Input.GetMouseButtonDown(0);
        }

        //버드 점프하기
        void Jump()
        {
            //아래쪽에서 위쪽으로 힘을 준다
            //rb2D.AddForce(Vector2.up * 힘);
            rb2D.linearVelocity = Vector2.up * jumpForce;
        }

        //버드 회전하기
        void RotateBird()
        {
            //점프해서 올라갈때 최대 + 30도 까지 회전 : rotateSpeed = 2.5 upRotate
            //내려갈때 최소 - 90도까지 회전 : rotateSpeed = 5 downRotate
            float rotateSpeed = 0f;
            if(rb2D.linearVelocity.y > 0f)
            {
                rotateSpeed = upRotate;
            }
            else if(rb2D.linearVelocity.y < 0f)
            {
                rotateSpeed = downRotate;
            }
            birdRotation = new Vector3(0f, 0f,Mathf.Clamp((birdRotation.z + rotateSpeed), -90f, 30));
            this.transform.eulerAngles = birdRotation;
        }

        void MoveBird()
        {
            this.transform.Translate(Vector3.right * Time.deltaTime * moveSpeed, Space.World);
        }
        #endregion
    }

}
