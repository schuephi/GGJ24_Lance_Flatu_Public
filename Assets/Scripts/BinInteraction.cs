using UnityEngine;

public class BinInteraction : MonoBehaviour
{
    private bool isPlayerInside = false;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isPlayerInside)
            {
                UnhidePlayer();
            }
            else
            {
                HidePlayer();
            }
        }
    }

    private void HidePlayer()
    {
        // Play hide animation
        animator.SetBool("IsHidden", true);
        isPlayerInside = true;
    }

    private void UnhidePlayer()
    {
        // Play unhide animation
        animator.SetBool("IsHidden", false);
        isPlayerInside = false;
    }
}
