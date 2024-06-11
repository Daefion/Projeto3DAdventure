using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireTrigger : MonoBehaviour
{
    [SerializeField] GameObject key;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Item requiredItemType; // The item type that can activate the plate
    [SerializeField] GameObject particleEffectPrefab; // Particle system prefab
    [SerializeField] Transform particleEffectPosition; // Empty GameObject for particle system position

    private void OnTriggerEnter(Collider other)
    {
        ItemType itemType = other.gameObject.GetComponent<ItemType>();
        // Check if the entering object has a specific tag or component
        if (itemType != null && itemType.itemType == requiredItemType)
        {
            // Instantiate the item at the specified spawn point
            Instantiate(key, spawnPoint.position, spawnPoint.rotation);

            // Instantiate and play the particle effect at the empty GameObject's position
            GameObject particleEffect = Instantiate(particleEffectPrefab, particleEffectPosition.position, particleEffectPosition.rotation);
            ParticleSystem ps = particleEffect.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Play();
            }

            // Optional: Destroy the particle effect after it finishes
            Destroy(particleEffect, ps.main.duration);

            // Optional: Destroy the throwable object if needed
            Destroy(other.gameObject);
        }
    }
}
