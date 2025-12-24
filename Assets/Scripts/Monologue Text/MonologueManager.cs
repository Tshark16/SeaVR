using UnityEngine;
using TMPro;
using System.Collections;

public class MonologueManager : MonoBehaviour
{
    public static MonologueManager Instance;

    [SerializeField] TMP_Text monologueTMP;
    [SerializeField] CanvasGroup canvasGroup;

    [Header("Typing Settings")]
    public float fadeTime = 0.3f;
    public float letterInterval = 0.04f; // 글자 출력 속도

    Coroutine currentRoutine;

    private void Awake()
    {
        Instance = this;
        canvasGroup.alpha = 0;
        monologueTMP.text = "";
    }

    public void Show(string text, float holdDuration = 4f)
    {
        Debug.Log("Show 호출됨 / text = " + text);

        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(ShowRoutine(text, holdDuration));
    }

    IEnumerator ShowRoutine(string text, float holdDuration)
    {
        // Fade In
        float t = 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = t / fadeTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;

        // Typing
        monologueTMP.text = "";
        for (int i = 0; i < text.Length; i++)
        {
            monologueTMP.text += text[i];
            yield return new WaitForSeconds(letterInterval);
        }

        // Hold
        yield return new WaitForSeconds(holdDuration);

        // Fade Out
        t = 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = 1f - (t / fadeTime);
            yield return null;
        }
        canvasGroup.alpha = 0f;

        monologueTMP.text = "";
        currentRoutine = null;
    }
}

