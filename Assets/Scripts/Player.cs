using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D Rigidbody;
    [SerializeField] private float JumpForce = 5f;
    [SerializeField] private float MoveForce = 5f;
    [SerializeField] private float TimeToSpawn = 1.5f;

    private float Health = 1f;
    private float GravityScale;
    private Vector2 CurrentCheckpoint;
    private float TimeInterval = 0.25f;
    private float Timer;

    private bool Stuck = false;
    private int UnstuckTries = 0;
    private Vector2 UnStuckDirection = new Vector2(0, 0);

    private Animator PlayerAnimator;

    private void Awake() {
        Rigidbody = GetComponent<Rigidbody2D>();
        GravityScale = Rigidbody.gravityScale;
        CurrentCheckpoint = transform.position;
        Timer = TimeInterval;
        PlayerAnimator = gameObject.GetComponentInChildren<Animator>();
    }

    void Update() {
        if(!Stuck) {
            PlayerMovement();
        }
        else {
            if(Keyboard.current.anyKey.wasPressedThisFrame) {
                TryToUnStuck();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // An interaction system with an Interface would be more appropiate :)
        GameObject collidedObj = collision.gameObject;
        if(collidedObj.tag == "Respawn") {
            CurrentCheckpoint = collidedObj.transform.position;
        }
        else if(collidedObj.tag == "StickyWall") {
            Stuck = true;
            Rigidbody.angularVelocity = 0f;
            Rigidbody.linearVelocity = Vector2.zero;
            Rigidbody.gravityScale = 0f;
            UnStuckDirection = collidedObj.transform.up;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        GameObject collidedObj = collision.gameObject;
        if(collidedObj.tag == "WindGust") {
            collidedObj.GetComponent<WindGust>().AddWindThrust(gameObject);
        }
    }

    private void TryToUnStuck() {
        UnstuckTries++;
        int random = Random.Range(4, 8);
        PlayerAnimator.Play("Stuck");
        if(UnstuckTries >= random) {
            Stuck = false;
            Rigidbody.gravityScale = GravityScale;
            Debug.Log("Unstuck! Nº of tries: " + UnstuckTries);
            UnstuckTries = 0;
            Rigidbody.AddForce(UnStuckDirection * JumpForce, ForceMode2D.Impulse);
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

    public void SetPlayerActive(bool enable) {
        if(enable) {
            Rigidbody.gravityScale = GravityScale;
        }
        else {
            Rigidbody.gravityScale = 0f;
        }
        gameObject.transform.GetChild(0).GetComponent<Renderer>().enabled = enable;
        gameObject.GetComponent<Collider2D>().enabled = enable;
    }

    private IEnumerator MoveToCheckpoint(float seconds) {
        yield return new WaitForSeconds(seconds);
        transform.position = CurrentCheckpoint;
        SetPlayerActive(true);
    }
}
