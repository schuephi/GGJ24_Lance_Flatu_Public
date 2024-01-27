using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    public GoonMovemenet Goon;

    public Vector3 LookDirection;
    public float RotationSpeed = 1.0f;
    public float WiggleAngle = 1.0f;
    public float WiggleSpeed = 1.0f;
    public float ConeAngle = 20f;
    public float ConeDistance = 3f;
    public float LanceAngle = 0f;
    public float HeatDamage = 1.0f;

    public GameObject Lance;
    public GameObject ViewCone;

    public GameObject Alert;

    // Start is called before the first frame update
    void Start()
    {
        Lance = FindFirstObjectByType<LanceScript>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        this.LookDirection = Quaternion.AngleAxis(Mathf.Sin(Time.time * WiggleSpeed) * WiggleAngle, Vector3.forward) * Goon.LookDirection;

        ViewCone.transform.rotation = Quaternion.Lerp(ViewCone.transform.rotation, Quaternion.FromToRotation(Vector3.up, LookDirection), RotationSpeed * Time.deltaTime);

        var lanceDirection = (Lance.transform.position - this.transform.position).normalized;
        LanceAngle = Vector3.Angle(lanceDirection, this.LookDirection);
        Alert.gameObject.SetActive(false);
        if (LanceAngle < ConeAngle)
        {
            var filter = new ContactFilter2D()
            {

            };

            var hits = new RaycastHit2D[1];
            
            if (Physics2D.Raycast(this.transform.position, lanceDirection, filter, hits) > 0)
            {
                if (hits[0].transform.gameObject.tag == "Lance")
                {
                    // Debug.Log("Lance detected!");
                    Alert.gameObject.SetActive(true);
                    var lanceScript = hits[0].transform.gameObject.GetComponent<LanceScript>();
                    lanceScript.Heat += HeatDamage * Time.deltaTime;
                }
                
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawLine(this.transform.position, this.transform.position + (this.LookDirection * 3));
        }




    }
}
