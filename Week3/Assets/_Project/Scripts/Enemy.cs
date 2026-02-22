using UnityEngine;
using NaughtyAttributes;

public class Enemy : MonoBehaviour
{
    // [Expandable]을 붙이면 SO를 더블클릭하지 않아도 인스펙터에서 바로 내용을 보고 수정할 수 있습니다.
    [Expandable]
    public EnemyData enemyData;

    private float currentHp;

    [Header("파괴 이펙트")]
    public GameObject destroyEf;

    void Start()
    {
        // 데이터가 없는 경우를 대비한 안전장치
        if (enemyData != null)
        {
            currentHp = enemyData.enemyHp;
        }
    }

    public float GetDamage() => currentHp;

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        if(currentHp<=0) Die();
    }

    void Die()
    {
        // 1. 파괴 이펙트 생성
        if (destroyEf != null)
        {
            Instantiate(destroyEf, transform.position, Quaternion.identity);
        }

        // 2. 아이템 드랍 로직 실행
        DropItem();

        // 3. 적 오브젝트 제거
        Destroy(gameObject);
    }

    void DropItem()
    {
        // 데이터 검증: EnemyData가 없거나, DropTable이 연결 안 되어 있으면 패스
        if (enemyData == null || enemyData.dropTable == null) return;

        // 드랍 테이블에게 아이템 추첨 요청
        ItemData selectedItem = enemyData.dropTable.PickItem();

        // 꽝이 아니면서(null), 실제 프리팹(item)이 존재하는 경우에만 생성
        if (selectedItem != null && selectedItem.item != null)
        {
            // 아이템 생성 (위치는 적의 위치, 회전은 기본값)
            Instantiate(selectedItem.item, transform.position, Quaternion.identity);
            Debug.Log($"아이템 드랍 성공: {selectedItem.itemName}");
        }
        else
        {
            Debug.Log("아이템 드랍: 꽝 (또는 데이터 없음)");
        }
    }

}