using UnityEngine;

/*
 * When making a script and attaching it to an object to be interacted with "via the player pressing E"
 * The script must inherit from the IIneteractable interface as shown below.
 * This makes the script inherit the Interact function that executes when the player presses E when they are both looking at the object, pressing E, and within
 * range. 
 * 
 * This range is set in the script attached to the "Player" object.
 * 
 * For this to work the object that the player is interacting with MUST have a collider!
 */

public class Example : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log(Random.Range(0, 100));
    }
}
