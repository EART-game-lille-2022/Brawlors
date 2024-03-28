using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Values")]
    private Vector2 moveDirection;
    private Rigidbody2D rb2D;
    [SerializeField] private float moveSpeed;
    public bool warpingCooldown = false;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void DoMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveDirection.x = context.ReadValue<Vector2>().x;
            rb2D.velocity = new Vector2(moveDirection.normalized.x * moveSpeed, rb2D.velocity.y);
        }
        if (context.canceled)
        {
            moveDirection = Vector2.zero;
            rb2D.velocity = new Vector2(moveDirection.x * moveSpeed, rb2D.velocity.y);
        }
    }
}
