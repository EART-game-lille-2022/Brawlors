using Unity.VisualScripting;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] private ObjectColorProperties canonColor;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPos;

    [SerializeField] private float bulletSpawnRate;
    private float bulletSpawnRateTreshold;

    private void Start()
    {
        bulletPrefab.GetComponent<ObjectColorProperties>().SwitchColor(canonColor.actualColor);
    }

    private void Update()
    {
        bulletSpawnRateTreshold += Time.deltaTime;
        if(bulletSpawnRateTreshold >= bulletSpawnRate)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPos.position, bulletSpawnPos.rotation);
            bullet.GetComponent<ObjectColorProperties>().SwitchColor(canonColor.actualColor);
            bulletSpawnRateTreshold = 0;
        }
    }
}