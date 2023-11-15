using UnityEngine;
using System.Collections.Generic;

public class GunSelector : MonoBehaviour
{
    [SerializeField] private GunType _gunType;

    [SerializeField] private Transform _gunParent;

    [Space]
    [Header("Quick slots")]
    [SerializeField] private List<Gun> _gunsList;

    [Space]
    [Header("Runtime selection")]
    [SerializeField] private Gun _activeGun;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
