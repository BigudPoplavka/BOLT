using UnityEngine;
using UnityEngine.Events;

public class Slot : MonoBehaviour
{
    [SerializeField] private PickableItem _item;

    [SerializeField] private int _count;

    [SerializeField] private bool _IsEmpty;

    [SerializeField] private UnityEvent<int> _countChanged;
    [SerializeField] private UnityEvent _abilityCountIsZero;
    [SerializeField] private UnityEvent _slotReseted;

    public GameObject ItemObject { get => _item.Object; }

    public bool IsEmpty { get => _item == null; }

    private void Start()
    {
        _IsEmpty = true;
    }

    public void SetItemToSlot(PickableItem item)
    {
        _item = item;
        _IsEmpty = false;

        IncreaseCount();
    }

    public void SetTheSameItemToSlot(PickableItem item)
    {
        if(_item.IsStackable)
        {
            IncreaseCount();
        }
    }

    public void DragItemOut()
    {
       
    }

    public void IncreaseCount()
    {
        _count++;
        _countChanged?.Invoke(_count);
    }

    public void DecreaseCount()
    {
        if (_count != 0)
        {
            _count--;
            _countChanged?.Invoke(_count);

            if (_count == 0)
            {
                ResetSlot();
                _abilityCountIsZero?.Invoke();
            }
        }
    }

    public void ResetSlot()
    {

    }
}
