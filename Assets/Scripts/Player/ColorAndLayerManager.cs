using System.Collections.Generic;
using UnityEngine;

public class ColorAndLayerManager : MonoBehaviour
{
    public static ColorAndLayerManager instance;

    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    [SerializeField] private SpriteRenderer playerBag1;
    [SerializeField] private SpriteRenderer playerBag2;

    [SerializeField] private ScriptableObjectColor defaultColor;
    [SerializeField] private List<ScriptableObjectColor> colorBag;

    [SerializeField] private LayerMask layerMaskFinal;

    [SerializeField] private Levitator levitator;

    public LayerMask defaultMask;
    public int playerLayer = 3;

    private void Awake()
    {
        instance = this;
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

    public void RemoveColor()
    {
        colorBag[0] = colorBag[1];
        colorBag[1] = colorBag[2];
        colorBag[2] = defaultColor;

        ApplyColorOnPlayerAndBag();
    }

    public void ApplyColorOnPlayerAndBag()
    {
        playerSpriteRenderer.color = colorBag[0].color;
        // gameObject.layer = colorBag[0].colorLayer;
        // default mask without the color's layer

        layerMaskFinal = defaultMask & ~(1 << colorBag[0].colorLayer);
        Physics2D.SetLayerCollisionMask(playerLayer, layerMaskFinal);

        playerBag1.color = colorBag[1].color;
        playerBag2.color = colorBag[2].color;
    }

    public void ApplyLayerMaskOnLevitator()
    {
        levitator.layerMask = layerMaskFinal;
    }

    public void ApplyColorAndLayerMaskOnObject(GameObject objectToModify)
    {
        if (objectToModify.GetComponent<ObjectColorProperties>())
        {
            objectToModify.GetComponent<ObjectColorProperties>().SwitchColor(colorBag[0]);
            RemoveColor();
        }
    }

    public bool isBagFull()
    {
        if (colorBag[2] != defaultColor)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool isStillAColorStored()
    {
        if (colorBag[0] != defaultColor)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
