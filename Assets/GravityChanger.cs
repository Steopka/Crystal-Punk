using UnityEngine;

public class GravityChanger : MonoBehaviour
{
    [SerializeField]
    private Vector2 _gravityVector;
    [SerializeField]
    private float _rotation_speed;

    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(_gravityVector, ForceMode2D.Force);
        collision.gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,0,90), Time.time *_rotation_speed);
    }
}
