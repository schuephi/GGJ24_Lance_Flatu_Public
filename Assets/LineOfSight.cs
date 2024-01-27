using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    public GoonMovemenet Goon;

    public Vector3 LookDirection;
    public float RotationSpeed = 1.0f;

    public GameObject ViewCone;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.LookDirection = Goon.LookDirection;

        ViewCone.transform.rotation = Quaternion.Lerp(ViewCone.transform.rotation, Quaternion.FromToRotation(Vector3.up, LookDirection), RotationSpeed * Time.deltaTime);
        
    }
}
