using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class Teleporter : MonoBehaviour
{
    public Transform destination; // The destination teleporter
    public UnityEvent OnPlayerJump;

    private bool isPlayerInReach = false;

    private Animator animator;

    IEnumerator TeleportPlayer()
    {
        // Play teleport animation
        OnPlayerJump?.Invoke();
        GameObject Lance = GameObject.FindGameObjectWithTag("Lance");
        Vector3 PlayerStartPosition = Lance.transform.position;
        for (float t = 0; t < 1.0f;)
        {
            Lance.transform.position = Vector3.Lerp(PlayerStartPosition, destination.position, t);
            t += Time.deltaTime;
            Debug.Log(t);
            yield return null;
        }
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
            StartCoroutine("TeleportPlayer");
        }
    }
}
