using UnityEngine;

public class ObjectColorProperties : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private ScriptableObjectColor startColor;
    [SerializeField] private ScriptableObjectColor actualColor;

    public void SwitchColor(Color colorToSwitchTo)
    {
        actualColor.color = colorToSwitchTo;
        spriteRenderer.color = actualColor.color;
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SwitchColor(startColor.color);
    }

    public void ApplyColor(Color colorToApply)
    {
        SwitchColor(colorToApply);
    }

    public void ColorStealed(Color colorStealed)
    {
        colorStealed = actualColor.color;
        SwitchColor(startColor.color);
    }

}
