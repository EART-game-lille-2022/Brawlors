using UnityEngine;

public class KeyUnit : MonoBehaviour
{
    public ScriptableObjectColor colorOfTheKey;
    [SerializeField] SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        ApplyColor();
    }

    public void ApplyColor()
    {
        spriteRenderer.color = colorOfTheKey.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetComponent<PlayerKeys>())
        {
            PlayerKeys playerKey = collision.GetComponent<PlayerKeys>();
            playerKey.AddKey(this);
            transform.gameObject.SetActive(false);
        }
    }
}
