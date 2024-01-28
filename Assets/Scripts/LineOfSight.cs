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

    public LanceScript Lance;
    public GameObject ViewCone;

    public GameObject Alert;

    public float GoonInvestigationHeatIncreasePerS = 50;
    public float GoonHeat = 0;
    public float GoonHeatDecayPerS = 50; 

    // Start is called before the first frame update
    void Start()
    {
        Lance = FindFirstObjectByType<LanceScript>();
    }

    // Update is called once per frame
    void Update()
    {
        this.LookDirection = Goon.LookDirection;
        Alert.gameObject.SetActive(false);

        if (Goon.GoonMode != GoonMode.LOOKING_AROUND) {
            this.LookDirection = Quaternion.AngleAxis(Mathf.Sin(Time.time * WiggleSpeed) * WiggleAngle, Vector3.forward) * Goon.LookDirection;

            var angle = Vector3.Angle(Vector3.up, this.LookDirection);
            Goon.GoonAnimator.SetFloat("Angle", Mathf.Clamp(Mathf.Abs(angle) / 180f, 0f, 1f));
        }
        
        if(Goon.GoonMode != GoonMode.PATROL)
        {
            Alert.gameObject.SetActive(true);
        }    

        var lanceDirection = (Lance.transform.position - this.transform.position).normalized;
        LanceAngle = Vector3.Angle(lanceDirection, this.LookDirection);

        if (LanceAngle < ConeAngle)
        {
            var filter = new ContactFilter2D()
            {

            };

            var hits = new RaycastHit2D[1];
            
            if (Physics2D.Raycast(this.transform.position, lanceDirection, filter, hits, ConeDistance) > 0)
            {
                if (hits[0].transform.gameObject.tag == "Lance" && Lance.IsHidden == false)
                {
                    Alert.gameObject.SetActive(true);
                    var lanceScript = hits[0].transform.gameObject.GetComponent<LanceScript>();
                    lanceScript.Heat += HeatDamage * Time.deltaTime;
                    if (Goon.GoonMode == GoonMode.PATROL)
                    {
                        GoonHeat = Mathf.Clamp(GoonHeat + GoonInvestigationHeatIncreasePerS * Time.deltaTime, 0, 100);

                        if (GoonHeat >= 100)
                        {
                            this.Goon.StartInvestigation(hits[0].transform.position);
                            this.GoonHeat = 0;
                        }
                    }
                }
                else
                {
                    GoonHeat = Mathf.Clamp(GoonHeat - GoonHeatDecayPerS * Time.deltaTime, 0, 100);
                }
                
            }
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.DrawLine(this.transform.position, this.transform.position + (this.LookDirection * ConeDistance));
    }
}
