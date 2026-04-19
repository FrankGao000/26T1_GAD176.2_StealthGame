using UnityEngine;

public class Projectiles : MonoBehaviour
{
    private float damage;
    private float projectileLifetime;

    public void SetDamage(float dmg)
    {
        damage = dmg;
    }

    public void SetLifetime(float lifetime)
    {
        projectileLifetime = lifetime;
        Destroy(gameObject, projectileLifetime);
    }


    void OnCollisionEnter(Collision other)
    {
        Debug.Log($"HIHI NOTHING BUT I HIT {other.gameObject.name} and my parent is {transform.parent}");
        if((other.gameObject.layer == LayerMask.NameToLayer("Player") || other.gameObject.layer == LayerMask.NameToLayer("Enemy")) && other.gameObject.transform != transform.parent)
        {
            
          Debug.Log("You've been Hit!");
        
        }
        if(other.transform.root.gameObject != transform.root.gameObject && other.gameObject.layer != LayerMask.NameToLayer("Environment"))
        {
            Destroy(gameObject);
        }
    }
}