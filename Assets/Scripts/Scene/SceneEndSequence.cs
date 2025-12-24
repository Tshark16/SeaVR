using UnityEngine;
using System.Collections;

public class SceneEndSequence : MonoBehaviour
{
    [Header("Monologue")]
    [TextArea]
    [SerializeField] private string monologueText;

    [Header("Timing")]
    [SerializeField] private float delayBeforeMonologue = 5f;
    [SerializeField] private float delayBeforeFade = 5f;
    [SerializeField] private float fadeDuration = 1.5f;

    [Header("Fade")]
    [SerializeField] private CanvasGroup fadeCanvas;

    private void Start()
    {
        // ✅ 씬 로드 완료 직후 자동 실행
        StartCoroutine(Sequence());
    }

    private IEnumerator Sequence()
    {
        // 1️⃣ 씬 로드 직후 대기
        yield return new WaitForSeconds(delayBeforeMonologue);

        // 2️⃣ 모노로그 출력
        MonologueManager.Instance.Show(monologueText, 3f);

        // 3️⃣ 다시 대기
        yield return new WaitForSeconds(delayBeforeFade);

        // 4️⃣ 검은 화면 페이드
        yield return StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeCanvas.alpha = t / fadeDuration;
            yield return null;
        }
        fadeCanvas.alpha = 1f;
    }
}

