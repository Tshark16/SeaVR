using UnityEngine;

public class ClueObject : MonoBehaviour
{
    public int clueIndex;
    private bool collected = false;

    public void MarkClueFound()
    {
        if (collected) return;
        collected = true;

        ClueManager.Instance.MarkClue(clueIndex);
        Debug.Log($"Clue {clueIndex} found!");
    }
}


