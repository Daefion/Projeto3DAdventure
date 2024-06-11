using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    public static Platforms Instance;

    public bool allActive = false;
    PressurePlate[] platforms;
    public int nActive = 0;

    // Define the delegate and event
    public delegate void AllPlatformsActiveAction();
    public event AllPlatformsActiveAction OnAllPlatformsActive;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        platforms = FindObjectsOfType<PressurePlate>();
    }

    public void CheckAllActive()
    {
        nActive = 0;

        foreach (PressurePlate platform in platforms)
        {
            if (platform.isActive)
            {
                nActive++;
            }
        }

        if (nActive == platforms.Length)
        {
            allActive = true;
            Debug.Log("All platforms are active!");
            OnAllPlatformsActive?.Invoke(); // Trigger the event if all platforms are active
        }
        else
        {
            allActive = false;
            Debug.Log("Not all platforms are active.");
        }
    }

    public void UpdatePlatformStatus()
    {
        CheckAllActive();
    }
}