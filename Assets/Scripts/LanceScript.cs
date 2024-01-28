using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class LanceScript : MonoBehaviour
{
    [SerializeField]
    private float flatulenceChargeSpeed = 1f;
    public float MovementSpeed = 1.0f;
    public PlayerInput Input;

    public Animator Animator;
    public FartManager FartManager;

    private Rigidbody2D rb2D;

    private Vector2 moveVector = Vector2.zero; 

    public float Heat = 0;
    public bool isDead = false;

    public bool InFlatuenceMode { get; set; }
    public float Flatulence = 0; // 0 -1;
    public bool IsImmobile { get; private set; }
    private readonly float flatuenceDownScaling = 0.001f;
    private readonly float minChargeValue = 0.05f;

    // Start is called before the first frame update
    private void Start()
    {
        Input.onActionTriggered += (context) =>
        {
            switch(context.action.name)
            {
                case "Move":
                    {
                        moveVector = context.action.ReadValue<Vector2>();

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
        var teleporter = FindObjectsByType<Teleporter>(FindObjectsSortMode.None);
        foreach (var teleport in teleporter)
        {
            teleport.OnPlayerJump.AddListener(OnPlayerJump);
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
        rb2D.velocity = move;
        HandleFlatuenceCharge(move);
    }

    private void HandleFlatuenceCharge(Vector2 move)
    {
        if (Flatulence < 1 && InFlatuenceMode)
        {
            var moveDependendCharge = move.magnitude * flatulenceChargeSpeed * flatuenceDownScaling;
            var minCharge = minChargeValue * Time.fixedDeltaTime;
            Flatulence += Mathf.Max(moveDependendCharge, minCharge);
        }
        else
        {
            Flatulence = 1;
        }
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

    private void OnPlayerJump()
    {
        // Handle player hide animation
        Debug.Log("Start jump in animation");
        Animator.SetTrigger("Jump_Over");
    }

    private void OnPlayerFailedInteraction()
    {
        // Handle player hide animation
        Debug.Log("Start fail animation");
        Animator.SetTrigger("Jump_Fail");
    }

}
