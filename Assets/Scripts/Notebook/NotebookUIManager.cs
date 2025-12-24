using UnityEngine;

public class NotebookUIManager : MonoBehaviour
{
    public static NotebookUIManager Instance;

    [SerializeField] GameObject notebookCanvas;
    private bool isOpen = false;

    private void Awake()
    {
        Instance = this;
        notebookCanvas.SetActive(false); // Ã³À½¿£ ´ÝÈù »óÅÂ
    }

    private void Update()
    {
        
    }

    public void ToggleNotebook()
    {
        isOpen = !isOpen;
        notebookCanvas.SetActive(isOpen);
    }
}

