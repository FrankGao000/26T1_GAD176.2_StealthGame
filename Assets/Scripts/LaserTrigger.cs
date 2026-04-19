using System.Collections.Generic;
using UnityEngine;

public class LaserTrigger : MonoBehaviour
{
    [SerializeField]
    private ITrigerrable laser;
    [SerializeField] private List<Material> materials;
    private Renderer rend;
    void Awake()
    {
        laser ??= GetComponentInParent<ITrigerrable>();
        rend = GetComponent<Renderer>();
        LaserTrap type = GetComponentInParent<LaserTrap>();
        rend.material = materials[(int)type.GetLaserType];

    }
    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player")) return;
        
        laser.OnEnter(other.gameObject);
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(!other.CompareTag("Player")) return;
        laser.OnStay(other.gameObject, Time.deltaTime);
    }

    private void OnTriggerExit(Collider other)
    {
        if(!other.CompareTag("Player")) return;
        laser.OnExit(other.gameObject);
    }
}