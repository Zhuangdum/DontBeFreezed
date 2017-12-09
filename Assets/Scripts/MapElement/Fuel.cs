﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : Element
{
    public override void BeEffected(Element sourceElement, ElementType reasonType)
    {
        UpdateNearby(sourceElement, reasonType);
    }
    
    private void UpdateNearby(Element sourceElement, ElementType sourceType)
    {
        List<Element> tempList;
        tempList = GameManager.instance.mapGenerator.GetNearbyBlock(sourceElement, sourceType);
            
        foreach (var item in tempList)
        {
            if(item.type == ElementType.Trap)
                continue;
            if(item.type == ElementType.Land)
                continue;
            if (item.type == ElementType.Fire)
                continue;
            if (item.type == ElementType.Fuel)
            {
                if (item.pos == this.pos)
                {
                    //self
                    item.GetComponent<SpriteRenderer>().color = Color.white;
                    item.GetComponent<Element>().type = ElementType.Land;
                }
                else
                {
                    //other
                    Fuel fuel = item as Fuel;
                    fuel.BeEffected(fuel, ElementType.Fuel);
                    continue;
                }
            }
            if (item.type == ElementType.Boomb)
            {
                continue;
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
                Wood wood = item as Wood;
                wood.BeEffected(wood, ElementType.Fuel);
                continue;
            }
            item.GetComponent<SpriteRenderer>().color = Color.white;
            item.GetComponent<Element>().type = ElementType.Land;
        }
    }
}
