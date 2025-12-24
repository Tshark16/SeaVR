using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ClueGrabInteraction : MonoBehaviour
{
    public ClueInteractionData interactionData;

    [Header("Clue")]
    [SerializeField] private int clueIndex = 2;

    private void Start()
    {
        var grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // 🔍 단서 발견 (즉시)
        ClueManager.Instance.MarkClue(clueIndex);

        // 🗣 모노로그
        MonologueManager.Instance.Show(interactionData.monologueText);
        Debug.Log("모노로그 보여짐");

        // 📒 노트 기록
        NotebookManager.Instance.AddNote(interactionData.notebookRecord);
    }
}


