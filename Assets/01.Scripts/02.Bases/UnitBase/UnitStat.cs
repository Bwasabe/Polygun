using UnityEngine;
using System;
using System.Reflection;

[System.Serializable]
public class UnitStat
{
    [SerializeField]
    private float _defaultHp;

    [SerializeField]
    private float _defaultDamage;
    [SerializeField]
    private float _defaultSpeed = 15f;
    [SerializeField]
    private float _defaultAttackRate;
    [SerializeField]
    private BasicHPSlider baseSlider;
    private float _damage;
    public float DamageStat => _damage;

    private float _speed;
    public float Speed
    {
        get => _speed;
        set => value = _speed;
    }
    
    private float _attackRate;
    public float AttackRate => _attackRate;

    private float _hp;
    public float HP => _hp;

    public virtual void Init()
    {
        CallInitMethod();
    }

    private void CallInitMethod()
    {
        Type type = this.GetType();
        MethodInfo[] methodInfos = type.GetMethods(
            BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
        foreach(MethodInfo method in methodInfos)
        {
            if(method.Name.IndexOf("Reset") != -1)
            {
                method.Invoke(this, null);
            }
        }
    }

    public void ResetHp()
    {
        _hp = _defaultHp;
        baseSlider?.InitSlider(_defaultHp);
    }
    public void ResetSpeed()
    {
        _speed = _defaultSpeed;

    }

	public void ResetDamage()
	{
        _damage = _defaultDamage;
    }
    public void ResetAttackRate()
    {
        _attackRate = _defaultAttackRate;
    }

    public void Damaged(float damage)
    {
        _hp -= damage;
        baseSlider?.SetSlider(_hp);
		Debug.Log(_hp);
    }
}
