using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] positions;
    [SerializeField]
    private int[] directions;
    [SerializeField]
    private Vector2 carLaunchRange;

    private void Start()
    {
        StartCoroutine(DelayRegistration());
    }

    private IEnumerator DelayRegistration()
    {
        yield return new WaitForEndOfFrame();
        GameManager.Instance.OnCarReachedGoal += Instance_OnCarReachedGoal;
        StartCoroutine(LaunchCar());
    }

    private void Instance_OnCarReachedGoal()
    {
        StartCoroutine(LaunchCar());
    }

    private IEnumerator LaunchCar()
    {
        yield return new WaitForSeconds(Random.Range(carLaunchRange.x, carLaunchRange.y));
        var index = Random.Range(0, positions.Length);
        Debug.LogWarning("Car starts at point: " + index);
        GameManager.Instance.LaunchCar(new Vector2(positions[index].position.x, positions[index].position.y), directions[index]);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnCarReachedGoal -= Instance_OnCarReachedGoal;
    }
}
