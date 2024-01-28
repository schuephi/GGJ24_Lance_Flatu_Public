using System;
using UnityEngine;
using UnityEngine.Events;

public class HidingSpots : MonoBehaviour
{
    public UnityEvent OnPlayerHide;
    public UnityEvent OnPlayerUnhide;

    private bool isPlayerInside = false;
    private bool isPlayerInReach = false;

    private void Update()
    {
        if (isPlayerInReach && Input.GetKeyDown(KeyCode.E))
        {
            OnPlayerHide?.Invoke();
            isPlayerInside = true;
            Debug.Log("Player tries to enter hiding spot");
        }

        if (isPlayerInside && Input.GetKeyUp(KeyCode.E))
        {
            OnPlayerUnhide?.Invoke();
            isPlayerInside = false;
            Debug.Log("Player leaves hiding spot");
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

}
