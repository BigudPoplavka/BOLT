using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;

public class FindRoomScreen : MonoBehaviour, IScreen
{
    [SerializeField] private Transform _roomListContent;
    [SerializeField] private GameObject _roomItemTemplate;

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
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
