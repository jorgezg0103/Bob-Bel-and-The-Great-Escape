using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{

    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject Tutorial;
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
}
