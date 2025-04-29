using UnityEngine;

namespace MyBird
{
    public class PipeKiller : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //닿은 오브젝트들은 무조건 모두 삭제
            Debug.Log("파이프 제거");
            Destroy(collision.gameObject);
        }
    }

}
