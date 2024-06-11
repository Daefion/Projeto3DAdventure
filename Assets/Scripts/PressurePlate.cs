using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public bool isActive = false;

    [SerializeField] Item requiredItemType; // The item type that can activate the plate
    [SerializeField] ParticleSystem activationParticles; // Reference to the particle system

    void OnTriggerEnter(Collider other)
    {
        ItemType itemType = other.gameObject.GetComponent<ItemType>();
        if (itemType != null && itemType.itemType == requiredItemType)
        {
            isActive = true;
            Debug.Log("Platform is active!");
            Platforms.Instance.UpdatePlatformStatus(); // Update the status

            // Play the particle system
            if (activationParticles != null)
            {
                activationParticles.Play();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        ItemType itemType = other.gameObject.GetComponent<ItemType>();
        if (itemType != null && itemType.itemType == requiredItemType)
        {
            isActive = false;
            Debug.Log("Platform is inactive!");
            Platforms.Instance.UpdatePlatformStatus(); // Update the status

            // Stop the particle system
            if (activationParticles != null)
            {
                activationParticles.Stop();
            }
        }
    }
}
