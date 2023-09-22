using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] public Transform player;
    private Transform endOfLevelCameraLocation;
    public float moveSpeed  = 2f;
    public float zoomSpeed  = 2f;
    public float zoomAmount = 2f;
    public bool isCameraMoving = false;
    
    public bool isAtEndOfLevel = false;
    void Start()
    {
        transform.position = new Vector3(player.position.x, player.position.y, player.position.z-2);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAtEndOfLevel)
        {
            FollowPlayer();
        }
        else 
        {
            EndOfLevelCameraMovement(endOfLevelCameraLocation);
        }
    }

    void FollowPlayer()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z); // to follow the player character
    }

    public void EndOfLevelCameraMovement(Transform endOfLevelCameraLocation)
    {
        if(!isCameraMoving)
        {
            StartCoroutine(MoveCameraToNewAnchor(endOfLevelCameraLocation));
        }
    }

    IEnumerator MoveCameraToNewAnchor(Transform endOfLevelCameraLocation)
    {
        isCameraMoving      = true;
        transform.parent    = null;
        
        Vector3 initialPosition     = transform.position;
        Vector3 targetPosition      = endOfLevelCameraLocation.position;

        float initialSize   = Camera.main.orthographicSize;
        float targetSize    = initialSize / zoomAmount;

        float elapsedTime = 0f;

        while (elapsedTime < moveSpeed)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / moveSpeed);
            Camera.main.orthographicSize = Mathf.Lerp(initialSize, targetSize, elapsedTime / zoomSpeed);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position              = targetPosition;
        Camera.main.orthographicSize    = targetSize;

        isCameraMoving = false;
    }
}
