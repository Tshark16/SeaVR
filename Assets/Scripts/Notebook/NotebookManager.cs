using UnityEngine;
using TMPro;

public class NotebookManager : MonoBehaviour
{
    public static NotebookManager Instance;

    public Transform contentParent;           // ScrollView의 Content
    public GameObject notebookEntryPrefab;    // 한 줄 텍스트 Prefab

    private void Awake()
    {
        Instance = this;
    }

    public void AddNote(string text)
    {
        GameObject entry = Instantiate(notebookEntryPrefab, contentParent);
        
        TMP_Text tmp = entry.GetComponentInChildren<TMP_Text>();
        tmp.text = text;
    }
}