using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinAttack : BaseSkill, ISkillInitAble
{
    private AssassinData _data;
    private List<GameObject> _attackObjectList = new List<GameObject>();
    private List<Bullet> _attackObjBulletList = new List<Bullet>();
    private PlayerAttack _playerAttack;

    private int _prevAttackIndex;
    private int _attackIndex;

    public AssassinAttack(BaseEquipment parent) : base(parent)
    {
        _data = _parent.GetData<AssassinData>();
        _playerAttack = GameManager.Instance.Player.GetPlayerComponent<PlayerAttack>();

        for (int i = 0; i < _data.AttackPrefabList.Count; ++i)
        {
            GameObject attack = GameObject.Instantiate(_data.AttackPrefabList[i], _parent.transform);

            Bullet bullet = attack.GetComponentInChildren<Bullet>();
            bullet.Damage = _data.AttackDamages[i];
            bullet.Speed = 0f;
            bullet.HitLayer = _data.HitLayer;

            _attackObjBulletList.Add(bullet);
            _attackObjectList.Add(attack);
            attack.SetActive(false);
        }

        _attackObjectList.ForEach(x => x.transform.localPosition = Vector3.zero);

    }


    //TODO: 근접공격 오브젝트 생성
    public override void Skill()
    {
        _attackObjectList[_prevAttackIndex].SetActive(false);
        _prevAttackIndex = _attackIndex;
        _attackObjectList[_attackIndex].SetActive(true);
        _playerAttack.SetBulletRate(_data.AttackCoolTime[_attackIndex]);
        _attackIndex = (_attackIndex + 1) % _attackObjectList.Count;
        CameraManager.Instance.CameraShake();
    }

    // TODO: 여기서 공격 시간 조정해주기
    public void SkillInit()
    {
        // _attackObjectList.ForEach(x =>
        // {

        //     Vector3 rot = x.transform.localEulerAngles;
        //     rot.y += Define.MainCam.transform.eulerAngles.y;
        //     x.transform.rotation = Quaternion.Euler(rot);

        // });
    }
}

public partial class AssassinData
{
    [SerializeField]
    private List<GameObject> _attackPrefabList;
    public List<GameObject> AttackPrefabList => _attackPrefabList;

    [SerializeField]
    private List<float> _attackCoolTime;
    public List<float> AttackCoolTime => _attackCoolTime;

    [SerializeField]
    private List<float> _attackDamages;
    public List<float> AttackDamages => _attackDamages;

    [SerializeField]
    private LayerMask _hitLayer;
    public LayerMask HitLayer => _hitLayer;
}
