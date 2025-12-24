using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    private bool isAssistantDialogue = false;

    public static DialogueManager Instance;
    private int askedCount;

    [Header("UI")]
    public Canvas dialogueCanvas;
    public TMP_Text dialogueText;

    [Header("NPC Buttons (A/B/C)")]
    public Button askIntroButton;
    public Button askOtherButton1;
    public Button askOtherButton2;

    [Header("Assistant Buttons")]
    public Button askButtonA;
    public Button askButtonB;
    public Button askButtonC;
    public Button askButtonCrowbar;

    [Header("Common")]
    public Button endTalkButton;
    public float typingSpeed = 0.03f;

    private NPCDialogueData currentData;
    private Coroutine typingRoutine;

    [Header("After Dialogue Monologue")]
    [TextArea]
    [SerializeField] private string afterDialogueMonologue;

    [SerializeField] private float afterDialogueDelay = 4f;


    // ⭐ 현재 대화 중인 NPC 애니메이션
    private NPCAnimationController currentNPCAnim;

    private void Awake()
    {
        Instance = this;
        dialogueCanvas.gameObject.SetActive(false);
    }

    // =================================================
    // NPC A / B / C 대화
    // =================================================
    public void StartDialogue(NPCDialogueData data, NPCAnimationController npcAnim)
    {
        isAssistantDialogue = false;
        Debug.Log("NPC StartDialogue 호출됨");
        Debug.Log($"data: {data}, npcAnim: {npcAnim}");

        currentNPCAnim = npcAnim;
        currentNPCAnim.SetTalking(true);

        currentData = data;
        dialogueCanvas.gameObject.SetActive(true);
        dialogueText.text = "";

        HideAllButtons();
        SetupNPCButtons(data);
    }

    private void SetupNPCButtons(NPCDialogueData data)
    {
        Debug.Log("SetupNPCButtons 호출됨");
        askedCount = 0;
        askIntroButton.gameObject.SetActive(true);
        askOtherButton1.gameObject.SetActive(true);
        askOtherButton2.gameObject.SetActive(true);
        endTalkButton.gameObject.SetActive(false);

        askIntroButton.onClick.RemoveAllListeners();
        askOtherButton1.onClick.RemoveAllListeners();
        askOtherButton2.onClick.RemoveAllListeners();
        endTalkButton.onClick.RemoveAllListeners();

        askIntroButton.onClick.AddListener(() =>
            ShowTextTyped(data.introAnswer, askIntroButton));

        switch (data.npcType)
        {
            case NPCType.A:
                askOtherButton1.GetComponentInChildren<TMP_Text>().text = "Ask B";
                askOtherButton2.GetComponentInChildren<TMP_Text>().text = "Ask C";
                askOtherButton1.onClick.AddListener(() =>
                    ShowTextTyped(data.aboutB, askOtherButton1));
                askOtherButton2.onClick.AddListener(() =>
                    ShowTextTyped(data.aboutC, askOtherButton2));
                break;

            case NPCType.B:
                askOtherButton1.GetComponentInChildren<TMP_Text>().text = "Ask A";
                askOtherButton2.GetComponentInChildren<TMP_Text>().text = "Ask C";
                askOtherButton1.onClick.AddListener(() =>
                    ShowTextTyped(data.aboutA, askOtherButton1));
                askOtherButton2.onClick.AddListener(() =>
                    ShowTextTyped(data.aboutC, askOtherButton2));
                break;

            case NPCType.C:
                askOtherButton1.GetComponentInChildren<TMP_Text>().text = "Ask A";
                askOtherButton2.GetComponentInChildren<TMP_Text>().text = "Ask B";
                askOtherButton1.onClick.AddListener(() =>
                    ShowTextTyped(data.aboutA, askOtherButton1));
                askOtherButton2.onClick.AddListener(() =>
                    ShowTextTyped(data.aboutB, askOtherButton2));
                break;
        }

        endTalkButton.onClick.AddListener(EndDialogue);
    }

    // =================================================
    // Assistant 대화
    // =================================================
    public void StartAssistantDialogue(
        string aboutA,
        string aboutB,
        string aboutC,
        string aboutCrowbar,
        NPCAnimationController npcAnim
    )
    {
        isAssistantDialogue = true;
        currentNPCAnim = npcAnim;
        currentNPCAnim.SetTalking(true);

        dialogueCanvas.gameObject.SetActive(true);
        dialogueText.text = "";

        HideAllButtons();

        askButtonA.gameObject.SetActive(true);
        askButtonB.gameObject.SetActive(true);
        askButtonC.gameObject.SetActive(true);
        askButtonCrowbar.gameObject.SetActive(true);
        endTalkButton.gameObject.SetActive(true);

        askButtonA.onClick.RemoveAllListeners();
        askButtonB.onClick.RemoveAllListeners();
        askButtonC.onClick.RemoveAllListeners();
        askButtonCrowbar.onClick.RemoveAllListeners();
        endTalkButton.onClick.RemoveAllListeners();

        askButtonA.GetComponentInChildren<TMP_Text>().text = "A에 대해 묻기";
        askButtonB.GetComponentInChildren<TMP_Text>().text = "B에 대해 묻기";
        askButtonC.GetComponentInChildren<TMP_Text>().text = "C에 대해 묻기";
        askButtonCrowbar.GetComponentInChildren<TMP_Text>().text = "쇠지렛대에 대해 묻기";

        askButtonA.onClick.AddListener(() =>
            ShowTextTyped(aboutA, askButtonA));
        askButtonB.onClick.AddListener(() =>
            ShowTextTyped(aboutB, askButtonB));
        askButtonC.onClick.AddListener(() =>
            ShowTextTyped(aboutC, askButtonC));
        askButtonCrowbar.onClick.AddListener(() =>
            ShowTextTyped(aboutCrowbar, askButtonCrowbar));

        endTalkButton.onClick.AddListener(EndDialogue);
    }

    // =================================================
    // 공통 로직
    // =================================================
    private void ShowTextTyped(string text, Button pressedButton)
    {
        if (typingRoutine != null)
            StopCoroutine(typingRoutine);

        typingRoutine = StartCoroutine(TypeText(text));

        pressedButton.gameObject.SetActive(false);

        // ⭐ 질문 횟수 증가
        askedCount++;

        // ⭐ 3번 질문했으면 End 버튼 등장
        if (askedCount >= 3)
        {
            endTalkButton.gameObject.SetActive(true);
        }
    }


    private IEnumerator TypeText(string line)
    {
        dialogueText.text = "";
        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void HideAllButtons()
    {
        askIntroButton.gameObject.SetActive(false);
        askOtherButton1.gameObject.SetActive(false);
        askOtherButton2.gameObject.SetActive(false);

        askButtonA.gameObject.SetActive(false);
        askButtonB.gameObject.SetActive(false);
        askButtonC.gameObject.SetActive(false);
        askButtonCrowbar.gameObject.SetActive(false);

        endTalkButton.gameObject.SetActive(false);
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        dialogueCanvas.gameObject.SetActive(false);
        dialogueText.text = "";

        if (currentNPCAnim != null)
        {
            currentNPCAnim.SetTalking(false);
            currentNPCAnim = null;
        }

        // ✅ 조수일 때만 실행
        if (isAssistantDialogue && !string.IsNullOrEmpty(afterDialogueMonologue))
        {
            StartCoroutine(ShowAfterDialogueMonologue());
        }
    }


    private IEnumerator ShowAfterDialogueMonologue()
    {
        yield return new WaitForSeconds(afterDialogueDelay);

        MonologueManager.Instance.Show(afterDialogueMonologue, 3f);
    }


}












