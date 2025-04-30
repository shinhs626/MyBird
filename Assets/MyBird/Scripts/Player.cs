using TMPro;
using UnityEngine;

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
        [SerializeField] private float moveSpeed = 3f;

        //대기
        //아래로 떨어지지 않을 만큼의 새를 받히는 힘
        [SerializeField] private float readyForce = 0.295f;

        //애니메이션
        public Animator animator;

        //Ready UI
        public GameObject readyUI;

        //Gameover UI
        public GameObject resultUI;

        //오디오 소스
        public AudioSource getPoint;
        #endregion

        #region Unity Event Method


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            rb2D = this.GetComponent<Rigidbody2D>();
            getPoint = this.GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            //인풋 처리
            InputBird();

            if (GameManager.IsStart == false)
            {
                ReadyBird();
                //UI
                readyUI.SetActive(true);
                return;
            }
            else
            {
                //UI 히든
                readyUI.SetActive(false);
                //버드 회전
                RotateBird();

                //버드 이동
                MoveBird();
            } 
        }
        private void FixedUpdate()
        {
            if (keyJump)
            {
                // Jump
                Jump();
                keyJump = false;
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            //collision : 부딛힌 콜라이더 정보를 가지고 있다
            if(collision.gameObject.tag == "Ground")
            {
                DieBird();
                //Debug.Log("Ground에 부딛힘");
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //collision : 부딛힌 콜라이더 정보를 가지고 있다.
            if (collision.gameObject.tag == "Point")
            {
                //Debug.Log("점수 획득");
                GameManager.Score++;

                //오디오 소스
                getPoint.Play();
            }
            else if (collision.gameObject.tag == "Pipe")
            {
                DieBird();
                //Debug.Log("Pipe에 부딛힘");
            }
        }
        #endregion

        #region Custon Method
        //인풋처리
        void InputBird()
        {
            if (GameManager.IsDeath)
            {
                return;
            }

#if UNITY_EDITOR
            //스페이스키 또는 마우스 왼클릭 입력 받기
            keyJump |= Input.GetKeyDown(KeyCode.Space);
            keyJump |= Input.GetMouseButtonDown(0);
#else
            //터치 인풋 처리
             if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);    //첫 번째 터치만 처리

                //tab처리
                if(touch.phase == TouchPhase.Began)
                {
                    keyJump |= true;
                }
            }
#endif
            //게임 시작전이고 키가 눌리면 
            if (GameManager.IsStart == false && keyJump == true)
            {
                GameManager.IsStart = true;
            }
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

        //버드 대기
        void ReadyBird()
        {
            //아래쪽에서 떨어지지 않도록 위쪽으로 힘을 준다
            if(rb2D.linearVelocity.y < 0f)
            {
                rb2D.linearVelocity = Vector2.up * readyForce;
            }
        }

        void MoveBird()
        {
            if (GameManager.IsStart == false || GameManager.IsDeath == true)
                return;

            this.transform.Translate(Vector3.right * Time.deltaTime * moveSpeed, Space.World);
        }

        //버드 죽음
        void DieBird()
        {
            //두번 죽음 체크
            if (GameManager.IsDeath)
                return;

            GameManager.IsDeath = true;
            animator.enabled = false;
            rb2D.linearVelocity = Vector2.zero;

            resultUI.SetActive(true);
        }
#endregion
    }

}
