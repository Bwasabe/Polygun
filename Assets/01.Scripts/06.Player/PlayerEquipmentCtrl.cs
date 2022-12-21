using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentCtrl : BasePlayerComponent
{
    [SerializeField]
    private LayerMask _equipmentLayer;
    [SerializeField]
    private TMPro.TMP_Text _getEquipmentUI;
    [SerializeField]
    private float _radius = 10f;
    [SerializeField]
    private Transform _equipmentTransform;

    private List<BaseEquipment> _equipmentList = new List<BaseEquipment>();
    private PlayerSkillCtrl _playerSkillCtrl;

    // [SerializeField]
    // private bool _isShopEquipment = false;

    // private BaseEquipment _currentEquipment;
    // public BaseEquipment CurrentEquipment => _currentEquipment;

    private int _currentEquipmentIndex = 0;

    // private const string ONE = "ONE";
    // private const string SECOND = "SECOND";
    // private const string TAB = "TAB";

    protected override void Start()
    {
        base.Start();

        _playerSkillCtrl = _player.GetPlayerComponent<PlayerSkillCtrl>();

        _equipmentList.Add(null);
    }
    public void GetEquipment(BaseEquipment equipment)
    {
        equipment.transform.SetParent(_equipmentTransform);
        equipment.transform.localPosition = Vector3.zero;
        equipment.transform.localRotation = Quaternion.identity;
        SetEquipment(equipment);
    }
    private void SetEquipment(BaseEquipment equipment)
    {
        if (_equipmentList[_currentEquipmentIndex] != null)
        {
            // 아이템 드롭하는 것
            _equipmentList[_currentEquipmentIndex].transform.SetParent(null);
            _equipmentList[_currentEquipmentIndex].IsEquip = false;
            _equipmentList[_currentEquipmentIndex].gameObject.SetActive(false);

            _playerSkillCtrl.RemovePlayerSkill<PlayerAttack>(_equipmentList[_currentEquipmentIndex].Attack);
            _playerSkillCtrl.RemovePlayerSkill<PlayerSubSkill>(_equipmentList[_currentEquipmentIndex].SubSkill);
        }

        _playerSkillCtrl.AddPlayerSkill<PlayerAttack>(equipment.Attack);
        _playerSkillCtrl.AddPlayerSkill<PlayerSubSkill>(equipment.SubSkill);

        equipment.ParticleActive(false);
        equipment.IsEquip = true;
        equipment.GetComponent<Collider>().enabled = false;
        _equipmentList[_currentEquipmentIndex] = equipment;

    }

    // : 나중에 장비가 바뀌는 UI
    // private void ChangeEquipment()
    // {
    //     _currentEquipmentIndex = (_currentEquipmentIndex + 1) % _equipmentList.Count;

    //     _currentEquipment = _equipmentList[_currentEquipmentIndex];
    // }
    // private void ChangeEquipment0()
    // {
    //     _currentEquipmentIndex = 0;
    // }
    // private void ChangeEquipment1()
    // {
    //     _currentEquipmentIndex = 1;

    // }
    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius, _equipmentLayer);
        if (colliders.Length > 0)
        {
            _getEquipmentUI.gameObject.SetActive(true);
            // 장비 구매 or 획득 UI띄어주기
            IPurchaseAble purchaseAble = colliders[0].transform.GetComponentInChildren<IPurchaseAble>();
            if (purchaseAble != null)
            {
                if (GameManager.Instance.CoinAmount >= purchaseAble.Price)
                {
                    _getEquipmentUI.text = PriceText(purchaseAble.Price);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        GameManager.Instance.CoinAmount -= purchaseAble.Price;
                        purchaseAble.PurchaseCallBack();
                        // GetEquipment(equipment);
                    }
                }
                else
                {
                    _getEquipmentUI.text = $"돈이 부족합니다";
                }
            }

        }
        else
        {
            _getEquipmentUI.gameObject.SetActive(false);

        }
    }

    private string PriceText(int value)
    {
        return $"E를 눌러 아이템을 {value.ToString()}원에 구매하세요";
    }

}
