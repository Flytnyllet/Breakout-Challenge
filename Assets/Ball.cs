using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Vector2 direction;

    private void Start()
    {
        direction = new Vector2(Random.Range(-1f, 1f), 1f).normalized;
    }

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Block"))
        {
            direction = Vector2.Reflect(direction, collision.GetContact(0).normal);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            Player.playerLives -= 1f;
        }
    }
}
