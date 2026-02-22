using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance = null;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;
    
    [Header("Audio Clips - BGM")]
    public AudioClip main_Background;

    [Header("SFX Pooling")]
    [SerializeField] private int sfxPoolCount = 5; // 동시에 날 수 있는 효과음 개수
    private AudioSource[] sfxPool;
    private int poolIndex = 0;

    [Header("Audio Clips - SFX")]
    public AudioClip jump;
    public AudioClip eDamanged;
    public AudioClip die;
    public AudioClip land;
    public AudioClip getItem;

    void Awake()
    {
        if(null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

        void OnEnable()
    {
        // 씬 로드가 완료되었을 때 실행될 함수 등록
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // 메모리 누수 방지를 위해 해제
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {

        switch(scene.name)
        {
            case "Stage1":
            PlayBGM(main_Background);
            break;
        }
    }

    public static SoundManager Instance
    {
        get
        {
            if(null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    void Start()
    {
        // 시작 시 AudioSource 여러 개를 미리 생성 (풀링)
        sfxPool = new AudioSource[sfxPoolCount];
        for (int i = 0; i < sfxPoolCount; i++)
        {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            newSource.playOnAwake = false;
            // SFX 설정 (보통 2D 게임이면 Spatial Blend를 0으로)
            newSource.spatialBlend = 0f; 
            sfxPool[i] = newSource;
        }
    }

    public void PlayBGM(AudioClip clip, bool loop = true)
    {
        if (clip == null || bgmSource == null) return;

        // 이미 재생 중인 곡이면 중복 재생 방지
        if (bgmSource.clip == clip && bgmSource.isPlaying) return;

        bgmSource.clip = clip;
        bgmSource.loop = loop;
        bgmSource.Play();
    }

    public void PlaySFX(AudioClip clip, float volume = 0.5f, float pitch = 1.0f)
    {
        if (clip == null) return;

        // 풀에서 돌아가며 소스 선택
        AudioSource source = sfxPool[poolIndex];
        
        source.clip = clip;
        source.volume = volume;
        source.pitch = Random.Range(0.01f, 0.05f) + pitch;
        
        source.Play();

        // 인덱스 순환
        poolIndex = (poolIndex + 1) % sfxPoolCount;
    }
}
