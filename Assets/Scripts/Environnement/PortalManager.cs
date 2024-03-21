using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public static PortalManager instance;

    [SerializeField] public List<WarpingPortal> warpingPortals = new List<WarpingPortal>();
    [SerializeField] public ScriptableObjectColor blackPortal;
    [SerializeField] public List<WarpingPortal> blackWarpingPortals = new List<WarpingPortal>();
    [SerializeField] public ScriptableObjectColor bluePortal;
    [SerializeField] public List<WarpingPortal> blueWarpingPortals = new List<WarpingPortal>();
    [SerializeField] public ScriptableObjectColor defaultPortal;
    [SerializeField] public List<WarpingPortal> defaultWarpingPortals = new List<WarpingPortal>();
    [SerializeField] public ScriptableObjectColor greenPortal;
    [SerializeField] public List<WarpingPortal> greenWarpingPortals = new List<WarpingPortal>();
    [SerializeField] public ScriptableObjectColor purplePortal;
    [SerializeField] public List<WarpingPortal> purpleWarpingPortals = new List<WarpingPortal>();
    [SerializeField] public ScriptableObjectColor redPortal;
    [SerializeField] public List<WarpingPortal> redWarpingPortals = new List<WarpingPortal>();
    [SerializeField] public ScriptableObjectColor yellowPortal;
    [SerializeField] public List<WarpingPortal> yellowWarpingPortals = new List<WarpingPortal>();

    private void Start()
    {
        instance = this;
        SortPortals();
    }

    public void SortPortals()
    {
        blackWarpingPortals.Clear();
        blueWarpingPortals.Clear();
        defaultWarpingPortals.Clear();
        greenWarpingPortals.Clear();
        purpleWarpingPortals.Clear();
        redWarpingPortals.Clear();
        yellowWarpingPortals.Clear();

        if (warpingPortals.Count > 0)
        {
            foreach (WarpingPortal portal in warpingPortals)
            {
                if(portal.GetComponent<ObjectColorProperties>() != null && portal.GetComponent<ObjectColorProperties>().actualColor != null)
                { 
                    if(portal.GetComponent<ObjectColorProperties>().actualColor == blackPortal)
                    {
                        blackWarpingPortals.Add(portal);
                    }
                    if (portal.GetComponent<ObjectColorProperties>().actualColor == bluePortal)
                    {
                        blueWarpingPortals.Add(portal);
                    }
                    if (portal.GetComponent<ObjectColorProperties>().actualColor == defaultPortal)
                    {
                        defaultWarpingPortals.Add(portal);
                    }
                    if (portal.GetComponent<ObjectColorProperties>().actualColor == greenPortal)
                    {
                        greenWarpingPortals.Add(portal);
                    }
                    if (portal.GetComponent<ObjectColorProperties>().actualColor == purplePortal)
                    {
                        purpleWarpingPortals.Add(portal);
                    }
                    if (portal.GetComponent<ObjectColorProperties>().actualColor == redPortal)
                    {
                        redWarpingPortals.Add(portal);
                    }
                    if (portal.GetComponent<ObjectColorProperties>().actualColor == yellowPortal)
                    {
                        yellowWarpingPortals.Add(portal);
                    }
                }
            }
        }
    }
}
