using System.Collections.Generic;
using UnityEngine;

public class WarpingPortal : MonoBehaviour
{
    public ObjectColorProperties actualColorProperties;

    public static Dictionary<ScriptableObjectColor, List<WarpingPortal>> portalsDictionnary = new Dictionary<ScriptableObjectColor, List<WarpingPortal>>();

    private void Start()
    {
        actualColorProperties = transform.GetComponent<ObjectColorProperties>();
        actualColorProperties.OnColorChange.AddListener(SetColor);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Player"))
        {
            if(GetTarget() == null)
            { return; }
            GameObject player = collision.gameObject;
            player.transform.position = new Vector3(GetTarget().transform.position.x, GetTarget().transform.position.y, GetTarget().transform.position.z);
        }
    }

    //Nello
    public WarpingPortal GetTarget()
    {
        if (lastColor == null) return null;

        List<WarpingPortal> _portalsList;
        if (!portalsDictionnary.TryGetValue(lastColor, out _portalsList) || _portalsList.Count < 2 || !_portalsList.Contains(this))
            return null;

        int index = _portalsList.IndexOf(this);
        return _portalsList[(index + 1) % _portalsList.Count];
    }

    //Nello
    ScriptableObjectColor lastColor;
    public void SetColor(ScriptableObjectColor scriptcolor)
    {
        if (lastColor == scriptcolor) return;

        if (lastColor != null)
        {
            portalsDictionnary[lastColor].Remove(this);
        }

        List<WarpingPortal> _portalsList;
        if (!portalsDictionnary.TryGetValue(scriptcolor, out _portalsList))
        {
            _portalsList = new List<WarpingPortal>();
            portalsDictionnary.Add(scriptcolor, _portalsList);
        }

        if (!_portalsList.Contains(this))
            _portalsList.Add(this);
    }
}
