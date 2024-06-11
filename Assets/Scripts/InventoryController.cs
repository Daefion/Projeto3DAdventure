using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public Item[] slots;
    public Image[] slotImage;
    public int[] slotAmount;

    private InterfaceController iController;
    private ItemVisibilityController itemVisibilityController;
    private int selectedSlot = 0;

    void Start()
    {
        iController = FindObjectOfType<InterfaceController>();
        itemVisibilityController = FindObjectOfType<ItemVisibilityController>();

        // Show the initial selected slot item
        UpdateSelectedItem();
    }

    void Update()
    {
        // Raycast to check for items in front of the player
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(ray, out hit, 5f))
        {
            if (hit.collider.CompareTag("Item"))
            {
                ItemType itemType = hit.transform.GetComponent<ItemType>();
                if (itemType != null)
                {
                    iController.ItemText.text = "Press (E) to collect the " + itemType.itemType.name;
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        CollectItem(itemType, hit);
                    }
                }
            }
            else
            {
                iController.ItemText.text = null;
            }
        }

        // Check if Q is pressed to drop an item
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropSelectedItem();
        }

        // Check for slot selection (1-6)
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectSlot(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SelectSlot(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) SelectSlot(4);
        if (Input.GetKeyDown(KeyCode.Alpha6)) SelectSlot(5);
    }

    void CollectItem(ItemType itemType, RaycastHit hit)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null || slots[i].name == itemType.itemType.name)
            {
                slots[i] = itemType.itemType;
                slotAmount[i]++;
                slotImage[i].sprite = slots[i].itemSprite;

                // Remove Rigidbody before destroying the game object
                Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Destroy(rb);
                }

                Destroy(hit.transform.gameObject);
                break;
            }
        }
    }

    void SelectSlot(int slotIndex)
    {
        selectedSlot = slotIndex;
        UpdateSelectedItem();
    }

    void UpdateSelectedItem()
    {
        // Show the item in the selected slot
        if (itemVisibilityController != null)
        {
            itemVisibilityController.ShowItem(slots[selectedSlot]);
        }
    }

    void DropSelectedItem()
    {
        if (slots[selectedSlot] != null && slotAmount[selectedSlot] > 0)
        {
            // Calculate drop position
            Vector3 dropPosition = transform.position + transform.forward + Vector3.up;

            // Instantiate the specific item prefab in the game world
            GameObject droppedItem = Instantiate(slots[selectedSlot].itemPrefab, dropPosition, Quaternion.identity);

            // Ensure the itemPrefab has the ItemType component and set its properties
            ItemType itemTypeComponent = droppedItem.GetComponent<ItemType>();
            if (itemTypeComponent == null)
            {
                Debug.LogWarning("Dropped item does not have an ItemType component.");
                return;
            }

            itemTypeComponent.itemType = slots[selectedSlot];

            // Add Rigidbody to the dropped item
            if (itemVisibilityController != null)
            {
                itemVisibilityController.AddRigidbody(droppedItem);
            }
            else
            {
                Debug.LogWarning("ItemVisibilityController is not assigned.");
            }

            // Update inventory
            slotAmount[selectedSlot]--;
            if (slotAmount[selectedSlot] <= 0)
            {
                slots[selectedSlot] = null;
                slotImage[selectedSlot].sprite = null;
            }

            // Update the selected item visibility
            UpdateSelectedItem();
        }
    }
}
