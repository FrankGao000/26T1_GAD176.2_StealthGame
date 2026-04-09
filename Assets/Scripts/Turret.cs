using UnityEngine;
public class Turret : IthielScript
{
    #region References for the object
    [Header("Base References")]
    // Horizontal Rotation Base
    [Tooltip("Where the turret stands on / where it should rotate horizontal/vertically")]
    [SerializeField]
    private Transform turretBase = null;


    // Up/down rotation base
    [Tooltip("Where the turret's barrels stand on for its rotation up/down")]
    [SerializeField]
    private Transform barrelBase = null;


    // Type of turret object
    [Tooltip("Assign the turretobject here, for the data like damage etc")]
    [SerializeField]
    private TurretObject turret = null;
    public TurretObject TurretObj => turret;
    #endregion
   

    #region Public Variables
    // The target position
    public Transform targetBlock;
    [HideInInspector]
    public Vector3 AimPosition => targetBlock.position;

    // Turret can aim or not

    public bool isIdle = true;
    #endregion


    #region Current Angle Values
    // Current Rotation Angle
    private float limitedRotationAngle = 0f;
    // Current Elevation
    private float elevation = 0f;
    #endregion


    #region Variables
    // If it has barrels for elevation rotation
    private bool hasBarrels = false;

    // The angle to the target
    private float angleToTarget = 0f;

    private bool aimed = false;
    /// <summary>
    /// true when the turret is aiming at the target
    /// </summary>
    public bool IsAimed => aimed;
    #endregion

    #region Position at rest
    private bool barrelAtRest = false;
    private bool baseAtRest = false;
    /// <summary>
    /// True if the turret is Idling and at resting position
    /// </summary>
    private bool TurretAtRest => barrelAtRest && baseAtRest;
    #endregion

    public void Shoot()
    {
        // Shooting Function
    }


    void Awake()
    {
        if (turret == null)
        {
            Error("Turret Data CANNOT be empty, please add a data");
        }

        turretBase = transform.Find("Base");
        if (turretBase == null)
        {
            Error($"{turret.Name}: Turret requires a turretBase to be assigned");
        }
        else
        {
            Debug.Log("Test");
            transform.Find("Barrelbase");
            hasBarrels = barrelBase != null;
        }

    }

    void Update()
    {
        if (turret == null)
        {
            return;
        }

        if (isIdle)
        {
            if (!TurretAtRest)
            {
                RotateToIdle();
            }
            aimed = false;
        }
        else
        {
            // Rotate to target
            RotateBaseToTarget(AimPosition);

            if (hasBarrels)
            {
                // Rotate barrel to target
                RotateBarrelsToTarget(AimPosition);
            }

            // Get angle to target
            // Turn aimed = true when it is then call shoot

            angleToTarget = GetAngleToTarget(AimPosition);

            aimed = angleToTarget < turret.AimThreshold;

            barrelAtRest = false;
            baseAtRest = false;
        }
    }

    private void RotateToIdle()
    {
        if (turret.RotationLimit)
        {
            limitedRotationAngle = Mathf.MoveTowards(limitedRotationAngle, 0f, turret.RotationSpeed * Time.deltaTime);

            if (Mathf.Abs(limitedRotationAngle) > Mathf.Epsilon)
            {
                turretBase.localEulerAngles = Vector3.up * limitedRotationAngle;
            }
            else
            {
                baseAtRest = true;
            }
        }
        else
        {
            turretBase.rotation = Quaternion.RotateTowards(turretBase.rotation, transform.rotation, turret.RotationSpeed * Time.deltaTime);

            baseAtRest = Mathf.Abs(turretBase.localEulerAngles.y) < Mathf.Epsilon;
        }

        if (hasBarrels)
        {
            elevation = Mathf.MoveTowards(elevation, 0f, turret.ElevationSpeed * Time.deltaTime);
            if (Mathf.Abs(elevation) > Mathf.Epsilon)
            {
                barrelBase.localEulerAngles = Vector3.right * -elevation;
            }
            else
            {
                barrelAtRest = true;
            }
        }
        else
        {
            barrelAtRest = true;
        }
    }

    private float GetAngleToTarget(Vector3 pos)
    {
        float angle = float.MaxValue;

        if (hasBarrels)
        {
            angle = Vector3.Angle(pos - barrelBase.position, barrelBase.forward);
        }
        else
        {
            Vector3 flattenedTarget = Vector3.ProjectOnPlane(pos - turretBase.position, turretBase.up);

            angle = Vector3.Angle(flattenedTarget - turretBase.position, turretBase.forward);

        }

        return angle;
    }

    private void RotateBarrelsToTarget(Vector3 pos)
    {
        Vector3 localTargetPos = turretBase.InverseTransformDirection(pos - barrelBase.position);
        Vector3 flattenedVecBarrels = Vector3.ProjectOnPlane(localTargetPos, Vector3.up);

        float targetElevation = Vector3.Angle(flattenedVecBarrels, localTargetPos);
        targetElevation *= Mathf.Sign(localTargetPos.y); // Going up or down

        targetElevation = Mathf.Clamp(targetElevation, -turret.MaxDepression, turret.MaxElevation);
        elevation = Mathf.MoveTowards(elevation, targetElevation, turret.ElevationSpeed * Time.deltaTime);

        if (Mathf.Abs(elevation) > Mathf.Epsilon)
        {
            barrelBase.localEulerAngles = Vector3.right * -elevation;
        }
    }

    private void RotateBaseToTarget(Vector3 pos)
    {
        Vector3 turretUp = transform.up;

        Vector3 vecToTarget = pos - turretBase.position;
        Vector3 flattenedVecBase = Vector3.ProjectOnPlane(vecToTarget, turretUp);

        if (turret.RotationLimit)
        {
            Vector3 turretForward = transform.forward;
            float targetRotation = Vector3.SignedAngle(turretForward, flattenedVecBase, turretUp);

            targetRotation = Mathf.Clamp(targetRotation, -turret.LeftLimit, turret.rightLimit);
            limitedRotationAngle = Mathf.MoveTowards(limitedRotationAngle, targetRotation, turret.RotationSpeed * Time.deltaTime);
            //Need a reference for epsilon
            if (Mathf.Abs(limitedRotationAngle) > Mathf.Epsilon)
            {
                turretBase.localEulerAngles = Vector3.up * limitedRotationAngle;
            }
        }
        else
        {
            turretBase.rotation = Quaternion.RotateTowards(Quaternion.LookRotation(turretBase.forward, turretUp), Quaternion.LookRotation(flattenedVecBase, turretUp), turret.RotationSpeed * Time.deltaTime);
        }

    }
}