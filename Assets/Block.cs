using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private int blockHealth = 1;

    public void HandleCollision(Ball ball, ref int totalblocks)
    {
        Debug.Log("Block hit!");
        blockHealth--;
        if (blockHealth <= 0)
        {
            UIManager.Instance.score += 10;
            totalblocks--;
            ball.ballSpeed += ball.ballSpeedIncrease;
            MaybeSpawnPowerUp(0.1f);
            Destroy(gameObject);
        }

        if(totalblocks <= 0)
        {
            UIManager.Instance.isGameWon = true;
            Debug.Log("All blocks destroyed! You win!");
        }
    }

    private void MaybeSpawnPowerUp(float chance)
    {
        // Implement power-up spawning logic here
    }
}
