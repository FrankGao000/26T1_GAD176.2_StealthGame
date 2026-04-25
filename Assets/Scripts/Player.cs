//This script is the player's Health for game testing my turrets and laser traps
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField]
    private int maxHealth = 100;
    private float currentHealth;

    void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        Debug.Log("Player's Health is now:"+currentHealth);
    }
}