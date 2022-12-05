using UnityEngine;

[System.Serializable]
public class BT_Data
{
	[SerializeField]
	private UnitStat _stat;

	public UnitStat Stat => _stat;
}
