using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public static FadeController Instance;

    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 1f;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        // 최초 실행 시(게임 처음 시작 씬)도 페이드 인
        fadeCanvasGroup.alpha = 1f;
        StartCoroutine(FadeIn());
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬이 바뀔 때마다 페이드 인
        fadeCanvasGroup.alpha = 1f;
        StartCoroutine(FadeIn());
    }

    public void FadeOutAndLoad(string sceneName)
    {
        StartCoroutine(FadeOutRoutine(sceneName));
    }

    private IEnumerator FadeOutRoutine(string sceneName)
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeCanvasGroup.alpha = t / fadeDuration; // 0 → 1
            yield return null;
        }

        fadeCanvasGroup.alpha = 1f;

        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator FadeIn()
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeCanvasGroup.alpha = 1f - (t / fadeDuration); // 1 → 0
            yield return null;
        }

        fadeCanvasGroup.alpha = 0f;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}


