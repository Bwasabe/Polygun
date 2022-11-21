using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ReadyToCharge에서 넘어온 다음 돌진 동작
public class Euclades_Charge : BT_Node
{

    // TODO: 속도 바꾸기 넣어주기
    public Euclades_Charge(BehaviorTree t, Euclades.EucladesPage page, List<BT_Node> c = null) : base(t, c)
    {
        _cc = _tree.GetComponent<CharacterController>();
        _data = _tree.GetData<Euclades_Data>();

        switch (page)
        {
            case Euclades.EucladesPage.Page1:
                _chargeDuartion = _data.Page1ChargeDuration;
                _chargeSpeed = _data.Page1ChargeSpeed;
                break;
            case Euclades.EucladesPage.Page2:
                _chargeDuartion = _data.Page2ChargeDuration;
                _chargeSpeed = _data.Page2ChargeSpeed;
                break;
            default:
                Debug.LogError("Parameter Page is Wrong Page");
                break;
        }

        _target = GameManager.Instance.Player.transform;
    }

    private CharacterController _cc;
    private Euclades_Data _data;

    private float _timer;

    private float _chargeDuartion;

    private float _chargeSpeed;

    private Transform _target;

    private Vector3 _dir;

    protected override void OnEnter()
    {
        base.OnEnter();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        _timer += Time.deltaTime;
        if(_data.IsCharge)
        {
            if(_timer <= _chargeDuartion)
            {

                _cc.Move(_dir * Time.deltaTime * _chargeSpeed);
                NodeResult = Result.RUNNING;
            }
            else
            {
                NodeResult = Result.SUCCESS;
            }
        }
        else
        {
            if(_timer <= _data.ChargeReadyDuration)
            {
                _tree.transform.LookAt(_target);
            }
            else
            {
                _dir = _target.transform.position - _tree.transform.position;
                _dir.Normalize();
                _timer = 0f;
                _data.IsCharge = true;
            }
            NodeResult = Result.RUNNING;
        }
    }

    protected override void OnExit()
    {
        base.OnExit();
    }

    public override Result Execute()
    {
        _timer += Time.deltaTime;

        if (_timer <= _chargeDuartion)
        {
            _data.IsCharge = true;
            // 돌진
            // TODO : 돌진 넣고 방향 추가하고 등등
            // _cc.Move(_data.ChargeSpeed * Time.deltaTime);
            return Result.RUNNING;
        }
        else
        {
            _data.IsCharge = false;
            return Result.SUCCESS;
        }
    }
}

public partial class Euclades_Data
{
    public bool IsCharge { get; set; } = false;

    [SerializeField]
    private float _page1ChargeDuraion;

    public float Page1ChargeDuration => _page1ChargeDuraion;

    [SerializeField]
    private float _page1ChargeSpeed;

    public float Page1ChargeSpeed => _page1ChargeSpeed;

    [SerializeField]
    private float _page2ChargeDuration;

    public float Page2ChargeDuration => _page2ChargeDuration;

    [SerializeField]
    private float _page2ChargeSpeed;

    public float Page2ChargeSpeed => _page2ChargeSpeed;

    [SerializeField]
    private float _chargeReadyDuration = 3f;

    public float ChargeReadyDuration => _chargeReadyDuration;
}
