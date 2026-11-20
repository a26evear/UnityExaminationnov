using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownController : MonoBehaviour
{
    // These variables hold the key-bind reference. They are then to be initialized in void Start.
    InputAction moveAction; 
    InputAction attackAction;
    InputAction sprintAction;

    // Serialized referenses
    [Header("References")]
    [SerializeField] InputActionAsset inputActionAsset;
    [SerializeField] Animator animator;
    [SerializeField] Transform avatarRoot;


    // Customizable settings
    [Header("Settings")]
    [SerializeField] float movementSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] bool flipSpriteToMoveDirection;

    // Private variables
    Rigidbody2D myRigidbody;
    Vector2 moveInput;
    float actualSpeed;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        // Assign the correct action to each variable
        moveAction = inputActionAsset.FindAction("Move");
        sprintAction = inputActionAsset.FindAction("Sprint");
        attackAction = inputActionAsset.FindAction("Attack");
    }

    // Update is called once per frame
    void Update()
    {
        // Move
        moveInput = moveAction.ReadValue<Vector2>().normalized;

        // Checks if  sprinting
        if (sprintAction.IsPressed())
        {
            actualSpeed = runSpeed; 
            animator.SetBool("Sprinting", true);
        }
        else
        {
            actualSpeed = movementSpeed;
            animator.SetBool("Sprinting", false);
        }

        if (animator != null)
        {
            // Set animator parameter speed & velocity
            animator.SetFloat("speedX", System.MathF.Round(moveInput.x, 1));
            animator.SetFloat("speedY", System.MathF.Round(moveInput.y, 1));
            animator.SetFloat("velocity", System.MathF.Round(myRigidbody.linearVelocity.magnitude, 2));

            // Set animator parameter for Moving
            if ((moveInput.x + moveInput.y) != 0)
            {
                animator.SetBool("Moving", true);
            }
            else
            {
                animator.SetBool("Moving", false);
            }
            
            // Set animator parameter for Attacking
            if (attackAction.IsPressed())
            {
                animator.SetBool("Attacking", true);
            }
            else
            {
                animator.SetBool("Attacking", false);
            }
        }

        // Flip character left or right according to direction
        if (avatarRoot && flipSpriteToMoveDirection == true)
        {
            if (moveInput.x > 0.01f)
            {
                avatarRoot.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (moveInput.x < -0.01f)
            {
                avatarRoot.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
               
    }

    // FixedUpdate is called every frame when the physics are calculated
    void FixedUpdate()
    {
        // Movement with acceleration
        myRigidbody.AddForce(moveInput * actualSpeed);

        // You could use the following Movement without acceleration instead 
        // myRigidbody.velocity = movementInput * movementSpeed;
    }

}
