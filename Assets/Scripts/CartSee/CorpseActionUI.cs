using UnityEngine;
using System.Collections;

public class CorpseActionUI : MonoBehaviour
{
    [Header("Clue Index")]
    public int clueIndex = 0; // 이 UI가 담당하는 단서 번호

    public void OnClickMoveCorpse()
    {
        // 🔍 단서 발견 처리 (즉시)
        ClueManager.Instance.MarkClue(clueIndex);

        // 📒 노트 기록 (즉시)
        NotebookManager.Instance.AddNote(
            "수상한 카트발견ㅎㅎ!"
        );

        // 🗣 모노로그는 2.5초 뒤
        StartCoroutine(DelayedMonologue());
    }

    private IEnumerator DelayedMonologue()
    {
        yield return new WaitForSeconds(2.5f);

        MonologueManager.Instance.Show(
            "오옷 이걸로 시체를 옮겼을수도 있다. 주변에 혈흔이 있는지 보자"
        );
    }
}


