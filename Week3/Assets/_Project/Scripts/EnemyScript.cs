using UnityEngine;
using NaughtyAttributes;

public class EnemyScript : MonoBehaviour
{
    // [Expandable]을 붙이면 SO를 더블클릭하지 않아도 인스펙터에서 바로 내용을 보고 수정할 수 있습니다.
    [Expandable]
    public EnemyData enemyData;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            
        }
    }
}