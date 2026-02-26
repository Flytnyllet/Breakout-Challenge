using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private int blockHealth = 1;
    [SerializeField] private Ball ball;

    public void HandleCollision()
    {
        Player.score += 10f;
        blockHealth--;
        if (blockHealth <= 0)
        {
            Ball.speed += 0.1f;
            MaybeSpawnPowerUp(0.05f);
            Destroy(gameObject);
        }
    }

    private void MaybeSpawnPowerUp(float chance)
    {
        // Implement power-up spawning logic here
    }
}
