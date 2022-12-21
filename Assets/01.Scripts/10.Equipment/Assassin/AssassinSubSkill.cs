using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Yields;

public class AssassinSubSkill : BaseSkill, ISkillPersistAble, ISkillInitAble
{
    private AssassinData _data;
    private Player _player;
    private PlayerStat _playerStat;
    private CharacterController _cc;
    public AssassinSubSkill(BaseEquipment parent) : base(parent)
    {
        _data = _parent.GetData<AssassinData>();
        _player = GameManager.Instance.Player;
        _playerStat = _player.PlayerStat;
        _cc = _player.GetComponent<CharacterController>();
    }

    public override void Skill()
    {
        Vector3 dir = Define.MainCam.transform.forward;
        dir.y = 0f;
        dir.Normalize();

        // TODO: 사운드
        SoundManager.Instance.Play(AudioType.IgnorePitch, _data.TeleportSound);

        // TODO: 풀링

        GameObject teleportStart = GameObject.Instantiate(_data.TeleportParticle, _player.transform.position, Quaternion.identity, null);
        teleportStart.SetActive(true);
        _parent.StartCoroutine(TeleportParticleFalse(teleportStart));

        StarFall starFall = GameObject.Instantiate(_data.StarFall, _parent.transform.position, Quaternion.identity, null);
        starFall.gameObject.SetActive(true);
        _parent.StartCoroutine(StarFallActiveFalse(starFall.gameObject));
        starFall.transform.forward = dir;
        

        if (Physics.Raycast(_parent.transform.position, dir, out RaycastHit hit, _data.SubSkillDistance, _data.HitLayer))
        {
            // 적 뒤로 가고 풀림
            starFall.SetScale((_player.transform.position - hit.transform.position).magnitude );

            Vector3 movePos = hit.point - hit.transform.position;
            movePos.y = 0f;

            Vector3 hitPoint = hit.transform.position;
            hitPoint.y = _player.transform.position.y;

            _cc.enabled = false;
            _player.transform.position = hitPoint + movePos * -1.5f; //+ hit.transform.forward * -1f; //* hit.collider.bounds.max.magnitude;
            _cc.enabled = true;
            Utils.VCam.m_XAxis.Value += 180f;

            GameObject teleportEnd = GameObject.Instantiate(_data.TeleportParticle, _player.transform.position, Quaternion.identity, null);
            teleportEnd.SetActive(true);
            _parent.StartCoroutine(TeleportParticleFalse(teleportEnd));
        }
        else
        {
            starFall.SetScale(_data.SubSkillDistance);
        }
    }

    private IEnumerator TeleportParticleFalse(GameObject falseObject)
    {
        yield return WaitForSeconds(_data.TeleportParticleDuartion);
        falseObject.SetActive(false);
    }

    private IEnumerator StarFallActiveFalse(GameObject starFall)
    {
        yield return WaitForSeconds(_data.SubSkillDuration);
        starFall.SetActive(false);
    }

    public void SkillInit()
    {
        _playerStat.SubSkillRatio = _data.SubSkillCoolTime;
    }

    public void SkillPersist()
    {
        //Debug.Log(Utils.VCam.transform.forward);
    }
}

public partial class AssassinData
{
    [SerializeField]
    private StarFall _starFall;
    public StarFall StarFall => _starFall;

    [SerializeField]
    private float _subSkillDistance = 30f;
    public float SubSkillDistance => _subSkillDistance;

    [SerializeField]
    private float _subSkillDuration = 0.1f;
    public float SubSkillDuration => _subSkillDuration;

    [SerializeField]
    private float _subSkillCoolTime = 5f;
    public float SubSkillCoolTime => _subSkillCoolTime;

    [SerializeField]
    private GameObject _teleportParticle;
    public GameObject TeleportParticle => _teleportParticle;

    [SerializeField]
    private float _teleportParticleDuration = 3f;
    public float TeleportParticleDuartion => _teleportParticleDuration;

    [SerializeField]
    private AudioClip _teleportSound;
    public AudioClip TeleportSound => _teleportSound;
}
