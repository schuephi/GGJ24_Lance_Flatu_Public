using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class HidingSpots : MonoBehaviour
{
    public UnityEvent OnPlayerHide;
    public UnityEvent OnPlayerUnhide;
    public UnityEvent OnPlayerFailedInteraction;

    private bool isPlayerInside = false;
    private bool isPlayerInReach = false;
    [SerializeField]
    private UnityEngine.UI.Image keyIndicator;

    private LanceScript lanceScript;
    public float MaxFlatulenceForInteraction = 0.6f;

    private void Start()
    {
        lanceScript = GameObject.FindGameObjectWithTag("Lance").GetComponent<LanceScript>();
        keyIndicator.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerInReach && Input.GetKeyDown(KeyCode.E))
        {
            if (!lanceScript.IsImmobile)
            {
                OnPlayerHide?.Invoke();
                isPlayerInside = true;
                lanceScript.IsHidden = true;

                Debug.Log("Player tries to enter hiding spot");
            }
            else
            {
                if (lanceScript.Flatulence > MaxFlatulenceForInteraction)
                {
                    OnPlayerFailedInteraction?.Invoke();
                }
            }
        }

        if (isPlayerInside && (Input.GetKeyUp(KeyCode.E) || isPlayerInReach == false))
        {
            OnPlayerUnhide?.Invoke();
            isPlayerInside = false;
            lanceScript.IsHidden = false;
            Debug.Log("Player leaves hiding spot");
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
