using UnityEngine;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int initialSize = 10;

    Queue<GameObject> pool = new Queue<GameObject>();

    void Start()
    {
        for(int i = 0; i< initialSize; i++)
        {
            var b = Instantiate(bulletPrefab, transform);
            b.SetActive(false);
            pool.Enqueue(b);
        }
    }

    public GameObject GetBullet()
    {
        GameObject b;
        if(pool.Count >0 ) b = pool.Dequeue();
        else b = Instantiate(bulletPrefab, transform);
        b.SetActive(true);
        
        return b;
    }

    public void ReturnBullet(GameObject b)
    {
        b.SetActive(false);
        pool.Enqueue(b);
    }
}
