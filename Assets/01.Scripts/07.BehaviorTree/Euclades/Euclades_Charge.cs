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
                _chargeSpeed = _data.Page1ChargeSpeed;
                break;
            case Euclades.EucladesPage.Page2:
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

    private Vector3 _destination;

    protected override void OnEnter()
    {
        Debug.Log("Charge들어옴");
        base.OnEnter();
    }

    protected override void OnUpdate()
    {
        Debug.Log("업데이트 도는 중");
        base.OnUpdate();

        _timer += Time.deltaTime;
        if (_data.IsCharge)
        {
            float distance = Vector3.Distance(_destination, _tree.transform.position);
            if (distance >= 2f)
            {
                _cc.Move(_dir * Time.deltaTime * _chargeSpeed);
                NodeResult = Result.RUNNING;
            }
            else
            {
                NodeResult = Result.SUCCESS;
                UpdateState = UpdateState.Exit;
            }
        }
        else
        {
            if (_timer <= _data.ChargeReadyDuration)
            {
                _tree.transform.LookAt(_target);
            }
            else
            {
                Debug.Log("차지 시작");
                _destination = _target.transform.position;

                _dir = _destination - _tree.transform.position;
                _dir.Normalize();

                _timer = 0f;
                _data.IsCharge = true;
            }
            NodeResult = Result.RUNNING;
        }
    }

    protected override void OnExit()
    {
        Debug.Log("나감");
        _destination = Vector3.zero;
        _dir = Vector3.zero;
        _data.IsCharge = false;
        NodeResult = Result.SUCCESS;
        base.OnExit();
    }

    public override Result Execute()
    {
        base.Execute();
        return NodeResult;
    }
}

public partial class Euclades_Data
{
    public bool IsCharge { get; set; } = false;


    [SerializeField]
    private float _page1ChargeSpeed;

    public float Page1ChargeSpeed => _page1ChargeSpeed;


    [SerializeField]
    private float _page2ChargeSpeed;

    public float Page2ChargeSpeed => _page2ChargeSpeed;

    [SerializeField]
    private float _chargeReadyDuration = 3f;

    public float ChargeReadyDuration => _chargeReadyDuration;
}
