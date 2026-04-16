using UnityEngine;

public class PickUp : MonoBehaviour
{

    [SerializeField] public int force;
    [SerializeField] public float distance;
    [SerializeField] public bool canHold = true;
    [SerializeField] public bool isHolding = true;

    [SerializeField] Vector3 objectPos;
    [SerializeField] public GameObject item;
    [SerializeField] public GameObject tempParent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(item.transform.position, tempParent.transform.position);
        if (distance >= 3f)
        {
            isHolding = false;
        }

        if (Input.GetMouseButtonDown(0)) // 0 is Left mouse click as this is an easy accessible button for the player, and it feels comfortable
        {

            if (isHolding == true)
            {
                isHolding = false;
            }
        }

        else
            if (distance <= 3f)
        {
            isHolding = true;
            item.GetComponent<Rigidbody>().useGravity = false; // If item is being held there is no rigid-body, if there was there may be clipping shenaningans.
        }

        if (isHolding == true)
        {
            item.GetComponent<Rigidbody>().linearVelocity = Vector3.zero; /// NOTE: Original guide [https://discussions.unity.com/t/pick-up-and-throw-object/770376] called for Velocity = Vector3.zero but that is no longer useable in Unity 6, so it was changed to linearVelocity = Vector3.zero
            item.GetComponent<Rigidbody>().angularVelocity = Vector3.zero; // This is the upwards vector, that alongside linearVelocity allows for a "sloped" vector, as this is a "throw" function
            item.transform.SetParent(tempParent.transform);


            if (Input.GetMouseButtonDown(1))
            {
                item.GetComponent<Rigidbody>().AddForce(tempParent.transform.forward * force); isHolding = false;
            }

            else
            {
                objectPos = item.transform.position;
                item.transform.SetParent(null);
                item.GetComponent<Rigidbody>().useGravity = true; // When item isn't being held by the character, it now has gravity
                item.transform.position = objectPos;
            }

        }


    }
}
