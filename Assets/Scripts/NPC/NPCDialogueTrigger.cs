using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour
{
    [Header("Type")]
    public DialogueTriggerType triggerType = DialogueTriggerType.NPC;

    [Header("NPC Dialogue (A/B/C용)")]
    public NPCDialogueData dialogueData;

    [Header("Assistant Dialogue (조수용)")]
    [TextArea] public string aboutA;
    [TextArea] public string aboutB;
    [TextArea] public string aboutC;
    [TextArea] public string aboutCrowbar;

    [Header("Animation")]
    public NPCAnimationController npcAnim;

    [Header("UI")]
    public GameObject headUI;
    public float showDistance = 2f;

    private Transform player;
    private bool isTalking = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        headUI.SetActive(false);
    }

    private void Update()
    {
        if (isTalking) return;

        float dist = Vector3.Distance(player.position, transform.position);
        headUI.SetActive(dist <= showDistance);
    }

    // ▶ 대화 버튼 눌렀을 때
    public void OnTalkPressed()
    {
        isTalking = true;
        headUI.SetActive(false);

        if (triggerType == DialogueTriggerType.NPC)
        {
            DialogueManager.Instance.StartDialogue(dialogueData, npcAnim);
        }
        else
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

    public void EndTalking()
    {
        isTalking = false;
    }
}




