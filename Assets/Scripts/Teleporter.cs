using UnityEngine;

public class Teleporter : MonoBehaviour
{

    private bool isPlayerInReach = false;
    public Transform destination; // The destination teleporter

    private void TeleportPlayer()
    {
        // Play teleport animation
        //animator.SetBool("IsTeleported", true);

        // Teleport player to destination
        GameObject.FindGameObjectWithTag("Lance").transform.position = destination.position;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerInReach)
        {
            TeleportPlayer();
        }
    }
}
