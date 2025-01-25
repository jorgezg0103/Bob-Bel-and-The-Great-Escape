using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private VideoPlayer MyVideoPlayer;

    private double VideoLength;

    private void Awake() {
        VideoLength = MyVideoPlayer.length;
        Player.GetComponent<Player>().SetPlayerActive(false);
    }

    private void Start() {
        StartCoroutine(SpawnPlayer());
    }

    IEnumerator SpawnPlayer() {
        yield return new WaitForSeconds((float)VideoLength);
        MyVideoPlayer.gameObject.SetActive(false);
        Player.GetComponent<Player>().SetPlayerActive(true);
    }
}
