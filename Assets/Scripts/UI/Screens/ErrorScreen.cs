using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ErrorScreen : MonoBehaviour, IScreen
{
    [SerializeField] private TMP_Text _errorDescription;
    [SerializeField] private Image _errorIcon;

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void SetErrorInfo(SystemMessage message)
    {
        _errorDescription.text = message.Message;
        _errorIcon.sprite = message.Icon.sprite;

        Show();
    }
}
