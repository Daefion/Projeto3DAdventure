using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{

    public Transform character;

    // Start is called before the first frame update
   void LateUpdate()
    {
        Vector3 newPosition = character.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        transform.rotation = Quaternion.Euler(90f, character.eulerAngles.y, 0f);
    }
}
