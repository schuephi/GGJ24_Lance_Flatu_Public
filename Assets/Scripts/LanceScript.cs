using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class LanceScript : MonoBehaviour
{
    public float MovementSpeed = 1.0f;
    public PlayerInput Input;

    public Animator Animator;
    public FartManager FartManager;

    private Rigidbody2D rb2D;

    private Vector2 moveVector = Vector2.zero; 

    public float Heat = 0;
    public bool isDead = false;

    public float Flatulence = 0;

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
                        Debug.Log(moveVector);
                        Animator.SetFloat("Speed", 1);

                        if(moveVector.x != 0) {
                            Animator.gameObject.transform.localScale = new Vector3(moveVector.x > 0 ? -1 : 1, 1, 1);
                        }

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

        var hidingSpots = FindObjectsByType<HidingSpots>(FindObjectsSortMode.None);
        foreach (var hidingSpot in hidingSpots)
        {
            hidingSpot.OnPlayerHide.AddListener(OnPlayerHide);
            hidingSpot.OnPlayerUnhide.AddListener(OnPlayerUnhide);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.isDead) return;

        if(Heat >= 100)
        {
            this.Animator.SetBool("Die", true);
            this.isDead = true;
            return;
        }
        var move = moveVector * MovementSpeed * Time.fixedDeltaTime;
        //this.transform.Translate(move.x, move.y, 0f);
        rb2D.velocity = move;
        //rb2D.MovePosition(new Vector2(rb2D.position.x + move.x, rb2D.position.y + move.y));
    }

    private void OnPlayerHide()
    {
        // Handle player hide animation
        Debug.Log("Start jump in animation");
        Animator.SetBool("Jump_In", true);
    }

    private void OnPlayerUnhide()
    {
        // Handle player unhide animation
        Debug.Log("Start jump out animation");
        Animator.SetBool("Jump_Out", true);
    }

}
