using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyBird
{
    public class ResultUI : MonoBehaviour
    {
        #region Variables
        public SceneFader fader;

        public GameObject newText;
        public TextMeshProUGUI bestScore;
        public TextMeshProUGUI nowScoreText;

        [SerializeField] private string retryScene = "PlayScene";
        [SerializeField] private string menuScene = "Title";
        #endregion

        private void OnEnable()
        {
            GameManager.BestScore = PlayerPrefs.GetInt("BestScore", 0);

            nowScoreText.text = GameManager.Score.ToString();
            int nowScore = GameManager.Score;

            if (GameManager.BestScore < nowScore)
            {
                GameManager.BestScore = nowScore;
                PlayerPrefs.SetInt("BestScore", nowScore);
                newText.SetActive(true);
            }

            bestScore.text = GameManager.BestScore.ToString();
        }


        public void Retry()
        {
            GameManager.ResetGame();
            fader.FadeTo(retryScene);
        }

        public void Menu()
        {
            fader.FadeTo(menuScene);
        }
    }
}

