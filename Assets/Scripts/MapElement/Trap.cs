using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Element
{
    [SerializeField]
    private Sprite showSprite;
    [SerializeField]
    private Sprite hideSprite;

    private SpriteRenderer spriteRender;
    private void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
    }

    public override void BeEffected(Element sourceElement, ElementType reasonType)
    {
        
    }

    public void ShowTrap()
    {
        spriteRender.sprite = showSprite;
    }

    public void HideTrap()
    {
        spriteRender.sprite = hideSprite;
    }
}
