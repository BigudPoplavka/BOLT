using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LauncherRenderer : MonoBehaviour, IScreen
{
    [SerializeField] private TMP_Text _connectionProcessText;
    [SerializeField] private Image _backGround; 
    
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void ShowStatusMessage(string message)
    {
        _connectionProcessText.text = message;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
