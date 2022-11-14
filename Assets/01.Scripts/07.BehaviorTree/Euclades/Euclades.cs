using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Page
{
    [SerializeField]
    private float _maxHp;
    public float MaxHp => _maxHp;

    [SerializeField]
    private float _minHp;

    public float MinHp => _minHp;
}

[DisallowMultipleComponent]
public class Euclades : BehaviorTree
{
    public enum EucladesPage
    {
        Page1,
        Page2,
        Page3,
    }
    [SerializeField]
    private Page[] _page = new Page[3];

    [SerializeField]
    private Euclades_Data _data;

    private List<BT_RandomNode> _pageRandomNodes = new List<BT_RandomNode>();

    protected override BT_Node SetupTree()
    {
        _root = new BT_Selector
        (this, new List<BT_Node>
            {
                new Euclades_Page_Condition(this, _page[0], EucladesPage.Page1,_root),
                new Euclades_Page_Condition(this, _page[1], EucladesPage.Page2,_root),
                new Euclades_Page_Condition(this, _page[2], EucladesPage.Page3,_root),
            }
        );

        BT_RandomNode page1RandomNode = new BT_RandomNode(this, 0, 3, new List<BT_Node>
            {
                new Euclades_Shockwave(this),

                new BT_Sequence(this, new List<BT_Node>
                    {
                        new Euclades_ReadyToCharge(this),
                        new Euclades_Charge(this)
                    }),
                new Euclades_TeleportBullet(this)
            });

        _pageRandomNodes.Add(page1RandomNode);

        

        return _root;
    }

    public void SetRandomNode(EucladesPage pageIndex)
    {
        _data.CurrentRandomNode = _pageRandomNodes[((int)pageIndex)];
    }
}

public partial class Euclades_Data
{
    public BT_RandomNode CurrentRandomNode { get; set; }
    public int PageIndex { get; private set; } = 0;

    public void ResetRandom()
    {
        CurrentRandomNode.ResetRandom();
    }

}
