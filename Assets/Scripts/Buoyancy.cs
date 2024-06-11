using UnityEngine;

public class Buoyancy : MonoBehaviour
{
    public float buoyancyForce = 0f; // Adjust this value based on your needs
    public LayerMask waterLayer; // Layer to identify water

    private Rigidbody rb;
    private bool isInWater = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true; // Ensure gravity is enabled
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & waterLayer) != 0)
        {
            isInWater = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & waterLayer) != 0)
        {
            isInWater = false;
        }
    }

    void FixedUpdate()
    {
        if (rb == null) return; // Check if rb is null

        if (isInWater)
        {
            ApplyBuoyancy();
        }
        else
        {
            rb.drag = 0f; // Reset drag when not in water
        }
    }

    void ApplyBuoyancy()
    {
        if (rb == null) return; // Check if rb is null

        // Apply a buoyancy force upward
        rb.AddForce(Vector3.up * buoyancyForce, ForceMode.Acceleration);
        rb.drag = 2f; // Add drag to simulate water resistance
    }
}
