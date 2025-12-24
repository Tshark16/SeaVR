using UnityEngine;

public enum NPCType { A, B, C }

[CreateAssetMenu(menuName = "Dialogue/NPCDialogueData")]
public class NPCDialogueData : ScriptableObject
{
    public NPCType npcType;

    [TextArea] public string introAnswer;

    [Header("Ask A")]
    [TextArea] public string aboutA;

    [Header("Ask B")]
    [TextArea] public string aboutB;

    [Header("Ask C")]
    [TextArea] public string aboutC;

    [Header("Notebook Record")]
    [TextArea] public string notebookRecord;
}




