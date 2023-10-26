using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using Photon.Realtime;

public class CreateRoomScreen : MonoBehaviour, IScreen
{
    [SerializeField] private int _choosenMaxCount;

    [SerializeField] private TMP_InputField _roomNameInput;
    [SerializeField] private TMP_Dropdown _maxPlayersInput;
    [SerializeField] private TextMeshProUGUI _creationButtonText;
    [SerializeField] private Button _creationButton;
    [SerializeField] private Toggle _isOpen;

    [SerializeField] private UnityEvent<string, RoomOptions> _roomCreateBtnClicked;
    [SerializeField] private UnityEvent _menuOpened;

    public void Hide()
    {
        gameObject.SetActive(false);
        _creationButton.interactable = true;
    }

    public void Show()
    {
        ResetFields();
        gameObject.SetActive(true);
        _menuOpened?.Invoke();
    }

    public void ResetFields()
    {
        _roomNameInput.text = string.Empty;
    }

    public void SetMaxCount(int value)
    {
        _choosenMaxCount = int.Parse(_maxPlayersInput.options[value].text);
        Debug.Log(value.ToString());
    }

    public void OnRoomCreateClicked()
    {
        if (!string.IsNullOrEmpty(_roomNameInput.text))
        {
            _creationButton.interactable = false;

            RoomOptions roomOptions = new RoomOptions();

            roomOptions.IsOpen = _isOpen.isOn;
            roomOptions.MaxPlayers = _choosenMaxCount;

            Debug.Log($"MaxPlayers: {roomOptions.MaxPlayers}");

            _roomCreateBtnClicked?.Invoke(_roomNameInput.text, roomOptions);
        }
        else
        {
            Debug.Log("Пустое имя");
        }
    }
}
