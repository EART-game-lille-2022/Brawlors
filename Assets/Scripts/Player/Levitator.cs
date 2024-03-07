using UnityEngine;
using UnityEngine.InputSystem;

public class Levitator : MonoBehaviour
{
    Rigidbody2D rb2D;
    public LayerMask layerMask;

    public float targetHeight;
    public float force = 5;
    public float jumpImpulse = 5;
    public float cooldownForJump;
    private float timeElapsed;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(timeElapsed >= cooldownForJump)
        {
            rb2D.AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);
            timeElapsed = 0;
        }
    }

    public void InversedJump(InputAction.CallbackContext context)
    {
        if (timeElapsed >= cooldownForJump)
        {
            rb2D.AddForce(Vector2.down * jumpImpulse, ForceMode2D.Impulse);
            timeElapsed = 0;
        }
    }
    void FixedUpdate()
    {
        timeElapsed += Time.fixedDeltaTime;
        RaycastHit2D hit2D = Physics2D.CircleCast(transform.position, .1f, Vector2.down, targetHeight, layerMask);

        if (hit2D.distance > 0 && hit2D.distance < targetHeight)
        {
            rb2D.AddForce(Vector2.up * force / hit2D.distance);
        }
    }
}
