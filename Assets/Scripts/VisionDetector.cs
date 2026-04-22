using UnityEngine;

public class VisionDetector : MonoBehaviour

    /// NOTE: RotatingItem is a seperate script that is referenced here
    /// NOTE: VisionDetector is like the engine while RotatingItem is the output.

{
    [SerializeField] public float range = 5f; // the "Range" is the Vision range, and how far the character must look for this whole thing to trigger
    [SerializeField] public GameObject currentItem; // I need visiondetector to share what the raycast detects with other scripts

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward); // Since this is attached to the camera, it starts at the cameras POSITION and goes FORWARD
        RaycastHit hit; // This is storing information, hit is commonly used as a raycast variable

        if (Physics.Raycast(ray, out hit, range)) // This checks to see if the raycast hits anything in front of it, we are checking for script RotatingItem
        {
            currentItem = hit.collider.gameObject; // acts as a storage container for the gameobject once detected
            RotatingItem item = hit.collider.GetComponent<RotatingItem>(); // Connects the component of RotatingItem Script to VisionDetector script

            if (item != null) // If the object has RotatingItem attached, start function (continued in RotatingItem Script)
            {
                item.StartRotating();
            }
        }
        else
        {
            currentItem = null;
        }
    }
}
