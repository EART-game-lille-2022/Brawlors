using UnityEngine;

public class Levitator : MonoBehaviour
{
    Rigidbody2D rb2D;
    public LayerMask layerMask;

    public float targetHeight;
    public float force = 5;
    public float jumpImpulse = 5;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Jump!");
            rb2D.AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        RaycastHit2D hit2D = Physics2D.CircleCast(transform.position, .1f, Vector2.down, targetHeight, layerMask);

        if(hit2D.distance > 0 && hit2D.distance < targetHeight) 
        {
            rb2D.AddForce(Vector2.up * force / hit2D.distance);
        }
    }
}
