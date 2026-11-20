using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class EventOnTrigger : MonoBehaviour
{
    [Tooltip("Specify the Tag that the colliding object must have")]
    [SerializeField] string tagToActivate = "Player";

    [SerializeField] UnityEvent onTriggerEnter, onTriggerExit;

    // If the colliding object has the right 'Tag', start the Unity Event. 
    // Here we check 2D-colliders.
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag(tagToActivate))
        {
            onTriggerEnter.Invoke();
        }
    }
    
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag(tagToActivate))
        {
            onTriggerExit.Invoke();
        }
    }

    // If the colliding object has the right 'Tag', start the Unity Event.
    // Here we check 3D-colliders.
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(tagToActivate))
        {
            onTriggerEnter.Invoke();
        }
    }
    
    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag(tagToActivate))
        {
            onTriggerExit.Invoke();
        }
    }
}
