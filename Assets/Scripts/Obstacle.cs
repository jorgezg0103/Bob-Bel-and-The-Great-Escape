using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] float Damage = 1f;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<Player>().TakeDamage(Damage);
        }
    }
}
