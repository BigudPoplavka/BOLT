using UnityEngine;
using UnityEngine.Pool;
using System.Collections;

[CreateAssetMenu(fileName ="Gun", menuName ="Gun/New Gun", order =3)]
public class Gun: ScriptableObject
{
    [SerializeField] private GunType _gunType;
    [SerializeField] private AmmoType ammoType;
    
    [SerializeField] private string _name;

    [SerializeField] private float _lastShootTime;

    [SerializeField] private GameObject _templatePrefab;
    [SerializeField] private GameObject _model;

    [SerializeField] private Vector3 _spawnPoint;
    [SerializeField] private Vector3 _spawnRotation;
    [SerializeField] private Vector3 _spread;

    [SerializeField] private GunConfiguration _gunConfiguration;
    [SerializeField] private TrailConfiguration _trailConfiguration;

    [SerializeField] private MonoBehaviour _activeMonoBehaviour;

    [SerializeField] private ObjectPool<TrailRenderer> _trailPool; 

    [SerializeField] private ParticleSystem _particleSystem;

    public void Spawn(Transform parent, MonoBehaviour monoBehaviour)
    {
        _activeMonoBehaviour = monoBehaviour;
        _spread = _gunConfiguration.Spread;
        _lastShootTime = 0;

        _trailPool = new ObjectPool<TrailRenderer>(CreateTrail);
        _model = Instantiate(_templatePrefab);

        _model.transform.SetParent(parent, false);
        _model.transform.localPosition = _spawnPoint;
        _model.transform.localRotation = Quaternion.Euler(_spawnRotation);

        _particleSystem = _model.GetComponentInChildren<ParticleSystem>();
    }

    public void Shoot()
    {
        if(Time.time > _gunConfiguration.FireRate + _lastShootTime)
        {
            _lastShootTime = Time.time;
            _particleSystem.Play();

            //Vector3 shootDirection = _particleSystem.transform.forward + new Vector3(
            //    Random.Range(-_gunConfiguration.Spread.x, _gunConfiguration.Spread.x),
            //    Random.Range(-_gunConfiguration.Spread.y, _gunConfiguration.Spread.y),
            //    Random.Range(-_gunConfiguration.Spread.z, _gunConfiguration.Spread.z)
            //    );

            Vector3 shootDirection = _particleSystem.transform.forward + new Vector3(
               Random.Range(-_spread.x, _spread.x),
               Random.Range(-_spread.y, _spread.y),
               Random.Range(-_spread.z, _spread.z)
               );

            shootDirection.Normalize();

            if(Physics.Raycast(_particleSystem.transform.position, 
                shootDirection, out RaycastHit hit, _gunConfiguration.MaxShootDistance, _gunConfiguration.HitMask))
            {
                _activeMonoBehaviour.StartCoroutine(RenderTrail(_particleSystem.transform.position, hit.point, hit));
            }
            else
            {
                _activeMonoBehaviour.StartCoroutine(RenderTrail(
                    _particleSystem.transform.position,
                    _particleSystem.transform.position + (shootDirection * _trailConfiguration.MissDistance), new RaycastHit()));
            }
        }
    }

    private TrailRenderer CreateTrail()
    {
        GameObject instance = new GameObject("Bullet Trail");
        TrailRenderer trailRenderer = instance.AddComponent<TrailRenderer>();

        trailRenderer.colorGradient = _trailConfiguration.ColorGradient;
        trailRenderer.material = _trailConfiguration.TrailMaterial;
        trailRenderer.widthCurve = _trailConfiguration.WidthCurve;
        trailRenderer.time = _trailConfiguration.Duration;
        trailRenderer.minVertexDistance = _trailConfiguration.MinVertexDistance;
        trailRenderer.emitting = false;
        trailRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        return trailRenderer;
    }

    private IEnumerator RenderTrail(Vector3 startPoint, Vector3 endPoinit, RaycastHit hit)
    {
        TrailRenderer trailRenderer = _trailPool.Get();

        trailRenderer.gameObject.SetActive(true);
        trailRenderer.transform.position = startPoint;

        yield return null;

        trailRenderer.emitting = true;

        float distance = Vector3.Distance(startPoint, endPoinit);
        float remainingDistance = distance;

        while(remainingDistance > 0)
        {
            trailRenderer.transform.position = Vector3.Lerp(
                startPoint, endPoinit, Mathf.Clamp01(1 - (remainingDistance / distance))
                );
            remainingDistance -= _trailConfiguration.SimulationSpeed * Time.deltaTime;

            yield return null;
        }

        trailRenderer.transform.position = endPoinit;

        if(hit.collider != null)
        {
            // Call Surface Reaction with gun ImpactType field here
        }

        yield return _trailConfiguration.Duration;
        yield return null;

        trailRenderer.emitting = false;
        trailRenderer.gameObject.SetActive(false);
        
        _trailPool.Release(trailRenderer);
    }
}
