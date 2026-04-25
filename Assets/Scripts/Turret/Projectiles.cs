// This is the script for the projectiles of the turrets which are bullets.
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    private float damage;
    private float projectileLifetime;

    public void SetDamage(float dmg)
    {
        damage = dmg;
    }

    public void Awake()
    {
        projectileLifetime = 2.5f;
        Destroy(gameObject, projectileLifetime);
    }

//This is for checking if the bullet hit the player character or the wall.
    void OnCollisionEnter(Collision other)
    {
        Debug.Log($"Entering fire range!!!");
        if(other.gameObject.layer == LayerMask.NameToLayer("Player") && other.gameObject.transform != transform.parent)
        {
            
          Debug.Log("You've been Hit!");
          if(other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(damage);
            }
        
        }
        if(other.transform.root.gameObject != transform.root.gameObject && other.gameObject.layer != LayerMask.NameToLayer("Traps"))
        {
            Destroy(gameObject);
        }
    }
}