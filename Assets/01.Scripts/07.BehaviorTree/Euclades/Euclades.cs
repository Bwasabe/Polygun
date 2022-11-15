using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        BT_RandomNode page2RandomNode = new BT_RandomNode(this, 0, 3, new List<BT_Node>
        {
            new BT_Sequence(this, new List<BT_Node>
                {
                    new Euclades_ReadyToCharge(this),
                    new Euclades_Charge(this)
                }),
            new BT_Sequence(this, new List<BT_Node>{
                new Boss_ViewChanger(this, Viewpoint.SideView),
                new Euclades_SideviewDash(this)
            }),
            new BT_Sequence(this, new List<BT_Node>{
                new Boss_ViewChanger(this, Viewpoint.TopView),
                new Euclades_TopdownShooter(this)
            }),
        });
        _pageRandomNodes.Add(page2RandomNode);

        BT_RandomNode page3RandomNode = new BT_RandomNode(this, 0, 3, new List<BT_Node>
        {
            new BT_Sequence(this, new List<BT_Node>{
                new Boss_ViewChanger(this, Viewpoint.SideView),
                new Euclades_SideviewPlatformer(this)
            }),
            new BT_Sequence(this, new List<BT_Node>
            {
                new Euclades_ReadyToCharge(this),
                new Euclades_PortalCharge(this)
            }),
            new BT_Sequence(this, new List<BT_Node>{
                new Euclades_Blackhole(this),
                new Euclades_SpawnJumpmap(this)
            }),
        });
        _pageRandomNodes.Add(page3RandomNode);


        _root = new BT_Selector(this, new List<BT_Node>
            {
                new Euclades_Page_Condition(this, _page[0], EucladesPage.Page1,page1RandomNode),
                new Euclades_Page_Condition(this, _page[1], EucladesPage.Page2,page2RandomNode),
                new Euclades_Page_Condition(this, _page[2], EucladesPage.Page3,page3RandomNode),
            }
        );

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
