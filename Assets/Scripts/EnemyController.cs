using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class enemyController : MonoBehaviour
{
    private NavMeshAgent nv;
    [SerializeField] private float range;
    private Transform player;
    [SerializeField] private GameObject deathPrefab; // Reference to the prefab to instantiate on enemy death
    private bool isDead = false; // Flag to prevent multiple death events

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start()
    {
        nv = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) < range)
        {
            nv.SetDestination(player.position);
            nv.isStopped = false;
        }
        else
        {
            nv.isStopped = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDead && other.CompareTag("Item"))
        {
            isDead = true; // Mark the enemy as dead to prevent multiple triggers
            Debug.Log("YOU KILLED THE ENEMY!");

            if (deathPrefab != null)
            {
                GameObject instantiatedPrefab = Instantiate(deathPrefab, transform.position, transform.rotation);

                Rigidbody rb = instantiatedPrefab.AddComponent<Rigidbody>();
                rb.mass = 1f;
                rb.drag = 0f;
                rb.angularDrag = 0.05f;
                rb.useGravity = true;
                rb.isKinematic = false;
            }

            Destroy(gameObject); // Destroy the enemy game object
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isDead && collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with enemy!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
