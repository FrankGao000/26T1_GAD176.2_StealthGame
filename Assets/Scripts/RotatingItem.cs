using UnityEngine;

public class RotatingItem : MonoBehaviour

    // This is where the rotating happens! actually really simple, and the IsRotating is just troubleshooting that isn't necessary BUT saves unnessecary data usage
{
    [SerializeField] public float rotationSpeed = 100f; //Speed
    [SerializeField] private bool isRotating = false; // rotating yes or no, this is honestly just damage control.
    [SerializeField] private AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>(); //Gives Audiosource to the object this script is attached to instead of manually being done in inspector.
    }

    void Update()
    {
        if (isRotating)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime); // Rotates the item on the Y axis, time.deltatime makes the rotation framerate dependent which is meant to make it smoother (I didn't notice a difference)

            if (!audioSource.isPlaying) // Starts the sound, if not already triggered
            {
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying) // Stops the sound playing when not looked at
            {
                audioSource.Stop();
            }
        }

            isRotating = false; // This is a failsafe that resets every frame (update) so that the item doesn't rotate when the raycast isn't looked at
    }

    public void StartRotating()
    {
        isRotating = true; // This is triggered back in Vision Detection, makes sure the item is rotating.
    }
}
