using UnityEngine;

namespace MyBird
{
    //게임의 전체 플로우를 관리하는 클래스
    //대기 -> 이동 -> 죽음(결과처리) || 게임 클리어
    //static 처리 : 싱글톤 클래스, 정적(static) 변수
    public class GameManager : MonoBehaviour
    {
        #region Property
        public static bool IsStart
        {
            get;
            set;
        }

        public static bool IsDeath
        {
            get;
            set;
        }
        public static int Score
        {
            get;
            set;
        }
        public static int BestScore
        {
            get;
            set;
        }
        #endregion

        public void Start()
        {
            //최고 점수 가져오기
            BestScore = PlayerPrefs.GetInt("BestScore", 0);

            //초기화
            IsStart = false;
            IsDeath = false;
            Score = 0;
        }

        public static void ResetGame()
        {
            //초기화
            IsStart = false;
            IsDeath = false;
            Score = 0;
        }
    }
}

