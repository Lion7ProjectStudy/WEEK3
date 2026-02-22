using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("총알 설정")]
    public float speed = 8.0f;
    public float lifeTime = 2.0f;
    Vector2 direction;

    float timer;
    BulletPool pool;

    public void SetPool(BulletPool pool)
    {
        this.pool = pool;
    }

    void OEnable()
    {
        timer = 0f;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            ReturnToPool();
        }
    }

    public void ShootTowardsPlayer(Vector2 startPos, Vector2 playerPos)
    {
        // 1. 방향 벡터 계산 (목적지 - 출발지)
        Vector2 dir = (playerPos - startPos).normalized;
        direction = dir;

        // 2. 각도 계산 (Atan2는 라디안을 반환하므로 Rad2Deg를 곱함)
        // 총알의 긴 부분이 오른쪽(X축)을 향하고 있다면 0도 기준입니다.
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // 3. 회전 적용 (2D이므로 Z축을 회전시킴)
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void OnBecameInvisible()
    {
        ReturnToPool();
    }

    void ReturnToPool()
    {
        if(pool != null) pool.ReturnBullet(gameObject);
        else gameObject.SetActive(false);
    }
}
