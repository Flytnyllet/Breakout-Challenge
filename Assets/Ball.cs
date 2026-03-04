using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ball : MonoBehaviour
{
    [SerializeField] private BlockSetup blocks;
    [SerializeField] private TextShaker textShaker;

    private InputAction input;

    private float startBallSpeed;
    [SerializeField] public float ballSpeed = 8f;
    [SerializeField] public float ballSpeedIncrease = 0.5f;
    [SerializeField] public bool isLifeLost;

    [SerializeField] private Player player;
    [SerializeField] private bool isAttachedToPlayer;
    [SerializeField] private float offsetY = 0.3f;
    private Vector2 direction;

    private void Awake()
    {
        player.GetComponent<Player>();
        textShaker.GetComponent<TextMeshProUGUI>();
        input = InputSystem.actions.FindAction("Jump");
    }

    private void Start()
    {
        startBallSpeed = ballSpeed;
        ResetBall();
    }

    private void Update()
    {
        if (isAttachedToPlayer && input.WasPressedThisFrame())
            LaunchBall();

        if (!isAttachedToPlayer && !UIManager.Instance.isGameWon && !UIManager.Instance.isGameOver)
            transform.Translate(ballSpeed * Time.deltaTime * direction);
    }

    private void LaunchBall()
    {
        gameObject.transform.parent = null;
        isAttachedToPlayer = false;
        direction = Vector2.up;
        Debug.Log("Ball launched with direction: " + direction);
    }

    private void ResetBall()
    {
        isAttachedToPlayer = true;
        UIManager.Instance.scoreMultiplier = 0;
        textShaker.GetComponent <TextMeshProUGUI>().fontSize = textShaker.initialFontSize;
        textShaker.shakeMultiplier = 1f;
        textShaker.constantShakeStrength = 0f;
        ballSpeed = startBallSpeed;
        player.playerWidthUpdated = false;
        player.isShort = false;
        gameObject.transform.parent = player.transform;
        transform.position = new Vector2(player.transform.position.x, player.transform.position.y + offsetY);
    }

    private void PlayerBounce()
    {
        direction = new Vector2(transform.position.x - player.transform.position.x, Mathf.Abs(direction.y)).normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            direction = Vector2.Reflect(direction, collision.GetContact(0).normal);
        }

        if (collision.gameObject.CompareTag("Roof"))
        {
            direction = Vector2.Reflect(direction, collision.GetContact(0).normal);
            if (!player.isShort)
            {
                player.isShort = true;
                player.playerWidthUpdated = false;
                Debug.Log("Player is now short. Player width will be updated in the next frame.");
            }
        }

        if (collision.gameObject.CompareTag("Block"))
        {
            direction = Vector2.Reflect(direction, collision.GetContact(0).normal);
            collision.gameObject.GetComponent<Block>().HandleCollision(this, ref blocks.totalBlocks, ref textShaker.start);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerBounce();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            Debug.Log("Ball hit the floor. Player loses a life.");
            player.playerLives -= 1;
            isLifeLost = true;
            if (player.playerLives > 0f)
                ResetBall();
            else
            {
                UIManager.Instance.isGameOver = true;
                direction = Vector2.zero;
                transform.position = new Vector2(0f, -20f); // Move ball off-screen when game over
            }
        }
    }
}
