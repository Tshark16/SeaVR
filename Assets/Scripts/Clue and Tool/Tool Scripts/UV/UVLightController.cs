using UnityEngine;
using System.Collections;

public class UVLightController : MonoBehaviour
{
    [Header("Switch")]
    [SerializeField] private Transform switchTransform;
    [SerializeField] private float switchZOffset = 0.15f;

    [Header("Light")]
    [SerializeField] private Light spotLight;

    [Header("Clue")]
    [SerializeField] private int clueIndex = 1;

    [Header("Text")]
    [TextArea]
    [SerializeField] private string monologueText;

    [TextArea]
    [SerializeField] private string notebookText;

    [Header("Timing")]
    [SerializeField] private float revealDelay = 5f;

    private Vector3 switchInitialLocalPos;
    private bool isOn = false;
    private bool hasRecorded = false;
    private Coroutine delayRoutine;

    private void Awake()
    {
        switchInitialLocalPos = switchTransform.localPosition;
        SetUV(false);
    }

    public void ToggleUV()
    {
        SetUV(!isOn);
    }

    public void SetUV(bool on)
    {
        isOn = on;

        // 스위치 위치
        switchTransform.localPosition = on
            ? switchInitialLocalPos + new Vector3(0f, 0f, switchZOffset)
            : switchInitialLocalPos;

        // 라이트
        if (spotLight != null)
            spotLight.enabled = on;

        // 🔍 단서 발견 (켜지는 순간)
        if (on)
        {
            ClueManager.Instance.MarkClue(clueIndex);
        }

        // ⏱ 타이머
        if (on && !hasRecorded && delayRoutine == null)
        {
            delayRoutine = StartCoroutine(DelayedReveal());
        }

        if (!on && delayRoutine != null)
        {
            StopCoroutine(delayRoutine);
            delayRoutine = null;
        }
    }

    private IEnumerator DelayedReveal()
    {
        yield return new WaitForSeconds(revealDelay);

        hasRecorded = true;
        delayRoutine = null;

        if (MonologueManager.Instance != null)
            MonologueManager.Instance.Show(monologueText);

        if (NotebookManager.Instance != null)
            NotebookManager.Instance.AddNote(notebookText);
    }
}




