using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] public static float playerLives = 3f;
    public static float score = 0f;
    [SerializeField] public static bool isShort = false;
    public static bool playerWidthUpdated = false;

    private InputAction moveAction;
    private SpriteRenderer spriteRenderer;

    private float screenHalfWidthInWorldUnits;
    private float playerWidth;
    private bool canMoveRight = true;
    private bool canMoveLeft = true;

    [SerializeField] private float moveSpeed = 5f;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize;
        playerWidth = spriteRenderer.bounds.size.x;
    }

    private void Update()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        HandleMovement(moveInput);
        HandlePlayerShort();
    }

    private void HandleMovement(Vector2 moveInput)
    {
        //Gameover return logic

        float screenRightLimit = screenHalfWidthInWorldUnits - playerWidth / 2;
        float screenLeftLimit = -screenHalfWidthInWorldUnits + playerWidth / 2;

        if (transform.position.x > screenRightLimit)
            canMoveRight = false;
        else if (transform.position.x < screenLeftLimit)
            canMoveLeft = false;
        else { canMoveRight = true; canMoveLeft = true; }

        if (moveInput.x > 0 && canMoveRight || moveInput.x < 0 && canMoveLeft)
        {
            transform.Translate(Vector2.right * moveInput.x * moveSpeed * Time.deltaTime);
        }
    }

    private void HandlePlayerShort()
    {
        if (isShort && !playerWidthUpdated)
        {
            var scale = spriteRenderer.transform.localScale;
            scale.Set(scale.x * 0.75f, scale.y, scale.z);
            spriteRenderer.transform.localScale = scale;
            playerWidth = spriteRenderer.bounds.size.x;
            playerWidthUpdated = true;
            Debug.Log("Player width updated to 75%");
        }

        if (!isShort && !playerWidthUpdated)
        {
            var scale = spriteRenderer.transform.localScale;
            scale.Set(3f, scale.y, scale.z);
            spriteRenderer.transform.localScale = scale;
            playerWidth = spriteRenderer.bounds.size.x;
            playerWidthUpdated = true;
            Debug.Log("Player width updated to 100%");
        }
    }

    private void OnEnable()
    {
        moveAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
    }
}
