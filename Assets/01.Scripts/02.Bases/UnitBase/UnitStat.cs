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

    private float _speed;
    public float Speed{
        get => _speed;
        set => value = _speed;
    }

    private int _hp;
	public int HP => _hp;


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
