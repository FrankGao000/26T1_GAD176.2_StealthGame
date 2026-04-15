using UnityEngine;

public class RotatingItem : MonoBehaviour

    // This is where the rotating happens! actually really simple, and the IsRotating is just troubleshooting that isn't necessary BUT saves unnessecary data usage
{
    [SerializeField] public float rotationSpeed = 100f; //Speed
    [SerializeField] private bool isRotating = false; // rotating yes or no, this is honestly just damage control.

    void Update()
    {
        if (isRotating)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime); // Rotates the item on the Y axis, time.deltatime makes the rotation framerate dependent which is meant to make it smoother (I didn't notice a difference)
        }

        
        isRotating = false; // This is a failsafe that resets every frame (update) so that the item doesn't rotate when the raycast isn't looked at
    }

    public void StartRotating()
    {
        isRotating = true; // This is triggered back in Vision Detection, makes sure the item is rotating.
    }
}
