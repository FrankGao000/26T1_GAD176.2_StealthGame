using UnityEngine;

public class ParentClass : MonoBehaviour
{
    [SerializeField]
    private bool debugMode = false;
    [SerializeField]
    private bool bypassWarning = true;


    protected void Log(string msg)
    {
        if (debugMode)
        {
            Debug.Log($"Log From: {gameObject}, \n Message: {msg}");
        }
    }

    protected void Error(string msg)
    {
        Debug.LogError($"Error From: {gameObject}, \n Message: {msg}");
    }

    protected void Warning(string msg)
    {
        if (debugMode || bypassWarning)
        {
            Debug.LogWarning($"Log From: {gameObject}, \n Message: {msg}");
        }
    }
}
