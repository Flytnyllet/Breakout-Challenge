using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private int blockHealth = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Player.score += 10f;
            blockHealth--;
            if (blockHealth <= 0)
            {
                MaybeSpawnPowerUp();
                Destroy(gameObject);
            }
        }
    }

    private void MaybeSpawnPowerUp()
    {
        // Implement power-up spawning logic here
    }
}
