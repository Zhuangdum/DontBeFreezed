﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static int fireNum = 15;
    private static int fuelNum = 3;
    private static int bombNum = 3;

    private Vector2 lastPointLand;
    private ElementType lastHandType;
    
    public ElementMenu configMenu;

    public static GameManager instance;

    public MapGenerator mapGenerator;

    public UIManager uiManager;
    
    public EffectManager effectManager;
    
    public Sprite snowland;
    public Sprite grassland;

    public Dictionary<ElementType, int> tools;
    public int fireCount {
        set
        {
            if(value<=0)
                uiManager.fireText.text = 0.ToString();
            else
                uiManager.fireText.text = tools[ElementType.Fire].ToString();
        }
    }
    public int fuelCount {
        set
        {
            if(value<=0)
                uiManager.fuelText.text = 0.ToString();
            else
                uiManager.fuelText.text = tools[ElementType.Fuel].ToString();
        }
    }
    public int boombCount {
        set
        {
            if(value<=0)
                uiManager.bombText.text = 0.ToString();
            else
                uiManager.bombText.text = tools[ElementType.Bomb].ToString();
        }
    }

    public bool interactable = false;
    private void Awake()
    {
        instance = this;
        tools = new Dictionary<ElementType, int>();
    }


    public void LoadMap(string name)
    {
        mapGenerator.GenerateMap(name);
        
        tools.Clear();
        tools.Add(ElementType.Fire, fireNum);
        tools.Add(ElementType.Fuel, fuelNum);
        tools.Add(ElementType.Bomb, bombNum);
        
        lastPointLand = new Vector2(0,0);
        lastHandType = ElementType.Fire;
        
        //set ui
        uiManager.InitTools();
        SetUIDirty();
    }

    public void SetUIDirty()
    {
        //set ui dirty
        fireCount = tools[ElementType.Fire];
        fuelCount = tools[ElementType.Fuel];
        boombCount = tools[ElementType.Bomb];
    }

    private void Update()
    {
        if (!interactable)
            return;
        showPoint2Land();
        if (Input.GetMouseButtonDown(0))
        {
            clickBlock();
        }
        mapGenerator.updateRenderState();
    }

    public void showPoint2Land()
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.CompareTag("Rock"))
            {
                Element clickedElement = hitInfo.collider.GetComponent<Element>();

                if (clickedElement.type == ElementType.Land)
                {
                    if (tools.ContainsKey(uiManager.handType) && tools[uiManager.handType] > 0)
                    {
                        Land land = clickedElement as Land;
                        if (land == null)
                        {
                            return;
                        }
                        
                        if (lastPointLand != land.pos || lastHandType != uiManager.handType) 
                        {
                            mapGenerator.unShowEffect(lastPointLand,lastHandType);
                        }
                        
                        mapGenerator.showEffect(land.pos, uiManager.handType);
                        lastPointLand = land.pos;
                        lastHandType = uiManager.handType;
                    }
                    
                }
            }
        }
    }
    
    public void clickBlock()
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.CompareTag("Rock"))
            {
                tools[uiManager.handType]--;
                SetUIDirty();

                if (tools.ContainsKey(uiManager.handType) && tools[uiManager.handType] > 0)
                {
                    Element clickedElement = hitInfo.collider.GetComponent<Element>();
                    if (clickedElement.type == ElementType.Trap)
                    {
                        Trap trap = clickedElement as Trap;
                        trap.ShowTrap();
                        AudioSource.PlayClipAtPoint(AudioManager.instance.trapClip, new Vector3(0,0,0));
                        return;
                    }
                    else if(clickedElement.type == ElementType.Land)
                    {
                        Land land = clickedElement as Land;
                        if (land == null)
                        {
                            Debug.LogError("xxxx");
                            return;
                        }
                        
                        land.BeEffected(land, uiManager.handType);
                        Debug.Log(" 使用道具类型： "+ uiManager.handType+"  道具数量： "+tools[uiManager.handType]);
                    }
                    else
                    {
                        Debug.Log("this place already have something");
                    }
                }
                else
                {
                    Debug.Log(" 道具数量不足 "+ uiManager.handType);
                }
            }
        }
    }

   
}
