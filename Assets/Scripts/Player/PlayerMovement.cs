using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Values")]
    private Vector2 moveDirection;
    private Rigidbody2D rb2D;
    [SerializeField] private float moveSpeed;
    [SerializeField][Tooltip("est-ce que le player est au dessus du sol ?")] private bool isOnGround = true;

    [Header("Animation Values")]
    [SerializeField] private AnimationCurve wavingCurve;
    [SerializeField] private float wavingValue;
    [SerializeField] private float frequency;
    [SerializeField] private float wavingForce;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        wavingValue = wavingCurve.Evaluate(Time.time * frequency) * wavingForce;
        rb2D.velocity = new Vector2(rb2D.velocity.x ,rb2D.velocity.y + wavingValue);
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
