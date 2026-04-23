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
    // Update is called once per frame
    void Update()
    {
        float time = Mathf.PingPong(Time.time * Speed,1f);
        transform.position = startPos + endPos * time;
    }
}
