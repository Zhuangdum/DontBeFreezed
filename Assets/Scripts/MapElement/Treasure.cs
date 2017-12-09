using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : Element
{
    public override void BeEffected(Element sourceElement, ElementType reasonType)
    {
        base.BeEffected(sourceElement, reasonType);
    }

    public void AddTools()
    {
        GameManager.instance.tools[ElementType.Fire] += 2;
        GameManager.instance.tools[ElementType.Fuel] += 2;
        GameManager.instance.tools[ElementType.Bomb] += 2;
        
        GameManager.instance.SetUIDirty();
    }

}
