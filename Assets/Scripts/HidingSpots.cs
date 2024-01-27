using UnityEngine;

public class InteractablesHiding : MonoBehaviour
{
    private bool isPlayerInside = false;
    private bool isPlayerInReach = false;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && isPlayerInside) // Leave hiding spot if already hidden
        {
            UnhidePlayer();
        }
        else if (Input.GetKeyDown(KeyCode.E) && !isPlayerInside && isPlayerInReach) // Enter Hiding spot if not already hidden and player is in reach
        {
            HidePlayer();
        }
    }

    private void HidePlayer()
    {
        // Play hide animation
        //animator.SetBool("IsHidden", true);

        isPlayerInside = true;
    }

    private void UnhidePlayer()
    {
        // Play unhide animation
        //animator.SetBool("IsHidden", false);
        
        isPlayerInside = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Lance"))
        {
            isPlayerInReach = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Lance"))
        {
            isPlayerInReach = false;
        }
    }

}
