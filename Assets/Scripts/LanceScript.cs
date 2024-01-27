using UnityEngine;
using UnityEngine.InputSystem;

public class LanceScript : MonoBehaviour
{
    public float MovementSpeed = 1.0f;
    public PlayerInput Input;

    public Animator Animator;
    public FartManager FartManager;

    private Vector2 moveVector = Vector2.zero;

    public float Heat = 0;
    public bool isDead = false;

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

    }

    // Update is called once per frame
    void Update()
    {
        if (this.isDead) return;

        if(Heat >= 100)
        {
            this.Animator.SetBool("Die", true);
            this.isDead = true;
            return;
        }
        var move = moveVector * MovementSpeed * Time.deltaTime;
        this.transform.Translate(move.x, move.y, 0f);
    }
}
