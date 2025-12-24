using UnityEngine;

public class WorldCanvasActivator : MonoBehaviour
{
    [SerializeField] GameObject targetCanvas;

    // 켜기
    public void Open()
    {
        if (targetCanvas != null)
            targetCanvas.SetActive(true);
    }

    // 끄기
    public void Close()
    {
        if (targetCanvas != null)
            targetCanvas.SetActive(false);
    }

    // 토글 (한 버튼으로 열고/닫기)
    public void Toggle()
    {
        if (targetCanvas != null)
            targetCanvas.SetActive(!targetCanvas.activeSelf);
    }
}

