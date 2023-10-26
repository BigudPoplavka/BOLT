using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Realtime;

public class FindRoomScreen : MonoBehaviour, IScreen
{
    [SerializeField] private Transform _roomListContent;
    [SerializeField] private GameObject _roomItemTemplate;

    [SerializeField] private UnityEvent _menuOpened;

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _menuOpened?.Invoke();
    }

    public void UpdateRoomListView(List<RoomInfo> roomInfos)
    {
        foreach(Transform transform in _roomListContent)
        {
            Destroy(transform.gameObject); 
        }

        foreach(RoomInfo roomInfo in roomInfos)
        {
            if(roomInfo.RemovedFromList)
            {
                continue;
            }

            Instantiate(_roomItemTemplate, _roomListContent).GetComponent<RoomItemRenderer>().RenderRoomItem(roomInfo);
        }
    }
}
