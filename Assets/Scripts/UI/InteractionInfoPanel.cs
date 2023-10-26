using UnityEngine;
using TMPro;

public class InteractionInfoPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _keyNameText;
    [SerializeField] private TMP_Text _actionTitleText;

    public void ShowInteractionPanel(InteractionData interactionData)
    {
        _keyNameText.text = interactionData.KeyName;
        _actionTitleText.text = interactionData.ActionTitle;
    }
}
