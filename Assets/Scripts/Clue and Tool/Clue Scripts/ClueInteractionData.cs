using UnityEngine;

[CreateAssetMenu(fileName = "NewClue", menuName = "Interaction/ClueInteractionData")]
public class ClueInteractionData : ScriptableObject
{
    public string clueName;

    [TextArea(2, 5)]
    public string monologueText;

    [TextArea(2, 5)]
    public string notebookRecord;
}

