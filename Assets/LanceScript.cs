using UnityEngine;
using UnityEngine.InputSystem;

public class LanceScript : MonoBehaviour
{
    public float MovementSpeed = 1.0f;
    public PlayerInput Input;

    public Animator Animator;

    private Vector2 moveVector = Vector2.zero; 

    // Start is called before the first frame update
    void Start()
    {
        Input.onActionTriggered += (context) =>
        {
            switch(context.action.name)
            {
                case "Move":
                    {
                        moveVector = context.action.ReadValue<Vector2>();
                        Animator.SetFloat("Speed", 1);
                        Animator.gameObject.transform.localScale = new Vector3(moveVector.x > 0 ? -1 : 1,1,1);

                        if (context.phase == InputActionPhase.Canceled)
                        {
                            Animator.SetFloat("Speed", 0);
                            moveVector = Vector2.zero;

                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }           
        };

    }

    // Update is called once per frame
    void Update()
    {
        var move = moveVector * MovementSpeed * Time.deltaTime;
        this.transform.Translate(move.x, move.y, 0f);
    }
}
