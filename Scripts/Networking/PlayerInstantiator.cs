using System.IO;
using UnityEngine;
using Photon.Pun;

public class PlayerInstantiator : MonoBehaviour
{
    [SerializeField] private PhotonView _photonView;

    [SerializeField] private Player _playerOwner;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    void Start()
    {
        if(_photonView.IsMine)
        {
            CreatePlayerController();
            SetupPlayerEvents();
        }
    }

    private void CreatePlayerController()
    {
        _playerOwner = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"),
                Vector3.zero, Quaternion.identity).GetComponent<Player>();
    }

    private void SetupPlayerEvents()
    {
        var interactionRenderer = FindObjectOfType<InteractionRenderer>();
        var interactInfo = FindObjectOfType<InteractionInfoPanel>();

        Debug.Log($"{interactionRenderer == null}," +
            $" {interactInfo == null}");

        _playerOwner.InteractebleObjectFocus.AddListener(interactionRenderer.RenderInteractebleObjectView);
        _playerOwner.InteractebleObjectFocus.AddListener(interactInfo.ShowInteractionPanel);
        _playerOwner.InteractebleDataInstalled.AddListener(interactInfo.gameObject.SetActive);
        _playerOwner.InteractebleObjectFocusLeft.AddListener(interactionRenderer.RenderDefaultObjectView);

        interactInfo.gameObject.SetActive(false);
    }
}
