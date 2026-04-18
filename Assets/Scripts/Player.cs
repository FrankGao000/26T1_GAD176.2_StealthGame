using System;
using UnityEngine;

public class Player : MonoBehaviour, ILaserListener
{   
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    private float lastLaserDamageTick;
    private LaserTrap currentLaser = null;
    private bool inLaser = false;
    void Awake()
    {
        currentHealth = maxHealth;
    }
    public void OnLaserEnter(LaserTrap laser)
    {
       inLaser = true;
       currentLaser = laser;
    }

    public void OnLaserExit(LaserTrap laser)
    {
       inLaser = false;
       currentLaser = null; 
    }

    public void OnLaserStay(LaserTrap laser)
    {
        switch(laser.GetLaserType)
        {
            case LaserType.Damage:
                HandleDamageLaser();
                break;
            case LaserType.Alert:
                Debug.Log("Alert! Player has trigger the Detection Trap!");
                break;
        }
    }
    private void HandleDamageLaser()
    {
        if (Time.time - lastLaserDamageTick < currentLaser.GetRate) return;
        if (!inLaser) return;   
        lastLaserDamageTick = Time.time;

        currentHealth = Mathf.Clamp(currentHealth - currentLaser.GetDamage, 0, maxHealth);

    }

    public void FixedUpdate()
        {
    if (currentHealth == 0)
    {
        Debug.LogWarning("Player has died");
        Destroy(gameObject);
    }
        }
}
