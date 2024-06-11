using UnityEngine;
using UnityEngine.UI;

public class SceneTeleport : MonoBehaviour
{
    public Vector3 targetPosition;
    public Text interactionText;
    public string interactionMessage = "Press E to teleport";

    private bool isPlayerNearby = false;

    void Start()
    {
        if (interactionText != null)
        {
            interactionText.text = "";
        }
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Teleport();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (interactionText != null)
            {
                interactionText.text = interactionMessage;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (interactionText != null)
            {
                interactionText.text = "";
            }
        }
    }

    void Teleport()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = targetPosition;
    }
}
