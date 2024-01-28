using UnityEngine;

public class GoonMovemenet : MonoBehaviour
{
    // Start is called before the first frame update
    public float MovementSpeed = 1.0f;
    public Vector3 LookDirection = Vector3.zero;

    public Vector3 TargetPoint= Vector3.zero;
    public Vector3 LastPosition= Vector3.zero;

    public Animator GoonAnimator;

    void Start()
    {
        TargetPoint = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var posDiff = this.TargetPoint - this.transform.position;

        var moveAmount = posDiff.normalized * MovementSpeed * Time.deltaTime;

        GoonAnimator.SetFloat("Speed", moveAmount.magnitude > 0 ? 1 : 0);

        GoonAnimator.transform.localScale = new Vector3(moveAmount.x > 0 ? -1 : 1, 1, 1);

        this.transform.Translate(moveAmount);

        LookDirection = posDiff.normalized;


       // this.transform.rotation = 
    }
}
