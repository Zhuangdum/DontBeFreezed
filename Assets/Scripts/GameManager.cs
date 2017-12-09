using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ElementMenu configMenu;

    public static GameManager instance;

    public MapGenerator mapGenerator;

    public UIManager uiManager;

    public Dictionary<ElementType, int> tools;
    public int fireCount {
        set
        {
            uiManager.fireText.text = tools[ElementType.Fire].ToString();
        }
    }
    public int fuelCount {
        set
        {
            uiManager.fuelText.text = tools[ElementType.Fuel].ToString();
        }
    }
    public int boombCount {
        set
        {
            uiManager.bombText.text = tools[ElementType.Bomb].ToString();
        }
    }
    private void Awake()
    {
        instance = this;
        
        tools = new Dictionary<ElementType, int>();
        tools.Add(ElementType.Fire, 3);
        tools.Add(ElementType.Fuel, 3);
        tools.Add(ElementType.Bomb, 3);
        
        //set ui
        uiManager.Init();
        SetUIDirty();
    }

    public void SetUIDirty()
    {
        //set ui dirty
        fireCount = 0;
        fuelCount = 0;
        boombCount = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.CompareTag("Rock"))
                {
                    Element clickedElement = hitInfo.collider.GetComponent<Element>();
                    if (clickedElement.type == ElementType.Trap)
                    {
                        Debug.LogError("you clicked a trap, pos : " + clickedElement.pos);
                        return;
                    }
                    else if(clickedElement.type == ElementType.Land)
                    {
                        
                        if (tools.ContainsKey(uiManager.handType) && tools[uiManager.handType] > 0)
                        {
                            Land land = clickedElement as Land;
                            if (land == null)
                            {
                                Debug.LogError("xxxx");
                                return;
                            }
                            land.BeEffected(land, uiManager.handType);
                            tools[uiManager.handType]--;
                            SetUIDirty();
                            Debug.Log(" 使用道具类型： "+ uiManager.handType+"  道具数量： "+tools[uiManager.handType]);
                        }
                        else
                        {
                            Debug.Log(" 道具数量不足 "+ uiManager.handType);
                        }
                    }
                    else
                    {
                        Debug.Log("this place already have something");
                    }
                }
            }
        }
    }

    
}
