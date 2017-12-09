using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour 
{
    public Vector2 pos;
    public ElementState state;
    public ElementType type;

    public virtual void BeEffected(Element sourceElement, ElementType reasonType)
    {
        
    }
    
    private void UpdateNearby(Element targetElement, ElementType sourceType)
    {
        List<Element> tempList;
        tempList = GameManager.instance.mapGenerator.GetNearbyBlock(targetElement, sourceType);
            
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
                Fuel fuel = item as Fuel;
                fuel.BeEffected(fuel, ElementType.Fuel);
                continue;
            }
            if (item.type == ElementType.Boomb)
            {
                Boomb boomb = item as Boomb;
                boomb.BeEffected(boomb, ElementType.Boomb);
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
                wood.BeEffected(wood, ElementType.Wood);
                continue;
            }
            item.GetComponent<SpriteRenderer>().color = Color.white;
            item.GetComponent<Element>().type = ElementType.Land;
        }
    }
}


public enum ElementState
{
    Freezed = 0,
    UnFreezed = 1,
    Other = 2
}