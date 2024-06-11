using UnityEngine;

public class ItemVisibilityController : MonoBehaviour
{
    [SerializeField] Transform itemHoldPosition; // Position where the item will be held
    [SerializeField] Item swordItem;
    [SerializeField] Item Scroll;
    private GameObject currentItem; // Currently visible item

    public void ShowItem(Item item)
    {
        // Destroy the currently held item if any
        if (currentItem != null)
        {
            Destroy(currentItem);
        }

        // Instantiate the new item model and set its position and parent
        if (item != null && item.itemPrefab != null)
        {
            
            currentItem = Instantiate(item.itemPrefab, itemHoldPosition.position, itemHoldPosition.rotation, itemHoldPosition);

            if(item == swordItem)
            {
                currentItem.transform.localRotation = Quaternion.Euler(90, 90, 90);
            }
            if (item == Scroll)
            {
                currentItem.transform.localRotation = Quaternion.Euler(90, 180, 0);
            }

            // Remove the Rigidbody if it exists
            Rigidbody rb = currentItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Destroy(rb);
            }
        }
    }

    public void HideItem()
    {
        if (currentItem != null)
        {
            Destroy(currentItem);
        }
    }

    public void AddRigidbody(GameObject item)
    {
        if (item != null)
        {
            Rigidbody rb = item.GetComponent<Rigidbody>();
            if (rb == null) // Only add if it doesn't already exist
            {
                rb = item.AddComponent<Rigidbody>();
                rb.mass = 1; // Set mass or other properties as needed
            }
        }
        else
        {
            Debug.LogWarning("Attempted to add Rigidbody to a null item.");
        }
    }
}
