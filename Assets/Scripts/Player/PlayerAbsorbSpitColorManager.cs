using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbsorbSpitColorManager : MonoBehaviour
{
    [SerializeField] private PlayerGrapUngrapManager _playerGrapUngrapManager;
    [SerializeField] private PlayerColorManager _playerColorManager;

    private void Awake()
    {
        _playerColorManager = GetComponent<PlayerColorManager>();
        _playerGrapUngrapManager = GetComponent<PlayerGrapUngrapManager>();
    }

    public ScriptableObjectColor colorStored;
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
    }
}
