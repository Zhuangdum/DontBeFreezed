﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : Element
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
                continue;
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
                continue;
            }
            if (item.type == ElementType.Wood)
            {
                if (item.pos == this.pos && (reasonType == ElementType.Bomb || reasonType == ElementType.Fuel))
                {
                    //self
                    GameManager.instance.mapGenerator.ReplaceElement(item.pos, ElementType.Land, item.state);
                    continue;
                }
                else
                {
                    if (reasonType == ElementType.Fuel)
                    {
                        Wood wood = GameManager.instance.mapGenerator.GetTargetElement(item.pos) as Wood;
                        if(wood !=null)
                            wood.BeEffected(wood, reasonType);
                        continue;
                    }
                }
            }
            GameManager.instance.mapGenerator.ReplaceElement(item.pos, ElementType.Land, item.state);
        }
    }
}
