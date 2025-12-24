using UnityEngine;

public class SettingUIManager : MonoBehaviour
{
    public static SettingUIManager Instance;
    [SerializeField] GameObject settingCanvas;

    private bool isOpen = false;

    private void Awake()
    {
        Instance = this;
        settingCanvas.SetActive(false);
    }

    public void ToggleSettings()
    {
        isOpen = !isOpen;
        settingCanvas.SetActive(isOpen);
    }
}