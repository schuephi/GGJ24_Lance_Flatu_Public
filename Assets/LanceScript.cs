using UnityEngine;
using UnityEngine.InputSystem;

public class LanceScript : MonoBehaviour
{
    public float MovementSpeed = 1.0f;
    public PlayerInput Input;

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
                        if(context.phase == InputActionPhase.Canceled) moveVector = Vector2.zero;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }           
        };

    }

    private void moveCharacter(Vector2 direction)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var move = moveVector * MovementSpeed * Time.deltaTime;
        this.transform.Translate(move.x, move.y, 0f);
    }
}
