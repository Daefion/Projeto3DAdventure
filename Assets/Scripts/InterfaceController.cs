using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class InterfaceController : MonoBehaviour
{
    public Text ItemText; // UI Text to display the item-specific message
    public Text CollectText; // UI Text to display "Collect Rewards"
    public Image BlackScreen; // UI Image for black screen overlay
    public float displayTime = 5f; // Time to display the black screen
    public LayerMask itemLayer; // Layer for items to be detected
    public Vector3 teleportPosition; // Position to teleport the player
    public string nextSceneName; // Name of the next scene to load

    private Camera mainCamera;
    private GameObject currentItem;

    private void Start()
    {
        ItemText.text = string.Empty;
        CollectText.text = "Collect Rewards";
        CollectText.gameObject.SetActive(false); // Ensure CollectText is initially hidden
        BlackScreen.gameObject.SetActive(false);
        mainCamera = Camera.main;
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, itemLayer))
        {
            GameObject item = hit.collider.gameObject;
            if (item != null)
            {
                // Assume the item has a component with a 'name' field, adjust as necessary
                string itemName = item.name; // Replace with the appropriate property/method if different
                ItemText.text = itemName;
                currentItem = item;

                // Show "Collect Rewards" text when looking at the item
                CollectText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    FakeCollectItem();
                }
            }
        }
        else
        {
            ItemText.text = string.Empty;
            CollectText.gameObject.SetActive(false); // Hide CollectText when not looking at an item
            currentItem = null;
        }
    }

    private void FakeCollectItem()
    {
        // Teleport the player
        TeleportPlayer();

        // Start the coroutine to change the scene after a delay
        StartCoroutine(ChangeSceneAfterDelay());
    }

    private void TeleportPlayer()
    {
        // Teleport the player to the specified position
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = teleportPosition;
    }

    private IEnumerator ChangeSceneAfterDelay()
    {
        // Display the black screen
        BlackScreen.gameObject.SetActive(true);

        // Wait for the specified display time
        yield return new WaitForSeconds(displayTime);

        // Hide the black screen
        BlackScreen.gameObject.SetActive(false);

        // Wait additional 10 seconds after teleporting before changing the scene
        yield return new WaitForSeconds(10f);

        // Change scene
        SceneManager.LoadScene(nextSceneName);
    }
}
