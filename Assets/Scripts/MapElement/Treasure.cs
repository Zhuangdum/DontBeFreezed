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
        AudioSource.PlayClipAtPoint(AudioManager.instance.treasureClip, new Vector3(0,0,0));
        int random = Random.Range(0, 3);
        if (random == 0)
        {
            GameManager.instance.tools[ElementType.Fire] += 2;
            GameManager.instance.effectManager.PlayTextAnimation(this.pos, "篝火 +2");
        }
        else if (random == 1)
        {
            GameManager.instance.tools[ElementType.Fuel] += 2;
            GameManager.instance.effectManager.PlayTextAnimation(this.pos, "燃料 +2");
        }
        else
        {
            GameManager.instance.tools[ElementType.Bomb] += 2;
            GameManager.instance.effectManager.PlayTextAnimation(this.pos, "炸弹 +2");
        }

        GameManager.instance.SetUIDirty();
    }

}
