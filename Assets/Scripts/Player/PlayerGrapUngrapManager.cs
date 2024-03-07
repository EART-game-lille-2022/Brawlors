using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


//l'effet de l'élastique fonctionne partiellement mais il reste des mouvements hératique à régler
public class PlayerGrapUngrapManager : MonoBehaviour
{
    [SerializeField] private SpringJoint2D springJoint2D;
    [SerializeField] private LayerMask colliderToVerify;
    public GameObject grappedObject;

    [SerializeField] private float distanceMaxForGrap;
    [SerializeField] private float distanceFromTheJointPoint;
    [SerializeField] private float startingFrequencyForJoint;
    [SerializeField] private float grapCooldown;

    [SerializeField] private bool canGrap;
    public bool isGrapActive;

    private void Start()
    {
        canGrap = true;
        springJoint2D = GetComponent<SpringJoint2D>();
        springJoint2D.frequency = startingFrequencyForJoint;
    }

    private void Update()
    {
        distanceFromTheJointPoint = Vector2.Distance(transform.position, springJoint2D.connectedAnchor);

        if (distanceFromTheJointPoint >= distanceMaxForGrap * 1.1f)
        {
            springJoint2D.frequency += Time.deltaTime / 2;
        }
        else
        {
            springJoint2D.frequency = startingFrequencyForJoint;
        }
    }

    public void GrapUngrap(InputAction.CallbackContext context)
    {
        springJoint2D.distance = distanceMaxForGrap;

        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = Camera.main.nearClipPlane;
        mousePos.z = -Camera.main.transform.position.z;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);

        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, mouseWorldPos - transform.position, distanceMaxForGrap, colliderToVerify);

        if (context.performed && isGrapActive && canGrap)
        {
            springJoint2D.enabled = false;
            isGrapActive = false;
            grappedObject = null;
        }
        else if (context.performed && hit2D)
        {
            springJoint2D.enabled = true;
            isGrapActive = true;
            springJoint2D.connectedAnchor = hit2D.point;
            grappedObject = hit2D.collider.gameObject;
            canGrap = false;
        }
        StartCoroutine(GrapCooldown(grapCooldown));
    }

    public IEnumerator GrapCooldown(float time)
    {
        yield return new WaitForSeconds(time);
        canGrap = true;
    }
}
