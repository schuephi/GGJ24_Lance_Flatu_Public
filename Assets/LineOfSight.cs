using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    public GoonMovemenet Goon;

    public Vector3 LookDirection;

    public GameObject ViewCone;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.LookDirection = Goon.LookDirection;
      
    }
}
