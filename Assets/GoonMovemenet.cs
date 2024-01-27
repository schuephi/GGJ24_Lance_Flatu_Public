using UnityEngine;

public class GoonMovemenet : MonoBehaviour
{
    // Start is called before the first frame update
    public float MovementSpeed = 1.0f;
    public Vector3 LookDirection = Vector3.zero;

    public Vector3 TargetPoint= Vector3.zero;

    void Start()
    {
        TargetPoint = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var posDiff = this.TargetPoint - this.transform.position;

        this.transform.Translate(posDiff.normalized * MovementSpeed * Time.deltaTime);

        LookDirection = posDiff.normalized;
       // this.transform.rotation = 
    }
}
