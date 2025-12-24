using UnityEngine;

public class CorpseInteraction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ToolComponent tool = other.GetComponentInParent<ToolComponent>();
        if (tool == null || tool.interactionData == null) return;

        var data = tool.interactionData;

        // 🔍 도구 종류별 Debug
        if (data.toolName == "Magnifier")
        {
            ClueManager.Instance.MarkClue(0);
        }
        if (data.toolName == "Thermometer")
        {
            ClueManager.Instance.MarkClue(1);
        }
        // 모노로그
        MonologueManager.Instance.Show(data.monologueText);

        // 노트 기록
        NotebookManager.Instance.AddNote(data.notebookRecord);

        // 온도계 수치 출력
        if (data.coreTemperature > 0)
        {
            Debug.Log("Core Temp: " + data.coreTemperature);
        }

        // 온도계 UI 업데이트
        ThermometerDisplay thermometer = other.GetComponentInParent<ThermometerDisplay>();
        if (thermometer != null && data.coreTemperature > 0)
        {
            thermometer.UpdateDisplay(data.coreTemperature);
        }
    }
}


