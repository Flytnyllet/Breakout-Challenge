using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private int blockHealth = 1;
    [SerializeField] private int ChanceToSpawnPowerup = 10;
    [SerializeField] List<GameObject> powerups = new List<GameObject>();

    public void HandleCollision(Ball ball, ref int totalblocks, ref bool shake)
    {
        Debug.Log("Block hit!");
        blockHealth--;
        if (blockHealth <= 0)
        {
            UIManager.Instance.scoreMultiplier++;
            shake = true;
            totalblocks--;
            ball.ballSpeed += ball.ballSpeedIncrease;
            MaybeSpawnPowerUp(ChanceToSpawnPowerup);
            UIManager.Instance.score += 10 * (UIManager.Instance.scoreMultiplier);
            Destroy(gameObject);
        }

        if (totalblocks <= 0)
        {
            UIManager.Instance.isGameWon = true;
            Debug.Log("All blocks destroyed! You win!");
        }
    }

    private void MaybeSpawnPowerUp(float chance)
    {
        int number = Random.Range(0, 100);
        Debug.Log($"{number}");

        if (chance <= number)
            return;

        int powerup = Random.Range(0, powerups.Count);

        Instantiate(powerups[powerup], transform.position, Quaternion.identity);
    }
}
