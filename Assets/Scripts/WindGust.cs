using UnityEngine;

public class WindGust : MonoBehaviour
{
    [SerializeField] float thrust = 0.2f;
    public void AddWindThrust(GameObject player) {
            player.gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.right * thrust, ForceMode2D.Impulse);
    }
}
