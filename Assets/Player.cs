using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] public static float playerLives = 3f;
    public static float score = 0f;

    private InputAction moveAction;

    [SerializeField] private float moveSpeed = 5f;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        transform.Translate(Vector2.right * moveInput.x * moveSpeed * Time.deltaTime);
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
