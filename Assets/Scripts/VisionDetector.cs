using UnityEngine;

public class VisionDetector : MonoBehaviour

{
    [SerializeField] private float range = 5f; // the "Range" is the Vision range, and how far the character must look for this whole thing to trigger
    public GameObject currentItem; // SF was only needed for testing purposes

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward); // Since this is attached to the camera, it starts at the cameras POSITION and goes FORWARD
        RaycastHit hit; // This is storing information, hit is commonly used as a raycast variable

        if (Physics.Raycast(ray, out hit, range))
        {
            currentItem = hit.collider.gameObject;

            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                interactable.OnLook(); // Works for ANY child class (before this it was specific for the RotatingItem, this is better for versatility in a scaffold
            }
        }
        else
        {
            currentItem = null;
        }
    }
}
