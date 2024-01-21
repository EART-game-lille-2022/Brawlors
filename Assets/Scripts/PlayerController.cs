using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Values")]
    [SerializeField] private Vector2 moveSpeed;
    [SerializeField] private float moveSpeedForce;
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
        //wavingValue = wavingCurve.Evaluate(Time.time * frequency);
        //wavingValue *= wavingForce;
        //rb2D.velocity = new Vector2(rb2D.velocity.x, wavingValue);
    }

    public void DoMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveSpeed = context.ReadValue<Vector2>();
            rb2D.velocity = new Vector2(moveSpeed.x * moveSpeedForce, rb2D.velocity.y);
        }
        else
        {
            moveSpeed = Vector2.zero;
            rb2D.velocity = new Vector2(moveSpeed.x * moveSpeedForce, rb2D.velocity.y);
        }
    }
}
