using System.Collections;
using UnityEngine;

public enum GoonMode
{
    PATROL,
    INVESTIGATE,
    LOOKING_AROUND,
    MOVE_BACK
}

public class GoonMovemenet : MonoBehaviour
{
    // Start is called before the first frame update
    public float MovementSpeed = 1.0f;
    public Vector3 LookDirection = Vector3.zero;

    public Vector3 TargetPoint= Vector3.zero;
    public Vector3 LastPosition= Vector3.zero;

    public Animator GoonAnimator;

    public GoonMode GoonMode = GoonMode.PATROL;

    public WaypointSet WaypointsSet;
    public int currentWaypointIndex = 0;

    public Vector3 investigationStartPoint;
    public float LookAroundTime = 1f;


    void Start()
    {
        TargetPoint = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        switch(GoonMode)
        {
            case GoonMode.PATROL:
                {
                    TargetPoint = WaypointsSet?.Waypoints[currentWaypointIndex] ?? this.transform.position;
                    if ((transform.position - TargetPoint).magnitude < 0.01)
                    {
                        currentWaypointIndex++;
                        if (currentWaypointIndex >= (WaypointsSet?.Waypoints.Count ?? 0)) { currentWaypointIndex = 0; }
                    }
                    break;
                }
            case GoonMode.INVESTIGATE:
                {
                    break;
                }
            case GoonMode.LOOKING_AROUND:
                {
                    return;
                }
            case GoonMode.MOVE_BACK:
                {
                    break;
                }
        }

        var posDiff = this.TargetPoint - this.transform.position;

        var moveAmount = posDiff.normalized * MovementSpeed * Time.deltaTime;

        GoonAnimator.SetFloat("Speed", moveAmount.magnitude > 0 ? 1 : 0);

        if(moveAmount.x != 0 && GoonMode != GoonMode.LOOKING_AROUND)
        {
            GoonAnimator.transform.localScale = new Vector3(moveAmount.x > 0 ? -1 : 1, 1, 1);
        }

        this.transform.Translate(moveAmount);

        LookDirection = posDiff.normalized;

        if(GoonMode == GoonMode.INVESTIGATE && posDiff.magnitude <= 0.1f)
        {
            GoonMode = GoonMode.LOOKING_AROUND;
            Debug.Log("Hmmm...");
            StartCoroutine(LookAround());
        }

        if(GoonMode == GoonMode.MOVE_BACK && posDiff.magnitude <= 0.1f)
        {            
            GoonMode = GoonMode.PATROL;
        }

    }

    public void StartInvestigation(Vector3 targetPosition)
    {
        Debug.Log("Start investigation");
        investigationStartPoint = this.transform.position;
        this.TargetPoint = targetPosition;
        this.GoonMode = GoonMode.INVESTIGATE;
    }

    IEnumerator LookAround()
    {
        GoonAnimator.SetFloat("Speed", 0);
        for (float angle = LookAroundTime; angle >= 0; angle -= Time.deltaTime)
        {
            GoonAnimator.SetFloat("Angle", angle / LookAroundTime);
            LookDirection = Quaternion.AngleAxis(GoonAnimator.transform.localScale.x *  angle / LookAroundTime * 180, Vector3.forward) * Vector3.up;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        GoonAnimator.transform.localScale = new Vector3(-GoonAnimator.transform.localScale.x, 1, 1);

        for (float angle = LookAroundTime; angle >= 0; angle -= Time.deltaTime)
        {
            GoonAnimator.SetFloat("Angle", angle / LookAroundTime);
            LookDirection = Quaternion.AngleAxis(GoonAnimator.transform.localScale.x * angle / LookAroundTime * 180, Vector3.forward) * Vector3.up;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        GoonMode = GoonMode.MOVE_BACK;
        TargetPoint = investigationStartPoint;
    }
}
