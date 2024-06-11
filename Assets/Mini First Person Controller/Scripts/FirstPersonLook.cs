using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    private const CursorLockMode locked = CursorLockMode.Locked;
    [SerializeField]
    Transform character;
    public float sensitivity = 2;
    public float smoothing = 1.5f;

    bool CursorActive = false;
    Vector2 velocity;
    Vector2 frameVelocity;


    void Reset()
    {
        // Get the character from the FirstPersonMovement in parents.
        character = GetComponentInParent<FirstPersonMovement>().transform;
    }

    void Start()
    {
        Cursor.lockState = locked;
    }

    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.P))
        {   
            CursorActive = !CursorActive;
        }

        if (!CursorActive)
        {
            Cursor.lockState = locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
        }

        // Get smooth velocity.
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
        frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
        velocity += frameVelocity;
        velocity.y = Mathf.Clamp(velocity.y, -90, 90);

        // Rotate camera up-down and controller left-right from velocity.
        transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
        character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
    }
}
