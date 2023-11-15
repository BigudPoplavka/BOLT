using Photon.Pun;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    #region Компоненты

    [SerializeField] private Camera _camera;
    [SerializeField] private PhotonView _photonView;

    [SerializeField] public InteractionData _objectOnFocus;

    [SerializeField] private LayerMask _interactebleObjects;
    [SerializeField] private LayerMask _pickableObjects;

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

    #region Инвентарь

    [SerializeField] private QuickAccessSlots _slots;

    [SerializeField] private Transform _hand;

    [SerializeField] private int _itemIndex;
    [SerializeField] private int _prevItemIndex = -1;

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

        if (_photonView.IsMine)
        {
            _itemIndex = 0;

            EquipItem(0);
        }
    }

    void Update()
    {
        GetEquipmentInputs();
        GetMouseScrollInput();
        Interact();
    }

    private void FixedUpdate()
    {

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

    private void GetEquipmentInputs()
    {
        for (int i = 0; i < _slots.QuickSlots.Count; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                EquipItem(i);
                break;
            }
        }
    }

    private void GetMouseScrollInput()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0 && _slots.IsAllSlotsFree)
        {
            return;
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            if (_itemIndex >= _slots.QuickSlots.FindAll(x => !x.IsEmpty).Count - 1)
            {
                EquipItem(0);
            }
            else
            {
                EquipItem(_itemIndex + 1);
            }
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            if (_itemIndex <= 0)
            {
                EquipItem(_slots.QuickSlots.FindAll(x => !x.IsEmpty).Count - 1);
            }
            else
            {
                EquipItem(_itemIndex - 1);
            }
        }
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

        if (Physics.Raycast(ray, out RaycastHit raycastHit, _maxInteractDistance, _interactebleObjects))
        {
            if (raycastHit.transform.TryGetComponent<InteractionData>(out InteractionData interactionData))
            {
                outData = interactionData;
                SendInteractDataToRenderer(interactionData);
            }
        }

        if (Physics.Raycast(ray, out RaycastHit raycastHitPickable, _maxInteractDistance, _pickableObjects))
        { 
            if (raycastHitPickable.transform.TryGetComponent<PickableItem>(out PickableItem itemData))
            {
                raycastHitPickable.transform.TryGetComponent<InteractionData>(out outData);
                SendInteractDataToRenderer(outData);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    TryPickUpItem(raycastHitPickable.transform.gameObject);
                }
            }
        }

        if (_objectOnFocus != null && outData == null)
        {
            _interactebleObjectFocusLeft?.Invoke(_objectOnFocus);
            _interactebleDataInstalled?.Invoke(false);
        }
    }

    private void SendInteractDataToRenderer(InteractionData interactionData)
    {
        _objectOnFocus = interactionData;
        _interactebleObjectFocus?.Invoke(interactionData);
        _interactebleDataInstalled?.Invoke(true);
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

    public void EquipItem(int index)
    {
        if (_slots.QuickSlots[index].IsEmpty || index == _prevItemIndex)
        {
            return;
        }

        _itemIndex = index;
        _slots.QuickSlots[index].ItemObject.SetActive(true);

        if(_prevItemIndex != -1)
        {
            _slots.QuickSlots[_prevItemIndex].ItemObject.SetActive(false);
        }

        _prevItemIndex = _itemIndex;
    }

    private void TryPickUpItem(GameObject itemObject)
    {
        if (_slots.TryAddItem(itemObject, _itemIndex))
        {
            SetPickedObjectInHand(itemObject);

            if (_slots.QuickSlots.FindAll(x => !x.IsEmpty).Count == 1)
            {
                EquipItem(0);
            }
        }
    }

    private void SetPickedObjectInHand(GameObject itemObject)
    {
        itemObject.SetActive(false);
        itemObject.transform.SetParent(_camera.transform);
        itemObject.transform.position = _hand.position;
        itemObject.transform.rotation = _hand.rotation;
        itemObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void DropItem()
    {

    }
}
