using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Values")]
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rb2D;
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
        wavingValue = wavingCurve.Evaluate(Time.time * frequency);
    }

    public void DoMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rb2D.velocity = context.ReadValue<Vector2>() * moveSpeed;
        }
    }
}
