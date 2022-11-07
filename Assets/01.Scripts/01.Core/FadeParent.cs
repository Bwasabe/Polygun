using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum FADECHILDS
{
    FADEOBJECT,
    TOPBAR,
    BOTTOMBAR,
}

public class FadeParent : MonoBehaviour
{
    private RectTransform _topBar = null;

    private RectTransform _bottomBar = null;

    private float _hideBarY = 0f;

    private void Awake()
    {

        _bottomBar = transform.Find(FADECHILDS.BOTTOMBAR.ToString()).GetComponent<RectTransform>();
        _topBar = transform.Find(FADECHILDS.TOPBAR.ToString()).GetComponent<RectTransform>();

        _hideBarY = Mathf.Abs(_topBar.anchoredPosition.y);

        Vector2 bottomAnchorPos = _bottomBar.anchoredPosition;
        bottomAnchorPos.y = -_hideBarY;
        _bottomBar.anchoredPosition = bottomAnchorPos;

        Vector2 topAnchorPos = _topBar.anchoredPosition;
        topAnchorPos.y = _hideBarY;
        _topBar.anchoredPosition = topAnchorPos;
    }
    public void ShowBar(bool isShow, float duration = 1f)
    {
        if (isShow)
        {
            _topBar.DOAnchorPosY(-_hideBarY, duration);
            _bottomBar.DOAnchorPosY(_hideBarY, duration);
        }
        else
        {
            _topBar.DOAnchorPosY(_hideBarY, duration);
            _bottomBar.DOAnchorPosY(-_hideBarY, duration);
        }
    }
}
