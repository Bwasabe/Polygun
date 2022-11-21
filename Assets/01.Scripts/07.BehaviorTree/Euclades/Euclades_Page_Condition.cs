using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Euclades_Page_Condition : BT_Condition
{
    private Page _page;
    private Euclades_Data _data;

    private Euclades.EucladesPage _pageIndex;

    public Euclades_Page_Condition(BehaviorTree tree, Page page, Euclades.EucladesPage pageIndex,List<BT_Node> c) : base(tree, c)
    {
        _page = page;
        _pageIndex = pageIndex;
        _data = _tree.GetData<Euclades_Data>();
    }

    public override Result Execute()
    {

        if (_data.Hp <= _page.MaxHp && _data.Hp <= _page.MinHp)
        {
            if (!_pageIndex.Equals(_data.PageIndex))
            {
                Euclades euclades = _tree as Euclades;
                euclades.SetRandomNode(_pageIndex);
            }
            return Result.SUCCESS;
        }
        else
        {
            return Result.FAILURE;
        }
    }

}
