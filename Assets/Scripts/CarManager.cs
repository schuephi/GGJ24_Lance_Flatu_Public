using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    [SerializeField]
    private Vector2 CarSpawnTimeRange;

    [SerializeField]
    private List<CarStartPosition> carSpawnZones;

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
        yield return new WaitForSeconds(Random.Range(CarSpawnTimeRange.x, CarSpawnTimeRange.y));
        var index = Random.Range(0, carSpawnZones.Count);
        var point = carSpawnZones[index].GetRandomPosition().ToVector2();
        GameManager.Instance.LaunchCar(point, carSpawnZones[index].Direction);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnCarReachedGoal -= Instance_OnCarReachedGoal;
    }
}
