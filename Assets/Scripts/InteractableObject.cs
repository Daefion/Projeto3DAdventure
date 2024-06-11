using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class InteractableObject : MonoBehaviour
{
    [SerializeField] private string ItemName;
    private Rigidbody ObjectRigidbody;
    private Transform objectGrabTransformPoint;

    private void Awake()
    {
        ObjectRigidbody = GetComponent<Rigidbody>();
    }
 
    public string GetItemName()
    {
        return ItemName;
    }

    public void Grab(Transform objectGrabTransformPoint){
        this.objectGrabTransformPoint = objectGrabTransformPoint;
        ObjectRigidbody.useGravity = false;
    }

    public void Release(){
        this.objectGrabTransformPoint = null;
        ObjectRigidbody.useGravity = true;
    }

    private void FixedUpdate()
    {
        if(objectGrabTransformPoint != null){
            float lerpSpeed = 15f;
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabTransformPoint.position, Time.deltaTime * lerpSpeed);
            ObjectRigidbody.MovePosition(newPosition);
        }
    }
}