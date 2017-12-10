using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Treasure : Element
{
    private void Start()
    {
        
    }

    public override void BeEffected(Element sourceElement, ElementType reasonType)
    {
        base.BeEffected(sourceElement, reasonType);
    }

    public void AddTools()
    {
        int random = Random.Range(0, 3);
        if (random == 0)
        {
            GameManager.instance.tools[ElementType.Fire] += 1;
            GameManager.instance.effectManager.PlayTextAnimation(this.pos, 1);
        }
        else if (random == 1)
        {
            GameManager.instance.tools[ElementType.Fuel] += 1;
            GameManager.instance.effectManager.PlayTextAnimation(this.pos, 2);
        }
        else
        {
            GameManager.instance.tools[ElementType.Bomb] += 1;
            GameManager.instance.effectManager.PlayTextAnimation(this.pos, 3);
        }

        GameManager.instance.SetUIDirty();
    }

}
