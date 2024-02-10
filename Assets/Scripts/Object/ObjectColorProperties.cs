using UnityEngine;

public class ObjectColorProperties : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private ScriptableObjectColor startColor;
    public ScriptableObjectColor actualColor;
    [SerializeField] private ScriptableObjectColor noColor;

    public bool isColorless;

    public void SwitchColor(ScriptableObjectColor colorToSwitchTo)
    {
        actualColor = colorToSwitchTo;
        spriteRenderer.color = actualColor.color;
        CheckIfColorless();
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
        CheckIfColorless();
    }

    public void StealColor(out ScriptableObjectColor colorStealed)
    {
        colorStealed = actualColor;
        SwitchColor(noColor);
        CheckIfColorless();
    }

    public void CheckIfColorless()
    {
        if (actualColor != noColor)
        {
            isColorless = false;
        }
        else
        {
            isColorless = true;
        }
    }
}
