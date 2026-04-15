using UnityEngine;

public class VisionDetector : MonoBehaviour

    // RotatingItem is a seperate script that is referenced here
    // VisionDetector is like the engine while RotatingItem is the output.

{
    [SerializeField] public float range = 5f; // the "Range" is the Vision range, and how far the character must look for this whole thing to trigger

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward); // Since this is attached to the camera, it starts at the cameras POSITION and goes FORWARD
        RaycastHit hit; // This is storing information, hit is commonly used as a raycast variable

        if (Physics.Raycast(ray, out hit, range)) // This checks to see if the raycast hits anything in front of it, we are checking for script RotatingItem
        {
            RotatingItem item = hit.collider.GetComponent<RotatingItem>(); // Connects the component of RotatingItem Script to VisionDetector script

            if (item != null) // If the object has RotatingItem attached, start function (continued in RotatingItem Script)
            {
                item.StartRotating();
            }
        }
    }
}
