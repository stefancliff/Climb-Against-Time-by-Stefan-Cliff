using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] public GameObject PlayerPrefabToInstantiate;
    [SerializeField] public GameObject cameraPrefab;
    [SerializeField] public Transform endOfLevelCameraLocation;
    CameraScript cameraScript;
    private GameObject InstantiatedPlayerPrefab;
    void Start()
    {
        InstantiatedPlayerPrefab = Instantiate(PlayerPrefabToInstantiate, transform.position, transform.rotation);
        
        //Instantiate the camera prefab and attach the camera script to it
        GameObject cameraInstance = Instantiate(cameraPrefab, InstantiatedPlayerPrefab.transform.position, Quaternion.identity);
        CameraScript cameraScript = cameraInstance.GetComponent<CameraScript>();
        cameraInstance.transform.SetParent(InstantiatedPlayerPrefab.transform);

        if (cameraScript != null)
        {
            cameraScript.player = InstantiatedPlayerPrefab.transform; // Assign the target for the camera to follow
        }

        
    }

    void Update()
    {
        if(cameraScript.isAtEndOfLevel)
        {
            cameraScript.EndOfLevelCameraMovement(endOfLevelCameraLocation);
        }
    }

    
}
