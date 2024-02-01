using System.Collections.Generic;
using UnityEngine;

public class PlayerColorManager : MonoBehaviour
{
    public static PlayerColorManager instance;

    [SerializeField] private List<ScriptableObjectColor> colorBag;
    [SerializeField] private ScriptableObjectColor colorToAdd;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        AddColor(colorToAdd);
    }

    public void AddColor(ScriptableObjectColor colorAdded)
    {
        for (int i = colorBag.Count - 1; i > 0; i--)
        {
            if(colorBag[i - 1] != null)
            {
                colorBag[i] = colorBag[i - 1];
            }
        }

        colorBag[0] = colorAdded;
    }
}
