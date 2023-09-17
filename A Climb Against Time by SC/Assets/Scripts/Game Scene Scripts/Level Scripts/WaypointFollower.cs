using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] Waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] private float platformSpeed = 2f;
    
    private void Update()
    {
        if(Vector2.Distance(Waypoints[currentWaypointIndex].transform.position, transform.position) < 0.1f) // checking the distance between the current platform and the next one. Using .1f instead of 0f to avoid bugs/awkward moments
        {
            currentWaypointIndex++;
            
            if(currentWaypointIndex >= Waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        
        transform.position = Vector2.MoveTowards(transform.position, Waypoints[currentWaypointIndex].transform.position, Time.deltaTime * platformSpeed);
    }
}
