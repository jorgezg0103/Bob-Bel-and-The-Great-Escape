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

    private void Awake() {
        Rigidbody = GetComponent<Rigidbody2D>();
        CurrentCheckpoint = transform.position;
    }
    void Start() {
        
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("Going up");
            Rigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
            Rigidbody.AddForce(Vector2.left * MoveForce, ForceMode2D.Impulse);
        }
        if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
            Rigidbody.AddForce(Vector2.right * MoveForce, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Respawn") {
            CurrentCheckpoint = collision.transform.position;
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
        SetPlayerActive(true);
        transform.position = CurrentCheckpoint;
    }
}
