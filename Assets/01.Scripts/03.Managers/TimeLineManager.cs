using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public enum TimeLine
{
	Test
}
public class TimeLineManager : MonoBehaviour
{
	[SerializeField]
	private TimelinePlayable[] _timelines;
}
