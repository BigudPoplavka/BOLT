using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class RoomScreen: MonoBehaviour, IScreen
{
    [SerializeField] private TMP_Text _roomName;

    [SerializeField] private UnityEvent _screenReady;

    [SerializeField] private Transform _playerListContent;

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void SetRoomName(string name)
    {
        _roomName.text = name;

        Setup();
    }

    private void Setup()
    {
        _screenReady?.Invoke();
    }

    public void ClearPreviousPlayerList()
    {
        foreach(Transform item in _playerListContent)
        {
            Destroy(item.gameObject);
        }
    }
}
