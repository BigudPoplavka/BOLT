using TMPro;
using UnityEngine;

public class InteractionRenderer : MonoBehaviour
{
    [SerializeField] private TMP_Text _interactActionTitle;

    public void RenderInteractPanel(InteractionData interactionData)
    {
        _interactActionTitle.text = interactionData.ActionTitle;
    }

    public void RenderInteractebleObjectView(InteractionData interactionData)
    {
        interactionData.RenderInteractebleState();
    }

    public void RenderDefaultObjectView(InteractionData interactionData)
    {
        interactionData.RenderDefaultState();
    }
}
