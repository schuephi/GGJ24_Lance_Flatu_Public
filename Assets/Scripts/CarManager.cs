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

    private bool isRunning;

    private void Start()
    {
        isRunning = true;
        StartCoroutine(LaunchCar());
    }

    private IEnumerator LaunchCar()
    {
        yield return new WaitForSeconds(1f);
        while (isRunning)
        {
            var index = Random.Range(0, positions.Length);
            GameManager.Instance.LaunchCar(new Vector2(positions[index].position.x, positions[index].position.y), directions[index]);
            yield return new WaitForSeconds(Random.Range(carLaunchRange.x, carLaunchRange.y));
        }
    }
}
