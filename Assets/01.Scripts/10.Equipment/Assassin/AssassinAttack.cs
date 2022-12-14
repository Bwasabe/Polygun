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


    public override void Skill()
    {
        // TODO : 사운드
        SoundManager.Instance.Play(AudioType.IgnorePitch, _data.AttackSound);

        _attackObjectList[_prevAttackIndex].SetActive(false);
        _prevAttackIndex = _attackIndex;
        _attackObjectList[_attackIndex].SetActive(true);
        _playerAttack.SetBulletRate(_data.AttackCoolTime[_attackIndex]);
        _attackIndex = (_attackIndex + 1) % _attackObjectList.Count;
        CameraManager.Instance.CameraShake();
    }

    public void SkillInit()
    {
        _playerAttack.SetReload(-1, 0f);
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

    [SerializeField]
    private AudioClip _attackSound;
    public AudioClip AttackSound => _attackSound;
}
