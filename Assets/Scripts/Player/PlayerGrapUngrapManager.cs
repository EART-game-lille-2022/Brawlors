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

    public LineRenderer lineRenderer;

    private void Start()
    {
        canGrap = true;
        springJoint2D = GetComponent<SpringJoint2D>();
        springJoint2D.frequency = startingFrequencyForJoint;
    }
    float ropeTime = 0.0f;
    public AnimationCurve ropeCurve;
    public AnimationCurve ropeSpring;
    public float curveAmount = 10;
    public int ropeQuality = 10;

    private void Update()
    {
        lineRenderer.enabled = springJoint2D.enabled;
        if (!springJoint2D.enabled)
        {
            ropeTime = 0;
            return;
        }
        distanceFromTheJointPoint = Vector2.Distance(transform.position, springJoint2D.connectedAnchor);

        if (distanceFromTheJointPoint >= distanceMaxForGrap * 1.1f)
        {
            springJoint2D.frequency += Time.deltaTime / 2;
        }
        else
        {
            springJoint2D.frequency = startingFrequencyForJoint;
        }

        Quaternion qrot = Quaternion.LookRotation(springJoint2D.connectedAnchor - (Vector2)transform.position);

        if (lineRenderer)
        {
            int resolution = Mathf.RoundToInt(ropeQuality * distanceFromTheJointPoint);
            lineRenderer.positionCount = resolution;
            Vector3[] points = new Vector3[resolution];
            for (int i = 0; i < resolution; i ++) {
                float t = i / (float) resolution;
                points[i] = Vector3.Lerp(transform.position, springJoint2D.connectedAnchor, t) + qrot*new Vector3(0, curveAmount* ropeSpring.Evaluate(ropeTime) * ropeCurve.Evaluate(t) , 0);
            }
            lineRenderer.SetPositions(points);

            ropeTime += Time.deltaTime;
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
