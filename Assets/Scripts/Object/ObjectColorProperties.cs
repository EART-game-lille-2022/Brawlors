using UnityEngine;
using UnityEngine.Events;

public class ObjectColorProperties : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private ScriptableObjectColor startColor;
    [SerializeField] private ScriptableObjectColor noColor;
    public ScriptableObjectColor actualColor;

    public bool isColorless;
    
    public UnityEvent<ScriptableObjectColor> OnColorChange;

    public void SwitchColor(ScriptableObjectColor colorToSwitchTo)
    {
        actualColor = colorToSwitchTo;
        spriteRenderer.color = actualColor.color;
        CheckIfColorless();
        gameObject.layer = colorToSwitchTo.colorLayer;

        OnColorChange.Invoke(colorToSwitchTo);
    }

    private void Awake()
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
        SwitchColor(PlayerAbsorbSpitColorManager.Instance.noColor);
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
