using UnityEngine;

public class SignPost : Interactable
{
    [SerializeField] private GameObject uiText; // The UI text object (InstructionText), this is where I drag the UI

    private void Update() //checks for this every frame
    {
        
        if (uiText != null) //Automatically turns the UI off every frame, this is because it is checked in the inspector
        {
            uiText.SetActive(false);
        }
    }

    
    public override void OnLook()
    {
        if (uiText != null)
        {
            uiText.SetActive(true); // Turn on while being looked at
        }
    }
}