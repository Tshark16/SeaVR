using UnityEngine;

public class AssistantNPC : MonoBehaviour
{
    [Header("Dialogue Text")]
    [TextArea] public string aboutA;
    [TextArea] public string aboutB;
    [TextArea] public string aboutC;
    [TextArea] public string aboutCrowbar;

    [Header("Animation")]
    public NPCAnimationController npcAnim;

    // ▶ 대화 시작
    public void Talk()
    {
        DialogueManager.Instance.StartAssistantDialogue(
            aboutA,
            aboutB,
            aboutC,
            aboutCrowbar,
            npcAnim
        );
    }
}


