using UnityEngine;

public class GSensor : MonoBehaviour
{
    Collider2D gSensor;
    [SerializeField] Player player;
    [SerializeField] GameObject gEf;

    void Start()
    {
        gSensor = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Enemy"))
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.land);
            player.JumpCountRst();
            GameObject ef = Instantiate(gEf, transform.position, Quaternion.identity);
            Destroy(ef, 1f);
        }        
    }
}
