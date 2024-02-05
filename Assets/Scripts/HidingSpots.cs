using Assets.Scripts;
using UnityEngine;
using UnityEngine.Events;

public class HidingSpots : MonoBehaviour, IInteractable
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Lance"))
        {
            isPlayerInReach = true;
            keyIndicator.gameObject.SetActive(true);
            lanceScript.SetCurrentInteractable(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Lance"))
        {
            isPlayerInReach = false;
            keyIndicator.gameObject.SetActive(false);
            lanceScript.SetCurrentInteractable(null);
        }
    }

    public void StartInteraction()
    {
        if (isPlayerInReach)
        {
            if (lanceScript.Flatulence > MaxFlatulenceForInteraction)
            {
                OnPlayerFailedInteraction?.Invoke();
                return;
            }

            if (!lanceScript.IsImmobile)
            {
                OnPlayerHide?.Invoke();
                isPlayerInside = true;
            }
        }
    }

    public void StopInteraction()
    {
        if (isPlayerInside || isPlayerInReach == false)
        {
            if (isPlayerInside)
            {
                OnPlayerUnhide?.Invoke();
            }
            isPlayerInside = false;
        }
    }
}
