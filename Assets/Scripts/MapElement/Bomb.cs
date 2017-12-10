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

        GameManager.instance.effectManager.PlayEffect(ElementType.Bomb, tempList);
        
        foreach (var item in tempList)
        {
            if (item.pos == this.pos)
            {
                GameManager.instance.mapGenerator.ReplaceElement(item.pos, ElementType.Land, ElementState.Other);
                continue;
            }
            if (item.type == ElementType.Trap)
                continue;
            if (item.type == ElementType.Land)
            {
                item.state = ElementState.Warm;
                continue;
            }
            if (item.type == ElementType.Fire)
                continue;
            if (item.type == ElementType.Fuel)
            {
                Fuel fuel = GameManager.instance.mapGenerator.GetTargetElement(item.pos) as Fuel;
                if(fuel !=null)
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
                    Bomb bomb = GameManager.instance.mapGenerator.GetTargetElement(item.pos) as Bomb;
                    if(bomb !=null)
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
                Treasure treasure = GameManager.instance.mapGenerator.GetTargetElement(item.pos) as Treasure;
                if(treasure !=null)
                    treasure.AddTools();
                GameManager.instance.mapGenerator.ReplaceElement(item.pos, ElementType.Land, item.state);
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
                    Wood wood = GameManager.instance.mapGenerator.GetTargetElement(item.pos) as Wood;
                    if(wood !=null)
                        wood.BeEffected(wood, ElementType.Bomb);
                    continue;
                }
            }
            GameManager.instance.mapGenerator.ReplaceElement(item.pos, ElementType.Land, item.state);
        }
    }
    
}
