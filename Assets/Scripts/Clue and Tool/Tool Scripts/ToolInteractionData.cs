using UnityEngine;

[CreateAssetMenu(fileName = "NewToolInteraction", menuName = "Interaction/ToolInteractionData")]
public class ToolInteractionData : ScriptableObject
{
    public string toolName;

    [TextArea(2, 5)]
    public string monologueText;

    [TextArea(2, 5)]
    public string notebookRecord;

    public float coreTemperature = -1f; //온도계용
}

