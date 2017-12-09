using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    //列
    private int row = 25;

    //行
    private int column = 20;

    private GameObject map;

    private Vector2 bounds;

    private Dictionary<Vector2, Element> mapDic = new Dictionary<Vector2, Element>();

    public string mapName;

    private void Start()
    {
        var datas = csvConvert.loadMap(string.Format("Assets/CSV/{0}.csv", mapName));

        bounds = new Vector2(row, column);
        map = new GameObject("Map");
        for (int x = column - 1; x >= 0; x--)
        {
            for (int y = 0; y < row; y++)
            {
                Vector2 tempPos = new Vector2(y, x);
                int temp = datas[(int) (column - 1 - tempPos.y), (int) (tempPos.x)];
                ElementType type = (ElementType) temp;

                ElementConfig config;
                if (type == ElementType.Default)
                {
                    config = GameManager.instance.configMenu.configList.Find(s => s.type == ElementType.Land);
                }
                else
                    config = GameManager.instance.configMenu.configList.Find(s => s.type == type);

                if (config == null)
                    Debug.LogError(type);

                //instantiate gamobject
                GameObject go = Instantiate(config.prefab, map.transform);
                go.transform.position = tempPos;
                go.GetComponent<SpriteRenderer>().color = config.color;
                var ui = go.GetComponentInChildren<Text>();
                ui.text = string.Format("{0}{1}", tempPos.x, tempPos.y);
                ui.enabled = false;
                Element element = go.GetComponent<Element>();
                element.pos = tempPos;
                element.state = GetState(type);

                mapDic.Add(tempPos, element);
            }
        }
    }

    private ElementState GetState(ElementType type)
    {
        if (type == ElementType.Default)
            return ElementState.Warm;
        if (type == ElementType.Land)
            return ElementState.Freezed;
        return ElementState.Other;
    }

    public List<Element> GetNearbyBlock(Element sourceElement, ElementType reasonType)
    {
        if (sourceElement.type == ElementType.Fire)
            return GetSingleNearBy(sourceElement.pos, ElementType.Fire);
        if (sourceElement.type == ElementType.Fuel)
            return GetFullNearBy(sourceElement.pos, ElementType.Fuel);
        if (sourceElement.type == ElementType.Wood)
            return GetSingleNearBy(sourceElement.pos, ElementType.Wood);
        if (sourceElement.type == ElementType.Bomb)
            return GetFullNearBy(sourceElement.pos, ElementType.Bomb);
        if (sourceElement.type == ElementType.Land)
            if (reasonType == ElementType.Fire)
                return GetSingleNearBy(sourceElement.pos, ElementType.Fire);
            else
                return GetFullNearBy(sourceElement.pos, reasonType);
        return GetSingleNearBy(sourceElement.pos, ElementType.Fire);
    }

    public List<Element> GetNearbyBlock3x3(Vector2 targetPos)
    {
        int radius = 3;
        List<Element> rocks = new List<Element>();

        if (radius / 2 == 0)
        {
            Debug.LogError("radius value can not be double");
            return rocks;
        }
        for (int i = -radius / 2; i <= radius / 2; i++)
        {
            for (int j = -radius / 2; j <= radius / 2; j++)
            {
                Element element;
                if (mapDic.TryGetValue(new Vector2(targetPos.x + j, targetPos.y + i), out element))
                {
                    rocks.Add(element);
                }
            }
        }
        return rocks;
    }

    private List<Element> GetSingleNearBy(Vector2 pos, ElementType sourceType)
    {
        int radius = GameManager.instance.configMenu.configList.FirstOrDefault(s => s.type == sourceType).radius;
        List<Element> rocks = new List<Element>();

        if (radius / 2 == 0)
        {
            Debug.LogError("radius value can not be double");
            return rocks;
        }
        for (int i = -radius / 2; i <= radius / 2; i++)
        {
            for (int j = -radius / 2; j <= radius / 2; j++)
            {
                if (Mathf.Abs(i) + Mathf.Abs(j) >= radius / 2 + 1)
                    continue;
                Element element;
                if (mapDic.TryGetValue(new Vector2(pos.x + j, pos.y + i), out element))
                {
                    rocks.Add(element);
                }
            }
        }
        return rocks;
    }

    private List<Element> GetFullNearBy(Vector2 pos, ElementType sourceType)
    {
        int radius = GameManager.instance.configMenu.configList.FirstOrDefault(s => s.type == sourceType).radius;
        List<Element> rocks = new List<Element>();

        if (radius / 2 == 0)
        {
            Debug.LogError("radius value can not be double");
            return rocks;
        }
        for (int i = -radius / 2; i <= radius / 2; i++)
        {
            for (int j = -radius / 2; j <= radius / 2; j++)
            {
                Element element;
                if (mapDic.TryGetValue(new Vector2(pos.x + j, pos.y + i), out element))
                {
                    rocks.Add(element);
                }
            }
        }
        return rocks;
    }

    public void ReplaceElement(Vector2 targetPos, ElementType newType, ElementState newState)
    {
        if (mapDic.ContainsKey(targetPos))
        {
            Destroy(mapDic[targetPos].gameObject);
            Element tempElement = GetElement(newType, targetPos, newState);
            if (newType == ElementType.Land)
            {
                tempElement.state = ElementState.Warm;
                
            }
            mapDic[targetPos] = tempElement;
        }
        else
        {
            Debug.LogError("map do not contain this element, targetpos: " + targetPos);
        }
    }

    private Element GetElement(ElementType elementType, Vector2 pos, ElementState state)
    {
        Vector2 tempPos = pos;
        ElementType type = elementType;

        ElementConfig config;
        if (type == ElementType.Default)
        {
            config = GameManager.instance.configMenu.configList.Find(s => s.type == ElementType.Land);
        }
        else
            config = GameManager.instance.configMenu.configList.Find(s => s.type == type);

        if (config == null)
            Debug.LogError(type);

        //instantiate gamobject
        GameObject go = Instantiate(config.prefab, map.transform);
        go.transform.position = tempPos;
        go.GetComponent<SpriteRenderer>().color = config.color;
        var ui = go.GetComponentInChildren<Text>();
        ui.text = string.Format("{0}{1}", tempPos.x, tempPos.y);
        ui.enabled = false;
        Element element = go.GetComponent<Element>();
        element.pos = tempPos;
        element.state = state;

        return element;
    }

    public Element GetTargetElement(Vector2 targetPos)
    {
        return mapDic[targetPos];
    }

    private void OnGUi()
    {
        if (GUI.Button(new Rect(0, 10, 100, 100), "Log"))
        {
            foreach (var item in mapDic)
            {
                
            }
        }
    }

    public void showEffect(Vector2 pos, ElementType itemType)
    {
        if (!mapDic.ContainsKey(pos)) return;
        switch (itemType)
        {
            case ElementType.Fire:
                showFireEffect(pos);
                break;
            case ElementType.Fuel:
                showFuelEffect(pos);
                break;
            case ElementType.Bomb:
                showBoombEffect(pos);
                break;
                
        }
    }
    
    public void unShowEffect(Vector2 pos, ElementType itemType)
    {
        if (!mapDic.ContainsKey(pos)) return;
        switch (itemType)
        {
            case ElementType.Fire:
                unShowFireEffect(pos);
                break;
            case ElementType.Fuel:
                unShowFuelEffect(pos);
                break;
            case ElementType.Bomb:
                unShowBoombEffect(pos);
                break;
                
        }
    }

    private void showFuelEffect(Vector2 pos)
    {
        List<Element> list = GetFullNearBy(pos, ElementType.Fuel);
        foreach (Element ele in list)
        {
            setVirtual(ele.pos);   
        }
    }
    
    private void unShowFuelEffect(Vector2 pos)
    {
        List<Element> list = GetFullNearBy(pos, ElementType.Fuel);
        foreach (Element ele in list)
        {
            unSetVirtual(ele.pos);   
        }
    }

    private void showBoombEffect(Vector2 pos)
    {
        List<Element> list = GetFullNearBy(pos, ElementType.Bomb);
        foreach (Element ele in list)
        {
            setVirtual(ele.pos);   
        }
    }
    
    private void unShowBoombEffect(Vector2 pos)
    {
        List<Element> list = GetFullNearBy(pos, ElementType.Bomb);
        foreach (Element ele in list)
        {
            unSetVirtual(ele.pos);   
        }
    }
    
    private void showFireEffect(Vector2 pos)
         {
             List<Element> list = GetSingleNearBy(pos, ElementType.Fire);
             foreach (Element ele in list)
             {
                  setVirtual(ele.pos);   
             }
         }
    
    private void unShowFireEffect(Vector2 pos)
    {
        List<Element> list = GetSingleNearBy(pos, ElementType.Fire);
        foreach (Element ele in list)
        {
            unSetVirtual(ele.pos);   
        }
    }

    private void setVirtual(Vector2 pos)
    {
        if (!mapDic.ContainsKey(pos)) return;

        if (mapDic[pos].type == ElementType.Land)
        {
            Color color = mapDic[pos].GetComponent<SpriteRenderer>().material.color;
            mapDic[pos].GetComponent<SpriteRenderer>().material.color = new Color(color.r, color.g, color.b, 0.55f);
        }
    }
    
    private void unSetVirtual(Vector2 pos)
    {
        if (!mapDic.ContainsKey(pos)) return;
        
        if (mapDic[pos].type == ElementType.Land)
        {
            Color color = mapDic[pos].GetComponent<SpriteRenderer>().material.color;
            mapDic[pos].GetComponent<SpriteRenderer>().material.color = new Color(color.r, color.g, color.b, 1.0f);
        }
    }

    public void updateRenderState()
    {
        foreach (var key in mapDic.Keys)
        {
            if (mapDic[key].state == ElementState.Warm)
            {
                if (mapDic[key].type == ElementType.Land)
                {
                    Color color = mapDic[key].GetComponent<SpriteRenderer>().material.color;
                    mapDic[key].GetComponent<SpriteRenderer>().material.color = new Color(color.r, color.g, color.b, 0.25f);
                }
            }
        }
    }

    public void setBlockState(Vector2 pos, ElementState state)
    {
        if (mapDic.ContainsKey(pos))
        {
            mapDic[pos].state = state;
        }
    }
}
