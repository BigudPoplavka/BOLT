using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    #region Компоненты

    [SerializeField] private Camera _camera;
    [SerializeField] private PhotonView _photonView;

    [SerializeField] public InteractionData _objectOnFocus;
    [SerializeField] private LayerMask _interactebleObjects;
 
    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _headAnimator;

    public InteractionData ObjectOnFocus { get; private set; }
   
    #endregion

    #region Параметры игрока

    [SerializeField] public StateParameter Health { get; private set; }
    [SerializeField] public StateParameter Stamina { get; private set; }
    [SerializeField] public StateParameter Armor { get; private set; }

    [SerializeField] private float _maxInteractDistance;
    
    #endregion

    #region Состояния игрока

    public StateIdle Idle { get; private set; }
    public StateWalk Walk { get; private set; }
    public StateSitdown Sitdown { get; private set; }
    public StateRun Run { get; private set; }

    public StateMachine StateMachine { get; private set; }
    
    #endregion

    #region Свойства

    public Animator Animator { get => _animator; private set => _animator = value; }
    public Animator HeadAnimator { get => _headAnimator; private set => _headAnimator = value; }
    
    public UnityEvent<bool> InteractebleDataInstalled { get => _interactebleDataInstalled; }
    public UnityEvent<InteractionData> InteractebleObjectFocus { get => _interactebleObjectFocus; }
    public UnityEvent<InteractionData> InteractebleObjectFocusLeft { get => _interactebleObjectFocusLeft; }
    #endregion

    #region События
    [SerializeField] private UnityEvent<bool> _interactebleDataInstalled;

    [SerializeField] private UnityEvent<InteractionData> _interactebleObjectFocus;
    [SerializeField] private UnityEvent<InteractionData> _interactebleObjectFocusLeft;

    #endregion

    #region Методы Unity

    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();

        InitPlayerStates();
    }

    void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _camera = GetComponentInChildren<Camera>();

        if (!_photonView.IsMine)
        {
            Destroy(_camera.gameObject);
        }
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Interact();
    }

    #endregion

    private void InitPlayerStates()
    {
        Health = new StateParameter();
        Stamina = new StateParameter();
        Armor = new StateParameter();

        Idle = new StateIdle(this);
        Walk = new StateWalk(this);
        Sitdown = new StateSitdown(this);
        Run = new StateRun(this);

        StateMachine = new StateMachine(Idle);
    }

    private void Interact()
    {
        if (!_photonView.IsMine)
        {
            return;
        }

        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        
        InteractionData outData = null;

        Debug.DrawRay(ray.origin, ray.direction, Color.red);

        if(Physics.Raycast(ray, out RaycastHit raycastHit, _maxInteractDistance, _interactebleObjects, QueryTriggerInteraction.Ignore))
        {
            if (raycastHit.transform.TryGetComponent<InteractionData>(out InteractionData interactionData))
            {
                _objectOnFocus = interactionData;
                outData = interactionData;
                _interactebleObjectFocus?.Invoke(interactionData);
                _interactebleDataInstalled?.Invoke(true);
            }
        }

        if (_objectOnFocus != null && outData == null)
        {
            _interactebleObjectFocusLeft?.Invoke(_objectOnFocus);
            _interactebleDataInstalled?.Invoke(false);
        }
    }

    public void GetDamage(float damage)
    {
        if (damage > Armor.Value)
        {
            float remains = damage - Armor.Value;
            float armorDamage = damage - remains;

            Armor.DecreaseValue(armorDamage);
            Health.DecreaseValue(remains);
        }
        else
        {
            Armor.DecreaseValue(damage);
        }
    }
}
