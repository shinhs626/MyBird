using UnityEngine;

namespace MyBird
{
    //Pipe 생성기 - 1초마다 기둥 하나 씩 생성
    public class PipeSpawner : MonoBehaviour
    {
        #region Variables
        //기둥 프리팹
        public GameObject pipePrefab;
        private float countdown;
        private float pipeTimer;

        //스폰 위치
        [SerializeField] private float maxSpawnY = 3.3f;
        [SerializeField] private float minSpawnY = -1.6f;

        //스폰 간격 0.95 ~ 1.05 -> 0.90 ~ 1.0 -> 0.8 ~ 0.9
        [SerializeField] private float maxSpawnTime = 1.05f;
        [SerializeField] private float minSpawnTime = 0.95f;
        [SerializeField] private float levelTime = 0.05f;
        #endregion

        private void Start()
        {
            //InvokeRepeating("SpawnPipe", 1f, pipeTimer);
        }
        private void Update()
        {
            if (!GameManager.IsStart)
                return;

            if (GameManager.IsDeath)
            {
                return;
            }

            countdown += Time.deltaTime;
            if (countdown >= pipeTimer)
            {
                SpawnPipe();
                ResetTimer();
            }

        }

        void SpawnPipe()
        {
            //1초마다 기둥 하나 씩 생성, 게임 시작시(IsStart == true)
            if (GameManager.IsStart == false)
                return;

            float spawnY = this.transform.position.y + Random.Range(minSpawnY, maxSpawnY);
            Vector3 spawnPosition = new Vector3(this.transform.position.x, spawnY, this.transform.position.z);
            Instantiate(pipePrefab, spawnPosition, Quaternion.identity);
        }
        
        void ResetTimer()
        {
            countdown = 0f;
            float levelingValue = (int)(GameManager.Score / 10) * levelTime;

            pipeTimer = Random.Range(minSpawnTime - levelingValue, maxSpawnTime - levelingValue);
        }
    }

}
