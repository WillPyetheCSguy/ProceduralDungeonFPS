using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypointManager : MonoBehaviour
{
    public List<GameObject> waypoints;
    // Start is called before the first frame update
    public void AddWayPoint(GameObject point)
    {
        waypoints.Add(point);
    }
    public List<GameObject> getWaypoints()
    {
        return waypoints;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
