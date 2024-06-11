using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSpawner : MonoBehaviour
{
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Transform spawnPoint;

    void Start()
    {
        Platforms.Instance.OnAllPlatformsActive += SpawnSword; // Subscribe to the event
    }

    void OnDestroy()
    {
        Platforms.Instance.OnAllPlatformsActive -= SpawnSword; // Unsubscribe to prevent memory leaks
    }

    private void SpawnSword()
    {
        Instantiate(swordPrefab, spawnPoint.position, spawnPoint.rotation);
        Debug.Log("Sword spawned!");

    }

}
