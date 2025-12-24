using UnityEngine;
using System.Collections;

public class GazeShowCanvas : MonoBehaviour
{
    public Transform vrCamera;      // XR Camera
    public CanvasGroup canvasGroup; // ¹öÆ° Canvas
    public float gazeTime = 1.5f;
    public float maxDistance = 3f;
    public float fadeSpeed = 2f;

    float timer = 0f;
    bool visible = false;

    void Start()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    void Update()
    {
        Ray ray = new Ray(vrCamera.position, vrCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance) &&
            hit.transform == transform)
        {
            timer += Time.deltaTime;

            if (timer >= gazeTime && !visible)
                Show();
        }
        else
        {
            timer = 0f;
            if (visible)
                Hide();
        }
    }

    void Show()
    {
        visible = true;
        StopAllCoroutines();
        StartCoroutine(Fade(1));
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    void Hide()
    {
        visible = false;
        StopAllCoroutines();
        StartCoroutine(Fade(0));
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    IEnumerator Fade(float target)
    {
        while (!Mathf.Approximately(canvasGroup.alpha, target))
        {
            canvasGroup.alpha = Mathf.MoveTowards(
                canvasGroup.alpha,
                target,
                Time.deltaTime * fadeSpeed
            );
            yield return null;
        }
    }
}

