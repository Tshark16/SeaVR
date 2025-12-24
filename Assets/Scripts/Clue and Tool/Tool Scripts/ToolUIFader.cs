using UnityEngine;

public class ToolUIFader : MonoBehaviour
{
    [Header("References")]
    public Transform palm;
    public Transform head;
    public CanvasGroup canvasGroup;

    [Header("Settings")]
    public float dotThreshold = 0.7f;
    public float fadeSpeed = 5f;

    private float targetAlpha = 0f;
    private bool previousFacing = false; // ★ 상태 저장

    void Update()
    {
        if (palm == null || head == null || canvasGroup == null)
            return;

        Vector3 toHead = (head.position - palm.position).normalized;
        float dot = Vector3.Dot(palm.forward, toHead);

        bool facing = dot > dotThreshold;

        // ★ 상태가 바뀔 때만 로그 출력
        if (facing != previousFacing)
        {
            if (facing)
                Debug.Log("캔버스 등장!");
            else
                Debug.Log("캔버스 숨김!");

            previousFacing = facing; // 상태 업데이트
        }

        targetAlpha = facing ? 1f : 0f;

        // 페이드 처리
        canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, Time.deltaTime * fadeSpeed);
        canvasGroup.alpha = Mathf.Clamp01(canvasGroup.alpha);   // ★ alpha 보정


        // UI 클릭 가능 여부 설정
        canvasGroup.blocksRaycasts = canvasGroup.alpha > 0.9f;
        canvasGroup.interactable = canvasGroup.alpha > 0.9f;
    }
}
