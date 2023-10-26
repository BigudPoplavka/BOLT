using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class PlayerStatsRenderer : MonoBehaviour
{
    [SerializeField] private Slider _staminaBar;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Slider _armorBar;

    [SerializeField] private TMP_Text _healthPoints;
    [SerializeField] private TMP_Text _armorPoints;

    public void UpdateStaminaSlider(float value)
    {
        _staminaBar.value = value;
    }

    public void UpdateHealthSlider(float value)
    {
        _healthBar.value = value;
    }

    public void UpdateArmorSlider(float value)
    {
        _armorBar.value = value;
    }

    public void UpdateHealthPointsText(string value)
    {
        _healthPoints.text = value;
    }

    public void UpdateArmorPointsText(string value)
    {
        _armorPoints.text = value;
    }
}
