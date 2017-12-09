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
                fuel.BeEffected(fuel, ElementType.Fire);
                continue;
            }
            if (item.type == ElementType.Boomb)
            {
                Boomb boomb = item as Boomb;
                boomb.BeEffected(boomb, ElementType.Fire);
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
                if (reasonType == ElementType.Fire)
                {
                    continue;
                }
                else if(reasonType == ElementType.Boomb || reasonType == ElementType.Fuel)
                {
                    Wood wood = item as Wood;
                    wood.BeEffected(wood, ElementType.Wood);
                    continue;
                }
            }
            item.GetComponent<SpriteRenderer>().color = Color.white;
            item.GetComponent<Element>().type = ElementType.Land;
        }
    }
}
