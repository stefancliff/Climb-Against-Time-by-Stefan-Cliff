using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The player's transform component
    public float smoothSpeed = 0.125f; // The smoothness of the camera movement

    private Vector3 offset;

    private void Start()
    {
        // Calculate the initial offset between the camera and the player
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        // Calculate the desired position for the camera
        Vector3 desiredPosition = target.position + offset;

        // Interpolate between the current position and the desired position smoothly
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera position
        transform.position = smoothedPosition;
    }
}
