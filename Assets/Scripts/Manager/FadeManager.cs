using UnityEngine;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;

    public CanvasGroup fadeCanvas;
    public float fadeSpeed = 1f;

    private void Awake()
    {
        Instance = this;
    }

    public void FadeOut(System.Action onComplete = null)
    {
        StartCoroutine(FadeRoutine(1f, onComplete));
    }

    public void FadeIn(System.Action onComplete = null)
    {
        StartCoroutine(FadeRoutine(0f, onComplete));
    }

    private System.Collections.IEnumerator FadeRoutine(float target, System.Action onComplete)
    {
        while (!Mathf.Approximately(fadeCanvas.alpha, target))
        {
            fadeCanvas.alpha = Mathf.MoveTowards(
                fadeCanvas.alpha,
                target,
                Time.deltaTime * fadeSpeed
            );
            yield return null;
        }

        onComplete?.Invoke();
    }
}

