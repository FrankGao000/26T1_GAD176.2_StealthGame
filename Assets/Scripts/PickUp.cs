using UnityEngine;

public class PickUp : MonoBehaviour
{

    [SerializeField] public float throwForce = 10f; // Separate force for throwing
    [SerializeField] public float dropForce = 2f;   // Separate force for dropping
    [SerializeField] public float distance;
    [SerializeField] public bool canHold = true;
    [SerializeField] public bool isHolding = false; // Start NOT holding (prevents bugs)

    [SerializeField] Vector3 objectPos;
    [SerializeField] public GameObject item;
    [SerializeField] public GameObject tempParent;
    [SerializeField] private VisionDetector vision; // Adding connectivity between pickUp and my VisionDetector script so that the raycast information can be processed and sent over here

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()

    {
        if (vision == null) return;  // If visiondetector isnt added in the inspector for PickUp on the player, return null (good if the player using this scaffold doesnt set up properly)

        if (tempParent == null) return; // Prevent errors if tempParent isn't assigned

        // ONLY assign item if NOT holding something (prevents switching items mid-hold)
        if (!isHolding && vision.currentItem != null)
        {
            item = vision.currentItem;
        }

        if (item == null) return; // If no item detected, stop here

        distance = Vector3.Distance(transform.position, item.transform.position); //  distance now checks from player to item (not tempParent, as when that was assigned, it would not detect unless at a very specific angle)

        // PICK UP (LEFT CLICK) - only if NOT already holding
        if (!isHolding && Input.GetMouseButtonDown(0) && distance <= 3f && canHold) // Makes sure that the player is close enough to the item AND that the boolean for holding it is checked (it is automatically checked)
        {
            isHolding = true;

            Rigidbody rb = item.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            item.transform.SetParent(tempParent.transform);
        }

        if (isHolding) // the below happens while the object is being held by the player, if these mouse-buttons are pressed outside of this action they do nothing.
        {
            // Keeps the item locked to the hold position
            item.transform.position = tempParent.transform.position;

            if (Input.GetMouseButtonDown(1)) // this is my THROW function 
            {
                Rigidbody rb = item.GetComponent<Rigidbody>();

                item.transform.SetParent(null);
                rb.useGravity = true;
                rb.AddForce(tempParent.transform.forward * throwForce, ForceMode.Impulse); // Stronger force

                isHolding = false;
                item = null; // Clear reference so it doesn't bug out
            }

            if (Input.GetKeyDown(KeyCode.Q)) // this is my DROP function (changed from left click to avoid conflict)
            {
                Rigidbody rb = item.GetComponent<Rigidbody>();

                item.transform.SetParent(null);
                rb.useGravity = true;
                rb.AddForce(tempParent.transform.forward * dropForce, ForceMode.Impulse); // Smaller force

                isHolding = false;
                item = null; // Clear reference
            }
        }
    }
}