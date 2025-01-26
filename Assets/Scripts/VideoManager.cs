using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{

    private VideoPlayer Video;
    private float VideoLength;
    void Awake()
    {
        Video = gameObject.GetComponent<VideoPlayer>();
        VideoLength = (float)Video.length;
    }

    private void Start() {
        StartCoroutine(ChangeScene());
    }

    private IEnumerator ChangeScene() {
        yield return new WaitForSeconds(VideoLength);
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if(currentScene == 1) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else {
            SceneManager.LoadScene(0);
        }
    }
}
