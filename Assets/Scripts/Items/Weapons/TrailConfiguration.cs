using UnityEngine;

[CreateAssetMenu(fileName = "TrailConfiguration", menuName = "Gun/Trail", order = 3)]
public class TrailConfiguration : ScriptableObject
{
    [SerializeField] private Material _trailMaterial;
    [SerializeField] private AnimationCurve _widthCurve;
    [SerializeField] private Gradient _colorGradient;

    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private float _minVertexDistance = 0.1f;
    [SerializeField] private float _missDistance = 100f;
    [SerializeField] private float _simulationSpeed = 100f;

    public Material TrailMaterial { get => _trailMaterial; }
    public AnimationCurve WidthCurve { get => _widthCurve; }
    public Gradient ColorGradient { get => _colorGradient; }
    public float Duration { get => _duration; }
    public float MinVertexDistance { get => _minVertexDistance; }
    public float MissDistance { get => _missDistance; }
    public float SimulationSpeed { get => _simulationSpeed; }
}
