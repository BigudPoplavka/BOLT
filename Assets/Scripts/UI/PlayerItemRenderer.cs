using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class PlayerItemRenderer : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text _nicknameText;
    [SerializeField] private Image _avatar;

    [SerializeField] private Photon.Realtime.Player _player;

    public void Setup(Photon.Realtime.Player player)
    {
        _player = player;

        _nicknameText.text = _player.NickName;
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Debug.Log("OnPlayerLeftRoom otherPlayer");
        if(_player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }

    public override void OnLeftRoom()
    {
        Debug.Log("OnPlayerLeftRoom Player");
        Destroy(gameObject);
    }
}
