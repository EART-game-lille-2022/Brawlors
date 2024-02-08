using UnityEngine;

public class ObjectColorProperties : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private ScriptableObjectColor startColor;
    [SerializeField] private ScriptableObjectColor actualColor;
    [SerializeField] private ScriptableObjectColor noColor;

    public void SwitchColor(ScriptableObjectColor colorToSwitchTo)
    {
        actualColor = colorToSwitchTo;
        spriteRenderer.color = actualColor.color;
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SwitchColor(startColor);
    }

    public void ApplyColor(ScriptableObjectColor colorToApply)
    {
        SwitchColor(colorToApply);
        actualColor = colorToApply;
    }

    public void ColorStealed(ScriptableObjectColor colorStealed)
    {
        if(actualColor == noColor)
        {
            return;
        }
        colorStealed = actualColor;
        SwitchColor(noColor);
    }

}
