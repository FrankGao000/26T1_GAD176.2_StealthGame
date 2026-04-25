//This the the script of the Laser Trap
using System;
using System.Collections.Generic;
using UnityEngine;

//ITrigerrable is for the child objects (Laser itself) to interact with the parent objects (Laser Trap)
//Idamageable just making the Player and the laser traps are damageable.
public class LaserTrap : MonoBehaviour, ITrigerrable, IDamageable
{
    private class TargetData
    {
        public IDamageable damageable;
        public float damageStep;
        public float damageTimer;

        public float totalTime;
    }

    [SerializeField]
    private float maxHealth = 100f;
    private float currentHealth;

//This is an event that is called when the laser trap set as alert trap, when the player enters it, it will trigger and call for the event.
    public static event Action<LaserTrap> OnAlertTriggered;
//This is an event that is called whenever the player stays inside the alert trap, and passes the time.
    public static event Action<LaserTrap, float> OnAlertStay;

    [SerializeField]
    private int damage;
    public int GetDamage => damage;


    [SerializeField]
    private LaserType type;
    public LaserType GetLaserType => type;


// The damage deal to player when player enters a damage trap.
    [SerializeField]
    private float damageRate = 0.5f;
    public float GetDamageRate => damageRate;
//This is the part for the alert laser trap, the longer player stays, the intersity of the laser gets higher, I haven't set the purpose for this but is can be use when I need.
    [SerializeField]
    private float alertRate = 0.25f;
    public float GetAlertRate => alertRate;
    private float alertIntensity;
    private float alertTimer;

    private readonly Dictionary<GameObject, TargetData> targets = new();
    private void Awake()
    {
        currentHealth = maxHealth;
    }
    private void OnEnable()
    {
        currentHealth = maxHealth;        
    }

    private void OnDisable()
    {
        targets.Clear();       
    }
    
    private bool IsValidTarget(GameObject other)
    {
        if(!other) return false;
        if(other.transform.root.gameObject == gameObject.transform.root.gameObject) return false;
        if (other.TryGetComponent<LaserTrap>(out _)) return false;

        return true;
    }

    private GameObject GetRoot(GameObject obj) => obj.transform.root.gameObject;

    public void OnEnter(GameObject other)
    {
        if(!IsValidTarget(other)) return;

        Debug.Log($"{other} ENTERED the laser \n at: {transform.position}");
        CleanDictionary();

        GameObject key = GetRoot(other);
        
        if(targets.Count == 0 && type == LaserType.Alert)
            OnAlertTriggered?.Invoke(this);

        if(targets.TryGetValue(key, out _)) return;
        
        IDamageable dmg = null;
        if(type == LaserType.Damage && !key.TryGetComponent(out dmg)) return;

        targets.Add(key, new TargetData {
            damageable = dmg,
            damageTimer = 0f,
        });
    }

    public void OnStay(GameObject other, float deltaTime)
    {
        if(!other) return;
        GameObject key = GetRoot(other);
        switch (type)
        {
            case LaserType.Damage:
                HandleDamage(key, deltaTime);
                break;
            case LaserType.Alert:
                HandleAlert(key, deltaTime);
                break;
        }

        if (targets.Count == 0)
        {
            alertIntensity = 0f;
        }
    }
    
    private void HandleDamage(GameObject key, float deltaTime)
    {
        if(!targets.TryGetValue(key, out TargetData data)) return;
        if(!key || data.damageable == null)
        {
            targets.Remove(key);
            return;
        }

        data.totalTime += deltaTime;

        float time = data.totalTime + deltaTime;

        int step = Mathf.FloorToInt(time / GetDamageRate) + 1;
        float dps = damage * step;

        float tickDamage = dps * deltaTime;

        data.damageable?.TakeDamage(tickDamage);
    }

    private void HandleAlert(GameObject key, float deltaTime)
    {
        if(!targets.TryGetValue(key, out TargetData data)) return;

        alertIntensity += deltaTime;
        alertTimer += deltaTime;
        if(alertTimer >= GetAlertRate)
        {
            alertTimer -= GetAlertRate;
            OnAlertStay?.Invoke(this, alertIntensity);
            Debug.Log("Alert called");
        }
    }

    public void OnExit(GameObject other)
    {
        GameObject key = GetRoot(other);
        targets.Remove(key);
        CleanDictionary();
    }

       private void CleanDictionary()
    {
        List<GameObject> toRemove = new();
        
        foreach (GameObject key in targets.Keys)
        {
            if (key == null || !key.activeInHierarchy)
                toRemove.Add(key);
        }

        foreach (GameObject key in toRemove)
        {
            targets.Remove(key);
        }
    }

    
    public void TakeDamage(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
    }
}