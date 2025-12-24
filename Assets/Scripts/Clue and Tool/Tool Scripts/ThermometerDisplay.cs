using UnityEngine;
using TMPro;

public class ThermometerDisplay : MonoBehaviour
{
    public TMP_Text displayText;

    private void Start()
    {
        displayText.text = "--¡ÆC";
    }

    public void UpdateDisplay(float temp)
    {
        displayText.text = temp.ToString() + "¡ÆC";
    }
}
