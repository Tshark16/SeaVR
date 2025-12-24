using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class VRSceneController : MonoBehaviour
{
    [Header("페이드 설정")]
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 1.0f;

    [Header("사운드 설정")]
    public AudioSource doorOpenAudio;    // 문 열리는 소리
    public AudioSource doorCloseAudio;   // 문 닫히는 소리
    public AudioSource footstepAudio;    // 발자국 소리
    public float footstepDuration = 3.0f; // 발자국 소리 재생 시간

    [Header("이동 설정")]
    public string targetSceneName;
    public GameObject interactionUI;     // 트리거 진입 시 뜰 UI

    private bool isTransitioning = false;

    void Start()
    {
        // 시작할 때 화면이 밝아짐
        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 1f;
            StartCoroutine(Fade(1, 0));
        }

        // UI는 처음에 꺼둠
        if (interactionUI != null) interactionUI.SetActive(false);
    }

    // --- 트리거 감지 부분 추가 ---
    private void OnTriggerEnter(Collider other)
    {
        // 캐릭터 컨트롤러가 있는 플레이어 태그 확인
        if (other.CompareTag("Player") && !isTransitioning)
        {
            if (interactionUI != null) interactionUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (interactionUI != null) interactionUI.SetActive(false);
        }
    }
    // ----------------------------

    public void StartSceneTransition()
    {
        if (!isTransitioning) StartCoroutine(TransitionSequence());
    }

    IEnumerator TransitionSequence()
    {
        isTransitioning = true;

        // 버튼 누르면 UI부터 끔
        if (interactionUI != null) interactionUI.SetActive(false);

        // 1. 화면 암전 (Fade In)
        yield return StartCoroutine(Fade(0, 1));

        // 2. 문 열리는 소리
        if (doorOpenAudio != null)
        {
            doorOpenAudio.Play();
            yield return new WaitForSeconds(doorOpenAudio.clip.length);
        }

        // 3. 문 닫히는 소리
        if (doorCloseAudio != null)
        {
            doorCloseAudio.Play();
            yield return new WaitForSeconds(doorCloseAudio.clip.length);
        }

        // 4. 발자국 소리
        if (footstepAudio != null)
        {
            footstepAudio.Play();
            yield return new WaitForSeconds(footstepDuration);
            footstepAudio.Stop();
        }

        // 5. 씬 이동
        SceneManager.LoadScene(targetSceneName);
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            yield return null;
        }
        fadeCanvasGroup.alpha = endAlpha;
    }
}