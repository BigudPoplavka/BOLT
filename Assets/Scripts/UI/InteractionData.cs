using UnityEngine;

public class InteractionData : MonoBehaviour
{
    [SerializeField] private string _actionTitle;
    [SerializeField] private string _keyName;

    [SerializeField] private Material _interactSelectionMaterial;
    [SerializeField] private Material _defaultMaterial;

    [SerializeField] private Color _interactSelectionColor;
    [SerializeField] private Color _defaultColor;

    [SerializeField] private MeshRenderer[] _childRenderers;
    [SerializeField] private MeshRenderer _renderer;

    public string ActionTitle => _actionTitle;
    public string KeyName => _keyName;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _childRenderers = GetComponentsInChildren<MeshRenderer>();

        _defaultColor = _renderer.material.color;
    }

    private bool IsObjectHasRenderers()
    {
        if (_childRenderers.Length != 0)
        {
            return true;
        }
        return false;
    }

    private void ChangeMaterialProperties(Color color)
    {
        foreach (MeshRenderer renderer in _childRenderers)
        {
            renderer.material.color = color;
        }
    }

    public void RenderInteractebleState()
    {
        if (IsObjectHasRenderers())
        {
            ChangeMaterialProperties(_interactSelectionColor);
        }
    }

    public void RenderDefaultState()
    {
        if (IsObjectHasRenderers())
        {
            ChangeMaterialProperties(_defaultColor);          
        }
    }
}
