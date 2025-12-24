using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ToolManager : MonoBehaviour
{
    [Header("Hand")]
    public Transform handAttach;

    [Header("Tools")]
    public GameObject magnifier;
    public GameObject uvLight;
    public GameObject thermometer;

    [Header("UI")]
    public GameObject toolCanvas; // 👈 Tool Canvas (public)

    private GameObject currentTool;

    public void EquipMagnifier()
    {
        EquipTool(magnifier);
    }

    public void EquipUV()
    {
        EquipTool(uvLight);
    }

    public void EquipThermometer()
    {
        EquipTool(thermometer);
    }

    private void EquipTool(GameObject tool)
    {
        if (currentTool == tool)
        {
            CloseCanvas();
            return;
        }

        // 기존 도구 해제
        if (currentTool != null)
        {
            var oldGrab = currentTool.GetComponent<XRGrabInteractable>();
            if (oldGrab != null && oldGrab.isSelected)
            {
                oldGrab.interactionManager.SelectExit(
                    oldGrab.interactorsSelecting[0],
                    oldGrab
                );
            }

            currentTool.SetActive(false);
        }

        // 새 도구 장착
        currentTool = tool;
        currentTool.SetActive(true);

        currentTool.transform.position = handAttach.position;
        currentTool.transform.rotation = handAttach.rotation;

        var grab = currentTool.GetComponent<XRGrabInteractable>();
        if (grab != null)
        {
            grab.interactionManager.SelectEnter(
                handAttach.GetComponent<IXRSelectInteractor>(),
                grab
            );
        }

        // 🔽 도구 장착 후 자동으로 Canvas 닫기
        CloseCanvas();
    }

    public void UnequipTool()
    {
        if (currentTool == null)
        {
            CloseCanvas();
            return;
        }

        var grab = currentTool.GetComponent<XRGrabInteractable>();
        if (grab != null && grab.isSelected)
        {
            grab.interactionManager.SelectExit(
                grab.interactorsSelecting[0],
                grab
            );
        }

        currentTool.SetActive(false);
        currentTool = null;

        CloseCanvas();
    }

    private void CloseCanvas()
    {
        if (toolCanvas != null)
            toolCanvas.SetActive(false);
    }

    // 버튼 하나로 Canvas 열고 닫고 싶을 때
    public void ToggleToolCanvas()
    {
        if (toolCanvas != null)
            toolCanvas.SetActive(!toolCanvas.activeSelf);
    }
}





