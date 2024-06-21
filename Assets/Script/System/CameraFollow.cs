using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Distances")]
    public float distance = 5f;
    public float minDistance = 1f;
    public float maxDistance = 7f;
    public Vector3 offset;

    [Header("Speeds")]
    public float smoothSpeed = 5f;
    public float scrollSensitivity = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate() 
{
    if(!target)
    {
        Debug.LogError("No Target set for the Camera!");
        return;
    }

    // Adjust the distance based on mouse scroll input
    float scrollInput = Input.GetAxis("Mouse ScrollWheel");
    distance -= scrollInput * scrollSensitivity;
    distance = Mathf.Clamp(distance, minDistance, maxDistance);

    // Calculate the desired position based on the target's position and distance
    Vector3 desiredPosition = target.position - transform.forward * distance + offset;

    // Smoothly move the camera towards the desired position
    transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
}

}
