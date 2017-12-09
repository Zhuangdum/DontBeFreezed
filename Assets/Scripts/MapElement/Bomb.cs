using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Element
{
    public override void BeEffected(Element sourceElement, ElementType reasonType)
    {
        UpdateNearby(sourceElement, reasonType);
    }

    private void UpdateNearby(Element sourceElement, ElementType reasonType)
    {
        List<Element> tempList;
        tempList = GameManager.instance.mapGenerator.GetNearbyBlock(sourceElement, reasonType);

        foreach (var item in tempList)
        {
            if (item.type == ElementType.Trap)
                continue;
            if (item.type == ElementType.Land)
                continue;
            if (item.type == ElementType.Fire)
                continue;
            if (item.type == ElementType.Fuel)
            {
                Fuel fuel = item as Fuel;
                fuel.BeEffected(fuel, ElementType.Bomb);
                continue;
            }
            if (item.type == ElementType.Bomb)
            {
                if (item.pos == this.pos)
                {
                    //self
                    GameManager.instance.mapGenerator.ReplaceElement(item.pos, ElementType.Land, item.state);
                }
                else
                {
                    Bomb bomb = item as Bomb;
                    bomb.BeEffected(bomb, ElementType.Bomb);
                    continue;
                }
            }
            if (item.type == ElementType.House)
            {
                continue;
            }
            if (item.type == ElementType.Treasure)
            {
                Debug.Log("获得宝箱一个， 里面还有各种资源");
                continue;
            }
            if (item.type == ElementType.Wood)
            {
                if (reasonType == ElementType.Bomb)
                {
                    //
                }
                else
                {
                    Wood wood = item as Wood;
                    wood.BeEffected(wood, ElementType.Bomb);
                    continue;
                }
            }
            GameManager.instance.mapGenerator.ReplaceElement(item.pos, ElementType.Land, item.state);
        }
    }
}
