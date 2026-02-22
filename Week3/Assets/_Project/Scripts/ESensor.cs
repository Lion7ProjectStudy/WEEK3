using System.Collections.Generic;
using UnityEngine;

public class ESensor : MonoBehaviour
{
    bool isTriggerOn = false;
    [SerializeField] BulletPool pool; // 인스펙터에서 BulletPool 오브젝트를 드래그해서 할당
    public Transform firePoint;      // 총알이 나갈 위치
    [SerializeField] float fireRate = 1f; // 초당 발사 횟수 (예: 1초에 1번)
    private float fireTimer = 0f;   // 시간을 잴 타이머

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 2. 타이머가 설정한 발사 간격(1/fireRate)보다 커지면 발사
            if (fireTimer >= 1f / fireRate)
            {
                Shoot(collision.transform.position);
                fireTimer = 0f; // 발사 후 타이머 초기화
            }
        }
    }

    void Shoot(Vector3 playerPos)
    {
        GameObject obj = pool.GetBullet();
        obj.transform.position = transform.position;

        Bullet bullet = obj.GetComponent<Bullet>();
        bullet.SetPool(pool);
        
        // 지난번에 만든 유도 발사 함수 호출
        bullet.ShootTowardsPlayer(transform.position, playerPos);
    }
}
