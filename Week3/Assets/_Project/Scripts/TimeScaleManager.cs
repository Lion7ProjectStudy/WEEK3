using UnityEngine;
using System.Collections;

public class TimeScaleManager : MonoBehaviour
{
    private static TimeScaleManager instance = null;

    // 슬로우 모션 지속 시간 및 부드러운 복구를 위한 변수
    private bool isSlowMo = false;
    private float targetTimeScale = 1.0f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static TimeScaleManager Instance => instance;

    /// <param name="scale">목표 속도 (0.1f = 10% 속도)</param>
    /// <param name="duration">지속 시간</param>
    public void SlowDown(float scale, float duration)
    {
        if (isSlowMo) StopAllCoroutines(); // 이미 실행 중이면 초기화 후 재실행
        StartCoroutine(Co_SlowDown(scale, duration));
    }

    private IEnumerator Co_SlowDown(float scale, float duration)
    {
        isSlowMo = true;
        Time.timeScale = scale;
        // 고정 프레임 업데이트 간격도 조절해야 화면이 끊기지 않음 (실무 필수 팁)
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

        // 지정된 시간만큼 대기 (Realtime으로 대기해야 함)
        yield return new WaitForSecondsRealtime(duration);

        // 부드럽게 원래 속도로 복구
        float elapsed = 0f;
        float restoreDuration = 0.5f; // 복구되는 데 걸리는 시간
        while (elapsed < restoreDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Lerp(scale, 1.0f, elapsed / restoreDuration);
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            yield return null;
        }

        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;
        isSlowMo = false;
    }
}