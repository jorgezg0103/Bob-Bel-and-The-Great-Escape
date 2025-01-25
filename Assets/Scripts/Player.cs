using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D Rigidbody;
    [SerializeField] private float JumpForce = 5f;
    [SerializeField] private float MoveForce = 5f;
    [SerializeField] private float TimeToSpawn = 1.5f;

    private float Health = 1f;
    private Vector2 CurrentCheckpoint;
    private float TimeInterval = 0.25f;
    private float Timer;

    private void Awake() {
        Rigidbody = GetComponent<Rigidbody2D>();
        CurrentCheckpoint = transform.position;
        Timer = TimeInterval;
    }

    void Update() {
        PlayerMovement();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // An interaction system with an Interface would be more appropiate :)
        GameObject collidedObj = collision.gameObject;
        if(collidedObj.tag == "Respawn") {
            CurrentCheckpoint = collidedObj.transform.position;
        }
        else if(collidedObj.tag == "StickyWall") {

        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        GameObject collidedObj = collision.gameObject;
        if(collidedObj.tag == "WindGust") {
            collidedObj.GetComponent<WindGust>().AddWindThrust(gameObject);
        }
    }

    private void PlayerMovement() {
        if(Timer >= TimeInterval) {
            if(Input.GetKeyDown(KeyCode.Space)) {
                Rigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
                Timer = 0f;
            }
            if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
                Rigidbody.AddForce(Vector2.left * MoveForce, ForceMode2D.Impulse);
                Timer = 0f;
            }
            if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
                Rigidbody.AddForce(Vector2.right * MoveForce, ForceMode2D.Impulse);
                Timer = 0f;
            }
        }
        else {
            Timer += Time.deltaTime;
        }
    }

    public void TakeDamage(float damage) {
        Health -= damage;
        if(Health <= 0) {
            Debug.Log("Game Over");
            SetPlayerActive(false);
            StartCoroutine(MoveToCheckpoint(TimeToSpawn));
        }
    }

    private void SetPlayerActive(bool enable) {
        gameObject.GetComponent<Renderer>().enabled = enable;
        gameObject.GetComponent<Collider2D>().enabled = enable;
    }

    private IEnumerator MoveToCheckpoint(float seconds) {
        yield return new WaitForSeconds(seconds);
        transform.position = CurrentCheckpoint;
        SetPlayerActive(true);
    }
}
