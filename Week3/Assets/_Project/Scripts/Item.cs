using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] ItemData data;
    private int itemCode;
    public bool isEaten = false;

    public Transform target; // 목표 대상
    public float smoothTime = 0.3f; // 도달 예상 시간 (작을수록 빠름)
    private Vector2 currentVelocity = Vector2.zero; // 내부 계산용 속도

    void Start()
    {
        itemCode = data.itemCode;
        isEaten = false;
    }

    void Update()
    {
        if(isEaten)
        {
            transform.position = Vector2.SmoothDamp(
            transform.position, 
            target.position, 
            ref currentVelocity, 
            smoothTime
            );
            if((int)transform.position.x == (int)target.transform.position.x && (int)transform.position.y == (int)target.transform.position.y)
            {
                Debug.Log("파괴단계진입");
                GameManager.Instance.GetItem(data);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Mag"))
        {
            Debug.Log("자석과 접촉");
            isEaten = true;
            target = collision.gameObject.transform;
        }
    }
}