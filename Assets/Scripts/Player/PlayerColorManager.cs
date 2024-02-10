using System.Collections.Generic;
using UnityEngine;

public class PlayerColorManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    [SerializeField] private SpriteRenderer playerBag1;
    [SerializeField] private SpriteRenderer playerBag2;

    [SerializeField] private List<ScriptableObjectColor> colorBag;

    private void Awake()
    {
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        ApplyColorOnPlayerAndBag();
    }

    public void AddColor(ScriptableObjectColor colorAdded)
    {
        for (int i = colorBag.Count - 1; i > 0; i--)
        {
            if (colorBag[i - 1] != null)
            {
                colorBag[i] = colorBag[i - 1];
            }
        }

        colorBag[0] = colorAdded;

        ApplyColorOnPlayerAndBag();
    }

    public void ApplyColorOnPlayerAndBag()
    {
        playerSpriteRenderer.color = colorBag[0].color;
        playerBag1.color = colorBag[1].color;
        playerBag2.color = colorBag[2].color;
    }
}
