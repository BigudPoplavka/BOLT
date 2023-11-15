using UnityEngine;

[CreateAssetMenu(fileName ="GunConfiguration", menuName = "Gun/Configuration", order = 2)]
public class GunConfiguration : ScriptableObject
{
    [SerializeField] private LayerMask _hitMask;
    
    [Space]
    [Header("Разброс")]
    [SerializeField] private Vector3 _spread = new Vector3(0.1f, 0.1f, 0.1f);

    [Header("Скорострельность")]
    [SerializeField] private float _fireRate;

    [Header("Максимальная дистанция")]
    [SerializeField] private float _maxShootDistance = float.MaxValue;

    public LayerMask HitMask { get => _hitMask; }
    public Vector3 Spread { get => _spread; }
    public float FireRate { get => _fireRate; }
    public float MaxShootDistance { get => _maxShootDistance; }
}
