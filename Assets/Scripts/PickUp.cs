using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] public float throwForce = 10f; // strong force so physics can go wild
    [SerializeField] public float dropForce = 2f; // weak force cause i hit my Learning Outcome one line earlier lmao
    [SerializeField] public float distance;
    [SerializeField] public bool canHold = true;

    [SerializeField] public GameObject item; // The item or well "Interactable" may change this cause item is no longer correct with the game logic
    [SerializeField] public GameObject tempParent; // Where the item is held, an empty game object
    [SerializeField] private VisionDetector vision; // Connects back to the visionDetector, so it can interact with the raycast

    [SerializeField] private Interactable currentInteractable;
    [SerializeField] private bool isHolding = false;

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