using System.Collections.Generic;
using UnityEngine;

public class PlayerKeys : MonoBehaviour
{
    [SerializeField] private List<KeyUnit> keysBag = new List<KeyUnit>();

    public void AddKey(KeyUnit keyAdded)
    {
        keysBag.Add(keyAdded);
    }

    public void RemoveKey(int KeyToRemove)
    {
        Destroy(keysBag[KeyToRemove].gameObject);
        keysBag.RemoveAt(KeyToRemove);
    }

    public int RemainingKeys()
    {
        return keysBag.Count;
    }

    public bool CheckIfPlayerStoreNeededKey(ScriptableObjectColor ColorOfTheKeyToSearch)
    {
        if (RemainingKeys() > 0)
        {
            for (int i = 0; i < keysBag.Count; i++)
            {
                if (keysBag[i].colorOfTheKey == ColorOfTheKeyToSearch)
                {
                    RemoveKey(i);
                    return true;
                }
            }
        }
        return false;
    }
}
