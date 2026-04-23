using UnityEngine;

public class ItemEvent : MonoBehaviour
{
   public AudioClip selectionFeedback;
    public AudioClip turretFeedback;

    public AudioSource selectionSource;

    //access type name
    public delegate void ItemDelegate();
    
    
    
    
    
    
    
    public ItemDelegate onSelectionShowInfo;
    public ItemDelegate onTurretDeath;


    private void OnEnable()
    {
        onSelectionShowInfo += ShowInformation;
        onTurretDeath += TurretItemDrop;
    }

    private void OnDisable()
    {
        onSelectionShowInfo -= ShowInformation;
        onTurretDeath -= TurretItemDrop;
    }

    private void ShowInformation()
    {

        // AUDIO
        selectionSource.PlayOneShot(selectionFeedback);
        // STRING TEXT
        // PHYSICAL ITEM
    }

    private void TurretItemDrop()
    {
        // AUDI
        selectionSource.PlayOneShot(turretFeedback);
        // STRING TEXT
        Debug.Log("Turret dropped misc item");
        // PHYSICAL ITEM
    }


}
