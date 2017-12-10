using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Element
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
            {
                item.state = ElementState.Warm;
                continue;
            }
            if (item.type == ElementType.Stone1)
                continue;
            if (item.type == ElementType.Stone2)
                continue;
            if (item.type == ElementType.Fire)
            {
                continue;
            }
            if (item.type == ElementType.Fuel)
            {
                Fuel fuel = item as Fuel;
                fuel.BeEffected(fuel, ElementType.Fuel);
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
                Wood wood = GameManager.instance.mapGenerator.GetTargetElement(item.pos) as Wood;
                if(wood !=null)
                    wood.BeEffected(wood, ElementType.Fire);
                continue;
            }

            //判断遮挡
            if (item.pos.x == 2)
            {
                Element element =
                    GameManager.instance.mapGenerator.GetTargetElement(new Vector2(item.pos.x - 1, item.pos.y));
                if (element.type == ElementType.House || element.type == ElementType.House1 ||
                    element.type == ElementType.Stone1 || element.type == ElementType.Stone2)
                    continue;
            }
            if (item.pos.x == -2)
            {
                Element element =
                    GameManager.instance.mapGenerator.GetTargetElement(new Vector2(item.pos.x + 1, item.pos.y));
                if (element.type == ElementType.House || element.type == ElementType.House1 ||
                    element.type == ElementType.Stone1 || element.type == ElementType.Stone2)
                    continue;
            }
            if (item.pos.y == 2)
            {
                Element element =
                    GameManager.instance.mapGenerator.GetTargetElement(new Vector2(item.pos.x, item.pos.y-1));
                if (element.type == ElementType.House || element.type == ElementType.House1 ||
                    element.type == ElementType.Stone1 || element.type == ElementType.Stone2)
                    continue;
            }
            if (item.pos.x == -2)
            {
                Element element =
                    GameManager.instance.mapGenerator.GetTargetElement(new Vector2(item.pos.x, item.pos.y+1));
                if (element.type == ElementType.House || element.type == ElementType.House1 ||
                    element.type == ElementType.Stone1 || element.type == ElementType.Stone2)
                    continue;
            }
            GameManager.instance.mapGenerator.ReplaceElement(item.pos, ElementType.Land, item.state);
        }
    }
}