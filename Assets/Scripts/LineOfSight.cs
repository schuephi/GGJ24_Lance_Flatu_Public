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
    public float HeatDamage = 1.0f;

    public LanceScript Lance;
    public GameObject LightCone;

    public GoonAlert Alert;

    public float InvestigationHeatIncreasePerS = 100;
    public float InvestigationHeat = 0;
    public float InvestigationHeatMax = 100;
    public float InvestigationHeatDecayPerS = 50;

    public AnimationCurve HeatDistanceFactor = AnimationCurve.Linear(0, 0, 1, 1);

    private float TimeSinceLastDetection = 0f;
    private float TimeUntilInvestigationHeatDecay = 1f;

    public DetectionZone DetectionZone;


    // Start is called before the first frame update
    void Start()
    {
        Lance = FindFirstObjectByType<LanceScript>();

    }

    // Update is called once per frame
    void Update()
    {
        this.LookDirection = Goon.LookDirection;

        if (Goon.GoonMode != GoonMode.LOOKING_AROUND) {
            this.LookDirection = Quaternion.AngleAxis(Mathf.Sin(Time.time * WiggleSpeed) * WiggleAngle, Vector3.forward) * Goon.LookDirection;

            var angle = Vector3.Angle(Vector3.up, this.LookDirection);
            Goon.GoonAnimator.SetFloat("Angle", Mathf.Clamp(Mathf.Abs(angle) / 180f, 0f, 1f));
        }

        DetectionZone.SetLookDirection(this.LookDirection);

        if(DetectionZone.isPlayerInZone && CheckIfPlayerVisible())
        {
            var distanceToLance = (Lance.transform.position - transform.position).magnitude;
            var heatFactor = HeatDistanceFactor.Evaluate(distanceToLance);

            Lance.AddHeat((HeatDamage * heatFactor) * Time.deltaTime);
            if (Goon.GoonMode == GoonMode.PATROL)
            {               
                InvestigationHeat = Mathf.Clamp(InvestigationHeat + (InvestigationHeatIncreasePerS * heatFactor) * Time.deltaTime, 0, InvestigationHeatMax);

                if (InvestigationHeat >= InvestigationHeatMax)
                {
                    this.Goon.StartInvestigation(Lance.transform.position);
                    this.InvestigationHeat = 0;
                }
            }
            TimeSinceLastDetection = 0f;
        }
        else
        {
            if(InvestigationHeat > 0)
            {
                TimeSinceLastDetection += Time.deltaTime;
            }

            if (TimeSinceLastDetection >= TimeUntilInvestigationHeatDecay)
            {
                InvestigationHeat = Mathf.Clamp(InvestigationHeat - InvestigationHeatDecayPerS * Time.deltaTime, 0, InvestigationHeatMax);
            }
        }

        
        Alert.SetFillLevel(Goon.GoonMode == GoonMode.PATROL ? InvestigationHeat / InvestigationHeatMax : 1f);
    }

    bool CheckIfPlayerVisible()
    {
        var playerIsVisible = false;
        
        var lanceDirection = (Lance.transform.position - this.transform.position).normalized;
        
        var filter = new ContactFilter2D()
        {

        };

        var hits = new RaycastHit2D[1];

        if (Physics2D.Raycast(this.transform.position, lanceDirection, filter, hits) > 0)
        {
            playerIsVisible = hits[0].transform.gameObject.tag == "Lance" && Lance.IsHidden == false;
        }

        return playerIsVisible;
    }
}
