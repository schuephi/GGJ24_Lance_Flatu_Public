using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LanceScript : MonoBehaviour
{
    [SerializeField]
    private float flatulenceChargeSpeed = 1f;
    public float CurrentSpeed = 0f;
    public float MaxSpeed = 100f;
    public PlayerInput Input;

    public Animator Animator;
    public FartManager FartManager;

    private Rigidbody2D rb2D;

    private Vector2 moveVector = Vector2.zero; 

    public float CurrentHeat = 0;
    public float MaxHeat = 100;

    public bool isDead = false;

    public bool InFlatuenceMode { get; set; }
    public float Flatulence = 0; // 0 -1;
    public bool IsImmobile { get; private set; }
    public bool IsHidden;
    private readonly float flatuenceDownScaling = 0.001f;
    private readonly float minChargeValue = 0.05f;

    public AnimationCurve HeatReductionFactor = AnimationCurve.Linear(0, 0, 1, 1);
    private float timeSinceLastHeatReceived = 0f;

    public float FartCooldownInS = 2f;
    private float timeSinceLastFart = 0;

    private IInteractable currentInteractable; 

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

                        Animator.SetFloat("Speed", moveVector.magnitude > 0 ? 1 : 0);

                        if(moveVector.x != 0) {
                            Animator.gameObject.transform.localScale = new Vector3(moveVector.x > 0 ? -1 : 1, 1, 1);
                        }

                        break;
                    }
                case "Fart":
                    {
                        if (context.phase == InputActionPhase.Started)
                        {
                            if (InFlatuenceMode)
                            {
                                if (timeSinceLastFart >= FartCooldownInS)
                                {
                                    Animator.SetBool("Fart_Start", true);
                                    FartManager.Fart(Flatulence);
                                    timeSinceLastFart = 0f;
                                }
                                else
                                {
                                    this.OnPlayerFailedInteraction();
                                }
                            }
                        }

                        if (context.canceled == true)
                        {
                            if (InFlatuenceMode)
                            {
                                Animator.SetBool("Fart_Start", false);
                                Animator.SetBool("Fart_Stop", true);
                                FartManager.StopFart();
                            }
                        }
                        break;
                    }
                case "Use":
                    {
                        if(currentInteractable != null)
                        {
                            if (context.phase == InputActionPhase.Started)
                            {
                                currentInteractable.StartInteraction();
                            }
                            if(context.canceled == true)
                            {
                                currentInteractable.StopInteraction();
                            }
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
            hidingSpot.OnPlayerFailedInteraction.AddListener(OnPlayerFailedInteraction);
        }
        var teleporter = FindObjectsByType<Teleporter>(FindObjectsSortMode.None);
        foreach (var teleport in teleporter)
        {
            teleport.OnPlayerJump.AddListener(OnPlayerJump);
            teleport.OnPlayerFailedInteraction.AddListener(OnPlayerFailedInteraction);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.isDead) return;

        if(CurrentHeat >= 100)
        {
            this.Animator.SetBool("Die", true);
            this.isDead = true;
            return;
        }
       
        if(InFlatuenceMode)
        {
            Animator.SetFloat("Fartiness", Flatulence);
        }
       
        var move = moveVector * CurrentSpeed * Time.fixedDeltaTime;
        rb2D.velocity = move;

        timeSinceLastHeatReceived += Time.fixedDeltaTime;
        timeSinceLastFart += Time.fixedDeltaTime;

        AddHeat(-Time.fixedDeltaTime * HeatReductionFactor.Evaluate(timeSinceLastHeatReceived));

        HandleFlatuenceCharge(move);
        this.CurrentSpeed = Mathf.Clamp(this.MaxSpeed - this.MaxSpeed * Flatulence, 0, MaxSpeed);
    }

    public void AddHeat(float heat)
    {
        CurrentHeat = Mathf.Clamp(CurrentHeat + heat, 0, MaxHeat);

        if (heat > 0)
        {
            timeSinceLastHeatReceived = 0;
        }
    }

    public void SetCurrentInteractable(IInteractable interactable)
    {
        if(currentInteractable != null)
        {
            currentInteractable.StopInteraction();
        }

        currentInteractable = interactable;
    }

    private void HandleFlatuenceCharge(Vector2 move)
    {
        if (InFlatuenceMode)
        {
            if (Flatulence < 1)
            {
                var moveDependendCharge = move.magnitude * flatulenceChargeSpeed * flatuenceDownScaling;
                var minCharge = minChargeValue * Time.fixedDeltaTime;
                Flatulence += Mathf.Max(moveDependendCharge, minCharge);
            }
            else
            {
                Flatulence = 1;
                FartManager.Fart(Flatulence);
            }
        }
        IsImmobile = Flatulence > 0.6f;
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
