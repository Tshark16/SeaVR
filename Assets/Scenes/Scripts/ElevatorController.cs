using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ElevatorSequenceController : MonoBehaviour
{
    [Header("Scene")]
    [SerializeField] private string nextSceneName;

    [Header("Fade")]
    [SerializeField] private CanvasGroup fadeCanvas;
    [SerializeField] private float fadeDuration = 1.0f;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip dingClip;
    [SerializeField] private AudioClip walkClip;
    [SerializeField] private AudioClip doorOpenClip;

    private XRSimpleInteractable interactable;
    private bool isPlaying = false;


    private void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();

        if (interactable == null)
        {
            Debug.LogError("XRSimpleInteractable이 이 오브젝트에 없습니다.");
            return;
        }

        interactable.selectEntered.AddListener(OnPressed);
    }


    private void OnDestroy()
    {
        interactable.selectEntered.RemoveListener(OnPressed);
    }

    private void OnPressed(SelectEnterEventArgs args)
    {
        if (isPlaying) return;
        isPlaying = true;

        StartCoroutine(ElevatorSequence());
    }

    private IEnumerator ElevatorSequence()
    {
        // 1. Fade In
        yield return StartCoroutine(Fade(0f, 1f));

        // 2. Ding
        if (dingClip != null)
        {
            audioSource.PlayOneShot(dingClip);
            yield return new WaitForSeconds(dingClip.length);
        }

        // 3. Walk sound
        if (walkClip != null)
        {
            audioSource.PlayOneShot(walkClip);
            yield return new WaitForSeconds(walkClip.length);
        }

        // 4. Door open sound
        if (doorOpenClip != null)
        {
            audioSource.PlayOneShot(doorOpenClip);
            yield return new WaitForSeconds(doorOpenClip.length);
        }

        // 5. Scene load
        SceneManager.LoadScene(nextSceneName);
    }

    private IEnumerator Fade(float from, float to)
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(from, to, t / fadeDuration);
            yield return null;
        }
        fadeCanvas.alpha = to;
    }
}

