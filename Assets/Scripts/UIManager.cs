using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public ElementType handType;

    public Text fireText;
    public Text fuelText;
    public Text bombText;

    public void Init()
    {
        handType = ElementType.Fire;
        HideChoice();
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

    public GameObject choicePanel;

    public void Confirm()
    {
        //TODO
        
        HideChoice();
    }

    public void Cancel()
    {
        //TODO
        
        HideChoice();
    }

    public void ShowChoice()
    {
        choicePanel.SetActive(true);
    }

    public void HideChoice()
    {
        choicePanel.SetActive(false);
    }
}
