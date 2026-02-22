using UnityEngine;

public class GSensor : MonoBehaviour
{
    Collider2D gSensor;
    [SerializeField] Player player;

    void Start()
    {
        gSensor = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Platform"))
        {
            player.JumpCountRst();
        }        
    }
}
