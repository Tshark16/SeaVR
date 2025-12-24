using UnityEngine;
using System.Collections;

public class GazeButtonActivator : MonoBehaviour
{
    public Transform vrCamera;           // XR Camera
    public float gazeTime = 1.5f;         // 바라보는 시간
    public float maxDistance = 3f;

    public CanvasGroup buttonCanvas;     // 버튼 캔버스
    public float fadeSpeed = 2f;

    float gazeTimer = 0f;
    bool isShowing = false;

    void Start()
    {
        buttonCanvas.alpha = 0f;
        buttonCanvas.interactable = false;
        buttonCanvas.blocksRaycasts = false;
    }

    void Update()
    {
        Ray ray = new Ray(vrCamera.position, vrCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            if (hit.transform == transform)
            {
                gazeTimer += Time.deltaTime;
                if (gazeTimer >= gazeTime && !isShowing)
                {
                    ShowButton();
                }
                return;
            }
        }

        // 시선이 벗어났을 때
        gazeTimer = 0f;
        if (isShowing)
            HideButton();
    }

    void ShowButton()
    {
        isShowing = true;
        StopAllCoroutines();
        StartCoroutine(FadeCanvas(1f));
        buttonCanvas.interactable = true;
        buttonCanvas.blocksRaycasts = true;
    }

    void HideButton()
    {
        isShowing = false;
        StopAllCoroutines();
        StartCoroutine(FadeCanvas(0f));
        buttonCanvas.interactable = false;
        buttonCanvas.blocksRaycasts = false;
    }

    IEnumerator FadeCanvas(float target)
    {
        while (!Mathf.Approximately(buttonCanvas.alpha, target))
        {
            buttonCanvas.alpha = Mathf.MoveTowards(
                buttonCanvas.alpha,
                target,
                Time.deltaTime * fadeSpeed
            );
            yield return null;
        }
    }

    void LateUpdate()
    {
        if (!isShowing) return;

        Vector3 dir = buttonCanvas.transform.position - vrCamera.position;
        dir.y = 0f; // 상하 기울기 고정 (원하면 제거)

        buttonCanvas.transform.rotation =
            Quaternion.LookRotation(dir);
    }

}
