using UnityEngine;

[CreateAssetMenu(fileName = "TurretObject", menuName = "Turrets/Turret Object Data")]
public class TurretObject : ScriptableObject
{
    [Header("Turret - Data")]
    [SerializeField]
    private string turretName = "Turret01";
    public string Name => turretName;

    // Prefab of the bullet
    [SerializeField]
    private GameObject bulletPrefab;
    public GameObject  GetBulletPrefab => bulletPrefab;

    [SerializeField]
    private float damage;
    public float GetDamage => damage;


    // Reference to the stat script



    [Header("Turret - Behaviour")]
    // How long must the player be in vision before it recognizes the player
    [Tooltip("Player must be in vision for this amount before the turret starts shooting and tracking")]
    [SerializeField, Range(0.2f, 3f)]
    private float recognitionTimer = 0.5f;
    public float GetRecognitionTimer => recognitionTimer;

    // Cooldown Between Attacks
    [Tooltip("The cooldown between attacks")]
    [SerializeField]
    private float attackCooldown = 1f;
    public float GetAttackCooldown => attackCooldown;

    [Header("Behaviour - Rotation")]
    // Rotation Speed of the turret
    [Tooltip("How fast can the turret rotate left/right")]
    [SerializeField]
    private float rotationSpeed = 60f;

    public float RotationSpeed => rotationSpeed;


    // Left and right rotation limits if true
    [Tooltip("Limits the rotation (horizontal) of the turret using the range provided")]
    [SerializeField]
    private bool rotationLimit = true;
    [SerializeField, Range(0, 179)] private float leftRotationLimit = 90f;
    [SerializeField, Range(0, 179)] private float rigthRotationLimit = 90f;

    public bool RotationLimit => rotationLimit;
    public float LeftLimit => leftRotationLimit;
    public float rightLimit => rigthRotationLimit;

    [Header("Behaviour - Elevation")]
    // Elevation Of the turret main gun
    [Tooltip("The speed of the turret aiming up/down")]
    [SerializeField]
    private float elevationSpeed = 30f;
    public float ElevationSpeed => elevationSpeed;

    // How high can the turret aim
    [Tooltip("How high can the turret aim")]
    [SerializeField]
    private float maxElevation = 45f;
    public float MaxElevation => maxElevation;


    // How low can the turret aim
    [Tooltip("How low can the turret aim")]
    [SerializeField]
    private float maxDepression = 15f;
    public float MaxDepression => maxDepression;

    [Header("Behaviour - Vision")]
    // Range for the turret to start raycasting & knows where the player is
    [SerializeField]
    private float awareRadius = 20f;
    public float GetAwareRadius => awareRadius;

    // How close should the barrel's aim be before being "Locked in"
    [Tooltip("The furthest angle from the turret to the target before being 'aimed' ")]
    [SerializeField]
    private float aimedThreshold = 5f;
    public float AimThreshold => aimedThreshold;

}