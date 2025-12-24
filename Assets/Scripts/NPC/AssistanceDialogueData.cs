using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Assistant Dialogue")]
public class AssistantDialogueData : ScriptableObject
{
    [TextArea] public string aboutA;
    [TextArea] public string aboutB;
    [TextArea] public string aboutC;
    [TextArea] public string aboutCrowbar;
}