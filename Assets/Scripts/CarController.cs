using System.Collections;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D carRigidBody;

    [SerializeField]
    private Vector2 CarSpeed;

    private void OnEnable()
    {
        StartCoroutine(DelayRegistration());
    }

    private IEnumerator DelayRegistration()
    {
        yield return new WaitForEndOfFrame();
        GameManager.Instance.OnLaunchCar += Instance_OnLaunchCar;
    }

    private void Instance_OnLaunchCar(Vector2 startPos, Vector2 direction)
    {
        carRigidBody.transform.position = startPos; // MovePosition(startPos);
        carRigidBody.gameObject.transform.localScale = new Vector3(direction.x > 0 ? -1 : 1, 1, 1);
        var speed = Random.Range(CarSpeed.x, CarSpeed.y);
        carRigidBody.velocity = direction * speed;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnLaunchCar -= Instance_OnLaunchCar;
    }
}
