using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
using System;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] private string _connectionProcessMsg;

    [SerializeField] private SystemMessage _connectionFailedMsg;
    [SerializeField] private SystemMessage _lobbyJoinedMsg;
    [SerializeField] private SystemMessage _roomCreatingError;

    [SerializeField] private UnityEvent<string> _beforeServerConnected;
    [SerializeField] private UnityEvent<string> _afterJoinedRoom;
    [SerializeField] private UnityEvent<int> _roomPlayersCountChanged;

    [SerializeField] private UnityEvent _afterCreatedRoom;
    [SerializeField] private UnityEvent _afterLeftRoom;

    [SerializeField] private UnityEvent<SystemMessage> _connectionFailed; 
    [SerializeField] private UnityEvent<SystemMessage> _afterLobbyJoined; 
    [SerializeField] private UnityEvent<SystemMessage> _afterDisconnected;
    [SerializeField] private UnityEvent<SystemMessage> _error;

    [SerializeField] private UnityEvent<List<RoomInfo>> _roomListUpdated;
    [SerializeField] private UnityEvent<Photon.Realtime.Player> _playerJoinedRoom;

    [SerializeField] private GameObject _loadGameButton;

    private void Start()
    {
        try
        {
            Debug.Log("Подключение к мастер-серверу");

            _beforeServerConnected?.Invoke(_connectionProcessMsg);

            PhotonNetwork.ConnectUsingSettings();
        }
        catch(Exception e)
        {
            Debug.Log($"Ошибка: {e.Message}");

            _error?.Invoke(_connectionFailedMsg);
        }
    }

    private void Update()
    {
        
    }

    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
        _loadGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    #region Методы и обработчики Room

    public void CreateRoom(string roomName)
    {
        PhotonNetwork.CreateRoom(roomName);
    }

    public void CreateRoom(string roomName, RoomOptions roomOptions)
    {
        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }

    public void JoinRoom(RoomInfo roomInfo)
    {
        PhotonNetwork.JoinRoom(roomInfo.Name);
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnJoinedRoom()
    {
        _afterJoinedRoom?.Invoke(PhotonNetwork.CurrentRoom.Name);
        _roomPlayersCountChanged?.Invoke(PhotonNetwork.PlayerList.Length);

        Photon.Realtime.Player[] players = PhotonNetwork.PlayerList;

        foreach (Photon.Realtime.Player player in players)
        {
            _playerJoinedRoom?.Invoke(player);
        }

        _loadGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnLeftRoom()
    {
        _afterLeftRoom?.Invoke();
        _roomPlayersCountChanged?.Invoke(PhotonNetwork.PlayerList.Length);
    }

    public override void OnCreatedRoom()
    {
        _afterCreatedRoom?.Invoke();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        _error?.Invoke(_roomCreatingError);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        _roomListUpdated?.Invoke(roomList);
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        _playerJoinedRoom?.Invoke(newPlayer);
    }

    #endregion

    #region Методы и обработчики Lobby

    public override void OnJoinedLobby()
    {
        Debug.Log("Вы присоединились к лобби");

        _afterLobbyJoined?.Invoke(_lobbyJoinedMsg);

        System.Random random = new System.Random();
        PhotonNetwork.NickName = "Player_" + random.Next(0, 1000).ToString("0000");
    }

    #endregion

    #region Загрузка игры

    public void LoadGameLevel()
    {
        PhotonNetwork.LoadLevel(1);
    }

    #endregion

    #region Методы и обработчики подключений

    public override void OnConnectedToMaster()
    {
        Debug.Log("Подключено к мастер-серверу");

        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"Соединение потеряно");

        _afterDisconnected?.Invoke(_connectionFailedMsg);
    }

    #endregion

    public void OnQuitConfirmed()
    {
        Application.Quit();
    }
}
