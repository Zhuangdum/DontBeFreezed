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
                    Fuel fuel = GameManager.instance.mapGenerator.GetTargetElement(item.pos) as Fuel;
                    if(fuel !=null)
                        fuel.BeEffected(fuel, ElementType.Fire);
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
                Treasure treasure = GameManager.instance.mapGenerator.GetTargetElement(item.pos) as Treasure;
                if(treasure !=null)
                    treasure.AddTools();
                GameManager.instance.mapGenerator.ReplaceElement(item.pos, ElementType.Land, item.state);
                continue;
            }
            if (item.type == ElementType.Wood)
            {
                Wood wood = GameManager.instance.mapGenerator.GetTargetElement(item.pos) as Wood;
                if(wood !=null)
                    wood.BeEffected(wood, ElementType.Fuel);
                continue;
            }
            GameManager.instance.mapGenerator.ReplaceElement(item.pos, ElementType.Land, item.state);
        }
    }
}
