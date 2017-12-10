using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    public Color warmColor;
    public Vector2 pos;
    public ElementState _state;
    public virtual ElementState state {
        get { return _state; }
        set
        {
            _state = value;
            if (_state == ElementState.Warm)
            {
//                GetComponent<SpriteRenderer>().material.color = warmColor;
            }
            else
            {
                
            }
        }
    }
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
            if (item.type == ElementType.Bomb)
            {
                Bomb bomb = item as Bomb;
                bomb.BeEffected(bomb, ElementType.Bomb);
                continue;
            }
            if (item.type == ElementType.House)
            {
                continue;
            }
            if (item.type == ElementType.Treasure)
            {
                Treasure treasure = item as Treasure;
                treasure.AddTools();
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
    
    public ParticleSystem particleSystem;

    public void PlayEffect()
    {
        particleSystem.gameObject.SetActive(true);
        particleSystem.Play();
    }
}


public enum ElementState
{
    Freezed = 0,
    Warm = 1,
    Other = 2
}