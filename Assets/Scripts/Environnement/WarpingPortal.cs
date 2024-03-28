using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpingPortal : MonoBehaviour
{
    public ObjectColorProperties actualColorProperties;
    public float warpCooldown;

    public static Dictionary<ScriptableObjectColor, List<WarpingPortal>> portalsDictionnary = new Dictionary<ScriptableObjectColor, List<WarpingPortal>>();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void Init()
    {
        portalsDictionnary.Clear();
    }

    public int delay = 0;

    private IEnumerator Start()
    {
        for (int i = 0; i < delay; i++) { yield return null; }

        actualColorProperties = transform.GetComponent<ObjectColorProperties>();
        actualColorProperties.OnColorChange.AddListener(SetColor);
        SetColor(actualColorProperties.actualColor);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            if(collision.gameObject.GetComponent<PlayerMovement>().warpingCooldown)
            {
                return;
            }
        }
        if (collision != null && collision.gameObject.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;
            StartCoroutine(ignorePlayerCooldown(player.GetComponent<PlayerMovement>()));
            WarpingPortal portal = GetTarget();
            Debug.Log(portal);
            if (portal == null)
            {
                return;
            }
            player.transform.position = new Vector3(portal.transform.position.x, portal.transform.position.y, portal.transform.position.z);
        }
    }

    public IEnumerator ignorePlayerCooldown(PlayerMovement playerWarpingCooldown)
    {
        playerWarpingCooldown.warpingCooldown = true;
        yield return new WaitForSeconds(warpCooldown);
        playerWarpingCooldown.warpingCooldown = false;
    }

    //Nello
    public WarpingPortal GetTarget()
    {
        if (lastColor == null)
        {
            Debug.Log("no last color");
            return null;
        }

        List<WarpingPortal> _portalsList;
        if (!portalsDictionnary.TryGetValue(lastColor, out _portalsList) || _portalsList.Count < 2 || !_portalsList.Contains(this))
        {
            Debug.Log("Invalid List");
            return null;
        }

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
        lastColor = scriptcolor;

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
