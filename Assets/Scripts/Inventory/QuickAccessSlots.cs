using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuickAccessSlots : MonoBehaviour
{
    [SerializeField] private List<Slot> _quickSlots;

    [SerializeField] private PhotonView _photonView;

    public List<Slot> QuickSlots { get => _quickSlots; }
    public bool IsAnySlotFree => _quickSlots.Any(x => x.IsEmpty);
    public bool IsAllSlotsFree => _quickSlots.All(x => x.IsEmpty);

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
    }

    public bool TryAddItem(GameObject item, int slotIndex)
    {
        PickableItem itemComponent = item.GetComponent<PickableItem>();

        if (IsAnySlotFree)
        {
            _quickSlots.Find(x => x.IsEmpty).SetItemToSlot(itemComponent);

            return true;
        }
        else if(IsTheSameItemInSlot(itemComponent))
        {
            _quickSlots.Find(x => x.name == itemComponent.Name)
                .SetTheSameItemToSlot(itemComponent);
        }
        else 
        {
            _quickSlots[slotIndex].DragItemOut();
            _quickSlots[slotIndex].SetItemToSlot(itemComponent);
        }

        return false;
    }

    private bool IsTheSameItemInSlot(PickableItem item)
    {
        return _quickSlots.Any(x => x.name == item.Name);
    }

    public void ResetAllSlots()
    {
        foreach (Slot slot in _quickSlots)
        {
            slot.ResetSlot();
        }
    }
}
