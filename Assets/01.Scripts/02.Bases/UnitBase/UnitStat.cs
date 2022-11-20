using UnityEngine;

[System.Serializable]
public class UnitStat
{
	[SerializeField]
	private int _defaultHp;
	
	[SerializeField]
	private int _damage;
	public int DamageStat => _damage;

    [SerializeField]
    private float _defaultSpeed;

    //TODO: 속도를 이용하여 움직이게 만들기
    private float _speed;
    public float Speed{
        get => _speed;
        set => value = _speed;
    }

    private int _hp;
	public int HP => _hp;

	public virtual void Init()
	{
        ResetHp();
        ResetSpeed();
    }

	public void ResetHp()
	{
		_hp = _defaultHp;
	}
    public void ResetSpeed()
    {
        _speed = _defaultSpeed;
    }

	public void Damaged(int damage)
	{
		_hp -= damage;
		Debug.Log(_hp);
	}
}
