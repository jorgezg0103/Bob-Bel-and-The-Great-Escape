using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{

    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject Tutorial;

    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject PauseButton;

    public void LoadScene(int index) {
        SceneManager.LoadScene(index);
    }

    public void GoToTutorial() {
        MainMenu.SetActive(false);
        Tutorial.SetActive(true);
    }

    public void GoToMainMenu() {
        MainMenu.SetActive(true);
        Tutorial.SetActive(false);
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

}
