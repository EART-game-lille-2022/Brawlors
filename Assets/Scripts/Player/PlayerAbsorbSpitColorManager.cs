using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbsorbSpitColorManager : MonoBehaviour
{
    public static PlayerAbsorbSpitColorManager Instance { get; private set; }

    [SerializeField] private PlayerGrapUngrapManager _playerGrapUngrapManager;

    [SerializeField] private GameObject objectToSpit;

    public ScriptableObjectColor noColor;
    public ScriptableObjectColor colorStored;

    private void Awake()
    {
        Instance = this;
        _playerGrapUngrapManager = GetComponent<PlayerGrapUngrapManager>();
    }

    public void AbsorbSpitColor(InputAction.CallbackContext context)
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = Camera.main.nearClipPlane;
        mousePos.z = -Camera.main.transform.position.z;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);

        if (context.performed && _playerGrapUngrapManager.isGrapActive)
        {
            ObjectColorProperties objectGrapped = _playerGrapUngrapManager.grappedObject.GetComponent<ObjectColorProperties>();

            if (objectGrapped != null && objectGrapped.isColorless == false && !ColorAndLayerManager.instance.isBagFull())
            {
                //colorStored = objectGrapped.actualColor;
                objectGrapped.StealColor(out colorStored);
                ColorAndLayerManager.instance.AddColor(colorStored);
                ColorAndLayerManager.instance.ApplyLayerMaskOnLevitator();
            }
        }
        else if (context.performed && !_playerGrapUngrapManager.isGrapActive && ColorAndLayerManager.instance.isStillAColorStored())
        {
            GameObject objectSpitted = Instantiate(objectToSpit, transform.position, Quaternion.identity);
            ColorAndLayerManager.instance.ApplyColorAndLayerMaskOnObject(objectSpitted);
            objectSpitted.GetComponent<Rigidbody2D>().AddForce((mouseWorldPos - transform.position).normalized * 12f, ForceMode2D.Impulse);
            ColorAndLayerManager.instance.ApplyLayerMaskOnLevitator();
        }
    }
}
