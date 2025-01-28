using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.IO;

public class VideoManager : MonoBehaviour
{
    [SerializeField] string VideoFileName;
    private VideoPlayer Video;
    private float VideoLength;
    void Awake()
    {
        Video = gameObject.GetComponent<VideoPlayer>();
        Video.url = Application.streamingAssetsPath + "/" + VideoFileName;
        Video.EnableAudioTrack(0, true);
        Video.Prepare();
        Video.prepareCompleted += Prepared;
    }

    void Prepared(VideoPlayer Video) {
        VideoLength = (float)(Video.frameCount / Video.frameRate);
        StartCoroutine(ChangeScene());
        Video.SetDirectAudioVolume(0, PlayerPrefs.GetFloat("MusicVolume"));
        Video.Play();
    }


    private IEnumerator ChangeScene() {
        yield return new WaitForSeconds(VideoLength);
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if(currentScene == 1) {
            PlayerPrefs.Save();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else {
            PlayerPrefs.Save();
            SceneManager.LoadScene(0);
        }
    }
}
