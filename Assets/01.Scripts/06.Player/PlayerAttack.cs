using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : BasePlayerSkillComponent
{
    [SerializeField]
    private Transform _attackPosition;
    public Transform AttackPos => _attackPosition;

    [SerializeField]
    private LayerMask _hitLayer;
    [SerializeField]
    private float _bulletSpeed = 25f;
    [SerializeField]
    private float _bulletRate;
    [SerializeField]
    private float _attackStateRate = 1f;
    [SerializeField]
    private float _reloadDuration = 2f;
    [SerializeField]
    private Image _reloadImage;
    [SerializeField]
    private TMPro.TMP_Text _reloadText;
    [SerializeField]
    private AudioClip _defaultAttackClip;


    public int ReloadCount { get; private set; }
    private int _currentReloadCount;

    private float _rateTimer;

    private PlayerStat _playerStat;


    public float BulletSpeed => _bulletSpeed;
    public LayerMask HitLayer => _hitLayer;
    public float Damage => _player.PlayerStat.DamageStat;

    protected override void Start()
    {
        base.Start();
        _playerStat = _player.PlayerStat;
        if (_skill == null)
            _player.GetPlayerComponent<PlayerSkillCtrl>().AddPlayerSkill<PlayerAttack>(new PlayerDefaultAttack(this, _defaultAttackClip));
    }
    private void Update()
    {
        _rateTimer += Time.deltaTime * GameManager.PlayerTimeScale;
        if (Input.GetKey(_input.GetInput("MOUSE_LEFTBUTTON")) && _bulletRate <= _rateTimer && _currentReloadCount != 0)
        {
            if (!_player.CurrentState.HasFlag(PLAYER_STATE.ATTACK))
            {
                _player.CurrentState |= PLAYER_STATE.ATTACK;
                _player.GetPlayerComponent<PlayerRotation>().ChangeRotate();
            }
            _rateTimer = 0;
            _skill.Skill();
            _currentReloadCount--;
            UpdateReloadText();
        }
        if(Input.GetKeyDown(_input.GetInput("RELOAD")) && _currentReloadCount != ReloadCount)
        {
            _currentReloadCount = 0;
        }

        if (_currentReloadCount == 0)
        {
            _reloadImage.fillAmount += Time.deltaTime * GameManager.PlayerTimeScale / _reloadDuration;
            if (_reloadImage.fillAmount >= 1f)
            {
                _reloadImage.fillAmount = 0f;
                _currentReloadCount = ReloadCount;
                UpdateReloadText();
            }
        }

        if (_rateTimer >= _attackStateRate)
        {
            _player.CurrentState &= ~PLAYER_STATE.ATTACK;
        }
    }

    public void SetReload(int reloadCount, float reloadDuration)
    {
        ReloadCount = reloadCount;
        _currentReloadCount = ReloadCount;
        _reloadDuration = reloadDuration;
        UpdateReloadText();
    }

    private void UpdateReloadText()
    {
        _reloadText.text = $"{_currentReloadCount.ToString()} / {ReloadCount.ToString()}";

    }

    public void SetBulletRate(float value)
    {
        _bulletRate = value;
    }

    protected override void RegisterInput()
    {
        _input.AddInput("MOUSE_LEFTBUTTON", KeyCode.Mouse0);
        _input.AddInput("RELOAD", KeyCode.R);
    }

}
