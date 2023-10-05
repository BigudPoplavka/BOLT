using UnityEngine;

public class SystemMessagesQueue: MonoBehaviour
{
    [SerializeField] private GameObject _messageTemplate;
    [SerializeField] private SysMessageRenderer _messageRenderer;
    [SerializeField] private Transform _messagesListView;

    private void Start()
    {
        _messageRenderer = _messageTemplate.GetComponent<SysMessageRenderer>();
    }

    public void ShowMessage(SystemMessage message)
    {
        _messageRenderer.SetMessage(message);

        Instantiate(_messageTemplate, _messagesListView).GetComponent<SysMessageRenderer>().ShowAndWaitForHide(); ;
    }
}
