using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private KeyCode spawnKey;
    [SerializeField] private GameObject bossLight; // Reference to the campfire light

    void Start()
    {
        Platforms.Instance.OnAllPlatformsActive += SpawnEnemy; // Subscribe to the event
    }

    void OnDestroy()
    {
        Platforms.Instance.OnAllPlatformsActive -= SpawnEnemy; // Unsubscribe to prevent memory leaks
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(spawnKey))
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        Debug.Log("Enemy spawned!");

        DestroyKeysOnPlatforms(); // Destroy keys after spawning the enemy

        // Optional: Turn on the light after spawning the enemy
        if (bossLight != null)
        {
            Debug.Log("Light turned on!");
            bossLight.SetActive(true);
        }
    }

    private void DestroyKeysOnPlatforms()
    {
        PressurePlate[] platforms = Platforms.Instance.GetComponentsInChildren<PressurePlate>(); // Get all pressure plates

        foreach (PressurePlate platform in platforms)
        {
            Collider[] colliders = Physics.OverlapBox(platform.transform.position, platform.GetComponent<Collider>().bounds.extents);

            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.CompareTag("Item"))
                {
                    Destroy(collider.gameObject); // Destroy the cube
                    Debug.Log("Item destroyed!");
                }
            }
        }
    }
}
