using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpingPortal : MonoBehaviour
{
    public Transform actualPosition;
    public ObjectColorProperties actualColorProperties;

    public static Dictionary<ScriptableObjectColor, List<WarpingPortal>> portalsDictionnary = new Dictionary<ScriptableObjectColor, List<WarpingPortal>>();

    private void Start()
    {
        actualPosition = transform;
        actualColorProperties = transform.GetComponent<ObjectColorProperties>();
        actualColorProperties.OnColorChange.AddListener(SetColor);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            
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
        return _portalsList[(index + 1)%_portalsList.Count];
    }

    //Nello
    ScriptableObjectColor lastColor;
    public void SetColor(ScriptableObjectColor scriptcolor) {
        if(lastColor == scriptcolor) return;

        if(lastColor != null) {
            portalsDictionnary[lastColor].Remove(this);
        }

        List<WarpingPortal> _portalsList;
        if(!portalsDictionnary.TryGetValue(scriptcolor, out _portalsList)) {
            _portalsList = new List<WarpingPortal>();
            portalsDictionnary.Add(scriptcolor, _portalsList);
        }

        if(!_portalsList.Contains(this)) 
            _portalsList.Add(this);
    }
}
