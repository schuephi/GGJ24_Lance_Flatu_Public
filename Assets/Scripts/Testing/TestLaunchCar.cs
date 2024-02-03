using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLaunchCar : MonoBehaviour
{
    [SerializeField]
    private Transform[] positions;
    [SerializeField]
    private int[] directions;

    private int currentIndex = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameManager.Instance.LaunchCar(new Vector2(positions[currentIndex].position.x, positions[currentIndex].position.y), new Vector2(directions[currentIndex++ % positions.Length], 0));
        }
    }
}
