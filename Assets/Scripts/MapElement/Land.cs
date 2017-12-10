using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : Element
{
    public override void BeEffected(Element sourceElement, ElementType reasonType)
    {
        UpdateNearby(sourceElement, reasonType);
    }

    private void UpdateNearby(Element sourceElement, ElementType reasonType)
    {
        List<Element> tempList;
        tempList = GameManager.instance.mapGenerator.GetNearbyBlock(sourceElement, reasonType);

        
        
        if (reasonType == ElementType.Fuel)
        {
            List<Element> list3x3 = GameManager.instance.mapGenerator.GetNearbyBlock3x3(sourceElement.pos);
            if (list3x3.Find(s => s.type == ElementType.Fire) == null)
            {
                GameManager.instance.mapGenerator.ReplaceElement(sourceElement.pos, ElementType.Fuel, sourceElement.state);
                return;
            }
        }
        
        if (reasonType == ElementType.Bomb)
        {
            List<Element> list3x3 = GameManager.instance.mapGenerator.GetNearbyBlock3x3(sourceElement.pos);
            if (list3x3.Find(s => s.type == ElementType.Fire) == null)
            {
                GameManager.instance.mapGenerator.ReplaceElement(sourceElement.pos, ElementType.Bomb, sourceElement.state);
                return;
            }
        }
        
        foreach (var item in tempList)
        {
            if (item.pos == this.pos)
            {
                GameManager.instance.mapGenerator.ReplaceElement(sourceElement.pos, (reasonType == ElementType.Bomb || reasonType == ElementType.Fuel)?ElementType.Land:reasonType, sourceElement.state);
                continue;
            }
            if (item.type == ElementType.Trap)
                continue;
            if (item.type == ElementType.Land)
            {
                if (reasonType == ElementType.Fire || reasonType == ElementType.Bomb || reasonType == ElementType.Fuel)
                {
                    GameManager.instance.mapGenerator.setBlockState(item.pos, ElementState.Warm);
                }
                continue;
            }
            if (item.type == ElementType.Fire)
                continue;
            if (item.type == ElementType.Stone1)
                continue;
            if (item.type == ElementType.Stone2)
                continue;
            if (item.type == ElementType.Fuel)
            {
                Fuel fuel = GameManager.instance.mapGenerator.GetTargetElement(item.pos) as Fuel;
                if(fuel !=null)
                    fuel.BeEffected(fuel, reasonType);
                continue;
            }
            if (item.type == ElementType.Bomb)
            {
                Bomb bomb = GameManager.instance.mapGenerator.GetTargetElement(item.pos) as Bomb;
                if(bomb !=null)
                    bomb.BeEffected(bomb, ElementType.Fire);
                continue;
            }
            if (item.type == ElementType.House)
            {
                continue;
            }
            if (item.type == ElementType.Treasure)
            {
                Treasure treasure = GameManager.instance.mapGenerator.GetTargetElement(item.pos) as Treasure;
                if(treasure !=null)
                    treasure.AddTools();
                GameManager.instance.mapGenerator.ReplaceElement(item.pos, ElementType.Land, item.state);
                continue;
            }
            if (item.type == ElementType.Wood)
            {
                if (reasonType == ElementType.Fire)
                {
                    continue;
                }
                if (reasonType == ElementType.Fuel || reasonType == ElementType.Bomb)
                {
                    Wood wood = GameManager.instance.mapGenerator.GetTargetElement(item.pos) as Wood;
                    if(wood !=null)
                        wood.BeEffected(wood, reasonType);
                    continue;
                }
            }
            GameManager.instance.mapGenerator.ReplaceElement(item.pos, ElementType.Land, item.state);
        }
    }
}
