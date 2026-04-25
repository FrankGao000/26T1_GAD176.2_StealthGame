//This script is for the Laser trap movement.
using UnityEditor.Rendering;
using UnityEngine;

public class LaserMovement : MonoBehaviour
{
    [SerializeField] private float Speed;
     private Vector3 startPos;
     [SerializeField] private Vector3 endPos;
    
     void Start ()
     {
        startPos = transform.position; 
     }
    //I'm using PingPong Unity Method to make the laser moving backwards and forwards.
    void Update()
    {
        float time = Mathf.PingPong(Time.time * Speed,1f);
        transform.position = startPos + endPos * time;
    }
}
