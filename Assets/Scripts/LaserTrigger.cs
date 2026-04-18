using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserTrigger : MonoBehaviour

{
    public event Action<GameObject> OnLaserEnter;
    public event Action<GameObject> OnLaserStay;
    public event Action<GameObject> OnLaserExit;
    private Renderer rend;
    void Awake ()
    {
        rend = GetComponent<Renderer>();
    }
[SerializeField] private List<Material> materials;
public void UpdateMaterial (LaserType type)
{
    if (rend == null) rend = GetComponent<Renderer>();
    switch(type)
    {
        case LaserType.Damage:
            rend.material= materials[0];
            break;
        case LaserType.Alert:
            rend.material= materials[1];
            break;
    }
 
}
   
    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player")) return;
        
        OnLaserEnter?.Invoke(other.gameObject);
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(!other.CompareTag("Player")) return;
        OnLaserStay?.Invoke(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if(!other.CompareTag("Player")) return;
        OnLaserExit?.Invoke(other.gameObject);
    }
}