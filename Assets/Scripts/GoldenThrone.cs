using UnityEngine;

public class GoldenThrone : MonoBehaviour
{

    private bool isPlayerInReach = false;
    [SerializeField]
    private UnityEngine.UI.Image keyIndicator;

    private void Start()
    {
        keyIndicator.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerInReach && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Player wins");
            GameManager.Instance.OnGameGoalReached();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Lance"))
        {
            isPlayerInReach = true;
            keyIndicator.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Lance"))
        {
            isPlayerInReach = false;
            keyIndicator.gameObject.SetActive(false);
        }
    }
}
