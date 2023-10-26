using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class SysMessageRenderer : MonoBehaviour
{
    [SerializeField] private TMP_Text _messageText;
    [SerializeField] private Image _messageIcon;

    [SerializeField] private float _showingTime;

    public void ShowAndWaitForHide()
    {
        StartCoroutine(WaitForHide());
    }

    public void SetMessage(SystemMessage message)
    {
        _messageText.text = message.Message;
        //_messageIcon.sprite = message.Icon.sprite;
    }

    private IEnumerator WaitForHide()
    {
        yield return new WaitForSeconds(_showingTime);

        Destroy(gameObject);
    }
}
