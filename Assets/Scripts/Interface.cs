//These are the interfaces use by the other scripts that defines what they have to do.
using UnityEngine;

public interface ITrigerrable
{
    void OnEnter(GameObject other);
    void OnStay(GameObject other, float deltaTime);
    void OnExit(GameObject other);
}

public interface IDamageable
{
    void TakeDamage(float amount);
}