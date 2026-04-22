using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] protected Rigidbody rb; // This is a protected class cause I ONLY need the children classes to inherit from this, it doesnt need to be public.

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Cache Rigidbody for reuse, instead of calling it again and again, this should save data
    }

    public virtual void OnLook()
    {
        // This does nothing initially but RotatingItem overrides this 
    }

    public virtual void OnPickup(Transform holdPoint)
    {
        if (rb != null) // Will ONLY run physics if the item has a rigidbody
        {
            rb.useGravity = false;              // While it is being held, gravity is disabled, so it doesn't drop + was causing some bugs
            rb.linearVelocity = Vector3.zero;   // No movement, cause I want the feedback loop to stop
            rb.angularVelocity = Vector3.zero;  // Same reasons as above
        }

        transform.SetParent(holdPoint); // Attach to player hold position
    }

    public virtual void OnDrop(Vector3 force)
    {
        transform.SetParent(null); // Detach from player

        if (rb != null)
        {
            rb.useGravity = true;
            rb.AddForce(force, ForceMode.Impulse); // Small push forward, so it still has some sort of dynamic movement
        }
    }

    public virtual void OnThrow(Vector3 force)
    {
        transform.SetParent(null);

        if (rb != null)
        {
            rb.useGravity = true;
            rb.AddForce(force, ForceMode.Impulse); // Strong push, but further defined in OnThrow(RotatingItem) Throw force is stronger so the throw is powerful.
        }
    }
}

///Resources used
///https://www.youtube.com/watch?v=8TIkManpEu4 (For general override basics and inheritance functionality
///https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/inheritance
///https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/virtual
///https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/polymorphism