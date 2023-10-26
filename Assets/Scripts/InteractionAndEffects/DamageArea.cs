using UnityEngine;
using UnityEngine.Events;

public class DamageArea : MonoBehaviour
{
    [SerializeField] private float _damage;

    [SerializeField] private UnityEvent<float> _playerEnter;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Player>(out Player player))
        {
            player.GetDamage(_damage);
        }
    }
}
