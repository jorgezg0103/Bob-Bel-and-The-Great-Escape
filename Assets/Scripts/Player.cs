using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D Rigidbody;
    [SerializeField] private float JumpForce = 5f;
    [SerializeField] private float MoveForce = 5f;

    private void Awake() {
        Rigidbody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    void Update()
    {
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
}
