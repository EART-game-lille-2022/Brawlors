using System.Collections;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] private ObjectColorProperties _objectColors;

    [SerializeField] float lifeDuration;

    private void Start()
    {
        _objectColors = GetComponent<ObjectColorProperties>();
        StartCoroutine(LifeDurationTimer(lifeDuration));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            
        if(collision != null && collision.gameObject.GetComponent<ObjectColorProperties>())
        {
            ObjectColorProperties collidedObjectColors = collision.gameObject.GetComponent<ObjectColorProperties>();
            collidedObjectColors.ApplyColor(_objectColors.actualColor);
            Destroy(gameObject);
        }
    }

    private IEnumerator LifeDurationTimer(float time)
    {
        yield return new WaitForSeconds(lifeDuration);
        Destroy(gameObject);
    }
}
