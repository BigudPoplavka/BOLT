using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainScreen : MonoBehaviour, IScreen
{
    [SerializeField] private GameObject _createRoomButton;
    [SerializeField] private GameObject _findRoomButton;

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _findRoomButton.SetActive(true);
        _createRoomButton.SetActive(true);
    }

    public void SetMenuToRoomMode()
    {
        _findRoomButton.SetActive(false);
        _createRoomButton.SetActive(false);
    }

    public void SetMenuToDefaultMode()
    {
        _findRoomButton.SetActive(true);
        _createRoomButton.SetActive(true);
    }
}
