using UnityEngine;

public class DetectionZone : MonoBehaviour
{

    public bool isPlayerInZone = false;
    public int CollidingColliders = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetLookDirection(Vector3 lookDirection)
    {
        this.transform.rotation = Quaternion.Euler(0, 0, (lookDirection.x > 0 ? -1 : 1) * Vector3.Angle(Vector3.up, lookDirection));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Lance")
        {
            CollidingColliders++;
            isPlayerInZone = CollidingColliders > 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Lance")
        {
            CollidingColliders--;
            isPlayerInZone = CollidingColliders > 0;
        }
    }
}
