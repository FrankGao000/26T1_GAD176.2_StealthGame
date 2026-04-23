using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private float throwForce = 10f; // strong force so physics can go wild
    [SerializeField] private float dropForce = 2f; // weak force cause i hit my Learning Outcome one line earlier lmao
    private float distance; // No need for Serializefield as it is a variable that isnt manually altered and is instead affected by vector logic

    private GameObject item; // No SF as I only needed this for making sure my items were being detected during testing
    [SerializeField] private GameObject tempParent; // Where the item is held, an empty game object
    [SerializeField] private VisionDetector vision; // Connects back to the visionDetector, so it can interact with the raycast

    private Interactable currentInteractable; // There is no need for me to see this in the inspector as it tells me nothing, hence no SF
    private bool isHolding = false;
    private bool canHold = true;

    /// The booleans only had SerializeFields when I was testing things out, since I no longer need to see them as they work, I have removed them
    /// SF = SerializedField


    void Update()
    {
        if (vision == null || tempParent == null) return;

        if (!isHolding && vision.currentItem != null)  // Only detect new item if not holding
        {
            item = vision.currentItem;
            currentInteractable = item.GetComponent<Interactable>();
        }

        if (item == null || currentInteractable == null) return;

        distance = Vector3.Distance(transform.position, item.transform.position);

        // PICKUP (This is assigned to the left click because it is what we use most often, (I think thats because most of the population is right-handed))
        if (!isHolding && Input.GetMouseButtonDown(0) && distance <= 3f && canHold)
        {
            isHolding = true;
            currentInteractable.OnPickup(tempParent.transform);
        }

        if (isHolding)
        {
            item.transform.position = tempParent.transform.position;

            // THROW (This is assigned to the right click!)
            if (Input.GetMouseButtonDown(1))
            {
                currentInteractable.OnThrow(tempParent.transform.forward * throwForce); // Call throw behavior with strong force, opposed to drop which has a weaker force

                isHolding = false;
                item = null;
                currentInteractable = null;
            }

            // DROP (This is assigned Q button cause middle mouse felt too awkward)
            if (Input.GetKeyDown(KeyCode.Q))
            {
                currentInteractable.OnDrop(tempParent.transform.forward * dropForce); // Has drop behavior with weaker force, should fall straight down

                isHolding = false;
                item = null;
                currentInteractable = null;
            }
        }
    }
}