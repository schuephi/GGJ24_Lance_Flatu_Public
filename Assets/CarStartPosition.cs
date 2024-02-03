using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarStartPosition : MonoBehaviour
{
    // Start is called before the first frame update
    private BoxCollider2D startZone;
    public Vector2 Direction;
    void Start()
    {
        startZone= GetComponent<BoxCollider2D>();
    }

    public Vector3 GetRandomPosition()
    {
        return new Vector3(
        Random.Range(startZone.bounds.min.x, startZone.bounds.max.x),
        Random.Range(startZone.bounds.min.y, startZone.bounds.max.y),
        Random.Range(startZone.bounds.min.z, startZone.bounds.max.z)
    );
    }
}
