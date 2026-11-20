using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class EventOnTriggerAndKeyBind : MonoBehaviour
{
    [Tooltip("Select the action reference that you want to trigger the event(s)")]
    [SerializeField] private InputActionReference actionToActivate;

    [Tooltip("Specify the Tag that the colliding object must have")]
    [SerializeField] string tagToActivate = "Player";

    [SerializeField] UnityEvent eventOnTriggerAndKeyBind;

    private bool correctCollider = false;
    private bool correctAction = false;

    private void Update()
    {
        // If the colliding object has the right 'Tag', and the right Action is pressed, start the Unity Event.
        if (correctCollider && correctAction)
        {
            eventOnTriggerAndKeyBind.Invoke();
        }

        // Check if the Action is pressed
        if (actionToActivate.action.WasPressedThisFrame())
        {
            correctAction = true;
        }
        else
        {
            correctAction = false;
        }
    }

    //Check if the collider has the right tag.
    // First we check 2D-colliders.
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag(tagToActivate))
        {
            correctCollider = true;
        }
    }
    private void OnTriggerExit2D()
    {
        correctCollider = false;
    }

    // Then we check 3D-colliders.
    private void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag(tagToActivate))
        {
            correctCollider = true;
        }
    }
    private void OnTriggerExit()
    {
        correctCollider = false;
    }    
}
