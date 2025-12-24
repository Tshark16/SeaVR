using UnityEngine;
using System.Collections;

public class ClueCompletionMonologue : MonoBehaviour
{
    [Header("Completion Monologue")]
    [TextArea]
    public string completionText;

    [SerializeField] private float delay = 4f; // ⭐ 여기서 조절

    public void Play()
    {
        StartCoroutine(PlayDelayed());
    }

    private IEnumerator PlayDelayed()
    {
        yield return new WaitForSeconds(delay);

        MonologueManager.Instance.Show(completionText, 3f);
    }
}



