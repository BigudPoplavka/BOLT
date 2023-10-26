using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;

public class RoomItemRenderer : MonoBehaviour
{
    [SerializeField] private TMP_Text _roomName;
    [SerializeField] private TMP_Text _activePlayersCount;
    [SerializeField] private TMP_Text _maxPlayersCount;
    [SerializeField] private Image _backGround;

    [SerializeField] private Launcher _launcher;
    [SerializeField] private RoomInfo _roomInfo;

    private void Start()
    {
        _launcher = FindObjectOfType<Launcher>();
    }

    public void RenderRoomItem(RoomInfo roomInfo)
    {
        _roomInfo = roomInfo;

        _roomName.text = roomInfo.Name;
        _activePlayersCount.text = roomInfo.PlayerCount.ToString();
        _maxPlayersCount.text = roomInfo.MaxPlayers.ToString();
    }

    public void RoomItemOnClick()
    {
        _launcher.JoinRoom(_roomInfo);
    }

    public void UpdateActivePlayersCount(int count)
    {
        _activePlayersCount.text = count.ToString();
    }
}
