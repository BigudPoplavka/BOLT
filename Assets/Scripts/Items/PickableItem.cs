using UnityEngine;

public class PickableItem : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;

    [SerializeField] private bool _isStackable;

    [SerializeField] private GameObject _object;

    [SerializeField] private Sprite _itemIcon;

    public string Name { get => _name; }
    public bool IsStackable { get => _isStackable; }
    public GameObject Object { get => _object; }
}
