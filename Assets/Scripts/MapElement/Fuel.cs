using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : Element
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
            if (item.pos == this.pos)
            {
                GameManager.instance.mapGenerator.ReplaceElement(item.pos, ElementType.Land, item.state);
                continue;
            }
            if(item.type == ElementType.Trap)
                continue;
            if(item.type == ElementType.Land)
            {
                item.state = ElementState.Warm;
                continue;
            }
            if (item.type == ElementType.Fire)
            {
                continue;
            }
            if (item.type == ElementType.Fuel)
            {
                if (item.pos == this.pos)
                {
                    //self
                    GameManager.instance.mapGenerator.ReplaceElement(item.pos, ElementType.Land, item.state);
                }
                else
                {
                    //other
                    Fuel fuel = item as Fuel;
                    fuel.BeEffected(fuel, ElementType.Fuel);
                    continue;
                }
            }
            if (item.type == ElementType.Bomb)
            {
                continue;
            }
            if (item.type == ElementType.House)
            {
                continue;
            }
            if (item.type == ElementType.Treasure)
            {
                Treasure treasure = item as Treasure;
                treasure.AddTools();
                continue;
            }
            if (item.type == ElementType.Wood)
            {
                Wood wood = item as Wood;
                wood.BeEffected(wood, ElementType.Fuel);
                continue;
            }
            GameManager.instance.mapGenerator.ReplaceElement(item.pos, ElementType.Land, item.state);
        }
    }
}
