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

    public GameManager gamanager;
    public GameObject panel;
    
    public void StartGame()
    {
        panel.gameObject.SetActive(false);
        GameManager.instance.LoadMap(mapName);
    }


    public void InitTools()
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

    public string mapName = "map_easy";
    public void RestartGame()
    {
        GameManager.instance.LoadMap(mapName);
    }

    public void GameMenu()
    {
        ShowChoice();
    }

    public GameObject choicePanel;

    public void LevelEasy()
    {
        //TODO
        HideChoice();
        mapName = "map_easy";
        GameManager.instance.LoadMap("map_easy");
    }

    public void LevelNormal()
    {
        //TODO
        HideChoice();
        mapName = "map_hard";
        GameManager.instance.LoadMap("map_hard");
    }
    public void LevelHard()
    {
        //TODO
        HideChoice();
        mapName = "map_boom";
        GameManager.instance.LoadMap("map_boom");
    }

    public void ShowChoice()
    {
        GameManager.instance.interactable = false;
        choicePanel.SetActive(true);
    }

    public void HideChoice()
    {
        GameManager.instance.interactable = true;
        choicePanel.SetActive(false);
    }
}
