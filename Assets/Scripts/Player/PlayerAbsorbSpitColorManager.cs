using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbsorbSpitColorManager : MonoBehaviour
{
    public static PlayerAbsorbSpitColorManager Instance { get; private set; }

    [SerializeField] private PlayerGrapUngrapManager _playerGrapUngrapManager;
    [SerializeField] private ColorAndLayerManager _playerColorManager;

    [SerializeField] private GameObject objectToSpit;

    public ScriptableObjectColor noColor;
    public ScriptableObjectColor colorStored;

    private void Awake()
    {
        Instance = this;
        _playerColorManager = GetComponent<ColorAndLayerManager>();
        _playerGrapUngrapManager = GetComponent<PlayerGrapUngrapManager>();
    }

    public void AbsorbSpitColor(InputAction.CallbackContext context)
    {
        if(context.performed && _playerGrapUngrapManager.isGrapActive)
        {
            ObjectColorProperties objectGrapped = _playerGrapUngrapManager.grappedObject.GetComponent<ObjectColorProperties>();

            if(objectGrapped != null && objectGrapped.isColorless == false)
            {
                //colorStored = objectGrapped.actualColor;
                objectGrapped.StealColor(out colorStored);
                _playerColorManager.AddColor(colorStored);
            }
        }
        else if(context.performed && !_playerGrapUngrapManager.isGrapActive && ColorAndLayerManager.instance.isStillAColorStored())
        {
            GameObject objectSpitted = Instantiate(objectToSpit, transform.position, Quaternion.identity);
            ColorAndLayerManager.instance.ApplyColorAndLayerMaskOnObject(objectSpitted);
        }
    }
}
