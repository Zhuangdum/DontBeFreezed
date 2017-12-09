using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public ElementType handType;

    public void Init()
    {
        handType = ElementType.Fire;
    }

    public void SetFire()
    {
        SetHandItemType(ElementType.Fire);
    }
    
    public void SetFuel()
    {
        SetHandItemType(ElementType.Fuel);
    }
    
    public void SetBoomb()
    {
        SetHandItemType(ElementType.Bomb);
    }

    private void SetHandItemType(ElementType type)
    {
        handType = type;
        Debug.Log("set handitem to: "+ type);
    }
}
