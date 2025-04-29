using UnityEngine;

namespace MyBird
{
    public class MainMenu : MonoBehaviour
    {
        #region MyRegion
        public SceneFader fader;
        [SerializeField] private string loadToScene = "PlayScene";

        [SerializeField] private bool isCheat = false;
        #endregion

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                ResetSaveData();
            }
        }
        public void Play()
        {
            fader.FadeTo(loadToScene);
        }

        //치트키
        void ResetSaveData()
        {
            if (isCheat == false)
                return;

            PlayerPrefs.DeleteAll();
            isCheat = false;
        }
    }
}
