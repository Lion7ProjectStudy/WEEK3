using Unity.VisualScripting;
using Unity.Cinemachine;
using UnityEngine;
using MoreMountains.Feedbacks;

public class Player : MonoBehaviour
{
    [Header("컴포넌트")]
    Rigidbody2D rd;
    

    [Header("스테이터스")]
    public PlayerData playerData;
    [SerializeField] float level; 
    [SerializeField] float power;
    [SerializeField] float spd;
    [SerializeField] int maxJumpCount;
    [SerializeField] int jumpCount;

    [Header("인벤토리")]

    [Header("자석 범위 콜라이더")]
    [SerializeField] CircleCollider2D MagCd;

    // 조작
    bool isJumpPressed = false;
    float timer = 0;
    float timerInterval = 1;

    [Header("파괴 이펙트")]
    public GameObject destroyEf;
    [SerializeField] private CinemachineImpulseSource _source;

    void Awake()
    {
        level = playerData.level;
        power = playerData.power;
        spd = playerData.spd;
        maxJumpCount = playerData.maxJumpCount;
    }

    void Start()
    {
        rd = gameObject.GetComponent<Rigidbody2D>();
        jumpCount = maxJumpCount;
    }

    void Update()
    {
        // 1. 입력은 매 프레임 체크 (정확도 100%)
        if (Input.GetKeyDown(KeyCode.W))
        {
            isJumpPressed = true;
        }
    }

    void FixedUpdate()
    {
        // 2. 물리 연산 시점에서 입력이 있었다면 점프 실행
        if (isJumpPressed)
        {
            Jump();
            isJumpPressed = false; // 실행 후 다시 초기화
        }
        Move();
    }

    void Jump()
    {
        if(jumpCount > 0)
        {
            rd.AddForce(Vector2.up * power, ForceMode2D.Impulse);
            jumpCount --;
            Debug.Log($"잔여 점프 횟수 {jumpCount}");
        }        
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rd.linearVelocityX -= spd*Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rd.linearVelocityX += spd*Time.deltaTime;
        }
    }

    public void JumpCountRst()
    {   
        jumpCount = maxJumpCount;
        Debug.Log("점프 횟수 초기화");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 방법 1: 상대 속도로 계산 (가장 직관적)
        float collisionForce = collision.relativeVelocity.magnitude;
        Enemy target = collision.gameObject.GetComponent<Enemy>();

        if (target != null)
        {  
            // 3. 충돌 강도(relativeVelocity)에 비례해서 데미지 입히기
            float damage = collisionForce;
            var mmFeedback = target.GetComponent<MoreMountains.Feedbacks.MMF_Player>();
            if (mmFeedback != null)
            {
                mmFeedback.PlayFeedbacks();
            }
            target.TakeDamage(damage);
        }
    }


    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Hole"))
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
            if(timer >= timerInterval)
            {
                timer = 0;
                Destroy(gameObject);
                GameObject ef = Instantiate(destroyEf, transform.position, Quaternion.identity);
                Destroy(ef, 1f);

                GameManager.Instance.EnterHole();
            }
        }

        if(collision.gameObject.CompareTag("Home"))
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
            if(timer >= timerInterval)
            {
                timer = 0;
                GameManager.Instance.EnterHome();
            }
        }
    }

    private void DoImpulse()
    {
        _source.GenerateImpulse();
    }

    // 아이템 처리
    void LevelUp()
    {
        
    }

    public void MagRangeUp(float mag)
    {
        MagCd.radius += mag;
    }
}
