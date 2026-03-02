using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private int blockHealth = 1;

    public void HandleCollision(Ball ball)
    {
        Debug.Log("Block hit!");
        Player.score += 10f;
        blockHealth--;
        if (blockHealth <= 0)
        {
            ball.ballSpeed += ball.ballSpeedIncrease;
            MaybeSpawnPowerUp(0.1f);
            Destroy(gameObject);
        }
    }

    private void MaybeSpawnPowerUp(float chance)
    {
        // Implement power-up spawning logic here
    }
}
