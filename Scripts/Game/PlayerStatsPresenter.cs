using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerStatsPresenter : MonoBehaviour, IPresenter
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerStatsRenderer _playerStatsRenderer;

    private const int STATS_MULTIPLIER = 100;

    void Start()
    {
        if(_player != null && _playerStatsRenderer != null)
        {
            Subscribe();
        }
    }

    public void Subscribe()
    {
        _player.Stamina.valueChanged.AddListener(OnStaminaChanged);
        _player.Health.valueChanged.AddListener(OnHealthChanged);
        _player.Armor.valueChanged.AddListener(OnArmorChanged);
    }

    public void Unsubscribe()
    {
        _player.Stamina.valueChanged.RemoveListener(OnStaminaChanged);
        _player.Health.valueChanged.RemoveListener(OnHealthChanged);
        _player.Armor.valueChanged.RemoveListener(OnArmorChanged);
    }

    private void OnDestroy()
    {
        if (_player != null && _playerStatsRenderer != null)
        {
            Unsubscribe();
        }
    }

    private void OnStaminaChanged(float value)
    {
        _playerStatsRenderer.UpdateStaminaSlider(value / STATS_MULTIPLIER);
    }

    private void OnHealthChanged(float value)
    {
        _playerStatsRenderer.UpdateHealthSlider(value / STATS_MULTIPLIER);
        _playerStatsRenderer.UpdateHealthPointsText(value.ToString());
    }

    private void OnArmorChanged(float value)
    {
        _playerStatsRenderer.UpdateArmorSlider(value / STATS_MULTIPLIER);
        _playerStatsRenderer.UpdateArmorPointsText(value.ToString());
    }
}
