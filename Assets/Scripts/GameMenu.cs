using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public void LoadScene(int index) {
        SceneManager.LoadScene(index);
    }
}
