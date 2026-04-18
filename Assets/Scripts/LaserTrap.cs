using UnityEngine;

using System;
using System.Collections.Generic;
public class LaserTrap : MonoBehaviour
{
    [SerializeField]
    private LaserTrigger trigger;

    public event Action<GameObject> OnLaserEnter;
    public event Action<GameObject> OnLaserStay;
    public event Action<GameObject> OnLaserExit;
 
    [SerializeField] private int damage;
    public int GetDamage => damage;
    [SerializeField] private LaserType type;
    public LaserType GetLaserType => type;
    [SerializeField] private float rate; 
    public float GetRate => rate;  
   
      private void Awake()
    {
        if(trigger == null)
            trigger = GetComponentInChildren<LaserTrigger>();
    }

    private void OnEnable()
    {
        trigger.UpdateMaterial(type);
        trigger.OnLaserEnter += HandlePlayerEnter;
        trigger.OnLaserExit+= HandlePlayerExit;
        trigger.OnLaserStay += HandlePlayerStay;
    }

    private void OnDisable()
    {
        trigger.OnLaserEnter -= HandlePlayerEnter;
        trigger.OnLaserExit -= HandlePlayerExit;
        trigger.OnLaserStay -= HandlePlayerStay;
    }

    private void HandlePlayerEnter(GameObject player)
    {
        Debug.Log($"{player} ENTERED the laser \n at: {transform.position}");
        OnLaserEnter?.Invoke(player);

        player.GetComponent<ILaserListener>()?.OnLaserEnter(this);
    }

    private void HandlePlayerStay(GameObject player)
    {
        OnLaserStay?.Invoke(player);

        player.GetComponent<ILaserListener>()?.OnLaserStay(this);
    }

    private void HandlePlayerExit(GameObject player)
    {
        Debug.Log($"{player} LEFT the laser \n at: {transform.position}");
        OnLaserExit?.Invoke(player);

        player.GetComponent<ILaserListener>()?.OnLaserExit(this);
    }
}