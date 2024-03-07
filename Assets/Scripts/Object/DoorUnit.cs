using UnityEngine;

public class DoorUnit : MonoBehaviour
{
    [SerializeField] private ScriptableObjectColor colorOfTheKeyNeeded;
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
        spriteRenderer.color = colorOfTheKeyNeeded.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetComponent<PlayerKeys>())
        {
            PlayerKeys playerKeys = collision.GetComponent<PlayerKeys>();

            if (playerKeys.CheckIfPlayerStoreNeededKey(colorOfTheKeyNeeded))
            {
                Destroy(gameObject);
            }
        }
    }
}