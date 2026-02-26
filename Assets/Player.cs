using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] public static float playerLives = 3f;
    public static float score = 0f;
    public static bool isShort = false;
    public bool playerWidthUpdated = false;

    private InputAction moveAction;
    private SpriteRenderer spriteRenderer;

    private float screenHalfWidthInWorldUnits;
    private float playerWidth;

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

        transform.Translate(Vector2.right * moveInput.x * moveSpeed * Time.deltaTime);

        if(isShort && !playerWidthUpdated)
        {
            var scale = spriteRenderer.gameObject.transform.localScale;
            scale.Set(scale.x * 0.75f, scale.y, scale.z);
            playerWidthUpdated = true;
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
