using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGrapSpitManager : MonoBehaviour
{
    [SerializeField] private DistanceJoint2D distanceJoint2D;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject grappedObject;

    [SerializeField] private float distanceMaxForGrap;
    [SerializeField] private float actualDistanceFromTheMouse;
    [SerializeField] private float grapCooldown;

    [SerializeField] private bool canGrap;
    [SerializeField] private bool isGrapActive;

    private void Start()
    {
        canGrap = true;
        distanceJoint2D = GetComponent<DistanceJoint2D>();
    }

    public void GrapUngrap(InputAction.CallbackContext context)
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = Camera.main.nearClipPlane;
        mousePos.z = -Camera.main.transform.position.z;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);

        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, mouseWorldPos - transform.position, layerMask);

        if (context.performed && isGrapActive && canGrap)
        {
            distanceJoint2D.enabled = false;
            isGrapActive = false;
            grappedObject = null;
        }
        else if (context.performed && hit2D.distance <= distanceMaxForGrap && canGrap)
        {
            distanceJoint2D.enabled = true;
            isGrapActive = true;
            distanceJoint2D.connectedAnchor = hit2D.point;
            grappedObject = hit2D.collider.gameObject;
            canGrap = false;
        }
        if (distanceJoint2D.connectedAnchor == Vector2.zero)
        {
            distanceJoint2D.enabled = false;
        }
        actualDistanceFromTheMouse = hit2D.distance;
        StartCoroutine(GrapCooldown(grapCooldown));
    }

    public IEnumerator GrapCooldown(float time)
    {
        yield return new WaitForSeconds(time);
        canGrap = true;
    }

    public void StealSpitColor(InputAction.CallbackContext context)
    {
        if (context.performed && isGrapActive)
        {

        }
    }
}
