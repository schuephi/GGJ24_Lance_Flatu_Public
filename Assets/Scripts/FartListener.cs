using UnityEngine;

public class FartListener : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve DetectionDistance = AnimationCurve.Linear(0, 0, 1, 6);
    private FartManager fartManager;

    public GoonMovemenet Movemenet;

    private void Awake()
    {
        fartManager = FindFirstObjectByType<LanceScript>().FartManager;
    }

    private void OnEnable()
    {
        fartManager.OnFart += FartManager_OnFart;
    }

    private void FartManager_OnFart(float intensity)
    {
        if (Vector3.Distance(fartManager.transform.position, transform.position) <= (DetectionDistance.Evaluate(intensity)))
        {
            var hits = new RaycastHit2D[1];
            if (Physics2D.Raycast(transform.position, fartManager.transform.position - transform.position, new ContactFilter2D(), hits) > 0)
            {
                if (hits[0].transform.gameObject.tag == "Lance")
                {
                    Movemenet.StartInvestigation(fartManager.transform.position);
                }
            }

        }
    }

    private void OnDisable()
    {
        fartManager.OnFart -= FartManager_OnFart;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach(var key in DetectionDistance.keys)
        {
            Gizmos.DrawWireSphere(this.transform.position, key.value);
        }
        
    }

}
