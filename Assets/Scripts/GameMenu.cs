using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class GameMenu : MonoBehaviour
{

    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject Tutorial;

    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject PauseButton;

    [SerializeField] private GameObject MainMenuImage;
    [SerializeField] private GameObject TutorialImage;

    [SerializeField] private TMP_Text CheckpointText;

    private void Awake() {
        if(CheckpointText != null) {
            CheckpointText.CrossFadeAlpha(0f, 0f, false);
        }
    }

    public void LoadScene(int index) {
        SceneManager.LoadScene(index);
    }

    public void GoToTutorial() {
        MainMenu.SetActive(false);
        Tutorial.SetActive(true);
        MainMenuImage.SetActive(false);
        TutorialImage.SetActive(true);
    }

    public void GoToMainMenu() {
        MainMenu.SetActive(true);
        Tutorial.SetActive(false);
        MainMenuImage.SetActive(true);
        TutorialImage.SetActive(false);
    }

    public void ShowPauseMenu() {
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
        PauseButton.SetActive(false);
    }

    public void HidePauseMenu() {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        PauseButton.SetActive(true);
    }

    public void ShowCheckpointSavedText() {
        StartCoroutine(FadeInAndOut(0.5f, 3));
    }

    private IEnumerator FadeInAndOut(float timeBtw, int iterations) {
        for(int i = 0; i < iterations; i++) {
            CheckpointText.CrossFadeAlpha(1f, timeBtw, false);
            yield return new WaitForSeconds(timeBtw);
            CheckpointText.CrossFadeAlpha(0f, timeBtw, false);
            yield return new WaitForSeconds(timeBtw);
        }
    }

    private IEnumerator FadeText(TMP_Text text, float startAlpha, float endAlpha, float duration) {
        float currentTime = 0f;
        while(currentTime <= duration) {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, currentTime / duration);
            text.alpha = alpha;
            currentTime += Time.deltaTime;
            yield return null;
        }
        yield break;
    }

}
