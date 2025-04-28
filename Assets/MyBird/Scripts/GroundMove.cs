using UnityEngine;

namespace MyBird
{
    //배경 스크롤 구현
    public class GroundMove : MonoBehaviour
    {
        #region Variables
        //스크롤 이동 속도
        [SerializeField]private float moveSpeed = 5f;

        //이 지점에 다다르면 롤링
        private float resetPosition = -8.4f;
        //롤링을 하기위한 시작 위치 저장
        private Vector3 startPosition;
        #endregion

        private void Start()
        {
            startPosition = this.transform.position;
        }

        //배경을 왼쪽방향으로 이동
        private void Update()
        {
            Move();
        }

        //배경을 왼쪽으로 이동 시킨다,
        //배경의 x좌표가 -8.4보다 같거나 작으면 x좌표를 제자리로 놓는다
        void Move()
        {
            if (GameManager.IsStart == false)
                return;

            this.transform.Translate(Vector3.left * Time.deltaTime * moveSpeed, Space.World);
            if (this.transform.localPosition.x <= resetPosition)
            {
                //this.transform.position = startPosition;
                this.transform.position = new Vector3(transform.position.x + 8.4f, this.transform.position.y, this.transform.position.z);
            } 
        }
    }

}
