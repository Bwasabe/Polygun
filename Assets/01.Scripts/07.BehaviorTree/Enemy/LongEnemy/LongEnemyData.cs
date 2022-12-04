using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public partial class LongEnemyData : BT_Data
{
	[Header("����")]
	[SerializeField]
	private UnitStat _stat;

	public UnitStat Stat => _stat;
}
