using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    // Start is called before the first frame update
    public GoonMovemenet Goon;
    public List<Vector3> Waypoints= new List<Vector3>();
    public int currentWaypointIndex = 0;

    void Start()
    {
        Waypoints = GetComponentsInChildren<WaypointMarker>().Select(x => x.transform.position).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        Goon.TargetPoint = Waypoints[currentWaypointIndex];
        if((Goon.transform.position - Goon.TargetPoint).magnitude < 0.01)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= Waypoints.Count) { currentWaypointIndex = 0; }
        }
    }
}
