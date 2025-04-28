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

        //스폰 간격
        [SerializeField] private float maxSpawnTime = 1.05f;
        [SerializeField] private float minSpawnTime = 0.95f;
        #endregion

        private void Start()
        {
            //InvokeRepeating("SpawnPipe", 1f, pipeTimer);
        }
        private void Update()
        {
            if (!GameManager.IsStart)
                return;

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
            pipeTimer = Random.Range(minSpawnTime, maxSpawnTime);
        }
    }

}
