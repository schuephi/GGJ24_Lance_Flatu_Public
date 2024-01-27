using UnityEngine;
using UnityEngine.InputSystem;

public class LanceScript : MonoBehaviour
{
    public float MovementSpeed = 1.0f;
    public PlayerInput Input;

    public Animator Animator;
    public FartManager FartManager;

    private Rigidbody2D rb2D;

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
                case "Fart":
                    {
                        if (context.phase == InputActionPhase.Started)
                        {
                            Animator.SetBool("Fart_Start", true);
                            FartManager.Fart();
                        }

                        if (context.canceled == true)
                        {
                            Animator.SetBool("Fart_Start", false);
                            Animator.SetBool("Fart_Stop", true);
                        }

                        break;
                    }
                default:
                    {
                        break;
                    }
            }           
        };
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var move = moveVector * MovementSpeed * Time.deltaTime;
        //this.transform.Translate(move.x, move.y, 0f);
        rb2D.MovePosition(new Vector2(rb2D.position.x + move.x, rb2D.position.y + move.y));
    }
}
