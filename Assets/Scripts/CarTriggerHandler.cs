using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTriggerHandler : MonoBehaviour
{
    private Rigidbody2D carRigidBody;

    private void Awake()
    {
        carRigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Limits"))
        {
            carRigidBody.velocity = Vector2.zero;
            GameManager.Instance.ReportCarReachedGoal();
        }
    }
}
