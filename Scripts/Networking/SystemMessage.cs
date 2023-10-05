using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="System Message", menuName ="Message")]
public class SystemMessage: ScriptableObject 
{
    [SerializeField] private string _message;
    [SerializeField] private Image _icon;

    public string Message { get => _message; }
    public Image Icon { get => _icon; }

    public void SetMessage(string message)
    {
        _message = message;
    }
}
