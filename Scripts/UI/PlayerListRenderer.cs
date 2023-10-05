using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerListRenderer : MonoBehaviour
{
    [SerializeField] private Transform _playerListContent;
    [SerializeField] private GameObject _playerItemTemplate;
   
    public void UpdatePlayerListView(Photon.Realtime.Player player)
    {
        Instantiate(_playerItemTemplate, _playerListContent).GetComponent<PlayerItemRenderer>().Setup(player);
    }
}
