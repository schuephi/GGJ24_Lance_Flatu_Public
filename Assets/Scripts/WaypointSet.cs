using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaypointSet : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Vector3> Waypoints = new List<Vector3>();


    void Start()
    {
        Waypoints = GetComponentsInChildren<WaypointMarker>().Select(x => x.transform.position).ToList();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
