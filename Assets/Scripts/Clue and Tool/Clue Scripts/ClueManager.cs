using UnityEngine;

public class ClueManager : MonoBehaviour
{
    public static ClueManager Instance;

    [Header("Clues")]
    [Tooltip("이 씬의 단서 개수만큼 크기 설정")]
    public bool[] clues;

    private bool hasCompleted = false; // 중복 방지

    private void Awake()
    {
        // Singleton 안전 처리
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 외부에서 단서 발견 시 호출
    public void MarkClue(int index)
    {
        Debug.Log($"MarkClue 호출: {index}");

        if (hasCompleted) return;
        if (index < 0 || index >= clues.Length) return;
        if (clues[index]) return;

        clues[index] = true;

        if (AllCluesFound())
        {
            hasCompleted = true;
            OnAllCluesFound();
        }
    }


    private bool AllCluesFound()
    {
        for (int i = 0; i < clues.Length; i++)
        {
            if (!clues[i]) return false;
        }
        return true;
    }

    // 🔽 네가 유지하고 싶다던 부분 (그대로 사용)
    private void OnAllCluesFound()
    {
        Debug.Log("모든 단서 발견");

        ClueCompletionMonologue completion =
            GetComponent<ClueCompletionMonologue>();

        if (completion != null)
        {
            completion.Play();
        }
        else
        {
            Debug.LogWarning("ClueCompletionMonologue가 없습니다.");
        }
    }
}



